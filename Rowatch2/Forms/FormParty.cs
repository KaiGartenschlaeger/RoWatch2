using System.Collections.Generic;
using System.Windows.Forms;
using ExtendedControls;
using Rowatch2.Embeding;

namespace Rowatch2.Forms
{
    public partial class FormParty : FormEx
    {
        #region Fields

        private List<PartyMember> _member;

        #endregion

        #region Constructor

        public FormParty(List<PartyMember> member)
        {
            InitializeComponent();

            _member = member;
            lvwMember.VirtualListSize = _member.Count;
        }

        #endregion

        #region Methods

        public void RefreshPartyMember()
        {
            lvwMember.VirtualListSize = _member.Count;
            lvwMember.Refresh();
        }

        #endregion

        #region Control Events

        private void lvwMember_RetrieveVirtualItem(object sender, RetrieveVirtualItemEventArgs e)
        {
            PartyMember member = _member[e.ItemIndex];

            ListViewItem lvi = new ListViewItem();

            // Name
            lvi.Text = member.Name;

            // Level
            lvi.SubItems.Add(string.Format("{0} / {1}",
                member.BaseLevel, member.JobLevel));

            // Map
            lvi.SubItems.Add(member.Map);

            // HP / SP
            lvi.SubItems.Add(string.Format("{0:n0}/{1:n0} - {2:n0}/{3:n0}",
                member.HP, member.MaxHP,
                member.SP, member.MaxSP));

            // BaseExp
            lvi.SubItems.Add(((float)member.BaseExp / member.BaseExpRequired).ToString("p2"));

            // JobExp
            lvi.SubItems.Add(((float)member.JobExp / member.JobExpRequired).ToString("p2"));

            // Kills
            lvi.SubItems.Add(member.KilledMobs.ToString());

            e.Item = lvi;
        }

        #endregion
    }
}