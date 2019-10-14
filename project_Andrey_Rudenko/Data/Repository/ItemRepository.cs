using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.EntityFrameworkCore;
using project_Andrey_Rudenko.Model;
using project_Andrey_Rudenko.Model.Interfaces;

namespace project_Andrey_Rudenko.Data
{
    public class ItemRepository : IItemRepository
    {
        private AppDbContext _ctx;
        private DbSet<Item> _items;

        public ItemRepository(AppDbContext context)
        {
            _ctx = context;
            _items = context.Items;
          
        }

        public void AddItem(Item item)
        {
            _ctx.Database.EnsureCreated();
            _items.Add(item);
            _ctx.SaveChanges();
        }

        public void RemoveItem(Item item)
        {

            _items.Remove(item);
            _ctx.SaveChanges();
        }

        public void SaveChanges()
        {
            _ctx.SaveChanges();
        }

        public Item GetItem(int id)
        {
            return _items.FirstOrDefault(a => a.Id == id);
        }

        public ICollection<Item> GetAllItems()
        {
            return _items.ToList();
        }
    }
}
