using ExtendedControls;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Rowatch2.Forms
{
    public partial class FormExpCounter : FormEx
    {
        #region Fields

        private class ExpCounter
        {
            public int Exp { get; set; }
            public int Counter { get; set; }
            public int ListViewIndex { get; set; }
        }

        private List<ExpCounter> m_counter;

        #endregion

        #region Constructor

        public FormExpCounter()
        {
            InitializeComponent();

            m_counter = new List<ExpCounter>();
        }

        #endregion

        #region Methods

        private int m_searchExp = -1;
        private bool FindCounter(ExpCounter counter)
        {
            if (m_searchExp == counter.Exp)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public void AddExp(int exp)
        {
            m_searchExp = exp;

            int index = m_counter.FindIndex(new Predicate<ExpCounter>(FindCounter));
            if (index != -1)
            {
                m_counter[index].Counter++;

                lvwCounter.Items[m_counter[index].ListViewIndex].SubItems[1].Text = m_counter[index].Counter.ToString("n0");
            }
            else
            {
                ExpCounter counter = new ExpCounter();
                counter.Exp = exp;
                counter.Counter = 1;

                int insertIndex = 0;
                for (int i = 0; i < m_counter.Count; i++)
                {
                    if (m_counter[i].Exp > exp)
                    {
                        m_counter[i].ListViewIndex++;
                    }
                    else
                    {
                        insertIndex++;
                    }
                }

                m_counter.Insert(insertIndex, counter);

                ListViewItem newItem = new ListViewItem();
                newItem.Text = exp.ToString("n0");
                newItem.SubItems.Add("1");
                lvwCounter.Items.Insert(insertIndex, newItem);
            }
        }

        #endregion
    }
}