using System;
using System.Collections.Generic;
using System.Text;
using project_Andrey_Rudenko.Model.Enum;

namespace project_Andrey_Rudenko.Model
{
    public class Item
    {
        public int Id { get; set; }
        public int MoneyValue { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public ItemCategory Category { get; set; }
        public int Quantity { get; set; }

        public Item(int moneyValue, string name, string description, ItemCategory cat, int quantity)
        {
            MoneyValue = moneyValue;
            Name = name;
            Description = description;
            Category = cat;
            Quantity = quantity;
        }

        public Item(Item item) : this(item.MoneyValue, item.Name, item.Description, item.Category, item.Quantity)
        {

           
        }
        protected Item() { }
    }
}
