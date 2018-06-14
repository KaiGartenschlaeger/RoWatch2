using Rowatch2.Enums;
using System.Globalization;

namespace Rowatch2.Embeding
{
    public class Mob
    {
        #region Constructor

        private Mob()
        {
        }

        #endregion

        #region Properties

        private int _id;
        public int ID
        {
            get { return _id; }
            set { _id = value; }
        }

        private string _sprite;
        public string Sprite
        {
            get { return _sprite; }
            set { _sprite = value; }
        }

        private string _kRoName;
        public string KRoName
        {
            get { return _kRoName; }
            set { _kRoName = value; }
        }

        private string _iRoName;
        public string IRoName
        {
            get { return _iRoName; }
            set { _iRoName = value; }
        }

        private int _level;
        public int Level
        {
            get { return _level; }
            set { _level = value; }
        }

        private int _hp;
        public int HP
        {
            get { return _hp; }
            set { _hp = value; }
        }

        private int _sp;
        public int SP
        {
            get { return _sp; }
            set { _sp = value; }
        }

        private int _exp;
        public int Exp
        {
            get { return _exp; }
            set { _exp = value; }
        }

        private int _jexp;
        public int JobExp
        {
            get { return _jexp; }
            set { _jexp = value; }
        }

        private int _str;
        public int Str
        {
            get { return _str; }
            set { _str = value; }
        }

        private int _agi;
        public int Agi
        {
            get { return _agi; }
            set { _agi = value; }
        }

        private int _vit;
        public int Vit
        {
            get { return _vit; }
            set { _vit = value; }
        }

        private int _int;
        public int Int
        {
            get { return _int; }
            set { _int = value; }
        }

        private int _dex;
        public int Dex
        {
            get { return _dex; }
            set { _dex = value; }
        }

        private int _luk;
        public int Luk
        {
            get { return _luk; }
            set { _luk = value; }
        }

        private MobElement _element;
        public MobElement Element
        {
            get { return _element; }
            set { _element = value; }
        }

        private int _elementLvl;
        public int ElementLvl
        {
            get { return _elementLvl; }
            set { _elementLvl = value; }
        }

        private MobRace _race;
        public MobRace Race
        {
            get { return _race; }
            set { _race = value; }
        }

        private MobScale _scale;
        public MobScale Scale
        {
            get { return _scale; }
            set { _scale = value; }
        }

        private MobMode _mode;
        public MobMode Mode
        {
            get { return _mode; }
            set { _mode = value; }
        }

        #endregion

        #region Methods

        public static Mob Load(string line)
        {
            Mob result = null;

            if (!string.IsNullOrEmpty(line))
            {
                string[] columns = line.Split(',');
                if (columns.Length >= 25)
                {
                    // id
                    int id;
                    int.TryParse(columns[MobDbColumns.ID].Trim(), out id);

                    // sprite
                    string sprite = columns[MobDbColumns.Sprite_Name].Trim();

                    // name
                    string kRoName = columns[MobDbColumns.kROName].Trim();
                    string iRoName = columns[MobDbColumns.iROName].Trim();

                    // level
                    int level;
                    int.TryParse(columns[MobDbColumns.LV].Trim(), out level);

                    // hp, sp
                    int hp, sp;
                    int.TryParse(columns[MobDbColumns.HP].Trim(), out hp);
                    int.TryParse(columns[MobDbColumns.SP].Trim(), out sp);

                    // exp, jexp
                    int exp, jexp;
                    int.TryParse(columns[MobDbColumns.EXP].Trim(), out exp);
                    int.TryParse(columns[MobDbColumns.JEXP].Trim(), out jexp);

                    // Range1, ATK1, ATK2, DEF, MDEF

                    // STR, AGI, VIT, INT, DEX, LUK
                    int attrStr, attrAgi, attrVit, attrInt, attrDex, attrLuk;
                    int.TryParse(columns[MobDbColumns.STR].Trim(), out attrStr);
                    int.TryParse(columns[MobDbColumns.AGI].Trim(), out attrAgi);
                    int.TryParse(columns[MobDbColumns.VIT].Trim(), out attrVit);
                    int.TryParse(columns[MobDbColumns.INT].Trim(), out attrInt);
                    int.TryParse(columns[MobDbColumns.DEX].Trim(), out attrDex);
                    int.TryParse(columns[MobDbColumns.LUK].Trim(), out attrLuk);

                    // Range2, Range3

                    // Scale
                    int scale;
                    int.TryParse(columns[MobDbColumns.Scale].Trim(), out scale);

                    // Element
                    int elementLV = 0, elementType = 0;
                    if (columns[MobDbColumns.Element].Trim().Length == 2)
                    {
                        int.TryParse(columns[MobDbColumns.Element].Trim().Substring(0, 1), out elementLV);
                        int.TryParse(columns[MobDbColumns.Element].Trim().Substring(1, 1), out elementType);
                    }

                    // Race
                    int race;
                    int.TryParse(columns[MobDbColumns.Race].Trim(), out race);

                    // Mode
                    int mode;
                    int.TryParse(columns[MobDbColumns.Mode].Trim().Replace("0x", ""), NumberStyles.HexNumber, CultureInfo.CurrentCulture, out mode);

                    if (id > 0)
                    {
                        Mob mob = new Mob();

                        mob.ID = id;

                        mob.KRoName = kRoName;
                        mob.IRoName = iRoName;

                        mob.Level = level;

                        mob.HP = hp;
                        mob.SP = sp;
                        mob.Exp = exp;
                        mob.JobExp = jexp;

                        mob.Str = attrStr;
                        mob.Agi = attrAgi;
                        mob.Vit = attrVit;
                        mob.Int = attrInt;
                        mob.Dex = attrDex;
                        mob.Luk = attrLuk;

                        mob.Element = (MobElement)elementType;
                        mob.ElementLvl = elementLV;

                        mob.Race = (MobRace)race;

                        mob.Scale = (MobScale)scale;

                        mob.Mode = (MobMode)mode;

                        result = mob;
                    }
                }
            }

            return result;
        }

        #endregion

        #region ToString

        public override string ToString()
        {
            return string.Format("{0}: {1}", _level, _iRoName);
        }

        #endregion
    }
}