using NUnit.Framework;
using System;
using HemtentaTdd2017.blog;

namespace HemtentaTdd2017.Test
{
	[TestFixture]
	public class BlogTest
	{
		//Arrange
        private IAuthenticator _authenticator;
        private IBlog blog;
		
        // Försöker logga in en användare. Man kan
        // se om inloggningen lyckades på property
        // UserIsLoggedIn.
        // Kastar ett exception om User är null.
        //void LoginUser(User u);

        [Test]
		public void IfUserIsNullShouldThrowException()
		{
			//Act
            blog = new Blog(_authenticator = new MockAuthenticator());

            //Assert Cast exception if null
            Assert.Throws<NoUserNameGivenException>(() => blog.LoginUser(null), "Did not throw an exception");

		}

        [TestCase("Annie", "qazwsx")]
        public void UserExistsAndIsLoggedIn(string username, string password)
        {
            blog = new Blog(_authenticator = new MockAuthenticator());

            blog.LoginUser(new User(username) { Password = password });
            var userIsLoggedIn = blog.UserIsLoggedIn;

            Assert.IsTrue(userIsLoggedIn, "User should be logged in");
        }


        // Försöker logga ut en användare. Kastar
        // exception om User är null.
        [Test]
        public void OnLogoutIfUserIsNullShouldThrowException()
        {
            blog = new Blog(_authenticator = new MockAuthenticator());
			            
            Assert.Throws<NoUserExistsException>(() => blog.LogoutUser(null), "Did not throw an exception");
        }

        [TestCase("Annie", "qazwsx")]
        public void UserWillBeLoggedOut(string username, string password)
        {
            blog = new Blog(_authenticator = new MockAuthenticator());
			            
            blog.LogoutUser(new User(username) { Password = password });
            Assert.That(blog.UserIsLoggedIn == false);
        }

        // För att publicera en sida måste Page vara
        // ett giltigt Page-objekt och användaren
        // måste vara inloggad.
        // Returnerar true om det gick att publicera,
        // false om publicering misslyckades och
        // exception om Page har ett ogiltigt värde.
		[TestCase("Annie", "qazwsx", "New page", "Some new content")]
		public void PageBeeingPublishedShouldSucceed(string username, string password, string title, string content)
        {
            blog = new Blog(_authenticator = new MockAuthenticator());

            //Loggar in anv. först  och kollar att det lyckades
            blog.LoginUser(new User(username) { Password = password});
            var userIsLoggedIn = blog.UserIsLoggedIn;

            Assert.IsTrue(userIsLoggedIn, "User login should be successful");

            //Skapa Page-objektet och kollar att det är ok och publicerat
            Assert.That(blog.PublishPage(new Page() { Title = title, Content = content })
                == true, "The new page should be successfully published");
        }

        [TestCase("New page", "Some new content")]
        public void PageBeeingPublishedShouldNotSucceedNoUserIsLoggedIn(string title, string content)
        {
            blog = new Blog(_authenticator = new MockAuthenticator());
			
            var userIsLoggedIn = blog.UserIsLoggedIn;

            Assert.IsFalse(userIsLoggedIn, "User login should not be successful");
            if (userIsLoggedIn)
            {
				//Skapa Page-objektet och kollar att det är ok och publicerat
				Assert.That(blog.PublishPage(new Page() { Title = title, Content = content })
                == true, "The new page should be successfully published");
            }
            
        }

        [TestCase("Annie", "qazwsx", "New page", "Some new content")]
        public void PageBeeingPublishedShouldNotSucceedIfPageObjectNotValid(string username, string password, string title, string content)
        {
            blog = new Blog(_authenticator = new MockAuthenticator());

            //Loggar in anv. först  och kollar att det lyckades
            blog.LoginUser(new User(username) { Password = password });
            var userIsLoggedIn = blog.UserIsLoggedIn;

            Assert.IsTrue(userIsLoggedIn, "User login should be successful");

            //Skapa Page-objektet misslyckas när null skickas med 
            Assert.Throws<PageIsNotCorrectObjektException>(() => blog.PublishPage(null), "The publish should not have been succesful");
        }


        // För att skicka e-post måste användaren vara
        // inloggad och alla parametrar ha giltiga värden.
        // Returnerar 1 om det gick att skicka mailet,
        // 0 annars.
        [TestCase("Annie", "qazwsx", "", "", "")]
        [TestCase("Annie", "qazwsx", "hotmail.com", "Hej", "Hej")]
        public void IfMailContentIsNullOrEmptyShouldThrowException(string username, string password, string address, string caption, string body)
        {
            blog = new Blog(_authenticator = new MockAuthenticator());
            blog.LoginUser(new User(username) { Password = password });

            var userIsLoggedIn = blog.UserIsLoggedIn;

            //Check to see that user is really logged in
            Assert.That(userIsLoggedIn == true, "User should be logged in");

            Assert.Throws<WrongEmailContentException>(() => blog.SendEmail(address, caption, body));
        }

        [TestCase("Annie", "qazwsx", "minadress@hotmail.com", "Hej", "Hej")]
        public void IfMailContentIsOKShouldSendEmail(string username, string password, string address, string caption, string body)
        {
            blog = new Blog(_authenticator = new MockAuthenticator());
            blog.LoginUser(new User(username) { Password = password });

            var userIsLoggedIn = blog.UserIsLoggedIn;

            //Check to see that user is really logged in
            Assert.That(userIsLoggedIn == true, "User should be logged in");

            var intreturned = blog.SendEmail(address, caption, body);

			Assert.That(intreturned == 1, "Message should be sent");
        }
    }
}




