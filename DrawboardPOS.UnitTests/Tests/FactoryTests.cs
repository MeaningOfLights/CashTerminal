using DrawboardPOS.Domain;
using DrawboardPOS.Factories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace DrawboardPOS.UnitTests.Tests
{


    public class FactoryTests
    {

        [Fact]
        public void Factory_ReturnsProducts()
        {
            // Arrange
            Product a = new Product
            {
                ProductId = 1,
                Name = "A",
                UnitPrice = 1.25m
            };
            Product b = new Product
            {
                ProductId = 2,
                Name = "B",
                UnitPrice = 4.25m
            };
            Product c = new Product
            {
                ProductId = 3,
                Name = "C",
                UnitPrice = 1.00m
            };
            Product d = new Product
            {
                ProductId = 4,
                Name = "D",
                UnitPrice = 0.75m
            };

            // Act
            ProductFactory productFactory = new ProductFactory();
            Product A = productFactory.CreateProduct("A");
            Product B = productFactory.CreateProduct("B");
            Product C = productFactory.CreateProduct("C");
            Product D = productFactory.CreateProduct("D");

            // Assert
            Assert.Equal(A, a);
            Assert.Equal(B, b);
            Assert.Equal(C, c);
            Assert.Equal(D, d);
        }
    }
}
