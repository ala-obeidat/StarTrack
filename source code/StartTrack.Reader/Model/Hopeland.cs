using RFIDReaderAPI;
using RFIDReaderAPI.Interface;
using RFIDReaderAPI.Models;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace StartTrack.Reader.Model
{
    public class Hopeland : IReader
    {
        
        public Hopeland(int checkIntervalSecond, eAntennaNo antNum, IntPtr handle)
        {
            CheckIntervalSecond = checkIntervalSecond;
            Log = new HopeLandLogger(antNum);
            Handle = handle;
        }
        private Hopeland() { }
        const string TcpConnID = "192.168.1.116:9090";
        public string ConnId { get; set; }
        public int CheckIntervalSecond { get; set; }
        public IntPtr Handle { set; get; }
        public HopeLandLogger Log { set; get; }
        public bool Connect(ConnectType ConnectType)
        {
            try
            {
                RFIDReader.SetAPILanguageType(eAPILanguage.English);
                Stop();
                Disconnect();
                bool isConnect = false;
                switch (ConnectType)
                {
                    case ConnectType.USB:
                        ConnId = RFIDReader.GetUsbHidDeviceList()[0];
                        isConnect = RFIDReader.CreateUsbConn(ConnId, Handle, Log);
                        break;
                    case ConnectType.WiFi:
                        ConnId = TcpConnID;
                        isConnect = RFIDReader.CreateTcpConn(ConnId, Log);
                        break;
                }
                return (isConnect && RFIDReader.CheckConnect(ConnId));
            }
            catch { return false; }

        }

        public void Disconnect()
        {
            Stop();
            RFIDReader.CloseConn(ConnId);
            RFIDReader.CloseAllConnect();
        }

        public void StartReading(Action<TagModel> action)
        {
            Log.Start(action, ConnId, CheckIntervalSecond);
        }

        public void Stop()
        {
            RFIDReader._RFIDConfig.Stop(ConnId);
            Log.Reset();
        }

        public bool Write(string EPC, string TID)
        {
            string result = RFIDReader._Tag6C.WriteUserData(ConnId, eAntennaNo._1, TID, 0, matchType: eMatchCode.EPC, matchCode: EPC, matchWordStartIndex: 0);
            return result == "0|OK";
        }
    }
    public class HopeLandLogger : IAsynchronousMessage
    {
        private HopeLandLogger() { }
        public HopeLandLogger(eAntennaNo antNum)
        {
            AntNum = antNum;
            ReadedTags = new Dictionary<string, TagModel>();
        }
        private Dictionary<string, TagModel> ReadedTags { get; set; }
        private int Interval { get; set; }
        private Action<TagModel> TagAction { get; set; }
        private eAntennaNo AntNum { get; set; }
        public void GPIControlMsg(GPI_Model gpi_model)
        {
        }
        public void Start(Action<TagModel> action, string ConnId, int checkIntervalSecond)
        {
            TagAction = action;
            Interval = checkIntervalSecond;
            RFIDReader._Tag6C.GetEPC_UserData(ConnId, AntNum, eReadType.Inventory, 0, 2);
        }
        public void OutPutTags(Tag_Model tag)
        {
            var ReadedTag = new TagModel()
            {
                Antenna = tag.ANT_NUM,
                TID = tag.UserData
            };
            if (ReadedTags.ContainsKey(ReadedTag.TID))
            {
                var existTag = ReadedTags[ReadedTag.TID];
                if(existTag.LastReadTime.AddSeconds(Interval) < DateTime.Now)
                {
                    existTag.LastReadTime = DateTime.Now;
                    switch (existTag.Direction)
                    {
                        case TagDirection.IN:
                            existTag.Direction = TagDirection.OUT;
                            break;
                        case TagDirection.OUT:
                            existTag.Direction = TagDirection.IN;
                            break;
                     }
                    TagAction(existTag);
                }
            }
            else
            {
                ReadedTags.Add(ReadedTag.TID,ReadedTag);
                TagAction(ReadedTag);
            }
            
        }

        public void OutPutTagsOver()
        {
        }

        public void PortClosing(string connID)
        {
        }

        public void PortConnecting(string connID)
        {
        }

        public void WriteDebugMsg(string msg)
        {
        }

        public void WriteLog(string msg)
        {
        }

        internal void Reset()
        {
            ReadedTags.Clear();
        }
    }
}
