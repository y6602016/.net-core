using Microsoft.AspNetCore.Mvc;
using Catalog.Repositories;
using Catalog.Dtos;
using Catalog.Entities;

namespace Catalog.Controllers
{
  // mark this controller as a ApiController, so we can use it's default behaviors
  [ApiController]
  // create the route
  [Route("items")]
  public class ItemsController : ControllerBase
  {
    private readonly IItemsRepository repository;

    // constructor, Dependency injection, take interface as parameter
    // controller just take interface(abstract container), the repository will
    // implement the interface and register the interface in startup.cs file
    // so controller doesn't care about which repository is used, every repository
    // implements the interface can be used. 
    public ItemsController(IItemsRepository repository)
    {
      // create the in-memory repositories
      this.repository = repository;
    }

    // let GetItems() react to the get request, declare [HttpGet]
    // [Get] url: /items
    [HttpGet]
    public async Task<IEnumerable<ItemDto>> GetItemsAsync()
    {
      // call extension method, item call AsDto, item will be the this parameter
      // of the AsDto function
      var items = (await repository.GetItemsAsync())
                  .Select(item => item.AsDto());
      return items;
    }

    // [Get] url: /items/{id}
    [HttpGet("{id}")]
    public async Task<ActionResult<ItemDto>> GetItemAsync(Guid id)
    {
      var item = await repository.GetItemAsync(id);
      if (item is null)
      {
        return NotFound();
      }
      return item.AsDto();
    }

    // [Post] url: /items
    [HttpPost]
    public async Task<ActionResult<ItemDto>> CreateItemAsync(CreateItemDto itemDto)
    {
      Item item = new()
      {
        Id = Guid.NewGuid(),
        Name = itemDto.Name,
        Price = itemDto.Price,
        CreatedDate = DateTimeOffset.UtcNow
      };

      await repository.CreateItemAsync(item);
      return CreatedAtAction(nameof(GetItemAsync), new { id = item.Id }, item.AsDto());
    }

    // we update object properties here, update object's index in repository in InMemItemsRepository.cs
    // [Put] url: /items/{id}
    [HttpPut("{id}")]
    public async Task<ActionResult> UpdateItemAsync(Guid id, UpdateItemDto itemDto)
    {
      var existingItem = await repository.GetItemAsync(id);

      // if not find item
      if (existingItem is null)
      {
        return NotFound();
      }

      // if found, update the property by "with" expression
      Item updatedItem = existingItem with
      {
        Name = itemDto.Name,
        Price = itemDto.Price
      };

      await repository.UpdateItemAsync(updatedItem);

      return NoContent();
    }

    // [Delete] url: /items/{id}
    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteItem(Guid id)
    {
      var existingItem = await repository.GetItemAsync(id);

      // if not find item
      if (existingItem is null)
      {
        return NotFound();
      }

      // if found, delete it
      await repository.DeleteItemAsync(id);

      return NoContent();
    }
  }
}