using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using ProductMicroservice.Controllers;
using ProductMicroservice.Models;
using ProductMicroservice.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ProductTesting
{
    public class Tests
    {
        List<Product> prod;

        int success = 1;
        int failure = 0;

        [SetUp]
        public void Setup()
        {
            prod = new List<Product>()
            {
                new Product{ ProductId = 6, ProductName = "Product 6", Description = "This is Product 6", ImageName = "product6.jpg", Price = 4999, Rating = 4},
                new Product{ ProductId = 7, ProductName = "Product 7", Description = "This is Product 7", ImageName = "product7.jpg", Price = 2250, Rating = 3},
                new Product{ ProductId = 8, ProductName = "Product 8", Description = "This is Product 8", ImageName = "product8.jpg", Price = 6900, Rating = 5}
            };
        }

        [Test]
        public void GetAllProducts_ReturnsOkRequest()
        {
            var mock = new Mock<ProductRepository>();

            ProductController obj = new ProductController(mock.Object);

            var data = obj.GetAllProducts();

            var res = data as ObjectResult;

            Assert.AreEqual(200, res.StatusCode);
        }

        [Test]
        public void GetAllProducts_ReturnsNotNullList()
        {
            var mock = new Mock<ProductRepository>();

            ProductController obj = new ProductController(mock.Object);

            var data = obj.GetAllProducts();

            Assert.IsNotNull(data);
        }

        [Test]
        public void SearchProductById_ValidInput_ReturnsOkRequest()
        {
            int id = 8;

            var mock = new Mock<IProductRepository>();

            mock.Setup(x => x.SearchProductById(id)).Returns((prod.Where(x => x.ProductId == id)).FirstOrDefault());

            ProductController obj = new ProductController(mock.Object);

            var data = obj.SearchProductById(id);

            var res = data as ObjectResult;

            Assert.AreEqual(200, res.StatusCode);
        }

        [Test]
        public void SearchProductById_InvalidInput_ReturnsNotFoundResult()
        {
            int id = 9;

            var mock = new Mock<IProductRepository>();

            mock.Setup(x => x.SearchProductById(id)).Returns((prod.Where(x => x.ProductId == id)).FirstOrDefault());

            ProductController obj = new ProductController(mock.Object);

            var data = obj.SearchProductById(id);

            var res = data as NotFoundResult;

            Assert.AreEqual(404, res.StatusCode);
        }

        [Test]
        public void SearchProductById_ReturnsNotNullList()
        {
            int id = 6;

            var mock = new Mock<IProductRepository>();

            mock.Setup(x => x.SearchProductById(id)).Returns((prod.Where(x => x.ProductId == id)).FirstOrDefault());

            ProductController obj = new ProductController(mock.Object);

            var data = obj.SearchProductById(id);

            Assert.IsNotNull(data);
        }

        [Test]
        public void SearchProductByName_ValidInput_ReturnsOkRequest()
        {
            string name = "Product 8";

            var mock = new Mock<IProductRepository>();

            mock.Setup(x => x.SearchProductByName(name)).Returns((prod.Where(x => x.ProductName == name)).FirstOrDefault());

            ProductController obj = new ProductController(mock.Object);

            var data = obj.SearchProductByName(name);

            var res = data as ObjectResult;

            Assert.AreEqual(200, res.StatusCode);
        }

        [Test]
        public void SearchProductByName_InvalidInput_ReturnsNotFoundResult()
        {
            string name = "Product 9";

            var mock = new Mock<IProductRepository>();

            mock.Setup(x => x.SearchProductByName(name)).Returns((prod.Where(x => x.ProductName == name)).FirstOrDefault());

            ProductController obj = new ProductController(mock.Object);

            var data = obj.SearchProductByName(name);

            var res = data as NotFoundResult;

            Assert.AreEqual(404, res.StatusCode);
        }

        [Test]
        public void SearchProductByName_ReturnsNotNullList()
        {
            string name = "Product 6";

            var mock = new Mock<IProductRepository>();

            mock.Setup(x => x.SearchProductByName(name)).Returns((prod.Where(x => x.ProductName == name)).FirstOrDefault());

            ProductController obj = new ProductController(mock.Object);

            var data = obj.SearchProductByName(name);

            Assert.IsNotNull(data);
        }

        [Test]
        public void AddProductRating_ValidInput_ReturnsOkRequest()
        {
            int id = 8;
            double rating = 4;

            var mock = new Mock<IProductRepository>();

            mock.Setup(x => x.AddProductRating(id, rating)).Returns(success);

            ProductController obj = new ProductController(mock.Object);

            var data = obj.AddProductRating(id, rating);

            var res = data as OkResult;

            Assert.AreEqual(200, res.StatusCode);
        }

        [Test]
        public void AddProductRating_InvalidInput_ReturnsNotFoundResult()
        {
            int id = 9;
            double rating = 4;

            var mock = new Mock<IProductRepository>();

            mock.Setup(x => x.AddProductRating(id, rating)).Returns(failure);

            ProductController obj = new ProductController(mock.Object);

            var data = obj.AddProductRating(id, rating);

            var res = data as NotFoundResult;

            Assert.AreEqual(404, res.StatusCode);
        }

        [Test]
        public void AddProductRating_ReturnsNotNull()
        {
            int id = 6;
            double rating = 3;

            var mock = new Mock<IProductRepository>();

            mock.Setup(x => x.AddProductRating(id, rating)).Returns(success);

            ProductController obj = new ProductController(mock.Object);

            var data = obj.AddProductRating(id, rating);

            Assert.IsNotNull(data);
        }
    }
}