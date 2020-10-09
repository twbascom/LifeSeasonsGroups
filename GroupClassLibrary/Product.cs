
using Newtonsoft.Json;

namespace GroupClassLibrary
{
    public class Product
    {
        public int SKU { get; set; }
        [JsonProperty("Unit Cost")]
        public double UnitCost { get; set; } = 0;

        public double RetailCost { get; set; } = 0;
    }
}
