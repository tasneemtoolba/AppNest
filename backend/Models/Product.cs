namespace DealsApp.Models;

public class Product
{
    public required String id { get; set; }
    public required String name { get; set; }
    public required double price { get; set; }
    public List<Deal>? deals { get; set; }
}
