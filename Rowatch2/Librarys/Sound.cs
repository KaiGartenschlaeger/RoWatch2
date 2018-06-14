using System;
using System.IO;
using System.Media;

namespace Rowatch2.Librarys
{
    public class Sound
    {
        #region Fields

        private SoundPlayer m_player;

        #endregion

        #region Constructor

        public Sound()
        {
        }

        #endregion

        #region Methods

        public bool Load(Stream stream)
        {
            bool result;

            try
            {
                m_player = new SoundPlayer(stream);
                result = true;
            }
            catch (Exception)
            {
                result = false;
            }

            return result;
        }

        public bool Load(string location)
        {
            bool result;

            try
            {
                m_player = new SoundPlayer(location);
                result = true;
            }
            catch (Exception)
            {
                result = false;
            }

            return result;
        }

        public bool Play(bool looping)
        {
            bool result;

            try
            {
                if (m_player != null)
                {
                    if (looping)
                    {
                        m_player.PlayLooping();
                    }
                    else
                    {
                        m_player.Play();
                    }

                    result = true;

                    m_isPlaying = true;
                }
                else
                {
                    result = false;
                }
            }
            catch (Exception)
            {
                result = false;
            }

            return result;
        }

        public void Stop()
        {
            if (m_player != null && m_player.IsLoadCompleted)
            {
                m_player.Stop();
                m_isPlaying = false;
            }
        }

        #endregion

        #region Properties

        private bool m_isPlaying;


        public bool IsPlaying
        {
            get { return m_isPlaying; }
        }
        
        #endregion
    }
}