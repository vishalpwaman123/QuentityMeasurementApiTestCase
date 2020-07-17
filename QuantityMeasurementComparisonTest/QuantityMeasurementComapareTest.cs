using BusinessLayer.Interface;
using BusinessLayer.Services;
using CommanLayer;
using CommanLayer.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QuantityMeasurementApi.Controllers;
using RepositoryLayer.Interface;
using RepositoryLayer.Services;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace QuantityMeasurementConvertTest
{
    public class QuantityMeasurementComapareTest
    {

        QuantityMeasurementController quantityMeasurementController;
        QuantityMeasurementInterfaceBusiness quantityMeasurementInterfaceBusiness;
        QuantityMeasurementInterfaceRepository quantityMeasurementRepository;

        public static DbContextOptions<ApplicationDbContext> Comparisons { get; }

        public static string sqlConnectionString = "Data Source=DESKTOP-OF8D1IH;Initial Catalog=QuantityMeasurementDb;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

        static QuantityMeasurementComapareTest()
        {
            Comparisons = new DbContextOptionsBuilder<ApplicationDbContext>().UseSqlServer(sqlConnectionString).Options;
        }

        public QuantityMeasurementComapareTest()
        {

            var dbContext = new ApplicationDbContext(Comparisons);
            this.quantityMeasurementRepository = new QuantityMeasurementRepository(dbContext);
            this.quantityMeasurementInterfaceBusiness = new QuantityMeasurementBusiness(this.quantityMeasurementRepository);
            this.quantityMeasurementController = new QuantityMeasurementController(this.quantityMeasurementInterfaceBusiness);

        }

        [Fact]
        public void GivenTwoFeetToInchAnd24Inch_WhenComparingBoth_ReturnsResult()
        {
            try
            {
                Compare result = new Compare
                {
                    MeasurementType = "length",
                    FirstValueUnit = "FeetToInch",
                    FirstValue = 2,
                    SecondValueUnit = "Inch",
                    SecondValue = 24
                };
                var okResult = quantityMeasurementController.CompareAndAdd(result);
                Assert.IsType<OkObjectResult>(okResult);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }


    }
}
