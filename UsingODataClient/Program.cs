// See https://aka.ms/new-console-template for more information
using Simple.OData.Client;

Console.WriteLine("Hello, World!");


Thread.Sleep(10000);

var client = new ODataClient("https://localhost:44334/odata/");

var companies = await client
    .For<Company>()
    .Top(10)
    .Skip(1)
    .FindEntriesAsync();


foreach (var item in companies)
{
    Console.WriteLine(item.ID);
}

Console.ReadKey();

public class Product
{
    public int ID { get; set; }
    public int CompanyID { get; set; }
    public string? Name { get; set; }
    public decimal Price { get; set; }
}


public class Company
{
    public int ID { get; set; }
    public string? Name { get; set; }
    public int Size { get; set; }
    public List<Product>? Products { get; set; }
}