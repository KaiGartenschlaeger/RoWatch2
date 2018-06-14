using System.Collections.Generic;
using System.Drawing;

namespace Rowatch2.Globals
{
    public class ProgramSettings
    {
        public ProgramSettings()
        {
            Addresses = new Addresses();
            AutoPotions = new AutoPotions();
            AutoKey = new AutoKey();
            AutoWing = new AutoWing();
            WindowPositions = new Dictionary<string, Rectangle>();
        }

        public string Language { get; set; }

        public string PartyServerIP { get; set; }

        public Addresses Addresses { get; set; }

        public AutoPotions AutoPotions { get; set; }
        public AutoWing AutoWing { get; set; }
        public AutoKey AutoKey { get; set; }

        public string WindowTitle { get; set; }
        public int LogTypes { get; set; }
        public bool AddTimeInLog { get; set; }
        public bool TopMost { get; set; }
        public bool Opacity { get; set; }

        public string MobDbUrl { get; set; }

        public string GameDirectory { get; set; }
        public string ClientExeFilename { get; set; }
        public string SetupExeFilename { get; set; }

        public bool BaseupScreenshot { get; set; }
        public int BaseupScreenshotDelay { get; set; }

        public bool JobupScreenshot { get; set; }
        public int JobupScreenshotDelay { get; set; }

        public int MinLevelScreenshots { get; set; }

        public bool CashRegisterSound_Enabled { get; set; }
        public int CashRegisterSound_MinimumZeny { get; set; }
        public int CashRegisterSound_MaximumZeny { get; set; }

        // Homunculus
        public bool HomunculusHungryWarnSound { get; set; }
        public string HomunculusHungryWarningSound { get; set; }
        public int HomunculusHungryMinValue { get; set; }
        public bool HomunculusHungryCloseClient { get; set; }

        public bool HomunculusRawExp { get; set; }

        public bool PetFeedAlarmSound { get; set; }
        public int PetMinimumHungryValue { get; set; }

        public bool MoveAllWindows { get; set; }

        public bool CheckVersion { get; set; }

        public Dictionary<string, Rectangle> WindowPositions { get; set; }
    }
}