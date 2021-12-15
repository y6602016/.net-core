// Dtos prevent exposing the entity information to client
namespace Catalog.Dtos
{
  public record ItemDto
  {
    // Guid is a data type, it's like the uuid, randomly generate id for objects
    public Guid Id { get; init; }
    public string Name { get; init; }
    public decimal Price { get; init; }
    public DateTimeOffset CreatedDate { get; init; }
  }
}