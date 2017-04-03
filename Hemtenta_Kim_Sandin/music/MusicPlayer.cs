using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HemtentaTdd2017.music;

namespace HemtentaTdd2017
{
    public class MusicPlayer : IMusicPlayer
    {
        private IMediaDatabase _mediaDb;
        private ISoundMaker _soundMaker;

        private const string  defaultSong = "Tystnad Råder";

        private List<ISong> songQueue;

        public List<ISong> SongQueue { get { return songQueue; } }

        // Antal sånger som finns i spellistan.
        // Returnerar alltid ett heltal >= 0.
        public int NumSongsInQueue
        {
            get
            {                
                return songQueue.Count();
            }
        }

        public MusicPlayer(IMediaDatabase mediaDb, ISoundMaker soundMaker)
        {
            _mediaDb = mediaDb;
            _soundMaker = soundMaker;
            songQueue = new List<ISong>();
        }

        public void LoadSongs(string search)
        {
            if (!String.IsNullOrEmpty(search))
            {
                var songs = _mediaDb.FetchSongs(search);

                if (songs != null && songs.Count() > 0)
                {
                    foreach (var song in songs)
                    {
                        songQueue.Add(song);
                    }
                }

                else
                {
                    throw new SearchFailedException();
                }

            }
            else
            {
                throw new SearchFailedException();
            }

        }

        public void Play()
        {
            if (String.IsNullOrEmpty(_soundMaker.NowPlaying))
            {
                if (songQueue.Count() > 0)
                {
                    _soundMaker.Play(songQueue.FirstOrDefault());
                    songQueue.Remove(SongQueue.First());

                }
            }
        }

        public void Stop()
        {
            if (!String.IsNullOrEmpty(_soundMaker.NowPlaying))
            {
                _soundMaker.Stop();
            }
        }

        public void NextSong()
        {
            if (songQueue.Count() > 0)
            {
                var nextSong = songQueue.FirstOrDefault();

                _soundMaker.Play(nextSong);
                songQueue.Remove(nextSong);
            }

            else
            {
                _soundMaker.Stop();
            }
        }

        public string NowPlaying()
        {
            if (String.IsNullOrEmpty(_soundMaker.NowPlaying))
            {
                return defaultSong;
            }
            else
            {
                return _soundMaker.NowPlaying + " spelas nu";
            }
        }
    }


}
