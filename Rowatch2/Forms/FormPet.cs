using ExtendedControls;
using System.Windows.Forms;

namespace Rowatch2.Forms
{
    public partial class FormPet : FormEx
    {
        #region Constructor

        public FormPet()
        {
            InitializeComponent();
            ResetValues();
        }

        #endregion

        #region Properties

        public string PetName
        {
            set
            {
                SetLabelText(lblName, value);
            }

            get { return lblName.Text; }
        }

        public int PetFriendly
        {
            set
            {
                if (value == prbFriendly.Value)
                {
                    return;
                }

                prbFriendly.Value = value;
                lblFriendlyText.Text = value.ToString("n0");
            }
        }

        public int PetHungry
        {
            set
            {
                if (value == prbHungry.Value)
                {
                    return;
                }

                prbHungry.Value = value;
                lblHungryText.Text = value.ToString("n0");
            }
        }

        #endregion

        #region Helper

        private void SetLabelText(Label control, string text)
        {
            if (control.Text != text)
            {
                control.Text = text;
            }
        }

        public void ResetValues()
        {
            SetLabelText(lblName, string.Empty);
            SetLabelText(lblFriendlyText, string.Empty);
            SetLabelText(lblHungryText, string.Empty);

            prbHungry.Value = 0;
            prbHungry.Value = 0;
        }

        #endregion
    }
}