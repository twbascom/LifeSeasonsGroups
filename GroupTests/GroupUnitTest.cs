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

            testSumsInGroup(example1);
        }

        [TestMethod]
        public void TestGroupingsExample2()
        {
            var sampleCatalog = GetSampleProductsFromFile();

            var example2 = new Group("G-201-202-203-204", 89.00, sampleCatalog);

            Assert.AreEqual(51, example2.ProductCatalog.Products.Count, "Expected 50 products in default database.");
            Assert.AreEqual(4, example2.ProductsInGroup.Count, "Expected 4 products in G-208-225-237-258.");

            Assert.AreEqual(18.51, example2.GroupUnitCost, "Expected group unit cost to be $18.51");

            Assert.AreEqual(0.37, example2.ProductsInGroup[0].DiscountPercent, "Expected 201 to discount to be 37%");
            Assert.AreEqual(0.1, example2.ProductsInGroup[1].DiscountPercent, "Expected 202 to discount to be 10%");
            Assert.AreEqual(0.09, example2.ProductsInGroup[2].DiscountPercent, "Expected 203 to discount to be 9%");
            
            Assert.AreEqual(32.93, example2.ProductsInGroup[0].GroupedRetailCost, "Expected 201 to cost 32.93");
            Assert.AreEqual(8.9, example2.ProductsInGroup[1].GroupedRetailCost, "Expected 202 to cost 8.9");
            Assert.AreEqual(8.01, example2.ProductsInGroup[2].GroupedRetailCost, "Expected 203 to cost 8.01");
            Assert.AreEqual(39.16, example2.ProductsInGroup[3].GroupedRetailCost, "Expected 204 to cost 39.16");

            testSumsInGroup(example2);

        }

        private void testSumsInGroup(Group example2)
        {
            double sum = 0.0;
            foreach (var product in example2.ProductsInGroup)
            {
                sum += product.GroupedRetailCost;
            }

            Assert.AreEqual(example2.RetailPrice, sum, "Expect sum of products to equal group discount");
        }
    }
}
