using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HemtentaTdd2017.music
{
    public class Song : ISong
    {
        private string _artist;
        private string _title;
        public Song(string artist, string title)
        {
            _artist = artist;
            _title = title;
        }
        public string Title
        {
            get { return _title; }
        }

        public string Artist
        {
            get { return _artist; }
        }
    }
}
