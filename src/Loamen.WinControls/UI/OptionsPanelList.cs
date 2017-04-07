using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Collections;
using System.ComponentModel.Design;

namespace Loamen.WinControls.UI
{
    public class OptionsPanelEventArgs : EventArgs
    {
        private OptionsPanel _Panel;

        #region Properties
        /// <summary>
        /// 
        /// </summary>
        public OptionsPanel Panel
        {
            get
            {
                return _Panel;
            }
        }

        #endregion

        public OptionsPanelEventArgs(OptionsPanel panel)
        {
            _Panel = panel;
        }
    }

    public delegate void OptionsPanelEventHandler(object sender, OptionsPanelEventArgs e);

    [Editor(typeof(OptionsPanelListEditor), typeof(System.Drawing.Design.UITypeEditor))]
    public class OptionsPanelList : IList<OptionsPanel>, ICollection<OptionsPanel>, IEnumerable<OptionsPanel>
    {
        private List<OptionsPanel> _Panels;

        [Browsable(true)]
        [Category("Options Panel")]
        public event OptionsPanelEventHandler PanelAdded;

        #region Properties

        public OptionsPanel this[int index]
        {
            get
            {
                return _Panels[index];
            }
            set
            {
                _Panels[index] = value;
            }
        }

        public int Count
        {
            get
            {
                return _Panels.Count;
            }
        }

        public bool IsReadOnly
        {
            get
            {
                return false;
            }
        }

        #endregion


        #region Construction


        public OptionsPanelList()
        {
            _Panels = new List<OptionsPanel>();
        }

        public OptionsPanelList(IEnumerable<OptionsPanel> collection)
        {
            _Panels = new List<OptionsPanel>(collection);
        }

        public OptionsPanelList(int capacity)
        {
            _Panels = new List<OptionsPanel>(capacity);
        }

        #endregion


        #region Methods

        public int IndexOf(OptionsPanel item)
        {
            return _Panels.IndexOf(item);
        }

        public int IndexOf(OptionsPanel item, int index)
        {
            return _Panels.IndexOf(item, index);
        }

        public int IndexOf(OptionsPanel item, int index, int count)
        {
            return _Panels.IndexOf(item, index, count);
        }

        public void Insert(int index, OptionsPanel item)
        {
            _Panels.Insert(index, item);
        }

        public void RemoveAt(int index)
        {
            _Panels.RemoveAt(index);
        }

        public void Add(OptionsPanel item)
        {
            _Panels.Add(item);

            if (PanelAdded != null)
            {
                OptionsPanelEventArgs args = new OptionsPanelEventArgs(item);

                PanelAdded(this, args);
            }
        }

        public void Clear()
        {
            _Panels.Clear();
        }

        public bool Contains(OptionsPanel item)
        {
            return _Panels.Contains(item);
        }

        public void CopyTo(OptionsPanel[] array)
        {
            _Panels.CopyTo(array);
        }

        public void CopyTo(OptionsPanel[] array, int arrayIndex)
        {
            _Panels.CopyTo(array, arrayIndex);
        }

        public void CopyTo(int index, OptionsPanel[] array, int arrayIndex, int count)
        {
            _Panels.CopyTo(index, array, arrayIndex, count);
        }

        public bool Remove(OptionsPanel item)
        {
            return _Panels.Remove(item);
        }

        IEnumerator<OptionsPanel> IEnumerable<OptionsPanel>.GetEnumerator()
        {
            return _Panels.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _Panels.GetEnumerator();
        }

        #endregion
    }

    public class OptionsPanelListEditor : CollectionEditor
    {
        public OptionsPanelListEditor()
            : base(typeof(OptionsPanelList))
        {
        }

        protected override object SetItems(object editValue, object[] value)
        {
            return base.SetItems(editValue, value);
        }
    }
}
