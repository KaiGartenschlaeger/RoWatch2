using ExtendedControls;
using Rowatch2.Globals;
using System.Collections.Generic;

namespace Rowatch2.Forms
{
    public partial class FormCharts : FormEx
    {
        #region Fields

        private ChartValues _chartValues;

        #endregion

        #region Constructor

        public FormCharts(ChartValues chartValues)
        {
            InitializeComponent();

            _chartValues = chartValues;

            cbxChartType.SelectedIndex = 0;
            cbxTime.SelectedIndex = 2;
        }

        #endregion

        #region Control Events

        private void cbxChartType_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            RefreshChart();
        }

        private void cbxTime_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            int barCount;
            switch (cbxTime.SelectedIndex)
            {
                default:
                    barCount = 10;
                    break;
                case 1: // 30
                    barCount = 30;
                    break;
                case 2: // 60
                    barCount = 60;
                    break;
                case 3: // 120
                    barCount = 120;
                    break;
                case 4: // 320
                    barCount = 320;
                    break;
            }

            barChart1.MaxLines = barCount;
            RefreshChart();
        }

        #endregion

        #region Methods

        public void RefreshChart()
        {
            barChart1.BeginUpdate();

            barChart1.Clear();

            List<int> values;
            switch (cbxChartType.SelectedIndex)
            {
                case 1: // Gained Job EXP
                    values = _chartValues.GainedJobExpValues;
                    break;
                case 2: // Killed Mobs
                    values = _chartValues.KilledMobsValues;
                    break;
                case 3: // Gained Homunculus EXP
                    values = _chartValues.GainedHomunculusExpValues;
                    break;

                default: // Gained Base EXP
                    values = _chartValues.GainedBaseExpValues;
                    break;
            }

            barChart1.AddRange(values);
            barChart1.EndUpdate();
        }

        #endregion
    }
}