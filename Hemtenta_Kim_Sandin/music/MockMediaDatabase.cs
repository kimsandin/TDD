using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HemtentaTdd2017.music
{
    // Databasen som har alla sånger.
    // Om man försöker använda databasen när den
    // är stängd, eller öppna den när den redan
    // är öppen, ska funktionen kasta motsvarande
    // exception. Ska inte implementeras.
    public class MockMediaDatabase : IMediaDatabase
    {
        private bool connected = false;

        //Lista med låtar
        private List<ISong> dbSongs = new List<ISong>()
        {
            new Song("Disturbed", "The Game") { },
            new Song("Disturbed", "Down With The Sickness") { },
            new Song("Disturbed", "The Light") { },
            new Song("Temper Fi", "Grinder") { },
            new Song("Temper Fi", "Nemesis") { },
            new Song("Marillion", "Kayleigh") { },
            new Song("Hellennium", "Heavy Metal Alliance") { },
            new Song("Gary Moore", "Empty Rooms") { },
            new Song("Mustasch", "Mine") { },
            new Song("Mustasch", "Bring me Everyone") { },
            new Song("Heart", "Alone")
        };

        public bool IsConnected
        {
            get
            {
                return connected;
            }
        }

        public void CloseConnection()
        {
            if (connected == true)
            {
                connected = false;
            }
            else
            {
                throw new DatabaseClosedException();
            }
        }

        public List<ISong> FetchSongs(string search)
        {

            if (IsConnected == true)
            {
                return dbSongs.Where(x => x.Title.Contains(search)|| x.Artist.Contains(search)).ToList();
            }
            else
            {
                throw new DatabaseClosedException();
            }
        }

        public void OpenConnection()
        {
            if (connected == false)
            {
                connected = true;
            }
            else
            {
                throw new DatabaseAlreadyOpenException();
            }
        }
        
    }
}



//public interface IMediaDatabase
//{
   

//    // Ansluter till databasen
//    void OpenConnection();

//    // Stänger anslutning till databasen
//    void CloseConnection();

//    // Returnerar alla sånger i databasen som
//    // matchar söksträngen.
//    // Tips: använd string.Contains(string)
//    List<ISong> FetchSongs(string search);
//}