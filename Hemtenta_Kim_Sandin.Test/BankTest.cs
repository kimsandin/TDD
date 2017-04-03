using HemtentaTdd2017.bank;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HemtentaTdd2017.Test
{
    [TestFixture]
    public class BankTest
    {
        private IAccount account;

        //Deposit
        [TestCase(double.PositiveInfinity)]
        [TestCase(double.NaN)]
        [TestCase(double.MinValue)]
        [TestCase(double.PositiveInfinity)]
        [TestCase(-1)]
        public void IfDepositAmountIsNegativeOrTooLargeShouldThrowException(double amount)
        {
            account = new Bank();
            Assert.Throws<IllegalAmountException>(() => account.Deposit(amount), "Should throw IllegalAmountException");
        }


        [TestCase(500)]
        [TestCase(double.MaxValue)]
        public void IfDepositAmountIsOkShouldDeposit(double amount)
        {
            account = new Bank();
            account.Deposit(amount);
            var total = account.Amount;

            Assert.AreEqual(amount, total, "total should now be same as amount");
        }



        //Withdraw
        [TestCase(100)]
        [TestCase(51)]
        [TestCase(double.MaxValue)]
        public void WithdrawalWhenInsufficientFundsShouldThrowException(double amount)
        {
            account = new Bank();
            account.Deposit(50);

            Assert.Throws<InsufficientFundsException>(() => account.Withdraw(amount), "Should throw InsufficientFundsException");
        }

        [TestCase(double.MinValue)]
        [TestCase(double.NegativeInfinity)]
        [TestCase(-500)]
        public void WithdrawalOfNegativeAmountShouldThrowException(double amount)
        {
            account = new Bank();
            account.Deposit(50);

            Assert.Throws<IllegalAmountException>(() => account.Withdraw(amount), "Should throw IllegalAmountExceptionException");
        }


        //Transfer
        [TestCase(300)]
        public void TransferFundsToNullAccountShouldThrowException(double amount)
        {
            account = new Bank();
            IAccount transferaccount = null;
            account.Deposit(500);

            Assert.Throws<OperationNotPermittedException>(() => account.TransferFunds(transferaccount, amount), "Should throw OperationNotPermittedException");
        }

        //Sätter in en högre summa före överföring så lyckas överföringen
        [Test]
        public void TransferFundsToAccountShouldBeOk()
        {
            account = new Bank();
            account.Deposit(500);
            IAccount transferaccount = new Bank();
            double amount = 300;
            account.TransferFunds(transferaccount, amount);
            var transferaccountAmount = transferaccount.Amount;

            Assert.AreEqual(amount, transferaccountAmount, "transferaccountAmount should now be same as amount");
        }

        //Sätter in en lägre summa än överföringen så misslyckas den
        [TestCase(500)]
        public void TransferOutOfBounderyFundsToAccountShouldThrowException(double amount)
        {
            account = new Bank();
            IAccount transferaccount = new Bank();
            account.Deposit(300);

            Assert.Throws<InsufficientFundsException>(() => account.TransferFunds(transferaccount, amount), "Should throw InsufficientFundsException");
        }

    }
}
