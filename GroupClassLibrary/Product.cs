﻿
using Newtonsoft.Json;

namespace GroupClassLibrary
{
    public class Product
    {
        public int SKU { get; }
        [JsonProperty("Unit Cost")]
        public double UnitCost { get; } = 0;

        public Product(int SKU, double unitcost)
        {
            this.SKU = SKU;
            this.UnitCost = unitcost;
        }
    }
}
