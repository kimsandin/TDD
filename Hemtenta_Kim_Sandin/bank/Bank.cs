﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HemtentaTdd2017.bank
{
    public class Bank : IAccount
    {
        // Representerar ett konto. Implementera den här!
        // Obs: i vanliga fall ska datatypen decimal användas
        // i stället för double när man hanterar pengar.
        private double _amount;
       
        public double Amount
        {
            get
            {
                return _amount;
            }
        }

        public void Deposit(double amount)
        {
            if (amount > 0 && amount < double.PositiveInfinity)
            {
                _amount += amount;
            }
            else
            {
                throw new IllegalAmountException();
            }
        }

        public void TransferFunds(IAccount destination, double amount)
        {
            if (destination != null)
            {
                if (amount > 0)
                {
                    if (amount <= _amount)
                    {
                        destination.Deposit(amount);
                        _amount -= amount;
                        
                    }
                    else
                    {
                        throw new InsufficientFundsException();
                    }
                }
                else
                {
                    throw new IllegalAmountException();
                }

            }
            else
            {
                throw new OperationNotPermittedException();
            }
        }

        public void Withdraw(double amount)
        {
            if (amount > 0)
            {
                if (amount <= _amount)
                {
                    _amount -= amount;
                }
                else
                {
                    throw new InsufficientFundsException();                 
                }
            }
            else
            {
                throw new IllegalAmountException();
            }
        }
        
    }
}
