using System;
using System.Drawing;
using System.Reflection;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;

namespace Loamen.PluginFramework
{
    public partial class DockPage : DockContent
    {
        public DockPage()
        {
            InitializeComponent();
        }

        #region GUI

        /// <summary>
        ///     创建控件
        /// </summary>
        /// <param name="name"></param>
        /// <param name="text"></param>
        /// <param name="type"></param>
        /// <param name="parent"></param>
        /// <param name="point"></param>
        /// <param name="size"></param>
        /// <param name="isDock"></param>
        /// <returns></returns>
        public virtual Control GUICreateControl(
            string name,
            string text,
            Type type,
            Control parent,
            Point? point,
            Size? size,
            DockStyle? dockStyle)
        {
            var control = (Control) Activator.CreateInstance(type);
            control.Name = name;
            control.Text = text;
            parent.Controls.Add(control);

            if (null != point)
            {
                control.Location = (Point) point;
            }

            if (null != size)
            {
                if (size.Value.Width > 0)
                    control.Width = size.Value.Width;
                if (size.Value.Height > 0)
                    control.Height = size.Value.Height;
            }

            if (null != dockStyle)
            {
                control.Dock = (DockStyle) dockStyle;
            }

            return control;
        }

        /// <summary>
        ///     绑定事件
        /// </summary>
        /// <param name="control"></param>
        /// <param name="eventName"></param>
        /// <param name="methodName"></param>
        public virtual void GUISetOnEvent(Control control, string eventName, string methodName)
        {
            MethodInfo methodInfo = GetType().GetMethod(methodName);

            Delegate ctrolEvent = Delegate.CreateDelegate(typeof (EventHandler), this, methodInfo);
            control.GetType().GetEvent(eventName).AddEventHandler(control, ctrolEvent);
        }

        #endregion

        #region Message

        /// <summary>
        ///     提示信息
        /// </summary>
        /// <param name="type">0：提示信息，1：错误信息，2：问题</param>
        /// <param name="message"></param>
        /// <returns></returns>
        public DialogResult ShowMsg(int type, string message)
        {
            switch (type)
            {
                case 0:
                    return MessageBox.Show(message, "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                case 1:
                    return MessageBox.Show(message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                case 2:
                    return MessageBox.Show(message, "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                default:
                    return MessageBox.Show(message, "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        /// <summary>
        ///     提示信息
        /// </summary>
        /// <param name="type">0：提示信息，1：错误信息，2：问题</param>
        /// <param name="title"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        public DialogResult ShowMsg(int type, string title, string message)
        {
            switch (type)
            {
                case 0:
                    return MessageBox.Show(message, title, MessageBoxButtons.OK, MessageBoxIcon.Information);
                case 1:
                    return MessageBox.Show(message, title, MessageBoxButtons.OK, MessageBoxIcon.Error);
                case 2:
                    return MessageBox.Show(message, title, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                default:
                    return MessageBox.Show(message, title, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        #endregion
    }
}