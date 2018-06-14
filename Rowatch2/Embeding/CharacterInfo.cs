using Rowatch2.Enums;
using System;

namespace Rowatch2.Embeding
{
    public class CharacterInfo
    {
        #region Constructor

        public CharacterInfo()
        {
            _name = string.Empty;
            m_jobClass = JobClassId.Unknown;
            _map = string.Empty;
        }

        public CharacterInfo(CharacterInfo characterInfo)
        {
            CopyFrom(characterInfo);
        }

        #endregion

        #region Properties

        private bool _characterSelection;
        private string _name;
        private bool _baseLevelUp;
        private int _gainedBaseLevels;
        private int _baseLevel;
        private int _jobLevel;
        private int _gainedJobLevels;
        private bool _jobLevelUp;
        private JobClassId m_jobClass;
        private int _baseExp;
        private float _baseExpPercent;
        private int _baseExpRequired;
        private int _jobExp;


        /// <summary>
        /// Liefert einen Wert der angibt, ab die Charakterauswahl aktiv ist.
        /// </summary>
        public bool CharacterSelection
        {
            get { return _characterSelection; }
            set { _characterSelection = value; }
        }

        /// <summary>
        /// Liefert den Name des Spieler.
        /// </summary>
        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        /// <summary>
        /// Liefert einen Wert der angibt, ob ein BaseLevelUP erreicht wurde.
        /// </summary>
        public bool BaseLevelUp
        {
            get { return _baseLevelUp; }
            set { _baseLevelUp = value; }
        }

        /// <summary>
        /// Anzahl an erreichten LevelUps.
        /// </summary>
        public int GainedBaseLevels
        {
            get { return _gainedBaseLevels; }
            set { _gainedBaseLevels = value; }
        }

        /// <summary>
        /// Aktuelle BaseLevel
        /// </summary>
        public int BaseLevel
        {
            get { return _baseLevel; }
            set { _baseLevel = value; }
        }

        /// <summary>
        /// Aktuelle JobLevel.
        /// </summary>
        public int JobLevel
        {
            get { return _jobLevel; }
            set { _jobLevel = value; }
        }

        /// <summary>
        /// Anzahl an JobLevelUPs.
        /// </summary>
        public int GainedJobLevels
        {
            get { return _gainedJobLevels; }
            set { _gainedJobLevels = value; }
        }

        /// <summary>
        /// Liefert einen Wert der angibt, ob ein JobLevelUP erreicht wurde.
        /// </summary>
        public bool JobLevelUp
        {
            get { return _jobLevelUp; }
            set { _jobLevelUp = value; }
        }

        /// <summary>
        /// Liefert die Job Klasse.
        /// </summary>
        public JobClassId JobClass
        {
            get { return m_jobClass; }
            set { m_jobClass = value; }
        }

        /// <summary>
        /// Liefert die aktuellen Base EXP.
        /// </summary>
        public int BaseExp
        {
            get { return _baseExp; }
            set { _baseExp = value; }
        }

        /// <summary>
        /// Liefert den aktuellen Fortschritt des BaseLevels zum LevelUP.
        /// </summary>
        public float BaseExpPercent
        {
            get { return _baseExpPercent; }
            set { _baseExpPercent = value; }
        }

        /// <summary>
        /// Liefert einen Wert der angibt, wie viel BaseEXP noch zum Level UP benötigt werden.
        /// </summary>
        public int BaseExpRequired
        {
            get { return _baseExpRequired; }
            set { _baseExpRequired = value; }
        }

        /// <summary>
        /// Liefert die aktuelle Anzahl an Job EXP.
        /// </summary>
        public int JobExp
        {
            get { return _jobExp; }
            set { _jobExp = value; }
        }


        // TODO: Beschreibungen hinzufügen

        private float _jobExpPercent;
        public float JobExpPercent
        {
            get { return _jobExpPercent; }
            set { _jobExpPercent = value; }
        }

        private int _jobExpRequired;
        public int JobExpRequired
        {
            get { return _jobExpRequired; }
            set { _jobExpRequired = value; }
        }

        private int _hp;
        public int HP
        {
            get { return _hp; }
            set { _hp = value; }
        }

        private int _gainedHP;
        public int GainedHP
        {
            get { return _gainedHP; }
            set { _gainedHP = value; }
        }

        private int _maxHp;
        public int MaxHP
        {
            get { return _maxHp; }
            set { _maxHp = value; }
        }

        private int _hpPercent;
        public int HPPercent
        {
            get { return _hpPercent; }
            set { _hpPercent = value; }
        }

        private int _sp;
        public int SP
        {
            get { return _sp; }
            set { _sp = value; }
        }

        private int _gainedSP;
        public int GainedSP
        {
            get { return _gainedSP; }
            set { _gainedSP = value; }
        }

        private int _maxSP;
        public int MaxSP
        {
            get { return _maxSP; }
            set { _maxSP = value; }
        }

        private int _spPercent;
        public int SPPercent
        {
            get { return _spPercent; }
            set { _spPercent = value; }
        }

        private string _map;
        public string Map
        {
            get { return _map; }
            set { _map = value; }
        }

        private int _gainedBaseExp;
        public int GainedBaseExp
        {
            get { return _gainedBaseExp; }
            set { _gainedBaseExp = value; }
        }

        private int _gainedJobExp;
        public int GainedJobExp
        {
            get { return _gainedJobExp; }
            set { _gainedJobExp = value; }
        }

        private int _killedMobs;
        public int KilledMobs
        {
            get { return _killedMobs; }
            set { _killedMobs = value; }
        }

        private int _killedMobsHour;
        public int KilledMobsHour
        {
            get { return _killedMobsHour; }
            set { _killedMobsHour = value; }
        }

        private DateTime _startTime;
        public DateTime StartTime
        {
            get { return _startTime; }
            set { _startTime = value; }
        }

        private TimeSpan _elapsedTime;
        public TimeSpan ElapsedTime
        {
            get { return _elapsedTime; }
            set { _elapsedTime = value; }
        }

        private int _baseExpPerHour;
        public int BaseExpPerHour
        {
            get { return _baseExpPerHour; }
            set { _baseExpPerHour = value; }
        }

        private int _jobExpPerHour;
        public int JobExpPerHour
        {
            get { return _jobExpPerHour; }
            set { _jobExpPerHour = value; }
        }

        private float _basePercentPerHour;
        public float BasePercentPerHour
        {
            get { return _basePercentPerHour; }
            set { _basePercentPerHour = value; }
        }

        private float _jobPercentPerHour;
        public float JobPercentPerHour
        {
            get { return _jobPercentPerHour; }
            set { _jobPercentPerHour = value; }
        }

        private int _zeny;
        public int Zeny
        {
            get { return _zeny; }
            set { _zeny = value; }
        }

        private int _earnedZeny;
        public int EarnedZeny
        {
            get { return _earnedZeny; }
            set { _earnedZeny = value; }
        }

        private int _leftBaseExp;
        public int LeftBaseExp
        {
            get { return _leftBaseExp; }
            set { _leftBaseExp = value; }
        }

        private float _leftBaseExpPercent;
        public float LeftBaseExpPercent
        {
            get { return _leftBaseExpPercent; }
            set { _leftBaseExpPercent = value; }
        }

        private int _leftJobExp;
        public int LeftJobExp
        {
            get { return _leftJobExp; }
            set { _leftJobExp = value; }
        }

        private float _leftJobExpPercent;
        public float LeftJobExpPercent
        {
            get { return _leftJobExpPercent; }
            set { _leftJobExpPercent = value; }
        }

        private TimeSpan _leftBaseTime;
        public TimeSpan LeftBaseTime
        {
            get { return _leftBaseTime; }
            set { _leftBaseTime = value; }
        }

        private TimeSpan _leftJobTime;
        public TimeSpan LeftJobTime
        {
            get { return _leftJobTime; }
            set { _leftJobTime = value; }
        }

        //
        // Homunculus
        //
        private string _homunculus_name;
        public string Homunculus_Name
        {
            get { return _homunculus_name; }
            set { _homunculus_name = value; }
        }

        private int _homunculus_HP;
        public int Homunculus_HP
        {
            get { return _homunculus_HP; }
            set { _homunculus_HP = value; }
        }

        private int _homunculus_MaxHP;
        public int Homunculus_MaxHP
        {
            get { return _homunculus_MaxHP; }
            set { _homunculus_MaxHP = value; }
        }

        private int _homunculus_PercentageHP;
        public int Homunculus_PercentageHP
        {
            get { return _homunculus_PercentageHP; }
            set { _homunculus_PercentageHP = value; }
        }

        private int _homunculus_exp;
        public int Homunculus_Exp
        {
            get { return _homunculus_exp; }
            set { _homunculus_exp = value; }
        }

        private int _homunculus_maxExp;
        public int Homunculus_MaxExp
        {
            get { return _homunculus_maxExp; }
            set { _homunculus_maxExp = value; }
        }

        private float _homunculus_PercentageExp;
        public float Homunculus_PercentageExp
        {
            get { return _homunculus_PercentageExp; }
            set { _homunculus_PercentageExp = value; }
        }

        private int _homunculus_hungry;
        public int Homunculus_Hungry
        {
            get { return _homunculus_hungry; }
            set { _homunculus_hungry = value; }
        }

        private int _homunculus_PercantageHungry;
        public int Homunculus_PercentageHungry
        {
            get { return _homunculus_PercantageHungry; }
            set { _homunculus_PercantageHungry = value; }
        }

        private int _humunculus_Friendly;
        public int Homunculus_Friendly
        {
            get { return _humunculus_Friendly; }
            set { _humunculus_Friendly = value; }
        }

        private int _homunculus_PercentageFriendly;
        public int Homunculus_PercentageFriendly
        {
            get { return _homunculus_PercentageFriendly; }
            set { _homunculus_PercentageFriendly = value; }
        }

        //
        // Pet
        //
        private string _petName;
        public string PetName
        {
            get { return _petName; }
            set { _petName = value; }
        }

        private int _petFriendly;
        public int PetFriendly
        {
            get { return _petFriendly; }
            set { _petFriendly = value; }
        }

        private int _petHungry;
        public int PetHungry
        {
            get { return _petHungry; }
            set { _petHungry = value; }
        }

        /// <summary>
        /// Liefert einen Wert der angibt, ob dieser Character ein Homunculus besitzen kann.
        /// </summary>
        public bool SupportHomunculus
        {
            get
            {
                return (m_jobClass == JobClassId.Alchemist
                    || m_jobClass == JobClassId.BabyAlchemist
                    || m_jobClass == JobClassId.Biochemist
                    || m_jobClass == JobClassId.BabyGenetic
                    || m_jobClass == JobClassId.Genetic
                    || m_jobClass == JobClassId.Genetic2);
            }
        }

        public ClassType ClassType
        {
            get
            {
                if (m_jobClass >= JobClassId.Novice && m_jobClass <= JobClassId.Thief)
                {
                    return ClassType.FirstClass;
                }
                if (m_jobClass >= JobClassId.Knight && m_jobClass <= JobClassId.PecoCrusader)
                {
                    return ClassType.SecondClass;
                }

                if (m_jobClass >= JobClassId.NoviceHigh && m_jobClass <= JobClassId.ThiefHigh)
                {
                    return ClassType.AdvancedFirstClass;
                }
                if (m_jobClass >= JobClassId.LordKnight && m_jobClass <= JobClassId.Paladin2)
                {
                    return ClassType.AdvancedSecondClass;
                }

                if (m_jobClass >= JobClassId.RuneKnight && m_jobClass <= JobClassId.Mechanic4)
                {
                    return ClassType.ThirdClass;
                }

                return ClassType.Unknown;
            }
        }


        private DateTime? _lastKill;
        public DateTime? LastKill
        {
            get { return _lastKill; }
            set { _lastKill = value; }
        }

        private DateTime? _lastDmg;
        public DateTime? LastDmg
        {
            get { return _lastDmg; }
            set { _lastDmg = value; }
        }

        #endregion

        #region Methods

        public void CopyFrom(CharacterInfo characterInfo)
        {
            // character
            _name = characterInfo.Name;

            _baseLevel = characterInfo.BaseLevel;
            _baseLevelUp = characterInfo._baseLevelUp;
            _gainedBaseLevels = characterInfo.GainedBaseLevels;

            _jobLevel = characterInfo.JobLevel;
            _jobLevelUp = characterInfo._jobLevelUp;
            _gainedJobLevels = characterInfo.GainedJobLevels;

            m_jobClass = characterInfo.JobClass;

            _baseExp = characterInfo.BaseExp;
            _baseExpRequired = characterInfo.BaseExpRequired;
            _jobExp = characterInfo.JobExp;
            _jobExpRequired = characterInfo._jobExpRequired;

            _hp = characterInfo.HP;
            _gainedHP = characterInfo.GainedHP;
            _maxHp = characterInfo.MaxHP;
            _hpPercent = characterInfo.HPPercent;

            _sp = characterInfo.SP;
            _gainedSP = characterInfo.GainedSP;
            _maxSP = characterInfo.MaxSP;
            _spPercent = characterInfo.SPPercent;

            _map = characterInfo.Map;

            _gainedBaseExp = characterInfo.GainedBaseExp;
            _gainedJobExp = characterInfo.GainedJobExp;

            _killedMobs = characterInfo.KilledMobs;
            _killedMobsHour = characterInfo.KilledMobsHour;

            _leftBaseExp = characterInfo._leftBaseExp;
            _leftJobExp = characterInfo._leftJobExp;

            _elapsedTime = characterInfo.ElapsedTime;

            _baseExpPerHour = characterInfo.BaseExpPerHour;
            _basePercentPerHour = characterInfo.BasePercentPerHour;

            _jobExpPerHour = characterInfo._jobExpPerHour;
            _jobPercentPerHour = characterInfo.JobPercentPerHour;

            _zeny = characterInfo.Zeny;
            _earnedZeny = characterInfo.EarnedZeny;

            // homunculus
            _homunculus_name = characterInfo.Homunculus_Name;
            _homunculus_HP = characterInfo.Homunculus_HP;
            _homunculus_MaxHP = characterInfo.Homunculus_MaxHP;
            _homunculus_exp = characterInfo.Homunculus_Exp;
            _homunculus_maxExp = characterInfo.Homunculus_MaxExp;
            _homunculus_hungry = characterInfo.Homunculus_Hungry;
            _humunculus_Friendly = characterInfo.Homunculus_Friendly;

            _homunculus_PercentageHP = characterInfo.Homunculus_PercentageHP;

            _lastDmg = characterInfo.LastDmg;
            _lastKill = characterInfo.LastKill;
        }

        #endregion
    }
}