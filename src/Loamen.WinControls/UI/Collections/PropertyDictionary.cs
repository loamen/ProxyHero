using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace Loamen.WinControls.UI.Collections
{
    public class PropertyDictionary<TKey, TValue> : IDictionary<TKey, TValue>
    {
        private readonly Dictionary<TKey, TValue> _internalDict = new Dictionary<TKey, TValue>();

        public string Name { get; set; }

        public void Add(TKey key, TValue value)
        {
            SendPropertyChanging();
            _internalDict.Add(key, value);
            SendPropertyChanged(key.ToString());
        }

        public bool ContainsKey(TKey key)
        {
            return _internalDict.ContainsKey(key);
        }

        public ICollection<TKey> Keys
        {
            get { return _internalDict.Keys; }
        }

        public bool Remove(TKey key)
        {
            SendPropertyChanging();
            var removed =_internalDict.Remove(key);
            SendPropertyChanged(key.ToString());
            return removed;
        }

        public bool TryGetValue(TKey key, out TValue value)
        {
            return _internalDict.TryGetValue(key, out value);
        }

        public ICollection<TValue> Values
        {
            get { return _internalDict.Values; }
        }

        public TValue this[TKey key]
        {
            get
            {
                return _internalDict[key];
            }
            set
            {
                SendPropertyChanging();
                _internalDict[key] = value;
                SendPropertyChanged(key.ToString());
            }
        }


        #region ICollection<KeyValuePair<int,TValue>> Members

        public void Add(KeyValuePair<TKey, TValue> item)
        {
            SendPropertyChanging();
            _internalDict.Add(item.Key, item.Value);
            SendPropertyChanged(item.Key.ToString());
        }

        public void Clear()
        {
            SendPropertyChanging();
            _internalDict.Clear();
        }

        public bool Contains(KeyValuePair<TKey, TValue> item)
        {
            return (_internalDict.ContainsKey(item.Key) && _internalDict.ContainsValue(item.Value));
        }

        public void CopyTo(KeyValuePair<TKey, TValue>[] array, int arrayIndex)
        {
            //Could be done but you prolly could figure this out yourself;
            throw new Exception("do not use");
        }

        public bool Remove(KeyValuePair<TKey, TValue> item)
        {
            SendPropertyChanging();
            var removed = (_internalDict.ContainsKey(item.Key) && _internalDict.Remove(item.Key));
            SendPropertyChanged(item.Key.ToString());
            return removed;
        }

        public int Count
        {
            get { return _internalDict.Count; }
        }

        public bool IsReadOnly
        {
            get { return false; }
        }

        #endregion

        #region IEnumerable<KeyValuePair<TKey,TValue>> Members

        public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator()
        {
            return _internalDict.GetEnumerator();
        }

        #endregion

        #region IEnumerable Members

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return _internalDict.GetEnumerator();
        }


        #endregion

        #region 属性事件

        public event PropertyChangingEventHandler PropertyChanging;

        public event PropertyChangedEventHandler PropertyChanged;

        private static readonly PropertyChangingEventArgs emptyChangingEventArgs =
            new PropertyChangingEventArgs(string.Empty);

        protected virtual void SendPropertyChanging()
        {
            if ((PropertyChanging != null))
            {
                PropertyChanging(this, emptyChangingEventArgs);
            }
        }

        protected virtual void SendPropertyChanged(string propertyName)
        {
            if ((PropertyChanged != null))
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        public static PropertyDictionary<string, object> Convert(object entity)
        {
            var op = new PropertyDictionary<string, object>();
            var type = entity.GetType();
            foreach (var pro in type.GetProperties())
            {
                op.Add(pro.Name, pro.GetValue(entity, null));
            }
            return op;
        }

        #endregion
    }
}
