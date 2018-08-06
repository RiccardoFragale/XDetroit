using System;
using System.Collections.Generic;
using NUnit.Framework;
using XDetroit.WebFrontend.Dal;
using XDetroit.WebFrontend.Interfaces;

namespace XDetroit.Testing.Behavioural.Providers
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

        [Test]
        public void FindEntityShouldReturnTheFoundEntity()
        {
            IDataProvider dataProvider = new InMemoryDataProvider<AppContext>();

            ItemCategory category = new ItemCategory
            {
                Name = "testCategory"
            };

            var persistedCategory = dataProvider.CreateEntity(category);
            dataProvider.SaveChanges();

            var foundEntity = dataProvider.Find<ItemCategory>(persistedCategory.Id);

            Assert.AreEqual(persistedCategory.Id,foundEntity.Id );
        }

        [Test]
        public void UpdateEntityShouldReturnTheFoundEntity()
        {
            IDataProvider dataProvider = new InMemoryDataProvider<AppContext>();

            ItemCategory category = new ItemCategory
            {
                Name = "testCategory"
            };

            var persistedCategory = dataProvider.CreateEntity(category);
            dataProvider.SaveChanges();
            var foundEntity = dataProvider.Find<ItemCategory>(persistedCategory.Id);
            Assert.AreEqual("testCategory", foundEntity.Name);

            persistedCategory.Name = "updatedName";

            dataProvider.UpdateEntity(persistedCategory);
            dataProvider.SaveChanges();
            foundEntity = dataProvider.Find<ItemCategory>(persistedCategory.Id);

            Assert.AreEqual("updatedName", foundEntity.Name);
        }
    }
}
