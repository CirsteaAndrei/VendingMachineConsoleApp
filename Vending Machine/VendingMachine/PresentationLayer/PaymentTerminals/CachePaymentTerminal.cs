using iQuest.VendingMachine.PresentationLayer.Exceptions;
using System;
using System.Collections.Generic;
using System.Text;

namespace iQuest.VendingMachine.PresentationLayer.PaymentTerminals
{
    internal class CachePaymentTerminal : DisplayBase
    {
        public float AskForMoney(float totalAmount, float price)
        {
            string insertedMoney;
            DisplayLine($"Amount to reach: {price}", ConsoleColor.White);
            DisplayLine($"Amount introduced: {totalAmount}", ConsoleColor.White);
            Console.WriteLine();
            Display("Introduce money: ", ConsoleColor.Cyan);
            insertedMoney = Console.ReadLine();
            Console.WriteLine();

            if (!string.IsNullOrEmpty(insertedMoney))
            {
                if (float.TryParse(insertedMoney, out var insertedMoneyAmount))
                {
                    return insertedMoneyAmount;
                }
            }
            else
            {
                GiveBackChange(totalAmount, price);
                throw new CancelException();
            }


            return totalAmount;
        }
        public void GiveBackChange(float insertedAmount, float price)
        {
            if (price <= insertedAmount)
            {
                DisplayLine($"Amount to reach: {price}", ConsoleColor.White);
                DisplayLine($"Amount introduced: {insertedAmount}", ConsoleColor.White);
                DisplayLine("Payment succeded.", ConsoleColor.White);
                Console.WriteLine();
                if (price < insertedAmount)
                {
                    float change = insertedAmount - price;
                    DisplayLine($"Please collect the change: {change}", ConsoleColor.Yellow);
                }
            }

            if (price > insertedAmount)
            {
                float change = insertedAmount;
                DisplayLine($"Please collect your money: {change}", ConsoleColor.Yellow);
            }
        }
    }
}
