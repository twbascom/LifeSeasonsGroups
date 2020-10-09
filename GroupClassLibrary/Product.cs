
using Newtonsoft.Json;

namespace GroupClassLibrary
{
    public class Product
    {
        public int SKU { get; set; }
        [JsonProperty("Unit Cost")]
        public double Cost { get; set; } = 0;

        public double retail { get; set; } = 0;
    }
}
