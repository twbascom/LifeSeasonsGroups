using System;

namespace GroupClassLibrary
{
    public class Group
    {
        public double RetailPrice { get; private set; }
        public string GroupCode { get; private set; }
        public Catalog catalog { get; private set; }

        public Group(string groupCode, double retailPrice, Catalog catalog)
        {
            this.GroupCode = groupCode;
            this.RetailPrice = retailPrice;
            this.catalog = catalog;
        }
    }
}
