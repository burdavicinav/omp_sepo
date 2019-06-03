using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace omp_sepo
{
    public class FixtureAttachFileLogCompare : IComparer
    {
        public int Column { get; set; }

        public int SortOrder { get; set; }

        public FixtureAttachFileLogCompare()
        {
        }

        public FixtureAttachFileLogCompare(int column, int order)
            : this()
        {
            this.Column = column;
            this.SortOrder = order;
        }

        public int Compare(object x, object y)
        {
            ListViewItem xItem = x as ListViewItem;
            ListViewItem yItem = y as ListViewItem;

            int result = 0;

            // DOC_ID
            if (Column == 0)
            {
                int xDoc = 0, yDoc = 0;

                if (xItem.Text != String.Empty)
                {
                    int.TryParse(xItem.Text, out xDoc);
                }

                if (yItem.Text != String.Empty)
                {
                    int.TryParse(yItem.Text, out yDoc);
                }

                if (xDoc > yDoc)
                {
                    result = 1;
                }
                else if (xDoc < yDoc)
                {
                    result = -1;
                }
                else
                {
                    result = 0;
                }

                return result * SortOrder;
            }
            else
            {
                return String.Compare(xItem.SubItems[Column].Text, yItem.SubItems[Column].Text) * SortOrder;
            }
        }
    }
}