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
    public async Task<IEnumerable<Item>> GetItemsAsync()
    {
      return await Task.FromResult(items);
    }

    // return the item based on the id
    public async Task<Item> GetItemAsync(Guid id)
    {
      var item = items.Where(item => item.Id == id).SingleOrDefault();
      return await Task.FromResult(item);
    }
    public async Task CreateItemAsync(Item item)
    {
      items.Add(item);
      await Task.CompletedTask;
    }

    // here we only update index, update object properties in controller
    public async Task UpdateItemAsync(Item item)
    {
      var index = items.FindIndex(exisitingitem => exisitingitem.Id == item.Id);
      items[index] = item;
      await Task.CompletedTask;
    }

    public async Task DeleteItemAsync(Guid id)
    {
      var index = items.FindIndex(exisitingitem => exisitingitem.Id == id);
      items.RemoveAt(index);
      await Task.CompletedTask;
    }
  }
}