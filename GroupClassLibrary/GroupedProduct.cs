using System;
using System.Collections.Generic;
using System.Text;

namespace GroupClassLibrary
{
    // class groups the discount and relative retail cost when in a group.
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
