using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SportsStore.Domain.Abstract;
using SportsStore.Domain.Entities;
using SportsStore.WebUI.Controllers;
using SportsStore.WebUI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

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

        [TestMethod]
        public void CanAddToCart()
        {
            //Arrange
            Mock<IProductRespository> mock = new Mock<IProductRespository>();
            mock.Setup(m => m.Products).Returns(new Product[] { new Product { ProductID = 1, Name = "P1", Category = "Apples" } }.AsQueryable());
            Cart cart = new Cart();
            CartController target = new CartController(mock.Object, null);

            //Act
            target.AddToCart(cart, 1, null);

            //Assert
            Assert.AreEqual(cart.Lines.Count(), 1);
            Assert.AreEqual(cart.Lines.ToArray()[0].Product.ProductID, 1);
        }

        [TestMethod]
        public void AddingProductToCartGoesToCartScreen()
        {
            //Arrange
            Mock<IProductRespository> mock = new Mock<IProductRespository>();
            mock.Setup(m => m.Products).Returns(new Product[] { new Product { ProductID = 1, Name = "P1", Category = "Apples" } }.AsQueryable());
            Cart cart = new Cart();
            CartController target = new CartController(mock.Object, null);

            //Act
            RedirectToRouteResult result = target.AddToCart(cart, 2, "myUrl");

            //Assert
            Assert.AreEqual(result.RouteValues["action"], "Index");
            Assert.AreEqual(result.RouteValues["returnUrl"], "myUrl");
        }

        [TestMethod]
        public void CanViewCartContent()
        {
            //Arrange
            Cart cart = new Cart();

            CartController target = new CartController(null, null);

            //Act
            CartIndexViewModel result = (CartIndexViewModel)target.Index(cart, "myUrl").ViewData.Model;

            //Assert
            Assert.AreSame(result.Cart, cart);
            Assert.AreEqual(result.ReturnUrl, "myUrl");
        }

        [TestMethod]
        public void CannotCheckoutEmptyCart()
        {
            //Arrange
            Mock<IOrderProcessor> mock = new Mock<IOrderProcessor>();
            Cart cart = new Cart();
            ShippingDetails shippingDetails = new ShippingDetails();
            CartController target = new CartController(null, mock.Object);

            //Act
            ViewResult result = target.Checkout(cart, shippingDetails);

            //Assert
            //check that the order hasn't been passed on the processor
            mock.Verify(m => m.ProcessOrder(It.IsAny<Cart>(), It.IsAny<ShippingDetails>()), Times.Never());

            //check that the method is returning the default view
            Assert.AreEqual("", result.ViewName);

            //check that I am passing an invalid model to the view
            Assert.AreEqual(false, result.ViewData.ModelState.IsValid);
        }

        [TestMethod]
        public void CanCheckoutAndSubmitOrder()
        {
            //Arrange create a mock order processor
            Mock<IOrderProcessor> mock = new Mock<IOrderProcessor>();
            
            //Arrange create a cart with an item
            Cart cart = new Cart();
            cart.AddItem(new Product(), 1);

            CartController target = new CartController(null, mock.Object);

            //Act
            ViewResult result = target.Checkout(cart, new ShippingDetails());

            //Assert-check that the order has been passed on to the processor
            mock.Verify(m => m.ProcessOrder(It.IsAny<Cart>(), It.IsAny<ShippingDetails>()), Times.Once());
            //Assert - check that the method is returning the completed view
            Assert.AreEqual("Completed", result.ViewName);
            //Assert - checks if the models passed to the view is valid
            Assert.AreEqual(true, result.ViewData.ModelState.IsValid);
        }
    }
}
