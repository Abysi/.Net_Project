using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Text;
using project_Andrey_Rudenko.Model.Enum;
using project_Andrey_Rudenko.Model.Interfaces;

namespace project_Andrey_Rudenko.Model
{
    public class Market
    {
        private readonly IItemRepository _itemRepository;
        private Order _order;
        public Market(IItemRepository repo)
        {
            _itemRepository = repo;
            _order = new Order();

        }


        public void AddItem(Item item)
        {
            _itemRepository.AddItem(item);
        }
        public void RemoveItem()
        {
            Console.Clear();
            Console.WriteLine("Enter Index");
            string str = Console.ReadLine();
            int.TryParse(str, out int indexResult);
            _itemRepository.RemoveItem(_itemRepository.GetItem(indexResult));

        }



        public void ShowDb()
        {
            var collection = _itemRepository.GetAllItems();

            var header = String.Format("{0,3}{1,10}{2,10}{3,20}{4,15}{5,12}\n",
                "Id", "Name", "Value", "Description", "Category", "Quantity");

            Console.WriteLine(header);
            foreach (var item in collection)
            {

                Console.WriteLine($"{item.Id,3}{item.Name,10}{item.MoneyValue,10}{item.Description,20}{item.Category,15}{ item.Quantity,12}");
            }
        }


        public void BuySomeItems()
        {

            bool exit = false;
            do
            {

                Console.Clear();
                Console.WriteLine();
                ShowDb();

                int chosenIndex;
                int chosenQuantity;

                Console.WriteLine("Enter Index");
                var str = Console.ReadLine();
                while (!int.TryParse(str, out chosenIndex))
                {
                    Console.WriteLine("Enter Integer!");
                    str = Console.ReadLine();
                }

                Console.WriteLine("Enter Quantity");
                str = Console.ReadLine();
                while (!int.TryParse(str, out chosenQuantity))
                {
                    Console.WriteLine("Enter Correct Quantity!");
                    str = Console.ReadLine();
                }


                var item = _itemRepository.GetItem(chosenIndex);
                while (item.Quantity < chosenQuantity)
                {
                    Console.WriteLine("We don't have so much ,enter current value!");
                    str = Console.ReadLine();
                    int.TryParse(str, out chosenQuantity);
                }

                item.Quantity -= chosenQuantity;
                _order.Add(new Item(item) { Quantity = chosenQuantity });
                _itemRepository.SaveChanges();

                Console.WriteLine("Your Item Add To Basket");
                Console.WriteLine();
                Console.WriteLine("Some Char-continue /0-Show Order");

                str = Console.ReadLine();
                int.TryParse(str, out int numbeResult);
                if (numbeResult == 0)
                {
                    exit = true;
                    _order.SaveToFile();
                    _order.Print();
                }

                Console.WriteLine("Enter for continue use program");
                Console.Read();
            } while (!exit);
        }

        public Item CreateNewItem()
        {
            Console.Clear();
            Console.WriteLine("Enter Value");
            string valueLine = Console.ReadLine();
            int.TryParse(valueLine, out int a);
            Console.WriteLine("Enter Name");
            string nameLine = Console.ReadLine();
            Console.WriteLine("Enter Description");
            string descriptionLine = Console.ReadLine();
            Console.WriteLine("Enter Category (Food/Drink)");
            string categoryLine = Console.ReadLine();
            System.Enum.TryParse(categoryLine, out ItemCategory catResult);
            Console.WriteLine("Enter Quantity");
            string quantityLine = Console.ReadLine();
            int.TryParse(quantityLine, out int b);

            Item item = new Item(a, nameLine, descriptionLine, catResult, b);
            return item;
        }

        public void Start()
        {
            while (true)
            {
                Console.Clear();
                ShowDb();
                Console.Write(
                    Environment.NewLine + new string('-', 20) +
                    Environment.NewLine + "1-Add Item To DataBase" +
                    Environment.NewLine + "2-Remove Item From DataBase" +
                    Environment.NewLine + "3-Buy Some Items" +
                    Environment.NewLine + "4-Exit");
                Console.WriteLine();
                char choise = Console.ReadKey().KeyChar;
                switch (choise)
                {
                    case '1':
                        AddItem(CreateNewItem());
                        break;
                    case '2':
                        RemoveItem();
                        break;
                    case '3':
                        BuySomeItems();
                        break;
                    case '4':
                        Environment.Exit(0);
                        break;
                }
            }
        }
    }
}
