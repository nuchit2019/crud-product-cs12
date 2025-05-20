namespace ProductAPI3.Domain.Entities
{
    public record Product(int Id, string Name, string? Description, decimal Price, int Stock);
    //public class Product(int id, string name, string? description, decimal price, int stock)
    //{
    //    public int Id { get; init; } = id;
    //    public string Name { get; set; } = name;
    //    public string? Description { get; set; } = description;
    //    public decimal Price { get; set; } = price;
    //    public int Stock { get; set; } = stock;
    //}
}
