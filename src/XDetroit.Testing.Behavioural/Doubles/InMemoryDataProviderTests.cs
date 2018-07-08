using System;
using System.Collections.Generic;
using NUnit.Framework;
using XDetroit.WebFrontend.Dal;

namespace XDetroit.Testing.Behavioural.Doubles
{
    [TestFixture]
    class InMemoryDataProviderTests
    {
        [Test]
        [TestCaseSource(nameof(ProductItems))]
        public void CreateEntityShouldThrowOnInvalidInput(IEnumerable<object> productItemsList)
        {
            IDataProvider dataProvider = new InMemoryDataProvider<AppContext>();

            var testDelegate = new TestDelegate(() => dataProvider.CreateEntity(productItemsList));

            Assert.Throws<ArgumentException>(testDelegate);
        }

        public static object[] ProductItems =
        {
            new List<object> {new ProductItem {Name = "product1"}},
            new List<object>()
        };



    }
}
