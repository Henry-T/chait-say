namespace ChaitClientApp
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
            this.tb_chatHistory = new System.Windows.Forms.TextBox();
            this.tb_send = new System.Windows.Forms.TextBox();
            this.btn_send = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // tb_chatHistory
            // 
            this.tb_chatHistory.Location = new System.Drawing.Point(13, 29);
            this.tb_chatHistory.Multiline = true;
            this.tb_chatHistory.Name = "tb_chatHistory";
            this.tb_chatHistory.Size = new System.Drawing.Size(259, 209);
            this.tb_chatHistory.TabIndex = 0;
            // 
            // tb_send
            // 
            this.tb_send.Location = new System.Drawing.Point(13, 244);
            this.tb_send.Name = "tb_send";
            this.tb_send.Size = new System.Drawing.Size(188, 21);
            this.tb_send.TabIndex = 0;
            // 
            // btn_send
            // 
            this.btn_send.Location = new System.Drawing.Point(207, 244);
            this.btn_send.Name = "btn_send";
            this.btn_send.Size = new System.Drawing.Size(75, 23);
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
            // PrivateForm
            // 
            this.AcceptButton = this.btn_send;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 277);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btn_send);
            this.Controls.Add(this.tb_send);
            this.Controls.Add(this.tb_chatHistory);
            this.Name = "PrivateForm";
            this.Text = "SingleChatForm";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox tb_send;
        private System.Windows.Forms.Button btn_send;
        public System.Windows.Forms.TextBox tb_chatHistory;
        private System.Windows.Forms.Label label1;
    }
}