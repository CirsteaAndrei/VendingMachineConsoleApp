using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using iQuest.VendingMachine.PresentationLayer.Exceptions;
using iQuest.VendingMachine.BusinessLayer.Payment;

namespace iQuest.VendingMachine.PresentationLayer
{
    internal class BuyView : DisplayBase, IBuyView
    {
        public int RequestProduct()
        {
            string columnId;
            Display("Type the column number of the desired product: ", ConsoleColor.Cyan);
            columnId = Console.ReadLine();
            Console.WriteLine();

            if (string.IsNullOrEmpty(columnId))
            {
                throw new CancelException();
            }
            if (short.TryParse(columnId, out var columnIdNumber))
            {
                return columnIdNumber;
            }
            throw new InvalidColumnException();
        }

        public void DispenseProduct(string productName)
        {
            DisplayLine($"The product: {productName} has been dispensed", ConsoleColor.Yellow);
        }

        public int AskForPaymentMethod(IEnumerable<PaymentMethod> paymentMethods)
        {
            string selectedMethodIdString;
            DisplayLine("Select the playing method you wish to proceed with", ConsoleColor.Cyan);
            foreach (var payMethod in paymentMethods)
            {
                DisplayLine($"{payMethod.Id} {payMethod.Name}", ConsoleColor.White);
            }
            selectedMethodIdString = Console.ReadLine();
            if (!string.IsNullOrEmpty(selectedMethodIdString))
            {
                if (int.TryParse(selectedMethodIdString, out var selectedMethodIdInt))
                {
                    return selectedMethodIdInt;
                }
                throw new InvalidPaymentIdException();
            }
            else
            {
                throw new CancelException();
            }
        }
    }
}
