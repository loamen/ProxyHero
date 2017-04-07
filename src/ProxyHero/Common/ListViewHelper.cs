using System.Collections.Generic;
using System.Windows.Forms;
using Loamen.PluginFramework;

namespace ProxyHero.Common
{
    internal class ListViewHelper : ViewDataAdapter<ProxyServer>
    {
        public ListViewHelper(ListView view) : base(view)
        {
        }

        protected override void OnCreateColumns()
        {
            ColumnHeader col;
            col = new ColumnHeader();
            col.Text = "代理";
            col.Width = 200;
            ListView.Columns.Add(col);
            col = new ColumnHeader();
            col.Text = "端口";
            col.Width = 500;
            ListView.Columns.Add(col);
        }

        protected override ListViewItem OnCreateItem(ProxyServer item)
        {
            return new ListViewItem(
                new[] {item.proxy, item.port + ""});
        }
    }

    /// <summary>
    ///     基于ListView控件的数据显示描述接口
    /// </summary>
    /// <typeparam name="T">相关数据实体类型</typeparam>
    public interface IViewData<T>
    {
        /// <summary>
        ///     获取当前选择的数据项
        /// </summary>
        T SelectItem { get; }

        /// <summary>
        ///     获取相关的ListView控件
        /// </summary>
        ListView ListView { get; }

        /// <summary>
        ///     数据绑定过程
        /// </summary>
        /// <param name="items">数据实体对象集</param>
        void DataBind(IList<T> items);

        /// <summary>
        ///     根据实体对象构造相关显示项
        /// </summary>
        /// <param name="item">数据实体对象</param>
        /// <returns>ListViewItem</returns>
        ListViewItem CreateItem(T item);

        /// <summary>
        ///     创建相关列信息
        /// </summary>
        void CreateColumns();

        /// <summary>
        ///     数据绑定事件,每一项数据绑定会引发这个事件
        /// </summary>
        event EventViewDataBound<T> ViewDataBound;
    }


    /// <summary>
    ///     数据绑定委托描述
    /// </summary>
    /// <typeparam name="T">类型实体类型</typeparam>
    /// <param name="item">实体对象</param>
    /// <param name="viewitem">列表项对象</param>
    public delegate void EventViewDataBound<T>(T item, ListViewItem viewitem);

    /// <summary>
    ///     数据显示适配器对象
    ///     抽象基本通过功能,简化派生类的实现
    /// </summary>
    /// <typeparam name="T">相关数据实体类型</typeparam>
    public abstract class ViewDataAdapter<T> : IViewData<T>
    {
        public ViewDataAdapter(ListView view)
        {
            mListView = view;
            CreateColumns();
            ListView.FullRowSelect = true;
        }

        #region IViewData<T> 成员

        private readonly ListView mListView;

        public void DataBind(IList<T> items)
        {
            ListViewItem vi;
            foreach (T item in items)
            {
                vi = CreateItem(item);
                if (ViewDataBound != null)
                    ViewDataBound(item, vi);
                ListView.Items.Add(vi);
            }
        }

        public ListViewItem CreateItem(T item)
        {
            ListViewItem vitem = OnCreateItem(item);
            vitem.Tag = item;
            return vitem;
        }

        public void CreateColumns()
        {
            if (ListView.Columns.Count > 0)
                return;
            OnCreateColumns();
        }

        public T SelectItem
        {
            get
            {
                if (ListView.SelectedItems.Count == 0)
                    return default(T);
                return (T) ListView.SelectedItems[0].Tag;
            }
        }

        public event EventViewDataBound<T> ViewDataBound;

        public ListView ListView
        {
            get { return mListView; }
        }

        protected abstract ListViewItem OnCreateItem(T item);
        protected abstract void OnCreateColumns();

        #endregion
    }
}