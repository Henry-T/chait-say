namespace ChaitPresClient
{
    partial class PrivateForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PrivateForm));
            this.tb_chatHistory = new System.Windows.Forms.TextBox();
            this.tb_send = new System.Windows.Forms.TextBox();
            this.btn_send = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.ptb_otherVideo = new System.Windows.Forms.PictureBox();
            this.ptb_selfVideo = new System.Windows.Forms.PictureBox();
            this.btn_videoCmd = new System.Windows.Forms.Button();
            this.grb_video = new System.Windows.Forms.GroupBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.btn_sendFile = new System.Windows.Forms.Button();
            this.btn_sendPause = new System.Windows.Forms.Button();
            this.label8 = new System.Windows.Forms.Label();
            this.btn_sendStop = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.btn_receiveStop = new System.Windows.Forms.Button();
            this.btn_receivePause = new System.Windows.Forms.Button();
            this.sts_fileReceive = new System.Windows.Forms.StatusStrip();
            this.sts_fileSend = new System.Windows.Forms.StatusStrip();
            this.ptb_capHolder = new System.Windows.Forms.PictureBox();
            this.ptb_myFace = new System.Windows.Forms.PictureBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.ptb_otherVideo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ptb_selfVideo)).BeginInit();
            this.grb_video.SuspendLayout();
            this.groupBox4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ptb_capHolder)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ptb_myFace)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            this.SuspendLayout();
            // 
            // tb_chatHistory
            // 
            this.tb_chatHistory.Location = new System.Drawing.Point(13, 29);
            this.tb_chatHistory.Multiline = true;
            this.tb_chatHistory.Name = "tb_chatHistory";
            this.tb_chatHistory.Size = new System.Drawing.Size(259, 307);
            this.tb_chatHistory.TabIndex = 0;
            // 
            // tb_send
            // 
            this.tb_send.Location = new System.Drawing.Point(12, 343);
            this.tb_send.Name = "tb_send";
            this.tb_send.Size = new System.Drawing.Size(185, 21);
            this.tb_send.TabIndex = 0;
            // 
            // btn_send
            // 
            this.btn_send.Location = new System.Drawing.Point(202, 342);
            this.btn_send.Name = "btn_send";
            this.btn_send.Size = new System.Drawing.Size(70, 23);
            this.btn_send.TabIndex = 1;
            this.btn_send.Text = "发送";
            this.btn_send.UseVisualStyleBackColor = true;
            this.btn_send.Click += new System.EventHandler(this.btn_send_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 11);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 2;
            this.label1.Text = "私聊消息";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 14);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(29, 12);
            this.label2.TabIndex = 3;
            this.label2.Text = "对方";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(7, 180);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(29, 12);
            this.label3.TabIndex = 3;
            this.label3.Text = "自己";
            // 
            // ptb_otherVideo
            // 
            this.ptb_otherVideo.Location = new System.Drawing.Point(9, 29);
            this.ptb_otherVideo.Name = "ptb_otherVideo";
            this.ptb_otherVideo.Size = new System.Drawing.Size(166, 143);
            this.ptb_otherVideo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.ptb_otherVideo.TabIndex = 4;
            this.ptb_otherVideo.TabStop = false;
            // 
            // ptb_selfVideo
            // 
            this.ptb_selfVideo.Location = new System.Drawing.Point(9, 195);
            this.ptb_selfVideo.Name = "ptb_selfVideo";
            this.ptb_selfVideo.Size = new System.Drawing.Size(166, 143);
            this.ptb_selfVideo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.ptb_selfVideo.TabIndex = 4;
            this.ptb_selfVideo.TabStop = false;
            // 
            // btn_videoCmd
            // 
            this.btn_videoCmd.Location = new System.Drawing.Point(323, 347);
            this.btn_videoCmd.Name = "btn_videoCmd";
            this.btn_videoCmd.Size = new System.Drawing.Size(100, 23);
            this.btn_videoCmd.TabIndex = 1;
            this.btn_videoCmd.Text = "请求视频聊天";
            this.btn_videoCmd.UseVisualStyleBackColor = true;
            this.btn_videoCmd.Click += new System.EventHandler(this.btn_videoCmd_Click);
            // 
            // grb_video
            // 
            this.grb_video.Controls.Add(this.ptb_otherVideo);
            this.grb_video.Controls.Add(this.ptb_selfVideo);
            this.grb_video.Controls.Add(this.label2);
            this.grb_video.Controls.Add(this.label3);
            this.grb_video.Location = new System.Drawing.Point(278, 3);
            this.grb_video.Name = "grb_video";
            this.grb_video.Size = new System.Drawing.Size(189, 343);
            this.grb_video.TabIndex = 5;
            this.grb_video.TabStop = false;
            this.grb_video.Text = "视频聊天";
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.ptb_capHolder);
            this.groupBox4.Controls.Add(this.btn_sendFile);
            this.groupBox4.Controls.Add(this.btn_sendPause);
            this.groupBox4.Controls.Add(this.label8);
            this.groupBox4.Controls.Add(this.btn_sendStop);
            this.groupBox4.Controls.Add(this.label4);
            this.groupBox4.Controls.Add(this.btn_receiveStop);
            this.groupBox4.Controls.Add(this.btn_receivePause);
            this.groupBox4.Location = new System.Drawing.Point(2, 376);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(465, 53);
            this.groupBox4.TabIndex = 11;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "文件传递";
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
            // btn_sendStop
            // 
            this.btn_sendStop.Enabled = false;
            this.btn_sendStop.Location = new System.Drawing.Point(174, 19);
            this.btn_sendStop.Name = "btn_sendStop";
            this.btn_sendStop.Size = new System.Drawing.Size(37, 23);
            this.btn_sendStop.TabIndex = 2;
            this.btn_sendStop.Text = "停止";
            this.btn_sendStop.UseVisualStyleBackColor = true;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(29, 24);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(53, 12);
            this.label4.TabIndex = 8;
            this.label4.Text = "文件发送";
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
            // 
            // sts_fileReceive
            // 
            this.sts_fileReceive.Location = new System.Drawing.Point(0, 450);
            this.sts_fileReceive.Name = "sts_fileReceive";
            this.sts_fileReceive.Size = new System.Drawing.Size(735, 22);
            this.sts_fileReceive.TabIndex = 12;
            this.sts_fileReceive.Text = "statusStrip1";
            // 
            // sts_fileSend
            // 
            this.sts_fileSend.Location = new System.Drawing.Point(0, 428);
            this.sts_fileSend.Name = "sts_fileSend";
            this.sts_fileSend.Size = new System.Drawing.Size(735, 22);
            this.sts_fileSend.TabIndex = 13;
            this.sts_fileSend.Text = "statusStrip2";
            // 
            // ptb_capHolder
            // 
            this.ptb_capHolder.Location = new System.Drawing.Point(427, 20);
            this.ptb_capHolder.Name = "ptb_capHolder";
            this.ptb_capHolder.Size = new System.Drawing.Size(24, 19);
            this.ptb_capHolder.TabIndex = 14;
            this.ptb_capHolder.TabStop = false;
            // 
            // ptb_myFace
            // 
            this.ptb_myFace.Location = new System.Drawing.Point(576, 102);
            this.ptb_myFace.Name = "ptb_myFace";
            this.ptb_myFace.Size = new System.Drawing.Size(91, 79);
            this.ptb_myFace.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.ptb_myFace.TabIndex = 16;
            this.ptb_myFace.TabStop = false;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Location = new System.Drawing.Point(523, 149);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(46, 32);
            this.pictureBox1.TabIndex = 15;
            this.pictureBox1.TabStop = false;
            // 
            // pictureBox2
            // 
            this.pictureBox2.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox2.Image")));
            this.pictureBox2.Location = new System.Drawing.Point(523, 101);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(46, 44);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox2.TabIndex = 14;
            this.pictureBox2.TabStop = false;
            // 
            // PrivateForm
            // 
            this.AcceptButton = this.btn_send;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(735, 472);
            this.Controls.Add(this.ptb_myFace);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.pictureBox2);
            this.Controls.Add(this.sts_fileSend);
            this.Controls.Add(this.sts_fileReceive);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.grb_video);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btn_videoCmd);
            this.Controls.Add(this.btn_send);
            this.Controls.Add(this.tb_send);
            this.Controls.Add(this.tb_chatHistory);
            this.Name = "PrivateForm";
            this.Text = "SingleChatForm";
            ((System.ComponentModel.ISupportInitialize)(this.ptb_otherVideo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ptb_selfVideo)).EndInit();
            this.grb_video.ResumeLayout(false);
            this.grb_video.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ptb_capHolder)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ptb_myFace)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox tb_send;
        private System.Windows.Forms.Button btn_send;
        public System.Windows.Forms.TextBox tb_chatHistory;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.PictureBox ptb_otherVideo;
        private System.Windows.Forms.Button btn_videoCmd;
        private System.Windows.Forms.GroupBox grb_video;
        public System.Windows.Forms.PictureBox ptb_selfVideo;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Button btn_sendFile;
        private System.Windows.Forms.Button btn_sendPause;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Button btn_sendStop;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btn_receiveStop;
        private System.Windows.Forms.Button btn_receivePause;
        private System.Windows.Forms.StatusStrip sts_fileReceive;
        private System.Windows.Forms.StatusStrip sts_fileSend;
        private System.Windows.Forms.PictureBox ptb_capHolder;
        private System.Windows.Forms.PictureBox ptb_myFace;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.PictureBox pictureBox2;
    }
}