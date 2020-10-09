using System;
using System.Collections.Generic;

namespace GroupClassLibrary
{
    public class Group
    {
        // retail price for all products in group
        public double RetailPrice { get; private set; }

        // track group unit cost for test purpose.
        public double GroupUnitCost { get; private set; }

        // refrence to product catalog for retrieving unit costs
        public Catalog ProductCatalog { get; private set; }

        // track parsed products for this group
        public List<GroupedProduct> ProductsInGroup { get; set; } = new List<GroupedProduct>();

        private const string PromotionPrefix = "G-";
        private const string PromotionDelemeter = "-";
        private const int RoundingPrecision = 2;

        // take groupcode string, retail price, and catalog reference for cost lookup.
        public Group(string groupCode, double retailPrice, Catalog catalog)
        {
            this.RetailPrice = retailPrice;
            this.ProductCatalog = catalog;
            ParseGroupCode(groupCode);
            CalculateGroupUnitCost();
        }

        // take input from string and parse into product objects.
        private void ParseGroupCode(string GroupCode)
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

        // use percent of unit cost to calculate individual product profit
        private void CalculateGroupUnitCost()
        {
            GroupUnitCost = 0;
            foreach (var product in ProductsInGroup)
            {
                GroupUnitCost += ProductCatalog.Products[product.SKU].UnitCost;
            }
            GroupUnitCost = Math.Round(GroupUnitCost, RoundingPrecision);

            // need skus count to be 2 or more to spreed out a group discount
            if (ProductsInGroup.Count > 1)
            {
                double missingRetailCost = RetailPrice;
                for (int i = 0; i < ProductsInGroup.Count - 1; i++)
                {
                    ProductsInGroup[i].DiscountPercent = Math.Round(ProductsInGroup[i].UnitCost / GroupUnitCost, RoundingPrecision);
                    ProductsInGroup[i].GroupedRetailCost = Math.Round(RetailPrice * ProductsInGroup[i].DiscountPercent, RoundingPrecision);
                    missingRetailCost = Math.Round(missingRetailCost - ProductsInGroup[i].GroupedRetailCost, RoundingPrecision);
                }
                ProductsInGroup[ProductsInGroup.Count - 1].GroupedRetailCost = missingRetailCost;
            }
        }
    }
}
