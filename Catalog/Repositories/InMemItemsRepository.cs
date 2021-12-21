using System.Collections.Generic;
using Catalog.Entities;

namespace Catalog.Repositories
{
  public class InMemItemsRepository : IItemsRepository
  {
    private readonly List<Item> items = new()
    {
      new Item { Id = Guid.NewGuid(), Name = "Potion", Price = 9, CreatedDate = DateTimeOffset.UtcNow },
      new Item { Id = Guid.NewGuid(), Name = "Iron Sword", Price = 20, CreatedDate = DateTimeOffset.UtcNow },
      new Item { Id = Guid.NewGuid(), Name = "Bronze Shield", Price = 18, CreatedDate = DateTimeOffset.UtcNow },
    };

    // return enumerable interface with a collection of items
    public IEnumerable<Item> GetItems()
    {
      return items;
    }

    // return the item based on the id
    public Item GetItem(Guid id)
    {
      return items.Where(item => item.Id == id).SingleOrDefault();
    }
    public void CreateItem(Item item)
    {
      items.Add(item);
    }

    // here we only update index, update object properties in controller
    public void UpdateItem(Item item)
    {
      var index = items.FindIndex(exisitingitem => exisitingitem.Id == item.Id);
      items[index] = item;
    }

    public void DeleteItem(Guid id)
    {
      var index = items.FindIndex(exisitingitem => exisitingitem.Id == id);
      items.RemoveAt(index);
    }
  }
}