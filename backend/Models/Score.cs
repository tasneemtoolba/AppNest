// Models/Deal.cs
namespace DealsApp.Models;

public class Score
{
    public required String id { get; set; }
    public required String Productid { get; set; }
    public required String product_name { get; set; }
    public required String Dealid { get; set; }
    public double score_value { get; set; }
}
