using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace project_Andrey_Rudenko.Model.Interfaces
{
    public interface IItemRepository
    {
        void AddItem(Item item);
        void RemoveItem(Item item);
        void SaveChanges();

        Item GetItem(int id);
        ICollection<Item> GetAllItems();
    }
}
