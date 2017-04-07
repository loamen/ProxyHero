using System;
using System.Windows.Forms;

namespace ProxyHero.Common
{
    public class MsgBox
    {
        /// <summary>
        ///     显示信息
        /// </summary>
        /// <param name="msg"></param>
        /// <returns></returns>
        public static DialogResult ShowMessage(string msg)
        {
            return MessageBox.Show(msg, Config.LocalLanguage.Messages.Information, MessageBoxButtons.OK,
                                   MessageBoxIcon.Information);
        }

        /// <summary>
        ///     显示错误信息
        /// </summary>
        /// <param name="msg"></param>
        /// <returns></returns>
        public static DialogResult ShowErrorMessage(string msg)
        {
            return MessageBox.Show(msg, Config.LocalLanguage.Messages.Error, MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        /// <summary>
        ///     显示异常数据
        /// </summary>
        /// <param name="ex"></param>
        /// <returns></returns>
        public static DialogResult ShowExceptionMessage(Exception ex)
        {
#if DEBUG
            Config.ConsoleEx.Debug(ex);
#endif
            var msg = new MsgBox();
            return msg.ShowThreadExceptionDialog(ex);
        }

        /// <summary>
        ///     显示问题信息
        /// </summary>
        /// <param name="msg"></param>
        /// <returns></returns>
        public static DialogResult ShowQuestionMessage(string msg)
        {
            return MessageBox.Show(msg, Config.LocalLanguage.Messages.Information, MessageBoxButtons.YesNo,
                                   MessageBoxIcon.Question);
        }

        private DialogResult ShowThreadExceptionDialog(Exception ex)
        {
            var infoform = new ExceptionForm(ex);
            return infoform.ShowDialog();
        }
    }
}