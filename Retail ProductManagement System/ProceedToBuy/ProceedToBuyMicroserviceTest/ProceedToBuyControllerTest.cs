using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using ProceedToBuyMicroservice.Controllers;
using ProceedToBuyMicroservice.Repositiory;
using System;

namespace ProceedToBuyMicroserviceTest
{
    public class ControllerTests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void AddProductToCart_ValidInput_ReturnsOkResult()
        {
            int CustomerId = 1;
            int ProductId = 1;
            int Zipode = 273001;
            
            var mock = new Mock<ProceedToBuy>();
            ProceedToBuyController ptbm = new ProceedToBuyController(mock.Object);
            var data = ptbm.AddProductToCart(CustomerId, ProductId, Zipode);
            var value = data.Result;
            var result = value as OkObjectResult;
            Assert.AreEqual(result.StatusCode, 200);
        }

        [Test]
        public void AddProductToCart_ReturnsNotNull()
        {
            int CustomerId=1;
            int ProductId=2;
            int Zipode=273001;

            var mock = new Mock<ProceedToBuy>();
            ProceedToBuyController ptbm = new ProceedToBuyController(mock.Object);
            var data = ptbm.AddProductToCart(CustomerId, ProductId, Zipode);
            var result = data.Result;
            Assert.IsNotNull(result);            
        }

        [Test]
        public void AddProductToCart_InvalidorNullInput_ReturnsNotFoundResult()
        {
            int CustomerId=1;
            int ProductId=100;
            int Zipode=273001;

            var mock = new Mock<ProceedToBuy>();
            ProceedToBuyController ptbm = new ProceedToBuyController(mock.Object);
            var data = ptbm.AddProductToCart(CustomerId, ProductId, Zipode);
            var value = data.Result;
            var result = value as NotFoundResult;
            Assert.AreEqual(result.StatusCode, 404);
        }

        [Test]
        public void AddProductToWishlist_ValidInput_ReturnsOkResult()
        {
            int CustomerId=1;
            int ProductId=2;

            var mock = new Mock<ProceedToBuy>();
            ProceedToBuyController ptbm = new ProceedToBuyController(mock.Object);
            var data = ptbm.AddProductToWishlist(CustomerId, ProductId);
            var value = data.Result;
            var result = value as OkObjectResult;
            Assert.AreEqual(result.StatusCode, 200);
        }

        [Test]
        public void AddProductToWishlist_ReturnsNotNull()
        {
            int CustomerId=1;
            int ProductId=2;

            var mock = new Mock<ProceedToBuy>();
            ProceedToBuyController ptbm = new ProceedToBuyController(mock.Object);
            var data = ptbm.AddProductToWishlist(CustomerId, ProductId);
            var res = data.Result;
            Assert.IsNotNull(res);
        }

        [Test]
        public void AddProductToWishlist_InvalidorNullInput_ReturnsNotFoundResult()
        {
            int CustomerId=1;
            int ProductId=100;

            var mock = new Mock<ProceedToBuy>();
            ProceedToBuyController ptbm = new ProceedToBuyController(mock.Object);
            var data = ptbm.AddProductToWishlist(CustomerId, ProductId);
            var res = data.Result;
            var s = res as NotFoundResult;
            Assert.AreEqual(s.StatusCode, 404);
        }
    }
}