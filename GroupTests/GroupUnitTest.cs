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
            var sampleCatalog = new Catalog(JSONSampleData);
            return sampleCatalog;
        }

        [TestMethod]
        public void TestGroupings()
        {
            var sampleCatalog = GetSampleProductsFromFile();

            var example1 = new Group("G-208-225", 44.99, sampleCatalog);

            Assert.AreEqual(50, example1.catalog.Products.Count, "Expected 50 products in default database.");
        }
    }
}
