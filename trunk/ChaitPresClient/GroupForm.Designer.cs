namespace ChaitPresClient
{
    partial class GroupForm
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
            this.label1 = new System.Windows.Forms.Label();
            this.lsb_members = new System.Windows.Forms.ListBox();
            this.label2 = new System.Windows.Forms.Label();
            this.btn_updateMembers = new System.Windows.Forms.Button();
            this.tb_groupHistory = new System.Windows.Forms.TextBox();
            this.btn_send = new System.Windows.Forms.Button();
            this.tb_chatStr = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(8, 6);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "群组聊天";
            // 
            // lsb_members
            // 
            this.lsb_members.FormattingEnabled = true;
            this.lsb_members.ItemHeight = 12;
            this.lsb_members.Location = new System.Drawing.Point(268, 25);
            this.lsb_members.Name = "lsb_members";
            this.lsb_members.Size = new System.Drawing.Size(120, 208);
            this.lsb_members.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(268, 6);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 12);
            this.label2.TabIndex = 0;
            this.label2.Text = "群组成员";
            // 
            // btn_updateMembers
            // 
            this.btn_updateMembers.Location = new System.Drawing.Point(268, 237);
            this.btn_updateMembers.Name = "btn_updateMembers";
            this.btn_updateMembers.Size = new System.Drawing.Size(120, 23);
            this.btn_updateMembers.TabIndex = 2;
            this.btn_updateMembers.Text = "更新成员（临时）";
            this.btn_updateMembers.UseVisualStyleBackColor = true;
            this.btn_updateMembers.Click += new System.EventHandler(this.btn_updateMembers_Click);
            // 
            // tb_groupHistory
            // 
            this.tb_groupHistory.Location = new System.Drawing.Point(8, 25);
            this.tb_groupHistory.Multiline = true;
            this.tb_groupHistory.Name = "tb_groupHistory";
            this.tb_groupHistory.Size = new System.Drawing.Size(252, 208);
            this.tb_groupHistory.TabIndex = 3;
            // 
            // btn_send
            // 
            this.btn_send.Location = new System.Drawing.Point(186, 237);
            this.btn_send.Name = "btn_send";
            this.btn_send.Size = new System.Drawing.Size(75, 23);
            this.btn_send.TabIndex = 4;
            this.btn_send.Text = "发送";
            this.btn_send.UseVisualStyleBackColor = true;
            this.btn_send.Click += new System.EventHandler(this.btn_send_Click);
            // 
            // tb_chatStr
            // 
            this.tb_chatStr.Location = new System.Drawing.Point(8, 237);
            this.tb_chatStr.Name = "tb_chatStr";
            this.tb_chatStr.Size = new System.Drawing.Size(172, 21);
            this.tb_chatStr.TabIndex = 5;
            // 
            // GroupForm
            // 
            this.AcceptButton = this.btn_send;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(393, 265);
            this.Controls.Add(this.tb_chatStr);
            this.Controls.Add(this.btn_send);
            this.Controls.Add(this.tb_groupHistory);
            this.Controls.Add(this.btn_updateMembers);
            this.Controls.Add(this.lsb_members);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "GroupForm";
            this.Text = "GroupChat";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ListBox lsb_members;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btn_updateMembers;
        private System.Windows.Forms.TextBox tb_groupHistory;
        private System.Windows.Forms.Button btn_send;
        private System.Windows.Forms.TextBox tb_chatStr;
    }
}