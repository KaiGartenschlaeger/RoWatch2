using System;

namespace Rowatch2.Embeding
{
    public class CharacterStatistic
    {
        #region Constructor

        public CharacterStatistic()
        {
        }

        #endregion

        #region Properties

        private long _gainedBaseExp;
        public long GainedBaseExp
        {
            get { return _gainedBaseExp; }
            set { _gainedBaseExp = value; }
        }

        private long _gainedJobExp;
        public long GainedJobExp
        {
            get { return _gainedJobExp; }
            set { _gainedJobExp = value; }
        }

        private long _mobKills;
        public long MobKills
        {
            get { return _mobKills; }
            set { _mobKills = value; }
        }

        private long _playedTime;
        public long PlayedTime
        {
            get { return _playedTime; }
            set { _playedTime = value; }
        }

        #endregion

        #region Methods

        public void Refresh(CharacterInfo charInfo, long elapsedMiliseconds)
        {
            // played time
            _playedTime += elapsedMiliseconds;

            // mob kills
            _mobKills += charInfo.KilledMobs;

            // gained Exp
            _gainedBaseExp += charInfo.GainedBaseExp;
            _gainedJobExp += charInfo.GainedJobExp;            
        }

        #endregion
    }
}