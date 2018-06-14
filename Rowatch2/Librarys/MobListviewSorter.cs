using System.Collections;
using System.Windows.Forms;

namespace Rowatch2.Librarys
{
    public class MobListViewSorter : IComparer
    {
        private SortOrder _sortOrder;
        public SortOrder SortOrder
        {
            get { return _sortOrder; }
            set { _sortOrder = value; }
        }

        private int _columnIndex;
        public int ColumnIndex
        {
            get { return _columnIndex; }
            set { _columnIndex = value; }
        }

        private int _lastColumn;
        public int LastColumn
        {
            get { return _lastColumn; }
            set { _lastColumn = value; }
        }

        public int Compare(object lviA, object lviB)
        {
            ListViewItem a = lviA as ListViewItem;
            ListViewItem b = lviB as ListViewItem;

            if (_columnIndex == 1 // name
                || _columnIndex == 7 // element
                || _columnIndex == 8 // race
                || _columnIndex >= 9) // size and misc
            {
                // name
                if (_sortOrder == SortOrder.Ascending)
                {
                    return string.Compare(a.SubItems[_columnIndex].Text, b.SubItems[_columnIndex].Text);
                }
                else if (_sortOrder == SortOrder.Descending)
                {
                    return string.Compare(b.SubItems[_columnIndex].Text, a.SubItems[_columnIndex].Text);
                }
            }
            else
            {
                int aValue = int.Parse(a.SubItems[_columnIndex].Text);
                int bValue = int.Parse(b.SubItems[_columnIndex].Text);

                if (_sortOrder == System.Windows.Forms.SortOrder.Descending)
                {
                    int tValue = aValue;
                    aValue = bValue;
                    bValue = tValue;
                }

                if (aValue > bValue)
                {
                    return 1;
                }
                else if (bValue > aValue)
                {
                    return -1;
                }
            }

            return 0;
        }
    }
}