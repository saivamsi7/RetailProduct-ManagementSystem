using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using VendorMicroservice.Controllers;
using VendorMicroservice.Models;
using VendorMicroservice.Repository;

namespace VendorMicroServiceTest
{
    public class Tests
    {
        List<Vendor> vend;
        List<VendorStock> vendstock;

        Vendor vendor = new Vendor { VendorId = 111, VendorName = "Amit", DeliveryCharge = 2150, Rating = 4, ExpectedDateOfDelivery = 7 };
        VendorStock stock = new VendorStock { ProductId = 1, VendorId = 111, StockInHand = 0, ExpectedStockReplenishmentDate = "2020-11-22" };

        [SetUp]
        public void Setup()
        {
            vend = new List<Vendor>()
            {
            new Vendor{VendorId = 112,VendorName="Aman",DeliveryCharge=2650,Rating=1,ExpectedDateOfDelivery=15},
            new Vendor{VendorId = 113,VendorName="Vikas",DeliveryCharge=2950,Rating=3,ExpectedDateOfDelivery=8},
            new Vendor{VendorId = 114,VendorName="Manoj",DeliveryCharge=1120,Rating=5,ExpectedDateOfDelivery=6},
            new Vendor{VendorId = 115,VendorName="Sahil",DeliveryCharge=1990,Rating=2,ExpectedDateOfDelivery=9}
            };
            
            vendstock = new List<VendorStock>()
            {
            new VendorStock { ProductId = 1, VendorId = 111, StockInHand = 0, ExpectedStockReplenishmentDate = "2020-11-22" },
            new VendorStock { ProductId = 2, VendorId = 111, StockInHand = 150, ExpectedStockReplenishmentDate = "2021-01-01" },
            new VendorStock { ProductId = 1, VendorId = 112, StockInHand = 76, ExpectedStockReplenishmentDate = "2021-01-14" },
            new VendorStock { ProductId = 2, VendorId = 112, StockInHand = 0, ExpectedStockReplenishmentDate = "2020-12-08" },
            new VendorStock { ProductId = 3, VendorId = 113, StockInHand = 210, ExpectedStockReplenishmentDate = "2020-12-12" },
            new VendorStock { ProductId = 1, VendorId = 113, StockInHand = 202, ExpectedStockReplenishmentDate = "2020-11-25" },
            new VendorStock { ProductId = 3, VendorId = 114, StockInHand = 240, ExpectedStockReplenishmentDate = "2020-11-27" },
            new VendorStock { ProductId = 2, VendorId = 115, StockInHand = 185, ExpectedStockReplenishmentDate = "2020-12-03" }
            };
        }

        [Test]
        public void GetVendorDetails_ValidInput_ReturnsOkRequest()
        {
            int ProductId = 1;

            var mock = new Mock<VendorRepository>();
            VendorController obj = new VendorController(mock.Object);
            var data = obj.GetVendorDetails(ProductId);
            var result = data as ObjectResult;
            Assert.AreEqual(200, result.StatusCode);
        }

        [Test]
        public void GetVendorDetails_InvalidInput_ReturnsNotFoundResult()
        {
            int ProductId = 0;

            var mock = new Mock<VendorRepository>();
            VendorController obj = new VendorController(mock.Object);
            var data = obj.GetVendorDetails(ProductId);
            var result = data as NotFoundResult;
            Assert.AreEqual(404, result.StatusCode);
        }

        [Test]
        public void GetVendorDetails_ReturnsNotNullList()
        {
            int ProductId = 1;

            var mock = new Mock<VendorRepository>();
            VendorController obj = new VendorController(mock.Object);
            var data = obj.GetVendorDetails(ProductId);
            Assert.IsNotNull(data);
        }

        [Test]
        public void GetAllVendorDetails_ValidInput_ReturnsOkRequest()
        {
            int ProductId = 1;

            var mock = new Mock<VendorRepository>();
            VendorController obj = new VendorController(mock.Object);
            var data = obj.GetAllVendorDetails(ProductId);
            var result = data as ObjectResult;
            Assert.AreEqual(200, result.StatusCode);
        }

        [Test]
        public void GetAllVendorDetails_InvalidInput_ReturnsNotFoundResult()
        {
            int ProductId = 0;

            var mock = new Mock<VendorRepository>();
            VendorController obj = new VendorController(mock.Object);
            var data = obj.GetAllVendorDetails(ProductId);
            var result = data as NotFoundResult;
            Assert.AreEqual(404, result.StatusCode);
        }

        [Test]
        public void GetAllVendorDetails_ReturnsNotNullList()
        {
            int ProductId = 1;

            var mock = new Mock<VendorRepository>();
            VendorController obj = new VendorController(mock.Object);
            var data = obj.GetAllVendorDetails(ProductId);
            Assert.IsNotNull(data);
        }
    }
}