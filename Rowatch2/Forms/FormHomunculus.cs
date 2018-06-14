using System.Drawing;
using System.Windows.Forms;
using ExtendedControls;

namespace Rowatch2.Forms
{
    public partial class FormHomunculus : FormEx
    {
        #region Constructor

        public FormHomunculus()
        {
            InitializeComponent();

            ResetValues();
        }

        #endregion

        #region Properties

        public string Homunculus_Name
        {
            get { return lblName.Text; }
            set
            {
                SetLabelText(lblName, value);
            }
        }

        public int Homunculus_Exp
        {
            get { return prbExp.Value; }
            set
            {
                prbExp.Value = value;
            }
        }
        public string Homunculus_ExpText
        {
            get { return lblExpText.Text; }
            set
            {
                SetLabelText(lblExpText, value);
            }
        }

        public int Homunculus_Hp
        {
            get { return prbHP.Value; }
            set
            {
                prbHP.Value = value;
            }
        }
        public string Homunculus_HpText
        {
            get { return lblHPText.Text; }
            set
            {
                SetLabelText(lblHPText, value);
            }
        }

        public int Homunculus_Hungry
        {
            get { return prbHungry.Value; }
            set
            {
                if (value == prbHungry.Value)
                {
                    return;
                }

                prbHungry.Value = value;

                if (prbHungry.Value <= 10 && prbHungry.StartColor != Color.Red)
                {
                    prbHungry.StartColor = Color.Red;
                }
                else if (prbHungry.Value >= 11 && prbHungry.Value <= 25)
                {
                    prbHungry.StartColor = Color.LimeGreen;
                }
                else if (prbHungry.Value > 25 && prbHungry.StartColor != Color.DeepSkyBlue)
                {
                    prbHungry.StartColor = Color.DeepSkyBlue;
                }
            }
        }
        public string Homunculus_HungryText
        {
            get { return lblHungryText.Text; }
            set
            {
                SetLabelText(lblHungryText, value);
            }
        }

        public int Homunculus_Friendly
        {
            get { return prbFriendly.Value; }
            set
            {
                if (prbFriendly.Value == value)
                {
                    return;
                }

                prbFriendly.Value = value;
            }
        }
        public string Homunculus_FriendlyText
        {
            get { return lblFriendlyText.Text; }
            set
            {
                SetLabelText(lblFriendlyText, value);
            }
        }

        #endregion

        #region Methods

        private string GetFriendlyText(int friendly)
        {
            //1-3	Hate with Passion
            //4-10	Hate
            //11-100	Awkward
            //101-250	Shy
            //251-750	Neutral
            //751-910	Cordial
            //911-1000	Loyal

            string result;
            if (friendly <= 3)
            {
                result = "Hate with Passion";
            }
            else if (friendly >= 4 && friendly <= 10)
            {
                result = "Hate";
            }
            else if (friendly >= 11 && friendly <= 100)
            {
                result = "Awkward";
            }
            else if (friendly >= 101 && friendly <= 250)
            {
                result = "Shy";
            }
            else if (friendly >= 251 && friendly <= 750)
            {
                result = "Neutral";
            }
            else if (friendly >= 751 && friendly <= 910)
            {
                result = "Cordial";
            }
            else
            {
                result = "Loyal";
            }

            return result;
        }

        public void ResetValues()
        {
            SetLabelText(lblName, string.Empty);
            SetLabelText(lblExpText, string.Empty);
            SetLabelText(lblFriendlyText, string.Empty);
            SetLabelText(lblHPText, string.Empty);
            SetLabelText(lblHungryText, string.Empty);

            prbExp.Value = 0;
            prbHungry.Value = 0;
            prbHP.Value = 0;
            prbHungry.Value = 0;
        }

        private void SetLabelText(Label control, string text)
        {
            if (control.Text != text)
            {
                control.Text = text;
            }
        }

        #endregion
    }
}