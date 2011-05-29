using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

namespace ChaitPresClient
{
    public class ExThreadUICtrl
    {
        #region 附加控件文本
        // 委托
        private delegate void delAddText(Control txtCtrl, String str);
        // 功能
        private static void addText(Control txtCtrl, String str)
        {
            txtCtrl.Text += str;
        }
        // 接口
        public static void AddText(Control holder, Control control, String str)
        {
            holder.Invoke(new delAddText(addText), control, str);
        }
        #endregion

        #region 附加文本行 （补加换行符）
        // 委托
        private delegate void delAddTextRow(Control txtCtrl, String str);
        // 功能
        private static void addTextRow(Control txtCtrl, String str)
        {
            txtCtrl.Text += str;
            txtCtrl.Text += "\r\n";
        }
        // 接口
        public static void AddTextRow(Control holder, Control control, String str)
        {
            holder.Invoke(new delAddTextRow(addTextRow), control, str);
        }
        #endregion

        #region 设置控件文本
        private delegate void delSetText(Control txtCtrl, String str);
        private static void setText(Control txtCtrl, String str)
        {
            txtCtrl.Text = str;
        }
        public static void SetText(Control holder, Control txtCtrl, String str)
        {
            holder.Invoke(new delSetText(setText), txtCtrl, str);
        }
        #endregion

        #region 设置控件可见性
        private delegate void delSetVisible(Control control, bool visible);
        private static void setVisible(Control control, bool visible)
        {
            control.Visible = visible;
        }
        public static void SetVisible(Control holder, Control control, bool visible)
        {
            holder.Invoke(new delSetVisible(setVisible), control, visible);
        }
        #endregion

        #region 设置控件可用性
        private delegate void delSetEnabled(Control control, bool enabled);
        private static void setEnabled(Control control, bool enabled)
        {
            control.Enabled = enabled;
        }
        public static void SetEnabled(Control holder, Control control, bool enabled)
        {
            holder.Invoke(new delSetEnabled(setEnabled), control, enabled);
        }
        #endregion

        #region 显示对话框
        private delegate DialogResult delShowMessageBox(String msg, String captain, MessageBoxButtons buttons, MessageBoxIcon icon);
        private static DialogResult showMessageBox(String msg, String captain, MessageBoxButtons buttons, MessageBoxIcon icon)
        {
            return MessageBox.Show(msg, captain, buttons, icon);
        }
        public static DialogResult ShowMessageBox(Control holder, String msg, String captain, MessageBoxButtons buttons, MessageBoxIcon icon)
        {
           return (DialogResult)holder.Invoke(new delShowMessageBox(showMessageBox), msg, captain, buttons, icon);
        }
        #endregion

        #region 显示错误对话框
        private delegate DialogResult delShowErrorBox(String msg, String captain, MessageBoxButtons buttons, MessageBoxIcon icon);
        private static DialogResult showErrorBox(String msg, String captain, MessageBoxButtons buttons, MessageBoxIcon icon)
        {
            return MessageBox.Show(msg, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        public static DialogResult ShowErrorBox(Control holder, String msg)
        {
            return (DialogResult)holder.Invoke(new delShowErrorBox(showErrorBox), msg);
        }
        #endregion

        #region 修改PictureBox的显示图片
        private delegate void delSetPictureBoxImage(PictureBox ptb, Image img);
        private static void setPictureBoxImage(PictureBox ptb, Image img)
        {
            ptb.Image = img;
        }
        public static void SetPictureBoxImage(Control holder, PictureBox ptb, Image img)
        {
            holder.Invoke(new delSetPictureBoxImage(setPictureBoxImage), ptb, img);
        }
        #endregion

        #region 创建窗体
        //private delegate Form delCreateForm(PictureBox ptb, Image img);
        //private static Form createForm(PictureBox ptb, Image img)
        //{
        //    ptb.Image = img;
        //}
        //public static Form SetPictureBoxImage(Control holder, PictureBox ptb, Image img)
        //{
        //    Form form = (Form)holder.Invoke(new delCreateForm(createForm), ptb, img);
        //    return form;
        //}
        #endregion

        #region 列表控件开关控制 (如果指定项不存在则添加，如果存在则删除)
        private delegate void delSwitchListItem(ListBox listBox, Object value);
        private static void switchListItem(ListBox listBox, Object value)
        {
            ListBox lsb = (ListBox)listBox;
            if (lsb.Items.Contains(value))
            {
                lsb.Items.Remove(value);
            }
            else
            {
                lsb.Items.Add(value);
            }
        }
        public static void SwitchListItem(Control holder, ListBox listBox, Object value)
        {
            holder.Invoke(new delSwitchListItem(switchListItem), listBox, value);
        }
        #endregion
    }
}
