using Catalog.Dtos;
using Catalog.Entities;

namespace Catalog
{
  // An extension method must be static
  public static class Extentions
  {
    // 'this' is the syntax for declaring extension methods
    // who calls this extension method will be the "this"
    public static ItemDto AsDto(this Item item)
    {
      return new ItemDto
      {
        Id = item.Id,
        Name = item.Name,
        Price = item.Price,
        CreatedDate = item.CreatedDate
      };
    }
  }
}