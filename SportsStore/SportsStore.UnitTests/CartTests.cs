using Microsoft.VisualStudio.TestTools.UnitTesting;
using SportsStore.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportsStore.UnitTests
{
    [TestClass]
    public class CartTests
    {
        [TestMethod]
        public void CanAddNewLines()
        {
            //Arrgane
            var p1 = new Product { ProductID = 1, Name = "P1" };
            var p2 = new Product { ProductID = 2, Name = "P2" };

            Cart target = new Cart();

            //Act
            target.AddItem(p1, 1);
            target.AddItem(p2, 1);
            var result = target.Lines.ToArray();

            //Assert
            Assert.AreEqual(result[0].Product, p1);
            Assert.AreEqual(result[1].Product, p2);
            Assert.AreEqual(result.Length, 2);
        }

        [TestMethod]
        public void CanAddQuantityForExistingLines()
        {
            //Arrange
            var p1 = new Product { ProductID = 1, Name = "P1" };
            var p2 = new Product { ProductID = 2, Name = "P2" };

            Cart target = new Cart();

            //Act
            target.AddItem(p1, 1);
            target.AddItem(p2, 1);
            target.AddItem(p1, 10);
            var result = target.Lines.OrderBy(x => x.Product.ProductID).ToArray();

            //Assert
            Assert.AreEqual(2, result.Length);
            Assert.AreEqual(11, result[0].Quantity);
            Assert.AreEqual(1, result[1].Quantity);
        }

        [TestMethod]
        public void CanRemoveLine()
        {
            //Arrange
            var p1 = new Product { ProductID = 1, Name = "P1" };
            var p2 = new Product { ProductID = 2, Name = "P2" };
            var p3 = new Product { ProductID = 3, Name = "P3" };

            Cart target = new Cart();

            //Act
            target.AddItem(p1, 1);
            target.AddItem(p2, 1);
            target.AddItem(p3, 1);
            target.RemoveLine(p1);

            //Assert
            Assert.AreEqual(2, target.Lines.Count());
            Assert.AreEqual(0, target.Lines.Where(p => p.Product.Name == "p1").Count());
        }

        [TestMethod]
        public void CalculateCartTotal()
        {
            //Arrange
            var p1 = new Product { ProductID = 1, Name = "P1", Price = 10 };
            var p2 = new Product { ProductID = 2, Name = "P2", Price = 20 };
            var p3 = new Product { ProductID = 3, Name = "P3", Price = 30 };

            Cart target = new Cart();

            target.AddItem(p1, 1);
            target.AddItem(p2, 1);
            target.AddItem(p3, 1);

            //Act
            var result = target.ComputeTotalValue();

            //Assert
            Assert.AreEqual(60, result);
        }

        [TestMethod]
        public void CanClearContent()
        {
            //Arrange
            var p1 = new Product { ProductID = 1, Name = "P1", Price = 10 };
            var p2 = new Product { ProductID = 2, Name = "P2", Price = 20 };
            var p3 = new Product { ProductID = 3, Name = "P3", Price = 30 };

            Cart target = new Cart();

            target.AddItem(p1, 1);
            target.AddItem(p2, 1);
            target.AddItem(p3, 1);

            //Act
            target.Clear();
            var result = target.Lines.ToArray();

            //Assert
            Assert.AreEqual(0, result.Length);
        }
    }
}
