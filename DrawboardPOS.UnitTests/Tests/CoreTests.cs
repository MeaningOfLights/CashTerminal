using DrawboardPOS.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace DrawboardPOS.UnitTests.Tests
{
    public class CoreTests
    {

        [Fact]
        public void Should_Work_ABCDABA()
        {
            // Arrange
            var discounts = Utilities.CreateDiscounts();
            var posTerminal = new PointOfSaleTerminal(discounts);
            posTerminal.AddProducts(Utilities.CreateProducts(new List<string> { "A", "B", "C", "D", "A", "B", "A", }));

            // Act
            var subTotal = posTerminal.SubTotal;
            var discountsApplied = posTerminal.GetBasketDiscounts().ToArray();
            var totalPrice = subTotal - discountsApplied.Sum(item => item.Amount);

            // Assert
            Assert.Equal(totalPrice, 13.25m);
        }


        [Fact]
        public void Should_Work_CCCCCCC()
        {
            // Arrange
            var discounts = Utilities.CreateDiscounts();
            var posTerminal = new PointOfSaleTerminal(discounts);
            posTerminal.AddProducts(Utilities.CreateProducts(new List<string> { "C", "C", "C", "C", "C", "C", "C", }));

            // Act
            var subTotal = posTerminal.SubTotal;
            var discountsApplied = posTerminal.GetBasketDiscounts().ToArray();
            var totalPrice = subTotal - discountsApplied.Sum(item => item.Amount);

            // Assert
            Assert.Equal(totalPrice, 6m);
        }

        [Fact]
        public void Should_Work_ABCD()
        {
            // Arrange
            var discounts = Utilities.CreateDiscounts();
            var posTerminal = new PointOfSaleTerminal(discounts);
            posTerminal.AddProducts(Utilities.CreateProducts(new List<string> { "A", "B", "C", "D" }));

            // Act
            var subTotal = posTerminal.SubTotal;
            var discountsApplied = posTerminal.GetBasketDiscounts().ToArray();
            var totalPrice = subTotal - discountsApplied.Sum(item => item.Amount);

            // Assert
            Assert.Equal(totalPrice, 7.25m);
        }

    }
}
