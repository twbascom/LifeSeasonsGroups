using GroupClassLibrary;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;

namespace GroupTests
{
    [TestClass]
    public class GroupUnitTest
    {
        public const string DefaultSampleProductsFile = @"..\..\..\products.json";

        private Catalog GetSampleProductsFromFile()
        {
            var JSONSampleData = string.Empty;
            using (var defaultProductsJSONFile = new StreamReader(DefaultSampleProductsFile)) 
            {
                JSONSampleData = defaultProductsJSONFile.ReadToEnd();
            }
            var sampleInventory = new Catalog(JSONSampleData);
            return sampleInventory;
        }

        [TestMethod]
        public void TestGroupings()
        {
            var sampleInventory = GetSampleProductsFromFile();

            var example1 = new Group("G-208-225", 44.99, sampleInventory);
        }
    }
}
