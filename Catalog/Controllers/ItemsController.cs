using Microsoft.AspNetCore.Mvc;
using Catalog.Repositories;
using Catalog.Entities;

namespace Catalog.Controllers
{
  // mark this controller as a ApiController, so we can use it's default behaviors
  [ApiController]
  // create the route
  [Route("items")]
  public class ItemsController : ControllerBase
  {
    private readonly InMemItemsRepository repository;

    // constructor
    public ItemsController()
    {
      // create the in-memory repositories
      repository = new InMemItemsRepository();
    }

    // let GetItems() react to the get request, declare [HttpGet]
    // [Get] url: /items
    [HttpGet]
    public IEnumerable<Item> GetItems()
    {
      return repository.GetItems();
    }

    // [Get] url: /items/{id}
    [HttpGet("{id}")]
    public ActionResult<Item> GetItem(Guid id)
    {
      var item = repository.GetItem(id);
      if (item is null)
      {
        return NotFound();
      }
      return item;
    }
  }
}