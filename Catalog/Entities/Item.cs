using System;

namespace Catalog.Entities
{
  // record is used for immutable refereance objects
  public record Item
  {
    // Guid is a data type, it's like the uuid, randomly generate id for objects
    public Guid Id { get; init; }
    public string Name { get; init; }
    public decimal Price { get; init; }
    public DateTimeOffset CreatedDate { get; init; }
  }
}