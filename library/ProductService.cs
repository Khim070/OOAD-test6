using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace productlib
{
    public class ProductService
    {
        public static string DataFile { get; set; } = "products.txt";
        protected List<Product> _store = new();
        protected static List<ProductCreateReq> reqs = new List<ProductCreateReq>()
        {
            new()
            {
                Code = "PRD001",
                Name = "Coca",
                Category = "Food"
            },
            new()
            {
                Code = "PRD002",
                Name = "Honda",
                Category = "Vehicle"
            },
            new()
            {
                Code = "PRD003",
                Name = "T-short",
                Category = "Cloth"
            },
        };

        protected void Save()
        {
            File.WriteAllText(DataFile, JsonSerializer.Serialize<List<Product>>(_store));
        }

        public string? Create(ProductCreateReq req)
        {
            return Create(req, true);
        }

        protected string? Create(ProductCreateReq req, bool isSaved)
        {
            req.Code = req.Code.Trim();
            if(string.IsNullOrEmpty(req.Code)) return null;
            if (_store.Exists(x => x.Code.ToLower() == req.Code.ToLower())) return null;
            Product entity = req.ToEntity();
            _store.Add(entity);
            if (isSaved == true) Save();
            return entity.Id;
        }

        public List<string?> Initialize()
        {
            if (!File.Exists(DataFile))
            {
                var result = reqs.Select(x => Create(x, false)).ToList();  
                Save();
                return result;
            }

            string jsonData = File.ReadAllText(DataFile) ?? "[]";
            _store = JsonSerializer.Deserialize<List<Product>>(jsonData) ?? new();
            return _store.Select(x => x.Id).ToList();
        }

        public List<ProductResponse> ReadAll()
        {
            return _store.Select(x => x.ToResponse()).ToList();
        }

        public ProductResponse? Read(string key)
        {
            key = key.ToLower();
            var entity = _store.FirstOrDefault(x => x.Id?.ToLower() == key || x.Code.ToLower() == key);
            return entity?.ToResponse();
        }
    }
}
