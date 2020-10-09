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
        public List<Product> ProductInGroup { get; set; } = new List<Product>();

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

            ProductSKUsInGroup.Clear();
            foreach (var productCode in promotionWithoutPrefix.Split(PromotionDelemeter))
            {
                ProductSKUsInGroup.Add(int.Parse(productCode));
            }
        }

        private void CalculateGroupUnitCost()
        {
            GroupUnitCost = 0;
            foreach (var sku in ProductSKUsInGroup)
            {
                GroupUnitCost += ProductCatalog.Products[sku].UnitCost;
            }
            // need skus count to be 2 or more to spreed out a group discount
            if (ProductSKUsInGroup.Count > 1)
            {
                for (int i = 0; i < ProductSKUsInGroup.Count - 1; i++)
                {

                }
            }
        }
    }
}
