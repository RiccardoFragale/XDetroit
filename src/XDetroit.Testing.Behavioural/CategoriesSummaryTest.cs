using System.Linq;
using NUnit.Framework;
using XDetroit.WebFrontend.Dal;

namespace XDetroit.Testing.Behavioural
{
    [TestFixture]
    public class CategoriesSummaryTest
    {
        [Test]
        public void TheRequestedPageOfCategoriesShouldBeReturned()
        {
            IDataLayer dataLayer = new DataLayer(new DataProvider());
            var categories = dataLayer.GetCategories(10, 0);

            Assert.IsTrue(categories.Any());
        }
    }
}
