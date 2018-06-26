using System;
using System.Linq;
using NUnit.Framework;
using XDetroit.WebFrontend.Dal;
using XDetroit.WebFrontend.Models;

namespace XDetroit.Testing.Behavioural
{
    [TestFixture]
    public class CategoriesSummaryTest
    {
        [TestCase(2, 0, "sample1", 2)]
        [TestCase(2, 1, "sample3", 1)]
        [TestCase(1, 2, "sample3", 1)]
        public void TheRequestedPageOfCategoriesShouldBeReturned(int pageSize, int pageNumber, string expectedName, int expectedCount)
        {
            var dataProvider = new InMemoryDataProvider();
            dataProvider.CreateEntity(new ItemCategory { Name = "sample1" });
            dataProvider.CreateEntity(new ItemCategory { Name = "sample2" });
            dataProvider.CreateEntity(new ItemCategory { Name = "sample3" });
            dataProvider.SaveChanges();

            IDataLayer dataLayer = new DataLayer(dataProvider);
            var behaviourResult = dataLayer.GetCategories(pageSize, pageNumber);

            Assert.IsTrue(behaviourResult.Value.Any(
                    x => x.Name.Equals(expectedName, StringComparison.InvariantCultureIgnoreCase)), "Category not found.");
            Assert.AreEqual(expectedCount, behaviourResult.Value.Count, "Count is incorrect.");
        }
    }
}
