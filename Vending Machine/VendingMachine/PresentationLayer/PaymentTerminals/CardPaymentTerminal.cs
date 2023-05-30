using iQuest.VendingMachine.PresentationLayer.Exceptions;
using System;
using System.Collections.Generic;
using System.Text;

namespace iQuest.VendingMachine.PresentationLayer.PaymentTerminals
{
    internal class CardPaymentTerminal : DisplayBase
    {
        public void AskForCardNumber(float price)
        {
            Display("Introduce the card number: ", ConsoleColor.Cyan);
            string cardNumber = Console.ReadLine();
            Console.WriteLine();

            if (!string.IsNullOrEmpty(cardNumber))
            {
                if (int.TryParse(cardNumber, out var cardNumberInt))
                {
                    Display($"Payment succeded: {price} has been taken from your account.", ConsoleColor.Yellow);
                    Console.WriteLine();
                }
                else
                {
                    throw new InvalidCardNumberException();
                }
            }
            else
            {
                throw new CancelException();
            }
        }
    }
}
