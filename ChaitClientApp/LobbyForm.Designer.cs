namespace ChaitClientApp
{
    partial class LobbyForm
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LobbyForm));
            this.label1 = new System.Windows.Forms.Label();
            this.tb_neckName = new System.Windows.Forms.TextBox();
            this.tb_serverIP = new System.Windows.Forms.TextBox();
            this.btn_connect = new System.Windows.Forms.Button();
            this.tb_chatHistory = new System.Windows.Forms.TextBox();
            this.lsb_friends = new System.Windows.Forms.ListBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.tb_send = new System.Windows.Forms.TextBox();
            this.btn_sendMsg = new System.Windows.Forms.Button();
            this.btn_sendFile = new System.Windows.Forms.Button();
            this.sts_fileReceive = new System.Windows.Forms.StatusStrip();
            this.label5 = new System.Windows.Forms.Label();
            this.tb_serverPort = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label7 = new System.Windows.Forms.Label();
            this.tb_localIP = new System.Windows.Forms.TextBox();
            this.btn_sendPause = new System.Windows.Forms.Button();
            this.btn_sendStop = new System.Windows.Forms.Button();
            this.sts_fileSend = new System.Windows.Forms.StatusStrip();
            this.btn_receiveStop = new System.Windows.Forms.Button();
            this.btn_receivePause = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.label10 = new System.Windows.Forms.Label();
            this.btn_updateGroups = new System.Windows.Forms.Button();
            this.btn_updateNecks = new System.Windows.Forms.Button();
            this.tb_groupName = new System.Windows.Forms.TextBox();
            this.btn_joinGroup = new System.Windows.Forms.Button();
            this.lsb_groups = new System.Windows.Forms.ListBox();
            this.label9 = new System.Windows.Forms.Label();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(23, 44);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(29, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "昵称";
            // 
            // tb_neckName
            // 
            this.tb_neckName.Location = new System.Drawing.Point(54, 40);
            this.tb_neckName.Name = "tb_neckName";
            this.tb_neckName.Size = new System.Drawing.Size(108, 21);
            this.tb_neckName.TabIndex = 1;
            this.tb_neckName.Text = "神奇的名字";
            // 
            // tb_serverIP
            // 
            this.tb_serverIP.Location = new System.Drawing.Point(54, 13);
            this.tb_serverIP.Name = "tb_serverIP";
            this.tb_serverIP.Size = new System.Drawing.Size(108, 21);
            this.tb_serverIP.TabIndex = 1;
            this.tb_serverIP.Text = "111.0.235.160";
            // 
            // btn_connect
            // 
            this.btn_connect.Location = new System.Drawing.Point(276, 53);
            this.btn_connect.Name = "btn_connect";
            this.btn_connect.Size = new System.Drawing.Size(160, 23);
            this.btn_connect.TabIndex = 2;
            this.btn_connect.Text = "连接";
            this.btn_connect.UseVisualStyleBackColor = true;
            this.btn_connect.Click += new System.EventHandler(this.btn_connect_Click);
            // 
            // tb_chatHistory
            // 
            this.tb_chatHistory.Location = new System.Drawing.Point(6, 30);
            this.tb_chatHistory.Multiline = true;
            this.tb_chatHistory.Name = "tb_chatHistory";
            this.tb_chatHistory.Size = new System.Drawing.Size(297, 241);
            this.tb_chatHistory.TabIndex = 3;
            // 
            // lsb_friends
            // 
            this.lsb_friends.FormattingEnabled = true;
            this.lsb_friends.ItemHeight = 12;
            this.lsb_friends.Location = new System.Drawing.Point(309, 30);
            this.lsb_friends.Name = "lsb_friends";
            this.lsb_friends.Size = new System.Drawing.Size(132, 244);
            this.lsb_friends.TabIndex = 4;
            this.lsb_friends.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.lsb_friends_MouseDoubleClick);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(311, 15);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(95, 12);
            this.label3.TabIndex = 0;
            this.label3.Text = "朋友 [双击私聊]";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(4, 14);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(53, 12);
            this.label4.TabIndex = 0;
            this.label4.Text = "大厅聊天";
            // 
            // tb_send
            // 
            this.tb_send.Location = new System.Drawing.Point(6, 279);
            this.tb_send.Name = "tb_send";
            this.tb_send.Size = new System.Drawing.Size(215, 21);
            this.tb_send.TabIndex = 1;
            // 
            // btn_sendMsg
            // 
            this.btn_sendMsg.Enabled = false;
            this.btn_sendMsg.Location = new System.Drawing.Point(227, 277);
            this.btn_sendMsg.Name = "btn_sendMsg";
            this.btn_sendMsg.Size = new System.Drawing.Size(75, 23);
            this.btn_sendMsg.TabIndex = 2;
            this.btn_sendMsg.Text = "大厅消息";
            this.btn_sendMsg.UseVisualStyleBackColor = true;
            this.btn_sendMsg.Click += new System.EventHandler(this.btn_sendMsg_Click);
            // 
            // btn_sendFile
            // 
            this.btn_sendFile.Enabled = false;
            this.btn_sendFile.Location = new System.Drawing.Point(88, 19);
            this.btn_sendFile.Name = "btn_sendFile";
            this.btn_sendFile.Size = new System.Drawing.Size(37, 23);
            this.btn_sendFile.TabIndex = 2;
            this.btn_sendFile.Text = "请求";
            this.btn_sendFile.UseVisualStyleBackColor = true;
            this.btn_sendFile.Click += new System.EventHandler(this.btn_sendFile_Click);
            // 
            // sts_fileReceive
            // 
            this.sts_fileReceive.Location = new System.Drawing.Point(0, 473);
            this.sts_fileReceive.Name = "sts_fileReceive";
            this.sts_fileReceive.Size = new System.Drawing.Size(604, 22);
            this.sts_fileReceive.TabIndex = 5;
            this.sts_fileReceive.Text = "sts_receive";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(178, 16);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(29, 12);
            this.label5.TabIndex = 0;
            this.label5.Text = "端口";
            // 
            // tb_serverPort
            // 
            this.tb_serverPort.Location = new System.Drawing.Point(210, 12);
            this.tb_serverPort.Name = "tb_serverPort";
            this.tb_serverPort.Size = new System.Drawing.Size(38, 21);
            this.tb_serverPort.TabIndex = 1;
            this.tb_serverPort.Text = "1048";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(31, 16);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(17, 12);
            this.label6.TabIndex = 0;
            this.label6.Text = "IP";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.tb_serverIP);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.tb_serverPort);
            this.groupBox1.Controls.Add(this.tb_neckName);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(11, 10);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(257, 66);
            this.groupBox1.TabIndex = 6;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "聊天服务器";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label7);
            this.groupBox2.Controls.Add(this.tb_localIP);
            this.groupBox2.Enabled = false;
            this.groupBox2.Location = new System.Drawing.Point(274, 10);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(166, 39);
            this.groupBox2.TabIndex = 6;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "文件传递";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(8, 16);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(41, 12);
            this.label7.TabIndex = 0;
            this.label7.Text = "本地IP";
            // 
            // tb_localIP
            // 
            this.tb_localIP.Location = new System.Drawing.Point(54, 13);
            this.tb_localIP.Name = "tb_localIP";
            this.tb_localIP.Size = new System.Drawing.Size(108, 21);
            this.tb_localIP.TabIndex = 1;
            this.tb_localIP.Text = "111.0.235.160";
            // 
            // btn_sendPause
            // 
            this.btn_sendPause.Enabled = false;
            this.btn_sendPause.Location = new System.Drawing.Point(131, 19);
            this.btn_sendPause.Name = "btn_sendPause";
            this.btn_sendPause.Size = new System.Drawing.Size(37, 23);
            this.btn_sendPause.TabIndex = 2;
            this.btn_sendPause.Text = "暂停";
            this.btn_sendPause.UseVisualStyleBackColor = true;
            this.btn_sendPause.Click += new System.EventHandler(this.btn_sendFile_Click);
            // 
            // btn_sendStop
            // 
            this.btn_sendStop.Enabled = false;
            this.btn_sendStop.Location = new System.Drawing.Point(174, 19);
            this.btn_sendStop.Name = "btn_sendStop";
            this.btn_sendStop.Size = new System.Drawing.Size(37, 23);
            this.btn_sendStop.TabIndex = 2;
            this.btn_sendStop.Text = "停止";
            this.btn_sendStop.UseVisualStyleBackColor = true;
            this.btn_sendStop.Click += new System.EventHandler(this.btn_sendFile_Click);
            // 
            // sts_fileSend
            // 
            this.sts_fileSend.Location = new System.Drawing.Point(0, 451);
            this.sts_fileSend.Name = "sts_fileSend";
            this.sts_fileSend.Size = new System.Drawing.Size(604, 22);
            this.sts_fileSend.TabIndex = 7;
            this.sts_fileSend.Text = "sts_send";
            // 
            // btn_receiveStop
            // 
            this.btn_receiveStop.Enabled = false;
            this.btn_receiveStop.Location = new System.Drawing.Point(346, 19);
            this.btn_receiveStop.Name = "btn_receiveStop";
            this.btn_receiveStop.Size = new System.Drawing.Size(37, 23);
            this.btn_receiveStop.TabIndex = 2;
            this.btn_receiveStop.Text = "停止";
            this.btn_receiveStop.UseVisualStyleBackColor = true;
            this.btn_receiveStop.Click += new System.EventHandler(this.btn_sendFile_Click);
            // 
            // btn_receivePause
            // 
            this.btn_receivePause.Enabled = false;
            this.btn_receivePause.Location = new System.Drawing.Point(303, 19);
            this.btn_receivePause.Name = "btn_receivePause";
            this.btn_receivePause.Size = new System.Drawing.Size(37, 23);
            this.btn_receivePause.TabIndex = 2;
            this.btn_receivePause.Text = "暂停";
            this.btn_receivePause.UseVisualStyleBackColor = true;
            this.btn_receivePause.Click += new System.EventHandler(this.btn_sendFile_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(29, 24);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 12);
            this.label2.TabIndex = 8;
            this.label2.Text = "文件发送";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(244, 24);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(53, 12);
            this.label8.TabIndex = 8;
            this.label8.Text = "文件接收";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.label10);
            this.groupBox3.Controls.Add(this.btn_updateGroups);
            this.groupBox3.Controls.Add(this.btn_updateNecks);
            this.groupBox3.Controls.Add(this.tb_groupName);
            this.groupBox3.Controls.Add(this.btn_joinGroup);
            this.groupBox3.Controls.Add(this.tb_chatHistory);
            this.groupBox3.Controls.Add(this.lsb_groups);
            this.groupBox3.Controls.Add(this.label9);
            this.groupBox3.Controls.Add(this.lsb_friends);
            this.groupBox3.Controls.Add(this.label3);
            this.groupBox3.Controls.Add(this.label4);
            this.groupBox3.Controls.Add(this.tb_send);
            this.groupBox3.Controls.Add(this.btn_sendMsg);
            this.groupBox3.Location = new System.Drawing.Point(11, 82);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(581, 306);
            this.groupBox3.TabIndex = 9;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "聊天";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(448, 34);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(29, 12);
            this.label10.TabIndex = 0;
            this.label10.Text = "名称";
            // 
            // btn_updateGroups
            // 
            this.btn_updateGroups.Location = new System.Drawing.Point(447, 278);
            this.btn_updateGroups.Name = "btn_updateGroups";
            this.btn_updateGroups.Size = new System.Drawing.Size(129, 23);
            this.btn_updateGroups.TabIndex = 5;
            this.btn_updateGroups.Text = "更新群组(临时)";
            this.btn_updateGroups.UseVisualStyleBackColor = true;
            this.btn_updateGroups.Click += new System.EventHandler(this.btn_updateGroups_Click);
            // 
            // btn_updateNecks
            // 
            this.btn_updateNecks.Location = new System.Drawing.Point(309, 278);
            this.btn_updateNecks.Name = "btn_updateNecks";
            this.btn_updateNecks.Size = new System.Drawing.Size(132, 23);
            this.btn_updateNecks.TabIndex = 5;
            this.btn_updateNecks.Text = "更新好友(临时)";
            this.btn_updateNecks.UseVisualStyleBackColor = true;
            this.btn_updateNecks.Click += new System.EventHandler(this.btn_updateNecks_Click);
            // 
            // tb_groupName
            // 
            this.tb_groupName.Location = new System.Drawing.Point(484, 29);
            this.tb_groupName.Name = "tb_groupName";
            this.tb_groupName.Size = new System.Drawing.Size(90, 21);
            this.tb_groupName.TabIndex = 6;
            // 
            // btn_joinGroup
            // 
            this.btn_joinGroup.Enabled = false;
            this.btn_joinGroup.Location = new System.Drawing.Point(447, 52);
            this.btn_joinGroup.Name = "btn_joinGroup";
            this.btn_joinGroup.Size = new System.Drawing.Size(128, 23);
            this.btn_joinGroup.TabIndex = 5;
            this.btn_joinGroup.Text = "创建/加入";
            this.btn_joinGroup.UseVisualStyleBackColor = true;
            this.btn_joinGroup.Click += new System.EventHandler(this.btn_joinGroup_Click);
            // 
            // lsb_groups
            // 
            this.lsb_groups.FormattingEnabled = true;
            this.lsb_groups.ItemHeight = 12;
            this.lsb_groups.Location = new System.Drawing.Point(447, 77);
            this.lsb_groups.Name = "lsb_groups";
            this.lsb_groups.Size = new System.Drawing.Size(129, 196);
            this.lsb_groups.TabIndex = 4;
            this.lsb_groups.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.lsb_groups_MouseDoubleClick);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(449, 15);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(95, 12);
            this.label9.TabIndex = 0;
            this.label9.Text = "群组 [双击加入]";
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.btn_sendFile);
            this.groupBox4.Controls.Add(this.btn_sendPause);
            this.groupBox4.Controls.Add(this.label8);
            this.groupBox4.Controls.Add(this.btn_sendStop);
            this.groupBox4.Controls.Add(this.label2);
            this.groupBox4.Controls.Add(this.btn_receiveStop);
            this.groupBox4.Controls.Add(this.btn_receivePause);
            this.groupBox4.Enabled = false;
            this.groupBox4.Location = new System.Drawing.Point(11, 395);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(581, 53);
            this.groupBox4.TabIndex = 10;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "文件传递";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(496, 1);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(90, 82);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 11;
            this.pictureBox1.TabStop = false;
            // 
            // LobbyForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.ClientSize = new System.Drawing.Size(604, 495);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.sts_fileSend);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.sts_fileReceive);
            this.Controls.Add(this.btn_connect);
            this.Name = "LobbyForm";
            this.Text = "聊天大厅";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tb_neckName;
        private System.Windows.Forms.TextBox tb_serverIP;
        private System.Windows.Forms.Button btn_connect;
        private System.Windows.Forms.TextBox tb_chatHistory;
        private System.Windows.Forms.ListBox lsb_friends;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox tb_send;
        private System.Windows.Forms.Button btn_sendMsg;
        private System.Windows.Forms.Button btn_sendFile;
        private System.Windows.Forms.StatusStrip sts_fileReceive;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox tb_serverPort;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox tb_localIP;
        private System.Windows.Forms.Button btn_sendPause;
        private System.Windows.Forms.Button btn_sendStop;
        private System.Windows.Forms.StatusStrip sts_fileSend;
        private System.Windows.Forms.Button btn_receiveStop;
        private System.Windows.Forms.Button btn_receivePause;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Button btn_updateNecks;
        private System.Windows.Forms.Button btn_updateGroups;
        private System.Windows.Forms.ListBox lsb_groups;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox tb_groupName;
        private System.Windows.Forms.Button btn_joinGroup;
    }
}

