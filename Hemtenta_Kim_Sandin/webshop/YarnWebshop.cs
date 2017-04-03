using System;
using HemtentaTdd2017.webshop;

namespace HemtentaTdd2017.Test
{
    internal class YarnWebshop : IWebshop
    {
        private IBasket _basket;
        private IBilling _billing;
        
        public IBasket Basket
        {
            get
            {
                return _basket;
            }
        }

        public YarnWebshop(IBasket basket)
        {
            if (basket != null)
            {
                _basket = basket;
            }
            else
            {
                throw new MissingBasketException();
            }
        }

        public void Checkout(IBilling billing)
        {
            if (billing != null)
            {
                _billing = billing;

                _billing.Pay(_basket.TotalCost);
            }
            else
            {
                throw new WrongBillingException();
            }
        }
    }
}