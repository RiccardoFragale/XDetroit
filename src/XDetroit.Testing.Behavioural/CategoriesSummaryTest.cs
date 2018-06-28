using System;
using System.Linq;
using NUnit.Framework;
using XDetroit.WebFrontend.Dal;

namespace XDetroit.Testing.Behavioural
{
    [TestFixture]
    public class CategoriesSummaryTest
    {
        private IDataProvider dataProvider;
        private DataLayer dataLayer;

        [SetUp]
        public void Setup()
        {
            dataProvider = new InMemoryDataProvider();
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
