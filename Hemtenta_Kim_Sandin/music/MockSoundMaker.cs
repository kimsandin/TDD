using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HemtentaTdd2017.music
{
    public class MockSoundMaker : ISoundMaker
    {
        private string currentlyPlaying = "";
        public string NowPlaying
        {
            get
            {
                return currentlyPlaying;
            }
        }

        public void Play(ISong song)
        {
            if (song != null)
            {
                currentlyPlaying = song.Title;
            }

            else
            {
                currentlyPlaying = "";
            }
        }

        public void Stop()
        {
            currentlyPlaying = "";
        }
    }
}
