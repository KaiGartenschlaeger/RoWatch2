using System;
using System.Collections.Generic;
using System.Drawing;
using ExtendedControls;
using Rowatch2.Embeding;
using Rowatch2.Enums;

namespace Rowatch2.Forms
{
    public partial class FormCharCalculator : FormEx
    {
        #region Fields

        private List<JobClass> _jobClasses;

        private int _baseLevel;
        private int _jobLevel;

        private int _str;
        private int _strBoni;
        private int _agi;
        private int _agiBoni;
        private int _int;
        private int _intBoni;
        private int _vit;
        private int _vitBoni;
        private int _dex;
        private int _dexBoni;
        private int _luk;
        private int _lukBoni;

        #endregion

        #region Constructor

        public FormCharCalculator(List<JobClass> jobClasses)
        {
            InitializeComponent();

            _jobClasses = jobClasses;
            foreach (var jobClass in _jobClasses)
            {
                cbxJobClass.Items.Add(jobClass);
            }
            if (cbxJobClass.Items.Count > 0)
            {
                cbxJobClass.SelectedIndex = 0;
            }

            InitializeControls();
        }

        #endregion

        #region Initialize Methods

        private void InitializeControls()
        {
        }

        #endregion

        #region Calculation

        private void CalcJobBoni()
        {
            JobClass jobClass = cbxJobClass.SelectedItem as JobClass;
            if (jobClass != null)
            {
                _strBoni = 0;
                _agiBoni = 0;
                _vitBoni = 0;
                _intBoni = 0;
                _dexBoni = 0;
                _lukBoni = 0;

                foreach (var jobBoni in jobClass.JobBonus)
                {
                    foreach (var boniLevel in jobBoni.JobLevels)
                    {
                        if (_jobLevel >= boniLevel)
                        {
                            switch (jobBoni.StatsType)
                            {
                                case StatsType.Str:
                                    _strBoni++;
                                    break;
                                case StatsType.Agi:
                                    _agiBoni++;
                                    break;
                                case StatsType.Vit:
                                    _vitBoni++;
                                    break;
                                case StatsType.Int:
                                    _intBoni++;
                                    break;
                                case StatsType.Dex:
                                    _dexBoni++;
                                    break;
                                case StatsType.Luk:
                                    _lukBoni++;
                                    break;
                            }
                        }
                    }
                }

            }
        }

        private int CalcRequiredStatPoints(int statusPoints)
        {
            int result = 0;
            result = (int)Math.Floor(((double)statusPoints - 2) / 10) + 2;

            return result;
        }

        private int CalcStatuspoints()
        {
            // current
            double stPoints = 0;

            JobClass jobClass = cbxJobClass.SelectedItem as JobClass;
            if (jobClass != null)
            {
                stPoints = jobClass.Statspoints;
            }

            for (int i = 1; i < _baseLevel; i++)
            {
                stPoints += Math.Floor((double)i / 5) + 3;
            }

            // required
            double rStPoints = 0;

            for (int i = 2; i <= _str; i++)
            {
                rStPoints += CalcRequiredStatPoints(i);
            }
            for (int i = 2; i <= _agi; i++)
            {
                rStPoints += CalcRequiredStatPoints(i);
            }
            for (int i = 2; i <= _vit; i++)
            {
                rStPoints += CalcRequiredStatPoints(i);
            }
            for (int i = 2; i <= _int; i++)
            {
                rStPoints += CalcRequiredStatPoints(i);
            }
            for (int i = 2; i <= _dex; i++)
            {
                rStPoints += CalcRequiredStatPoints(i);
            }
            for (int i = 2; i <= _luk; i++)
            {
                rStPoints += CalcRequiredStatPoints(i);
            }

            // result
            return (int)(stPoints - rStPoints);
        }

        private int CalcStatusAtk()
        {
            int result = 0;

            result = _str + _strBoni;

            result += ((_dex + _dexBoni) / 5);
            result += ((_luk + _lukBoni) / 3);
            result += (_baseLevel / 4);

            return result;
        }

        private int CalcHit()
        {
            int result = 0;
            result = _baseLevel + (_dex + _dexBoni) + (int)Math.Floor((double)(_luk + _lukBoni) / 3) + 175; // + Hit Equip + Hit Skills

            return result;
        }

        private int CalcFlee()
        {
            int result = 0;
            result = _baseLevel + (_agi + _agiBoni) + (int)Math.Floor((double)(_luk + _lukBoni) / 5) + 100; // + Flee Equip + Flee Skills

            return result;
        }

        private int CalcCritical()
        {
            int result = 0;
            result = (int)Math.Floor((_luk + _lukBoni) * 0.3);

            if (result == 0)
            {
                result = 1;
            }

            return result;
        }

        private int CalcStatusMatk()
        {
            int result = 0;
            result = (_int + _intBoni) + ((_int + _intBoni) / 2) + ((_dex + _dexBoni) / 5) + ((_luk + _lukBoni) / 3) + (_baseLevel / 4);

            return result;
        }

        private int CalcStatusMDef()
        {
            int result = 0;
            result = (_int + _intBoni) + ((_vit + _vitBoni) / 5) + ((_dex + _dexBoni) / 5) + (_baseLevel / 4);

            return result;
        }

        private void Calculate()
        {
            // Hit
            int hit = CalcHit();
            lblHit.Text = hit.ToString();

            // Flee
            int flee = CalcFlee();
            lblFlee.Text = flee.ToString();

            // Crit
            int crit = CalcCritical();
            lblCrit.Text = crit.ToString();

            // Status Atk
            int statusAtk = CalcStatusAtk();
            lblAtk.Text = statusAtk.ToString();

            // Status MAtk
            int statusMAtk = CalcStatusMatk();
            lblMatk.Text = statusMAtk.ToString();

            // Status MDef
            int statusMDEF = CalcStatusMDef();
            lblDef.Text = statusMDEF.ToString();

            // Status Points
            int statusPoints = CalcStatuspoints();

            lblStatuspoints.Text = statusPoints.ToString();
            if (statusPoints < 0)
            {
                lblStatuspoints.ForeColor = Color.Red;
            }
            else
            {
                lblStatuspoints.ForeColor = SystemColors.ControlText;
            }
        }

        #endregion

        #region Control Events

        private void cbxJobClass_SelectedIndexChanged(object sender, EventArgs e)
        {
            JobClass jobClass = cbxJobClass.SelectedItem as JobClass;
            if (jobClass != null)
            {
                cbxBaseLevel.BeginUpdate();
                cbxBaseLevel.Items.Clear();
                for (int i = 0; i < jobClass.MaxBLvl; i++)
                {
                    cbxBaseLevel.Items.Add((i + 1).ToString());
                }
                cbxBaseLevel.SelectedIndex = 0;
                cbxBaseLevel.EndUpdate();

                cbxJobLevel.BeginUpdate();
                cbxJobLevel.Items.Clear();
                for (int i = 0; i < jobClass.MaxJLvl; i++)
                {
                    cbxJobLevel.Items.Add((i + 1).ToString());
                }
                cbxJobLevel.SelectedIndex = 0;
                cbxJobLevel.EndUpdate();

                tbxStr.Text = "1";
                tbxAgi.Text = "1";
                tbxVit.Text = "1";
                tbxInt.Text = "1";
                tbxDex.Text = "1";
                tbxLuk.Text = "1";
            }
        }

        private void cbxBaseLevel_SelectedIndexChanged(object sender, EventArgs e)
        {
            _baseLevel = cbxBaseLevel.SelectedIndex + 1;

            Calculate();
        }

        private void cbxJobLevel_SelectedIndexChanged(object sender, EventArgs e)
        {
            _jobLevel = cbxJobLevel.SelectedIndex + 1;

            CalcJobBoni();
            lblStrBoni.Text = _strBoni.ToString();
            lblAgiBoni.Text = _agiBoni.ToString();
            lblVitBoni.Text = _vitBoni.ToString();
            lblIntBoni.Text = _intBoni.ToString();
            lblDexBoni.Text = _dexBoni.ToString();
            lblLukBoni.Text = _lukBoni.ToString();

            Calculate();
        }

        private void tbxStr_TextChanged(object sender, EventArgs e)
        {
            int.TryParse(tbxStr.Text.Trim(), out _str);

            Calculate();
        }

        private void tbxAgi_TextChanged(object sender, EventArgs e)
        {
            int.TryParse(tbxAgi.Text.Trim(), out _agi);

            Calculate();
        }

        private void tbxInt_TextChanged(object sender, EventArgs e)
        {
            int.TryParse(tbxInt.Text.Trim(), out _int);

            Calculate();
        }

        private void tbxVit_TextChanged(object sender, EventArgs e)
        {
            int.TryParse(tbxVit.Text.Trim(), out _vit);

            Calculate();
        }

        private void tbxDex_TextChanged(object sender, EventArgs e)
        {
            int.TryParse(tbxDex.Text.Trim(), out _dex);

            Calculate();
        }

        private void tbxLuk_TextChanged(object sender, EventArgs e)
        {
            int.TryParse(tbxLuk.Text.Trim(), out _luk);

            Calculate();
        }

        #endregion
    }
}