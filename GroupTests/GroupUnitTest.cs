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
        public void RunAllExamples()
        {
            TestGroupingsExample1();
            TestGroupingsExample2();
        }

        [TestMethod]
        public void TestGroupingsExample1()
        {
            var sampleCatalog = GetSampleProductsFromFile();

            var example1 = new Group("G-208-225", 44.99, sampleCatalog);

            Assert.AreEqual(51, example1.ProductCatalog.Products.Count, "Expected 50 products in default database.");
            Assert.AreEqual(2, example1.ProductsInGroup.Count, "Expected 2 products in G-208-225.");

            Assert.AreEqual(35.09, example1.ProductsInGroup[0].GroupedRetailCost, "Expected 208 to cost 35.09");
            Assert.AreEqual(9.90, example1.ProductsInGroup[1].GroupedRetailCost, "Expected 225 to cost 9.90");


            var example2 = new Group("G-208-225-237-258", 89.00, sampleCatalog);

        }
        [TestMethod]
        public void TestGroupingsExample2()
        {
            var sampleCatalog = GetSampleProductsFromFile();

            var example2 = new Group("G-208-225-237-258", 89.00, sampleCatalog);

            Assert.AreEqual(51, example2.ProductCatalog.Products.Count, "Expected 50 products in default database.");
            Assert.AreEqual(4, example2.ProductsInGroup.Count, "Expected 4 products in G-208-225-237-258.");

            Assert.AreEqual(41.99, example2.ProductsInGroup[0].GroupedRetailCost, "Expected 208 to cost 41.99");
            Assert.AreEqual(9.99, example2.ProductsInGroup[1].GroupedRetailCost, "Expected 225 to cost 9.99");
            Assert.AreEqual(9.99, example2.ProductsInGroup[2].GroupedRetailCost, "Expected 237 to cost 9.99");
            Assert.AreEqual(46.99, example2.ProductsInGroup[3].GroupedRetailCost, "Expected 258 to cost 9.99");



        }
    }
}
