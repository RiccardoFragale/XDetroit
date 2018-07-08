using System;
using System.Linq;
using NUnit.Framework;
using XDetroit.Testing.Behavioural.Doubles;
using XDetroit.WebFrontend.Dal;

namespace XDetroit.Testing.Behavioural.Units
{
    [TestFixture]
    public class CategoriesSummaryTest
    {
        private IDataProvider dataProvider;
        private DataLayer dataLayer;

        [SetUp]
        public void Setup()
        {
            dataProvider = new InMemoryDataProvider<AppContext>();
            dataLayer = new DataLayer(dataProvider);
        }

        [TestCase(2, 0, "sample1", 2)]
        [TestCase(2, 1, "sample3", 1)]
        [TestCase(1, 2, "sample3", 1)]
        public void TheRequestedPageOfCategoriesShouldBeReturned(int pageSize, int pageNumber, string expectedName, int expectedCount)
        {
            dataProvider.CreateEntity(new ItemCategory { Name = "sample1" });
            dataProvider.CreateEntity(new ItemCategory { Name = "sample2" });
            dataProvider.CreateEntity(new ItemCategory { Name = "sample3" });
            dataProvider.SaveChanges();

            var behaviourResult = dataLayer.GetCategories(pageSize, pageNumber);

            Assert.IsTrue(behaviourResult.Value.Any(
                    x => x.Name.Equals(expectedName, StringComparison.InvariantCultureIgnoreCase)), "Category not found.");
            Assert.AreEqual(expectedCount, behaviourResult.Value.Count, "Count is incorrect.");
        }

        [Test]
        public void TheCategoryShouldIncludeNaivgationPropertiesWhenRetrievedFromDataLayer()
        {
            var category = dataProvider.CreateEntity(new ItemCategory { Name = "sample category" });
            dataProvider.SaveChanges();

            dataProvider.CreateEntity(new ProductItem { Name = "sample product 1", ExtCategoryId = category.Id });
            dataProvider.CreateEntity(new ProductItem { Name = "sample product 2", ExtCategoryId = category.Id });
            dataProvider.SaveChanges();

            var behaviourResult = dataLayer.GetCategories(10, 0);

            Assert.AreEqual(2,
                behaviourResult.Value
                    .Single(x => x.Name.Equals(category.Name, StringComparison.InvariantCultureIgnoreCase)).ColProducts
                    .Count, "Products count is incorrect.");
        }

        [Test]
        public void CreateCategoryShouldPersistNewCategory()
        {
            dataLayer.CreateCategory(new ItemCategory
            {
                Name = "testCategory"
            });
            
            var categories = dataProvider.GetEntities<ItemCategory>();
            Assert.That(categories.Count(), Is.EqualTo(1));
        }
    }
}
