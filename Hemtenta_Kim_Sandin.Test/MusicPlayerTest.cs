using HemtentaTdd2017.music;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HemtentaTdd2017.Test
{
    [TestFixture]
    public class MusicPlayerTest
    {
        private IMediaDatabase mediaDb = new MockMediaDatabase();
        private ISoundMaker soundMaker = new MockSoundMaker();
        private IMusicPlayer musicPlayer;

        private const string defaultSong = "Tystnad Råder";

        [TestCase("Tystnad Råder")]
        public void NowPlayingShouldBeSilence(string title)
        {
            musicPlayer = new MusicPlayer(mediaDb, soundMaker);
            musicPlayer.Stop();
            string actual = musicPlayer.NowPlaying();
            Assert.AreEqual(defaultSong, actual, "Tystnad Råder?");
        }

        [TestCase("Mine")]
        public void NumberOfSongsInPlayListShouldReturnOne(string title)
        {
            musicPlayer = new MusicPlayer(mediaDb, soundMaker);
            int songsInQueue = musicPlayer.NumSongsInQueue;

            mediaDb.OpenConnection();
            musicPlayer.LoadSongs("Disturbed");
            mediaDb.CloseConnection();
            int presentSongsInQueue = musicPlayer.NumSongsInQueue;

            Assert.That(presentSongsInQueue == 3, "Inga sånger lades till");
        }

        [TestCase("Disturbed")]
        public void NumberOfSongsInPlayListShouldReturnThree(string title)
        {
            musicPlayer = new MusicPlayer(mediaDb, soundMaker);
            int songsInQueue = musicPlayer.NumSongsInQueue;

            mediaDb.OpenConnection();
            musicPlayer.LoadSongs("Disturbed");
            mediaDb.CloseConnection();
            int presentSongsInQueue = musicPlayer.NumSongsInQueue;

            Assert.That(presentSongsInQueue == 3, "Inga sånger lades till");
        }

        

        [TestCase("Disturbed")]
        public void IfNoSongIsPlayingStartNext(string search)
        {
            musicPlayer = new MusicPlayer(mediaDb, soundMaker);
            mediaDb.OpenConnection();
            musicPlayer.LoadSongs("Disturbed");
            mediaDb.CloseConnection();

            int presentSongsInQueue = musicPlayer.NumSongsInQueue;
            string newActual = "";            

            StringAssert.Contains("Tystnad Råder", musicPlayer.NowPlaying(), "Det spelades visst en låt!");
            musicPlayer.Play();
                newActual = musicPlayer.NowPlaying();
            //}
            Assert.Greater(presentSongsInQueue, 0, "No more songs in Playlist");
        }

        // Om en sång spelas ska den sluta spelas.
        // Sången ligger kvar i spellistan. Om ingen
        // sång spelas har funktionen ingen effekt.
        [TestCase("Mustasch")]
        public void StopThePlayingOfSongButKeepSongList(string search)
        {
            musicPlayer = new MusicPlayer(mediaDb, soundMaker);
            mediaDb.OpenConnection();
            musicPlayer.LoadSongs("Mustasch");
            mediaDb.CloseConnection();

            int presentSongsInQueue = musicPlayer.NumSongsInQueue;
            musicPlayer.Play();
            string actual = musicPlayer.NowPlaying();
            musicPlayer.Stop();
            Assert.AreNotEqual(defaultSong, actual, "Tystnad ska inte råda");

            string newActual = musicPlayer.NowPlaying();
            //}
            Assert.Greater(presentSongsInQueue, 0, "No more songs in Playlist");
        }


        [Test]
        public void IfDbNotOpenShouldThrowException()
        {
            musicPlayer = new MusicPlayer(mediaDb, soundMaker);
            Assert.Throws<DatabaseClosedException>(() => musicPlayer.LoadSongs("Disturbed"));
        }

        [Test]
        public void IfDbAlreadyOpenShouldThrowException()
        {
            musicPlayer = new MusicPlayer(mediaDb, soundMaker);
            mediaDb.OpenConnection();
            Assert.Throws<DatabaseAlreadyOpenException>(() => mediaDb.OpenConnection());
            mediaDb.CloseConnection();
        }


    }
       
}

    //// Databasen som har alla sånger.
    //// Om man försöker använda databasen när den
    //// är stängd, eller öppna den när den redan
    //// är öppen, ska funktionen kasta motsvarande
    //// exception. Ska inte implementeras.
    //public interface IMediaDatabase
    //{
    //    bool IsConnected { get; }

//    // Ansluter till databasen
//    void OpenConnection();

//    // Stänger anslutning till databasen
//    void CloseConnection();


//// Se till att databasklassen kastar dessa exceptions
//// när den ska göra det enligt specen.
//public class DatabaseClosedException : Exception { }
//public class DatabaseAlreadyOpenException : Exception { }