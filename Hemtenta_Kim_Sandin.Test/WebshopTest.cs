using HemtentaTdd2017.webshop;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HemtentaTdd2017.Test
{

    //Test att göra - Vilka metoder och properties behöver testas?

    //Metoder att testa

    //AddProduct
    //RemoveProduct
    //Checkout

    //Properties att testa, 
    //men det faller sig ganska naturligt att göra det inom ramen för objekten de tillhör, tycker jag.

    //TotalCost
    //Balance
    //Pay


    //_____________________________________

    //Exeptions - Ska några exceptions kastas?
    // Följande har jag använt mig av för det var de som kändes mest rätt och mest förklarande:

    // MissingBasketException
    // WrongAmountException
    // WrongPriceException
    // WrongProductException
    // WrongProductRemovalException
    // InsufficientFundsException
    // WrongBillingException


    //Vilka är domänerna för IWebshop och IBasket?
    // IWebshop:
    //- IBasket - null eller ett objekt
    //- IBilling - null eller ett objekt

    //IBasket:
    //Add och Remove Product Är båda objekt som även kan vara null, de har också domänen Product p som innehåller en decimal price 
    //och en int amount.
    //Product kan vara ett objekt (Får ej vara null). Amount kan ha värdena över 0 till och med Int.MaxValue(2,147,483,647)
    //Decimal TotaltCost som kan ha värderna över 0.00 och mindre än Decimal.MaxValue (79,228,162,514,264,337,593,543,950,335)


    [TestFixture]
    public class WebshopTest
    {
        private IWebshop webshop;
        private IBasket basket;
        private IBilling billing;

        //Create Webshop
        [Test]
        public void CreateMyWebshopWhenBasketIsNullShouldThrowException()
        {
            Assert.Throws<MissingBasketException>(() => webshop = new YarnWebshop(null), "Webshop has no basket yet.");
        }

        //Add product
        [TestCase(500)]
        public void AddingProductWithoutValidPriceShouldThrowException(int amount)
        {
            webshop = new YarnWebshop(basket = new Basket());
            Product p = new Product() { Price = 300m };
            webshop.Basket.AddProduct(p, amount); 
            Assert.AreEqual(p.Price * amount, webshop.Basket.TotalCost, "Productpricing is wrong");
        }

        [Test]
        public void AddingProductWithoutPriceShouldThrowException()
        {
            webshop = new YarnWebshop(basket = new Basket());
            Product p = new Product() { Price = 0 };
            Assert.Throws<WrongPriceException>(() => webshop.Basket.AddProduct(p, 1), "Productpricing is zero");
        }

        [Test]
        public void AddingProductWithValidPriceAndFormatShouldBeSuccessful()
        {
            webshop = new YarnWebshop(basket = new Basket());
            Product p = new Product() { Price = 300m };
            webshop.Basket.AddProduct(p, 3);
            Assert.AreEqual(p.Price * 3, webshop.Basket.TotalCost, "Product should be added");
        }


        //RemoveProduct
        [TestCase(1)]
        public void RemovingProductWithValidPriceAndFormatShouldSucceed(int amount)
        {
            webshop = new YarnWebshop(basket = new Basket());
            Product p = new Product() { Price = 10.00m };
            webshop.Basket.AddProduct(p, amount);
            webshop.Basket.RemoveProduct(p, amount);

            Assert.That(webshop.Basket.TotalCost, Is.EqualTo(0), "Should be the same");
        }

        [Test]
        public void RemovingToManyShouldThrowException()
        {
            webshop = new YarnWebshop(basket = new Basket());
            Product p = new Product() { Price = 150.00m };
            webshop.Basket.AddProduct(p, 2);
            
            Assert.Throws<WrongProductRemovalException>(() => webshop.Basket.RemoveProduct(p, 4), "Should be removed");
        }

        //TotalCost
        // funkar som den ska i t.ex. RemovingProductWithValidPriceAndFormatShouldSucceed()


        //Checkout

        [Test]
        public void CheckoutWithBillingNullShouldThrowException()
        {
            webshop = new YarnWebshop(basket = new Basket());
            Product p = new Product() { Price = 295.00m };
            webshop.Basket.AddProduct(p, 4);

            Assert.Throws<WrongBillingException>(() => webshop.Checkout(null), "Basket should be billed first");
        }

        [Test]
        public void AllValidCheckoutShouldBeSuccessful()
        {
            webshop = new YarnWebshop(basket = new Basket());
            Product p = new Product() { Price = 295.00m };
            webshop.Basket.AddProduct(p, 4);
            webshop.Checkout(billing = new MockBilling(1180m));
            var newbalance = billing.Balance;
            Assert.AreEqual(0, newbalance,  "Money should cover billing");
        }

        [Test]
        public void TooLittleOnBalanceShouldThrowException()
        {
            webshop = new YarnWebshop(basket = new Basket());
            Product p = new Product() { Price = 295.00m };
            webshop.Basket.AddProduct(p, 4);            
            var newbalance = billing.Balance;

            Assert.Throws<InsufficientFundsException>(() => webshop.Checkout(billing = new MockBilling(600m)), "Money should not be sufficient");
            
        }

    }
}
