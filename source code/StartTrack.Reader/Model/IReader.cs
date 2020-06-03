
using System;

namespace StartTrack.Reader.Model
{
    public interface IReader
    {
        bool Connect(ConnectType ConnectType);
        void StartReading(Action<TagModel> action);
        bool Write(string EPC, string TID);
        void Stop();
        void Disconnect();
    }
    public enum ConnectType
    {
        USB,
        WiFi
    }
}
