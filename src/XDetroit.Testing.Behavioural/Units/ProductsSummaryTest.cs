using System;
using System.Linq;
using NUnit.Framework;
using XDetroit.Testing.Behavioural.Providers;
using XDetroit.WebFrontend.Dal;
using XDetroit.WebFrontend.Interfaces;

namespace XDetroit.Testing.Behavioural.Units
{
    [TestFixture]
    public class ProductsSummaryTest
    {
        private IDataProvider dataProvider;
        private DataLayer dataLayer;

        [SetUp]
        public void Setup()
        {
            dataProvider = new InMemoryDataProvider<AppContext>();
            dataLayer = new DataLayer(dataProvider);
        }

        [Test]
        public void TheRequestedPageOfProductsShouldBeReturned()
        {
            var category = dataProvider.CreateEntity(new ItemCategory { Name = "sample category" });
            dataProvider.SaveChanges();

            var product1 = dataProvider.CreateEntity(new ProductItem { Name = "sample product 1", ExtCategoryId = category.Id});
            dataProvider.CreateEntity(new ProductItem { Name = "sample product 2", ExtCategoryId = category.Id});
            dataProvider.SaveChanges();

            var behaviourResult = dataLayer.GetProducts(10, 0);

            Assert.IsTrue(behaviourResult.Value.Any(
                    x => x.Name.Equals(product1.Name, StringComparison.InvariantCultureIgnoreCase)), "Product not found.");

            Assert.AreEqual(2, behaviourResult.Value.Count, "Count is incorrect.");

            Assert.That(behaviourResult.Value.All(x=>x.ExtCategoryId != 0), "External reference Id is empty.");

            Assert.That(behaviourResult.Value.All(x=>x.ExtCategory != null), "Navigation property is empty.");
        }
    }
}
