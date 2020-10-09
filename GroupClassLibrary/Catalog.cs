using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace GroupClassLibrary
{
    public class Catalog
    {
        private string productJSON;
        public Dictionary<int, Product> Products { get; private set; } = new Dictionary<int, Product>();

        public Catalog(string productJSON)
        {
            this.productJSON = productJSON;

            var tempProducts = JsonConvert.DeserializeObject<List<Product>>(productJSON);
            foreach (var product in tempProducts)
            {
                if (Products.ContainsKey(product.SKU)) throw new Exception($"Inventory already contains Product SKU {product.SKU}");                
                Products[product.SKU] = product;
            }
        }
    }
}
