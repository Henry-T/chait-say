namespace ChaitPresServer
{
    partial class ServerForm
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
            this.components = new System.ComponentModel.Container();
            this.label1 = new System.Windows.Forms.Label();
            this.btn_start = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.tb_port = new System.Windows.Forms.TextBox();
            this.tb_log = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.cbb_localIP = new System.Windows.Forms.ComboBox();
            this.tmr_update = new System.Windows.Forms.Timer(this.components);
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(8, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "本地IP";
            // 
            // btn_start
            // 
            this.btn_start.Location = new System.Drawing.Point(285, 8);
            this.btn_start.Name = "btn_start";
            this.btn_start.Size = new System.Drawing.Size(75, 23);
            this.btn_start.TabIndex = 2;
            this.btn_start.Text = "启动";
            this.btn_start.UseVisualStyleBackColor = true;
            this.btn_start.Click += new System.EventHandler(this.btn_start_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(198, 13);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(29, 12);
            this.label2.TabIndex = 0;
            this.label2.Text = "端口";
            // 
            // tb_port
            // 
            this.tb_port.Location = new System.Drawing.Point(229, 10);
            this.tb_port.Name = "tb_port";
            this.tb_port.Size = new System.Drawing.Size(32, 21);
            this.tb_port.TabIndex = 1;
            this.tb_port.Text = "1048";
            // 
            // tb_log
            // 
            this.tb_log.Location = new System.Drawing.Point(3, 62);
            this.tb_log.Multiline = true;
            this.tb_log.Name = "tb_log";
            this.tb_log.Size = new System.Drawing.Size(361, 350);
            this.tb_log.TabIndex = 3;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(5, 44);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(65, 12);
            this.label3.TabIndex = 4;
            this.label3.Text = "服务器记录";
            // 
            // cbb_localIP
            // 
            this.cbb_localIP.FormattingEnabled = true;
            this.cbb_localIP.Location = new System.Drawing.Point(55, 10);
            this.cbb_localIP.Name = "cbb_localIP";
            this.cbb_localIP.Size = new System.Drawing.Size(121, 20);
            this.cbb_localIP.TabIndex = 5;
            // 
            // tmr_update
            // 
            this.tmr_update.Enabled = true;
            this.tmr_update.Tick += new System.EventHandler(this.tmr_update_Tick);
            // 
            // ServerForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(372, 415);
            this.Controls.Add(this.cbb_localIP);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.tb_log);
            this.Controls.Add(this.btn_start);
            this.Controls.Add(this.tb_port);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "ServerForm";
            this.Text = "Chait服务器";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btn_start;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox tb_port;
        private System.Windows.Forms.TextBox tb_log;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cbb_localIP;
        private System.Windows.Forms.Timer tmr_update;
    }
}