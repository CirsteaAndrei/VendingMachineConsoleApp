using System;
using System.Collections.Generic;
using System.Text;
using LiteDB;

namespace iQuest.VendingMachine.DataAccessLayer
{
    internal class Product
    {
        [BsonId]
        public int ColumnId { get; set; }
        public string Name { get; set; }
        public float Price { get; set; }
        public int Quantity { get; set; }

        public Product(int columnId, string name, float price, int quantity)
        {
            ColumnId = columnId;
            Name = name;
            Price = price;
            Quantity = quantity;
        }
    }
}
