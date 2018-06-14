using System.Windows.Forms;

namespace PluginSample
{
    public partial class FormDebug : Form
    {
        public FormDebug()
        {
            InitializeComponent();
        }

        public void AddToLog(string text)
        {
            lbxLog.Items.Add(text);
            lbxLog.SelectedIndex = lbxLog.Items.Count - 1;
        }
    }
}