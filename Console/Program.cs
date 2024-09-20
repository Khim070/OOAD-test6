using productlib;

namespace ProductConsole;

class Program
{
    static void Main(string[] args)
    {
        var req = new ProductCreateReq()
        {
            Code = "P001",
            Name = "Pepsi",
            Category = Category.Drink.ToString(),
        };
        Console.WriteLine("\nA new product was requested to create");
        Console.WriteLine($"\t+Code is: {req.Code}");
        Console.WriteLine($"\t+Name is: {req.Name}");
        Console.WriteLine($"\t+Category is: {req.Category}");
        Console.WriteLine("Any key to continue... ");
        Console.ReadKey();

        Product prd = req.ToEntity();

        Console.WriteLine("\nSuccessfully create a product");
        Console.WriteLine($"\t+Id is: {prd.Id}");
        Console.WriteLine($"\t+Code is: {prd.Code}");
        Console.WriteLine($"\t+Name is: {prd.Name}");
        Console.WriteLine($"\t+Category is: {prd.Category}");
        Console.WriteLine($"\t+Created date is: {prd.Created}");
        Console.WriteLine($"\t+Last Updated is: {prd.LastUpdated}");
        Console.WriteLine("Any key to continue... ");
        Console.ReadKey();

        ProductResponse res = prd.ToResponse();

        Console.WriteLine("\nSuccessfully response a product");
        Console.WriteLine($"\t+Id is: {res.Id}");
        Console.WriteLine($"\t+Code is: {res.Code}");
        Console.WriteLine($"\t+Name is: {res.Name}");
        Console.WriteLine($"\t+Category is: {res.Category}");
        Console.WriteLine("Any key to continue... ");
        Console.ReadKey();
    }
}
public static class ProductExtension
{
    public static Product ToEntity(this ProductCreateReq req)
    {
        var category = Category.None;
        Category.TryParse(req.Category, out category);
        return new Product
        {
            Id = Guid.NewGuid().ToString(),
            Name = req.Name,
            Code = req.Code,
            Category = category,
            Created = DateTime.Now,
            LastUpdated = null,
        };
    }

    public static ProductResponse ToResponse(this Product prd)
    {
        return new ProductResponse
        {
            Id = prd.Id,
            Code = prd.Code,
            Name = prd.Name,
            Category = prd.Category.ToString(),
        };
    }
}
