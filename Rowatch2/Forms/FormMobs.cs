using ExtendedControls;
using Rowatch2.Embeding;
using Rowatch2.Enums;
using Rowatch2.Globals;
using Rowatch2.Librarys;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

namespace Rowatch2.Forms
{
    public partial class FormMobs : FormEx
    {
        #region Fields

        private List<Mob> _mobs;

        private CharacterInfo _charInfo;
        private ProgramSettings _settings;

        private MobListViewSorter _sorter;

        #endregion

        #region Constructor

        public FormMobs(List<Mob> mobs, CharacterInfo charInfo, ProgramSettings settings)
        {
            InitializeComponent();

            _settings = settings;

            _sorter = new MobListViewSorter();
            lvwMobs.ListViewItemSorter = _sorter;

            _mobs = mobs;
            _charInfo = charInfo;

            cbxElement.Items.Add("All");
            cbxElement.SelectedIndex = 0;
            foreach (string element in Enum.GetNames(typeof(MobElement)))
            {
                if (element != "All")
                {
                    cbxElement.Items.Add(element);
                }
            }

            cbxRace.Items.Add("All");
            cbxRace.SelectedIndex = 0;
            foreach (string race in Enum.GetNames(typeof(MobRace)))
            {
                if (race != "All")
                {
                    cbxRace.Items.Add(race);
                }
            }

            lnkMobinfo.Enabled = false;
            lblSearchstatus.Text = string.Empty;

            SearchMobs(false);
        }

        #endregion

        #region Methods

        private void SearchMobs(bool bySearch)
        {
            // recalc exp factor
            float expServerFactor = (float)nudServerExpFactor.Value / 100f;
            int lvlDifference = (int)nudOptLvlDiff.Value;

            MobElement searchedElement = MobElement.All;
            Helper.ToEnum<MobElement>(cbxElement.Text, searchedElement);

            MobRace searchedRace = MobRace.All;
            Helper.ToEnum<MobRace>(cbxRace.Text, searchedRace);

            int minLvl = 0, maxLvl = 0;
            int.TryParse(tbxMinLvl.Text.Trim(), out minLvl);
            int.TryParse(tbxMaxLvl.Text.Trim(), out maxLvl);

            // prepare listview
            lvwMobs.BeginUpdate();
            lvwMobs.Items.Clear();

            // get searchkeywords
            List<int> searchIDs = new List<int>();
            List<string> searchKeywords = new List<string>();
            if (bySearch)
            {
                foreach (string keyword in
                    tbxSearch.Text.Trim().ToLower().Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    if (Helper.IsNumber(keyword))
                    {
                        searchIDs.Add(Convert.ToInt32(keyword));
                    }
                    else
                    {
                        searchKeywords.Add(keyword);
                    }
                }
            }

            // search mobs
            foreach (Mob mob in _mobs)
            {
                // calculate
                bool addMob = false;
                int mobLvlDifference = _charInfo.BaseLevel - mob.Level;
                string mobname = mob.IRoName.ToLower() + " " + mob.KRoName.ToLower();

                // check mob for adding to listview
                if (bySearch)
                {
                    // level
                    if (minLvl > 0 && mob.Level < minLvl)
                    {
                        continue;
                    }
                    if (maxLvl > 0 && mob.Level > maxLvl)
                    {
                        continue;
                    }

                    // race
                    if (searchedElement != MobElement.All && searchedElement != mob.Element)
                    {
                        continue;
                    }
                    // element
                    if (searchedRace != MobRace.All && searchedRace != mob.Race)
                    {
                        continue;
                    }

                    if (searchIDs.Count == 0 && searchKeywords.Count == 0)
                    {
                        addMob = true;
                    }
                    else
                    {
                        // keyword
                        foreach (string searchKeyword in searchKeywords)
                        {
                            if (mobname.Contains(searchKeyword))
                            {
                                addMob = true;
                                break;
                            }
                        }

                        // ids
                        if (!addMob)
                        {
                            foreach (int id in searchIDs)
                            {
                                if (mob.ID == id)
                                {
                                    addMob = true;
                                    break;
                                }
                            }
                        }
                    }
                }
                else
                {
                    // mode
                    if (!chbBosMobs.Checked && ((mob.Mode & MobMode.MD_BOSS) == MobMode.MD_BOSS))
                    {
                        continue;
                    }

                    addMob = (!chbOptimizedLevel.Checked || (mobLvlDifference >= -lvlDifference && mobLvlDifference <= lvlDifference))
                        && (mob.Exp > 0 && mob.JobExp > 0);
                }

                // add mob
                if (addMob)
                {
                    AddMob(mob, mobLvlDifference, expServerFactor);
                }
            }

            // finish listview
            lvwMobs.EndUpdate();

            if (lvwMobs.Items.Count > 0)
            {
                lblSearchstatus.Text = string.Format("{0:n0} Mobs found", lvwMobs.Items.Count);
            }
            else
            {
                lblSearchstatus.Text = "Nothing found";
            }
        }

        private void AddMob(Mob mob, int lvlDifference, float expServerFactor)
        {
            float expFactor = GetExpFactor(mob);

            int bExp = (int)((mob.Exp * expFactor) * expServerFactor);
            int jExp = (int)((mob.JobExp * expFactor) * expServerFactor);

            int requiredHit = 200 + mob.Level + mob.Agi;
            int requiredFlee = 170 + mob.Level + mob.Dex;

            ListViewItem lvi = new ListViewItem();

            lvi.UseItemStyleForSubItems = true;
            if (lvlDifference <= -6)
            {
                lvi.BackColor = Color.FromArgb(255, 231, 231);
            }
            else if (lvlDifference < 6)
            {
                lvi.BackColor = Color.FromArgb(255, 254, 227);
            }

            lvi.Tag = mob.ID;

            lvi.Text = mob.ID.ToString();
            lvi.SubItems.Add(mob.IRoName);

            lvi.SubItems.Add(mob.Level.ToString());

            lvi.SubItems.Add(requiredHit.ToString());
            lvi.SubItems.Add(requiredFlee.ToString());

            lvi.SubItems.Add(bExp.ToString());
            lvi.SubItems.Add(jExp.ToString());

            lvi.SubItems.Add(mob.Element.ToString() + " " + (mob.ElementLvl / 2).ToString());
            lvi.SubItems.Add(mob.Race.ToString());
            lvi.SubItems.Add(mob.Scale.ToString());

            lvwMobs.Items.Add(lvi);
        }

        private float GetExpFactor(Mob mob)
        {
            float result = 0f;
            int difference = _charInfo.BaseLevel - mob.Level;

            if (difference < -10)
            {
                result = 0.4f;
            }
            else if (difference == -15)
            {
                result = 1.15f;
            }
            else if (difference == -14)
            {
                result = 1.2f;
            }
            else if (difference == -13)
            {
                result = 1.25f;
            }
            else if (difference == -12)
            {
                result = 1.3f;
            }
            else if (difference == -11)
            {
                result = 1.35f;
            }
            else if (difference == -10)
            {
                result = 1.4f;
            }
            else if (difference == -9)
            {
                result = 1.35f;
            }
            else if (difference == -8)
            {
                result = 1.3f;
            }
            else if (difference == -7)
            {
                result = 1.25f;
            }
            else if (difference == -6)
            {
                result = 1.2f;
            }
            else if (difference == -5)
            {
                result = 1.15f;
            }
            else if (difference == -4)
            {
                result = 1.1f;
            }
            else if (difference == -3)
            {
                result = 1.05f;
            }
            else if (difference >= -3 && difference <= 5)
            {
                result = 1f;
            }
            else if (difference >= 6 && difference <= 10)
            {
                result = 0.95f;
            }
            else if (difference >= 11 && difference <= 15)
            {
                result = 0.9f;
            }
            else if (difference >= 16 && difference <= 20)
            {
                result = 0.85f;
            }
            else if (difference >= 21 && difference <= 25)
            {
                result = 0.6f;
            }
            else if (difference >= 26 && difference <= 30)
            {
                result = 0.35f;
            }
            else if (difference > 30)
            {
                result = 0.1f;
            }

            return result;
        }

        #endregion

        #region Control Events

        private void lvwMobs_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            _sorter.ColumnIndex = e.Column;

            if (_sorter.LastColumn != e.Column)
            {
                _sorter.SortOrder = SortOrder.Ascending;
            }
            else
            {
                if (_sorter.SortOrder == SortOrder.Ascending)
                {
                    _sorter.SortOrder = SortOrder.Descending;
                }
                else
                {
                    _sorter.SortOrder = SortOrder.Ascending;
                }
            }

            lvwMobs.Sort();

            _sorter.LastColumn = e.Column;
        }

        private void btnRefresh_Click(object sender, System.EventArgs e)
        {
            SearchMobs(false);
        }

        private void lvwMobs_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            lnkMobinfo.Enabled = (lvwMobs.SelectedIndices.Count > 0);
        }

        private void lnkMobinfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (lvwMobs.SelectedIndices.Count > 0 && !string.IsNullOrEmpty(_settings.MobDbUrl))
            {
                int mobId = Convert.ToInt32(lvwMobs.SelectedItems[0].Tag);

                Process.Start(_settings.MobDbUrl.Replace("$mobID$", mobId.ToString()));
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            SearchMobs(true);
        }

        private void tbxSearch_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                SearchMobs(true);
            }
        }

        #endregion
    }
}