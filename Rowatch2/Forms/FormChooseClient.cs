using ExtendedControls;

namespace Rowatch2.Forms
{
    public partial class FormChooseClient : FormEx
    {
        #region Constructor

        public FormChooseClient()
        {
            InitializeComponent();
        }

        #endregion

        #region Methods

        public void AddClient(string name)
        {
            lbxClients.Items.Add(name);
            if (lbxClients.Items.Count == 1)
            {
                lbxClients.SelectedIndex = 0;
            }
        }

        #endregion

        #region Properties

        public int SelectedClient
        {
            get
            {
                return lbxClients.SelectedIndex;
            }
        }

        #endregion
    }
}