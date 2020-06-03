namespace StartTrack.Reader
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.txtLog = new System.Windows.Forms.RichTextBox();
            this.txtConnection = new System.Windows.Forms.TextBox();
            this.btnConnect = new System.Windows.Forms.Button();
            this.btnDisconnect = new System.Windows.Forms.Button();
            this.txtTime = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.chkDebug = new System.Windows.Forms.CheckBox();
            this.btnRead = new System.Windows.Forms.Button();
            this.btnStop = new System.Windows.Forms.Button();
            this.txtReaderType = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.chkAntenna1 = new System.Windows.Forms.CheckBox();
            this.chkAntenna2 = new System.Windows.Forms.CheckBox();
            this.chkAntenna3 = new System.Windows.Forms.CheckBox();
            this.chkAntenna4 = new System.Windows.Forms.CheckBox();
            this.selectType = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtConnectionString = new System.Windows.Forms.RichTextBox();
            this.label5 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.txtTime)).BeginInit();
            this.SuspendLayout();
            // 
            // txtLog
            // 
            this.txtLog.Location = new System.Drawing.Point(50, 411);
            this.txtLog.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.txtLog.Name = "txtLog";
            this.txtLog.ReadOnly = true;
            this.txtLog.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Vertical;
            this.txtLog.ShowSelectionMargin = true;
            this.txtLog.Size = new System.Drawing.Size(372, 234);
            this.txtLog.TabIndex = 1;
            this.txtLog.Text = "";
            // 
            // txtConnection
            // 
            this.txtConnection.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtConnection.Location = new System.Drawing.Point(163, 251);
            this.txtConnection.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.txtConnection.Name = "txtConnection";
            this.txtConnection.ReadOnly = true;
            this.txtConnection.Size = new System.Drawing.Size(256, 26);
            this.txtConnection.TabIndex = 3;
            // 
            // btnConnect
            // 
            this.btnConnect.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnConnect.Location = new System.Drawing.Point(43, 251);
            this.btnConnect.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btnConnect.Name = "btnConnect";
            this.btnConnect.Size = new System.Drawing.Size(101, 28);
            this.btnConnect.TabIndex = 4;
            this.btnConnect.Text = "Connect";
            this.btnConnect.UseVisualStyleBackColor = true;
            this.btnConnect.Click += new System.EventHandler(this.btnConnect_Click);
            // 
            // btnDisconnect
            // 
            this.btnDisconnect.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDisconnect.Location = new System.Drawing.Point(319, 280);
            this.btnDisconnect.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btnDisconnect.Name = "btnDisconnect";
            this.btnDisconnect.Size = new System.Drawing.Size(102, 35);
            this.btnDisconnect.TabIndex = 5;
            this.btnDisconnect.Text = "Disconnect";
            this.btnDisconnect.UseVisualStyleBackColor = true;
            this.btnDisconnect.Click += new System.EventHandler(this.btnDisconnect_Click);
            // 
            // txtTime
            // 
            this.txtTime.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTime.Location = new System.Drawing.Point(163, 89);
            this.txtTime.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.txtTime.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.txtTime.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.txtTime.Name = "txtTime";
            this.txtTime.Size = new System.Drawing.Size(41, 26);
            this.txtTime.TabIndex = 6;
            this.txtTime.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(57, 90);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(102, 20);
            this.label1.TabIndex = 7;
            this.label1.Text = "Group rading";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(212, 92);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(72, 20);
            this.label2.TabIndex = 8;
            this.label2.Text = "Seconds";
            // 
            // chkDebug
            // 
            this.chkDebug.AutoSize = true;
            this.chkDebug.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkDebug.Location = new System.Drawing.Point(54, 302);
            this.chkDebug.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.chkDebug.Name = "chkDebug";
            this.chkDebug.Size = new System.Drawing.Size(106, 24);
            this.chkDebug.TabIndex = 9;
            this.chkDebug.Text = "Debugging";
            this.chkDebug.UseVisualStyleBackColor = true;
            // 
            // btnRead
            // 
            this.btnRead.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRead.Location = new System.Drawing.Point(50, 342);
            this.btnRead.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btnRead.Name = "btnRead";
            this.btnRead.Size = new System.Drawing.Size(152, 43);
            this.btnRead.TabIndex = 10;
            this.btnRead.Text = "Reading";
            this.btnRead.UseVisualStyleBackColor = true;
            this.btnRead.Click += new System.EventHandler(this.btnRead_Click);
            // 
            // btnStop
            // 
            this.btnStop.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnStop.Location = new System.Drawing.Point(268, 342);
            this.btnStop.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btnStop.Name = "btnStop";
            this.btnStop.Size = new System.Drawing.Size(152, 43);
            this.btnStop.TabIndex = 11;
            this.btnStop.Text = "Stop";
            this.btnStop.UseVisualStyleBackColor = true;
            this.btnStop.Click += new System.EventHandler(this.btnStop_Click);
            // 
            // txtReaderType
            // 
            this.txtReaderType.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtReaderType.FormattingEnabled = true;
            this.txtReaderType.Items.AddRange(new object[] {
            "--Reader--",
            "ST-D01"});
            this.txtReaderType.Location = new System.Drawing.Point(163, 44);
            this.txtReaderType.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.txtReaderType.Name = "txtReaderType";
            this.txtReaderType.Size = new System.Drawing.Size(92, 28);
            this.txtReaderType.TabIndex = 12;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(91, 47);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(66, 20);
            this.label3.TabIndex = 13;
            this.label3.Text = "Reader ";
            // 
            // chkAntenna1
            // 
            this.chkAntenna1.AutoSize = true;
            this.chkAntenna1.Checked = true;
            this.chkAntenna1.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkAntenna1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkAntenna1.Location = new System.Drawing.Point(319, 16);
            this.chkAntenna1.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.chkAntenna1.Name = "chkAntenna1";
            this.chkAntenna1.Size = new System.Drawing.Size(102, 24);
            this.chkAntenna1.TabIndex = 14;
            this.chkAntenna1.Text = "Antenna 1";
            this.chkAntenna1.UseVisualStyleBackColor = true;
            // 
            // chkAntenna2
            // 
            this.chkAntenna2.AutoSize = true;
            this.chkAntenna2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkAntenna2.Location = new System.Drawing.Point(319, 47);
            this.chkAntenna2.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.chkAntenna2.Name = "chkAntenna2";
            this.chkAntenna2.Size = new System.Drawing.Size(102, 24);
            this.chkAntenna2.TabIndex = 15;
            this.chkAntenna2.Text = "Antenna 2";
            this.chkAntenna2.UseVisualStyleBackColor = true;
            // 
            // chkAntenna3
            // 
            this.chkAntenna3.AutoSize = true;
            this.chkAntenna3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkAntenna3.Location = new System.Drawing.Point(319, 76);
            this.chkAntenna3.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.chkAntenna3.Name = "chkAntenna3";
            this.chkAntenna3.Size = new System.Drawing.Size(102, 24);
            this.chkAntenna3.TabIndex = 16;
            this.chkAntenna3.Text = "Antenna 3";
            this.chkAntenna3.UseVisualStyleBackColor = true;
            // 
            // chkAntenna4
            // 
            this.chkAntenna4.AutoSize = true;
            this.chkAntenna4.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkAntenna4.Location = new System.Drawing.Point(319, 104);
            this.chkAntenna4.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.chkAntenna4.Name = "chkAntenna4";
            this.chkAntenna4.Size = new System.Drawing.Size(102, 24);
            this.chkAntenna4.TabIndex = 17;
            this.chkAntenna4.Text = "Antenna 4";
            this.chkAntenna4.UseVisualStyleBackColor = true;
            // 
            // selectType
            // 
            this.selectType.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.selectType.FormattingEnabled = true;
            this.selectType.Items.AddRange(new object[] {
            "--Type--",
            "USB",
            "WiFi"});
            this.selectType.Location = new System.Drawing.Point(163, 127);
            this.selectType.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.selectType.Name = "selectType";
            this.selectType.Size = new System.Drawing.Size(92, 28);
            this.selectType.TabIndex = 18;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(29, 127);
            this.label4.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(128, 20);
            this.label4.TabIndex = 19;
            this.label4.Text = "Connection Type";
            // 
            // txtConnectionString
            // 
            this.txtConnectionString.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtConnectionString.Location = new System.Drawing.Point(163, 190);
            this.txtConnectionString.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.txtConnectionString.Name = "txtConnectionString";
            this.txtConnectionString.Size = new System.Drawing.Size(259, 36);
            this.txtConnectionString.TabIndex = 20;
            this.txtConnectionString.Text = "Data Source=localhost\\SQLEXPRESS;Initial Catalog=RFID;Integrated Security=True;po" +
    "oling=true";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(46, 190);
            this.label5.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(115, 20);
            this.label5.TabIndex = 21;
            this.label5.Text = "Dashboard DB";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(502, 656);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.txtConnectionString);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.selectType);
            this.Controls.Add(this.chkAntenna4);
            this.Controls.Add(this.chkAntenna3);
            this.Controls.Add(this.chkAntenna2);
            this.Controls.Add(this.chkAntenna1);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtReaderType);
            this.Controls.Add(this.btnStop);
            this.Controls.Add(this.btnRead);
            this.Controls.Add(this.chkDebug);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtTime);
            this.Controls.Add(this.btnDisconnect);
            this.Controls.Add(this.btnConnect);
            this.Controls.Add(this.txtConnection);
            this.Controls.Add(this.txtLog);
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.Name = "Form1";
            this.Text = "StarTrack - Reader";
            ((System.ComponentModel.ISupportInitialize)(this.txtTime)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.RichTextBox txtLog;
        private System.Windows.Forms.TextBox txtConnection;
        private System.Windows.Forms.Button btnConnect;
        private System.Windows.Forms.Button btnDisconnect;
        private System.Windows.Forms.NumericUpDown txtTime;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.CheckBox chkDebug;
        private System.Windows.Forms.Button btnRead;
        private System.Windows.Forms.Button btnStop;
        private System.Windows.Forms.ComboBox txtReaderType;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.CheckBox chkAntenna1;
        private System.Windows.Forms.CheckBox chkAntenna2;
        private System.Windows.Forms.CheckBox chkAntenna3;
        private System.Windows.Forms.CheckBox chkAntenna4;
        private System.Windows.Forms.ComboBox selectType;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.RichTextBox txtConnectionString;
        private System.Windows.Forms.Label label5;
    }
}

