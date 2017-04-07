using System;

namespace ProxyHero.Model
{
    /// <summary>
    ///     ComboBox的项
    /// </summary>
    public class ListItem : Object
    {
        private string _Text = string.Empty;
        private string _Value = string.Empty;

        public ListItem(string text, string value)
        {
            _Value = value;
            _Text = text;
        }

        /// <summary>
        ///     值
        /// </summary>
        public string Value
        {
            get { return _Value; }
            set { _Value = value; }
        }

        /// <summary>
        ///     显示的文本
        /// </summary>
        public string Text
        {
            get { return _Text; }
            set { _Text = value; }
        }

        public override string ToString()
        {
            return _Text;
        }
    }
}