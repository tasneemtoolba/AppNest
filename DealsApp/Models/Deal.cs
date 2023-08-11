// Models/Deal.cs
namespace DealsApp.Models;

public class Deal
{
    public required String id { get; set; }
    public required String Productid { get; set; }

    public double discount_percentage { get; set; }
}
