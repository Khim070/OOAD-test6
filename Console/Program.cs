using productlib;

namespace ProductConsole;

class Program
{
    static void Main(string[] args)
    {
        //Creating a service and initialize or load products used by the services
        ProductService.DataFile = "products.txt";
        ProductService service = new ProductService();
        service.Initialize();

        //Read all products used by the servies and then show them out
        Console.WriteLine("\nGetting all products... ");
        var responses = service.ReadAll();
        show(responses);

        // Read from the service a product with given key that not represent a product Id
        string key = "7507b3ea-6d33-4794-9cf4-823fb2d93558";
        Console.WriteLine($"\n Getting a product with id: {key}");
        var found = service.Read(key);
        if(found == null)
        {
            Console.WriteLine(">Not found");
        }else
        {
            Console.WriteLine(">Product found");
            Show(found);
        }
        Pause();

        // Read from the service a product with given key that represent a product Code
        key = "PRD001";
        Console.WriteLine($"\nGetting a product with code: {key}");
        found = service.Read(key);
        if (found == null)
        {
            Console.WriteLine(">Not found");
        }
        else
        {
            Console.WriteLine(">Product found");
            Show(found);
        }
        Pause();

        // Create a request with input data as code, name and category
        Console.WriteLine("\nProvide a request to create a product");
        var req = new ProductCreateReq();
        Console.Write("+Code: "); req.Code = Console.ReadLine() ?? "";
        Console.Write("+Name: "); req.Name = Console.ReadLine();

        var cats = Enum.GetValues<Category>()
            .Where(x => x != Category.None)
            .Select(x => x.ToString())
            .Aggregate((a, b) => a + "," + b);
        Console.WriteLine($"Available Categories > {cats}");
        Console.Write("+Category: "); req.Category = Console.ReadLine() ?? "";

        Console.WriteLine("\nRequest to create a new product... ");
        string? id = service.Create(req);
        if(id == null)
        {
            Console.WriteLine("> Failed to create a new product");
        }
        else
        {
            Console.WriteLine($"> Successfully create a product id, {id}");
            responses = service.ReadAll();
            show(responses);
        }
        Pause();
    }

    static void show(List<ProductResponse> responses)
    {
        Console.WriteLine("Id\t\t\t\t\tCode\tName\tCategory");
        responses.ForEach(x => Console.WriteLine($"{x.Id}\t{x.Code}\t{x.Name}\t{x.Category}"));
    }

    static void Show(ProductResponse response)
    {
        Console.WriteLine($"Id is: {response.Id}");
        Console.WriteLine($"Code is: {response.Code}");
        Console.WriteLine($"Name is: {response.Name}");
        Console.WriteLine($"Category is: {response.Category}");
    }

    static void Pause()
    {
        Console.WriteLine("Press any key to continue... ");
        Console.ReadKey();
        Console.WriteLine();
    }
}
