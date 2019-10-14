using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace project_Andrey_Rudenko.Model
{
    public class Order
    {
        private List<Item> _itemCollection;

        public int summaryValue
        {
            get { return _itemCollection.Sum(a => a.Quantity * a.MoneyValue); }
        }

        public Order()
        {
            _itemCollection = new List<Item>();
        }

        public void Add(Item item)
        {
            _itemCollection.Add(item);
        }
        public void Print()
        {
            Console.Clear();
            foreach (var a in _itemCollection)
            {
                Console.WriteLine($"{a.Name}  {a.MoneyValue}  {a.Description}  {a.Category} { a.Quantity}");
            }

            Console.WriteLine();
            Console.WriteLine($"To Pay: {summaryValue}");
            SaveToFile();
        }

        public void SaveToFile()
        {
            System.IO.Directory.CreateDirectory("AllChecks");
            string path = $@"{Environment.CurrentDirectory}\AllChecks\{DateTime.Now.ToString("yyyyMMdd_hhmmss")}.txt";
            using (StreamWriter sw = File.CreateText(path))
            {
                foreach (var a in _itemCollection)
                {
                    sw.WriteLine($"Title:{a.Name} Description:{a.Description} Value:{a.MoneyValue}  Quantity:{ a.Quantity}");

                }
                sw.WriteLine($"To Pay: {summaryValue}");
            }
        }
    }
}
