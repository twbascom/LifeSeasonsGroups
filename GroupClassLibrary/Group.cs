using System;
using System.Collections.Generic;

namespace GroupClassLibrary
{
    public class Group
    {
        public double GroupRetailPrice { get; private set; }
        public double GroupUnitCost { get; private set; }
        public string GroupCode { get; private set; }
        public Catalog ProductCatalog { get; private set; }
        //public List<int> ProductSKUsInGroup { get; set; } = new List<int>();
        public List<Product> ProductsInGroup { get; set; } = new List<Product>();

        private const string PromotionPrefix = "G-";
        private const string PromotionDelemeter = "-";

        public Group(string groupCode, double retailPrice, Catalog catalog)
        {
            this.GroupCode = groupCode;
            this.GroupRetailPrice = retailPrice;
            this.ProductCatalog = catalog;

            ParseGroupCode();

            CalculateGroupUnitCost();
        }

        private void ParseGroupCode()
        {
            if (!GroupCode.Contains(PromotionPrefix)) throw new Exception($"Promotion text requires prefix: {PromotionPrefix}");

            var promotionWithoutPrefix = GroupCode.Substring(PromotionPrefix.Length);
            if (promotionWithoutPrefix.Length < 2) throw new Exception($"Promotion text requires two or more SKUs: {GroupCode}");

            ProductsInGroup.Clear();
            foreach (var productCode in promotionWithoutPrefix.Split(PromotionDelemeter))
            {
                var productToCopy = ProductCatalog.Products[int.Parse(productCode)];
                ProductsInGroup.Add(new Product(productToCopy.SKU, productToCopy.UnitCost));
            }
        }

        private void CalculateGroupUnitCost()
        {
            GroupUnitCost = 0;
            foreach (var product in ProductsInGroup)
            {
                GroupUnitCost += ProductCatalog.Products[product.SKU].UnitCost;
            }
            // need skus count to be 2 or more to spreed out a group discount
            if (ProductsInGroup.Count > 1)
            {
                for (int i = 0; i < ProductsInGroup.Count - 1; i++)
                {
                    double discountPercent = ProductsInGroup[i].UnitCost / GroupUnitCost;
                    ProductsInGroup[i].GroupedRetailCost = GroupRetailPrice * discountPercent;
                }
            }
        }
    }
}
