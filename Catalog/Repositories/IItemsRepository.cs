using Catalog.Entities;

namespace Catalog.Repositories
{
  public interface IItemsRepository
  {
    // return task since it's a async function now
    Task<Item> GetItemAsync(Guid id);
    Task<IEnumerable<Item>> GetItemsAsync();
    Task CreateItemAsync(Item item);
    Task UpdateItemAsync(Item item);
    Task DeleteItemAsync(Guid id);
  }
}