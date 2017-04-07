using System;
using System.ComponentModel;
using System.Windows.Forms;
using Loamen.WinControls.Properties;

namespace Loamen.WinControls.UI
{
    public partial class DataGridViewEx : DataGridView
    {
        public DataGridViewEx()
        {
            InitializeComponent();
        }

        public DataGridViewEx(IContainer container)
        {
            container.Add(this);

            InitializeComponent();
        }
    }

    #region [ DataGridViewDoneColumn ]

    public class DataGridViewDoneCell : DataGridViewImageCell
    {
        #region [ Constructor(s) ]

        #endregion

        #region [ Overrided Method(s) ]

        protected override object GetFormattedValue(object value, int rowIndex, ref DataGridViewCellStyle cellStyle,
                                                    TypeConverter valueTypeConverter,
                                                    TypeConverter formattedValueTypeConverter,
                                                    DataGridViewDataErrorContexts context)
        {
            if (value != null && value != DBNull.Value)
            {
                switch ((int) value)
                {
                    case 0:
                        return Resources.dead;
                    case 1:
                        return Resources.alive;
                    default:
                        return Resources.unknown;
                }
            }
            else
            {
                return null;
            }
        }

        #endregion
    }

    public class DataGridViewDoneColumn : DataGridViewImageColumn
    {
        #region [ Constructor(s) ]

        public DataGridViewDoneColumn()
        {
            CellTemplate = new DataGridViewDoneCell();
        }

        #endregion
    }

    #endregion
}