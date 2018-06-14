using System;
using System.IO;
using System.Windows.Forms;

namespace Rowatch2.Globals
{
    public class Paths
    {
        public Paths()
        {
            m_settingsFolder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "Rowatch2");
            m_settingsFile = Path.Combine(m_settingsFolder, "settings.xml");
            m_macrosFile = Path.Combine(m_settingsFolder, "macros.xml");

            m_soundsFolder = Path.Combine(Application.StartupPath, "Sounds");

            m_dbFolder = Path.Combine(Application.StartupPath, "DB");
            m_mobDbFile = Path.Combine(m_dbFolder, "mob_db.txt");
            m_jobClsFile = Path.Combine(m_dbFolder, "job_cls.xml");
            m_addressesFile = Path.Combine(m_dbFolder, "clients.xml");
            m_mapDbFile = Path.Combine(m_dbFolder, "maps.txt");
            m_skillTimerFile = Path.Combine(m_dbFolder, "skilltimer.xml");
            m_expTable = Path.Combine(m_dbFolder, "exp_table.xml");
        }

        private readonly string m_settingsFolder;
        public string SettingsFolder
        {
            get { return m_settingsFolder; }
        }

        private readonly string m_settingsFile;
        public string SettingsFile
        {
            get { return m_settingsFile; }
        }

        private readonly string m_dbFolder;
        public string DbFolder
        {
            get { return m_dbFolder; }
        }

        private readonly string m_mobDbFile;
        public string MobDbFile
        {
            get { return m_mobDbFile; }
        }

        private readonly string m_jobClsFile;
        public string JobClassesFile
        {
            get { return m_jobClsFile; }
        }

        private string m_addressesFile;
        public string AddressesFile
        {
            get { return m_addressesFile; }
            set { m_addressesFile = value; }
        }

        private string m_mapDbFile;
        public string MapDbFile
        {
            get { return m_mapDbFile; }
            set { m_mapDbFile = value; }
        }

        private string m_soundsFolder;
        public string SoundsFolder
        {
            get { return m_soundsFolder; }
            set { m_soundsFolder = value; }
        }

        private string m_skillTimerFile;
        public string SkillTimerFile
        {
            get { return m_skillTimerFile; }
        }

        private string m_macrosFile;
        public string MacrosFile
        {
            get { return m_macrosFile; }
            set { m_macrosFile = value; }
        }

        public string m_expTable;
        public string ExpTable
        {
            get { return m_expTable; }
            set { m_expTable = value; }
        }
    }
}