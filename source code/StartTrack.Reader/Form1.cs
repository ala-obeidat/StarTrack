using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using RFIDReaderAPI;
using StartTrack.Reader.Model;

namespace StartTrack.Reader
{
    public partial class Form1 : Form
    {
        public bool Connect { get; set; }
        public IReader Reader { get; set; }
        public Form1()
        {
            InitializeComponent();
            txtReaderType.SelectedIndex = 0;
            selectType.SelectedIndex = 0;
        }


        private void Log(string message)
        {
            MethodInvoker(() => { txtLog.Text += "\n" + message; }, txtLog);

        }
        private void MethodInvoker(Action method, Control control)
        {
            MethodInvoker mi = new MethodInvoker(method);
            if (control.InvokeRequired)
            {
                control.Invoke(mi);
            }
            else
            {
                mi.Invoke();
            }
        }

        private void btnConnect_Click(object sender, EventArgs e)
        {
            ConnectType type = ConnectType.USB;
            switch (selectType.SelectedIndex)
            {
                case 0:
                    MessageBox.Show("Please Select Connection Type", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                case 2:
                    type = ConnectType.WiFi;
                    break;
            }
            eAntennaNo antNum = 0;
            if (chkAntenna1.Checked)
                antNum |= eAntennaNo._1;
            if (chkAntenna2.Checked)
                antNum |= eAntennaNo._2;
            if (chkAntenna3.Checked)
                antNum |= eAntennaNo._3;
            if (chkAntenna4.Checked)
                antNum |= eAntennaNo._4;
            int interval = Convert.ToInt32(txtTime.Value);
            Reader = GetReader(txtReaderType.SelectedIndex, interval, antNum);
            if (Reader == null)
            {
                return;
            }
            if (Reader.Connect(type))
            {
                txtConnection.Text = "Connected Successfully";
                Connect = true;
                Log("Connected");
            }
            else
            {
                txtConnection.Text = "Connection Faild!";
                Log("Faild to connect");
            }
        }

        

        private void btnDisconnect_Click(object sender, EventArgs e)
        {
            if (!Connect)
            {
                MessageBox.Show("Reader is NOT connected", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            Reader.Disconnect();
            Connect = false;
            txtConnection.Text = "";
            Log("Disconnected");

        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            if (!Connect)
            {
                MessageBox.Show("Reader is NOT connected", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            Reader.Stop();
            Log(">>>>>> STOP <<<<<<");
        }

        private void btnRead_Click(object sender, EventArgs e)
        {
            if (!Connect)
            {
                MessageBox.Show("Reader is NOT connected", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            string connectionString = txtConnectionString.Text.Trim();
            Reader.StartReading((TagModel tag) =>
            {
                if (chkDebug.Checked)
                {
                    Log($"Adding Tag with TID: {tag.TID}, Direction: {tag.Direction}");
                }
                try
                {
                    using (var command = new SqlCommand("r_Movement_Insert", new SqlConnection(connectionString)))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@TID", tag.TID);
                        command.Parameters.AddWithValue("@AntinnaId", tag.Antenna);
                        command.Parameters.AddWithValue("@Direction", tag.Direction.ToString());
                        command.Parameters.AddWithValue("@Status", 1);
                        command.Connection.Open();
                        command.ExecuteNonQuery();
                        command.Connection.Close();
                    }
                }
                catch (Exception ex)
                {
                    Log("ERROR: " + ex.Message);
                }

                if (chkDebug.Checked)
                {
                    Log("Tag Added successfully");
                }
            });
            Log("START READING");
        }

        private IReader GetReader(int selectedIndex, int interval, eAntennaNo antNum)
        {
            IReader reader = null;
            switch (selectedIndex)
            {
                case 0:
                    MessageBox.Show("Please Select Reader Type", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    break;
                case 1:
                    reader = new Hopeland(interval, antNum, Handle);
                    break;
            }
            return reader;
        }
    }
}
