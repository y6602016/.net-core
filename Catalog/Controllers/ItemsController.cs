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
    public IEnumerable<ItemDto> GetItems()
    {
      // call extension method, item call AsDto, item will be the this parameter
      // of the AsDto function
      var items = repository.GetItems().Select(item => item.AsDto());
      return items;
    }

    // [Get] url: /items/{id}
    [HttpGet("{id}")]
    public ActionResult<ItemDto> GetItem(Guid id)
    {
      var item = repository.GetItem(id);
      if (item is null)
      {
        return NotFound();
      }
      return item.AsDto();
    }

    // [Post] url: /items
    [HttpPost]
    public ActionResult<ItemDto> CreateItem(CreateItemDto itemDto)
    {
      Item item = new()
      {
        Id = Guid.NewGuid(),
        Name = itemDto.Name,
        Price = itemDto.Price,
        CreatedDate = DateTimeOffset.UtcNow
      };

      repository.CreateItem(item);
      return CreatedAtAction(nameof(GetItem), new { id = item.Id }, item.AsDto());
    }
  }
}