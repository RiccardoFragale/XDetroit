using System.Linq;
using NUnit.Framework;
using XDetroit.WebFrontend.Dal;
using XDetroit.WebFrontend.Models;

namespace XDetroit.Testing.Behavioural
{
    [TestFixture]
    public class CategoriesSummaryTest
    {
        [Test]
        public void TheRequestedPageOfCategoriesShouldBeReturned()
        {
            var dataProvider = new InMemoryDataProvider();
            dataProvider.CreateEntity(new ItemCategory { Name = "sample" });
            dataProvider.SaveChanges();

            IDataLayer dataLayer = new DataLayer(dataProvider);
            var behaviourResult = dataLayer.GetCategories(10, 0);

            Assert.IsTrue(behaviourResult.Value.Any());
        }
    }
}
