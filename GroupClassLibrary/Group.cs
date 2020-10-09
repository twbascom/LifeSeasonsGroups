using System;
using System.Collections.Generic;

namespace GroupClassLibrary
{
    public class Group
    {
        public double RetailPrice { get; private set; }
        public double GroupUnitCost { get; private set; }
        public string GroupCode { get; private set; }
        public Catalog ProductCatalog { get; private set; }
        //public List<int> ProductSKUsInGroup { get; set; } = new List<int>();
        public List<GroupedProduct> ProductsInGroup { get; set; } = new List<GroupedProduct>();

        private const string PromotionPrefix = "G-";
        private const string PromotionDelemeter = "-";
        private const int RoundingPrecision = 2;

        public Group(string groupCode, double retailPrice, Catalog catalog)
        {
            this.GroupCode = groupCode;
            this.RetailPrice = retailPrice;
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
                ProductsInGroup.Add(new GroupedProduct(ProductCatalog.Products[int.Parse(productCode)]));
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
                double missingRetailCost = RetailPrice;
                for (int i = 0; i < ProductsInGroup.Count - 1; i++)
                {
                    double discountPercent = Math.Round(ProductsInGroup[i].UnitCost / GroupUnitCost, RoundingPrecision);
                    ProductsInGroup[i].GroupedRetailCost = Math.Round(RetailPrice * discountPercent, RoundingPrecision);
                    missingRetailCost = Math.Round(missingRetailCost - ProductsInGroup[i].GroupedRetailCost, RoundingPrecision);
                }
                ProductsInGroup[ProductsInGroup.Count - 1].GroupedRetailCost = missingRetailCost;
            }
        }
    }
}
