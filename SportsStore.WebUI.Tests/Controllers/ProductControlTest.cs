using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SportsStore.Domain.Entities;
using SportsStore.Domain.Abstract;
using System.Linq;
using SportsStore.WebUI.Controllers;
using System.Collections;
using System.Collections.Generic;

namespace SportsStore.WebUI.Tests.Controllers
{
    [TestClass]
    public class ProductControlTest
    {
        [TestMethod]
        public void Can_Paginate()
        {
            //arrange
            Mock<IProductRepository> mock = new Mock<IProductRepository>();
            mock.Setup(m => m.Products).Returns(new Product[]
            {
                new Product {ProductID = 1, Name = "P1" },
                new Product {ProductID = 2, Name = "P2" },
                new Product {ProductID = 3, Name = "P3" },
                new Product {ProductID = 4, Name = "P4" },
                new Product {ProductID = 5, Name = "P5" }
            }.AsQueryable());

            ProductController controller = new ProductController(mock.Object);
            controller.PageSize = 3;
            //action
            IEnumerable<Product> result = (IEnumerable<Product>)controller.List(2).Model;
            //assert
            Product[] proArray = result.ToArray();
            Assert.IsTrue(proArray.Length == 2);
            Assert.AreEqual(proArray[0].Name, "P4");
            Assert.AreEqual(proArray[1].Name, "P5");

        }
    }
}
