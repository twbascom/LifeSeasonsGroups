using System;
using System.Collections.Generic;
using System.Text;

namespace GroupClassLibrary
{
    public class GroupedProduct : Product
    {
        public double GroupedRetailCost { get; set; } = 0;

        // track and expose discount percent for test.
        public double DiscountPercent { get; internal set; }

        public GroupedProduct(Product product) : base(product.SKU, product.UnitCost)
        {
        }
    }
}
