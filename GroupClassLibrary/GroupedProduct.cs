using System;
using System.Collections.Generic;
using System.Text;

namespace GroupClassLibrary
{
    public class GroupedProduct : Product
    {
        public double GroupedRetailCost { get; set; } = 0;

        public GroupedProduct(Product product) : base(product.SKU, product.UnitCost)
        {
        }
    }
}
