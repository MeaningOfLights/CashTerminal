using Xunit;
using System;
using System.Collections.Generic;
using System.Linq;
using Moq;
using DrawboardPOS.Discounts;
using DrawboardPOS.Domain;
using DrawboardPOS.Enums;
using DrawboardPOS.UnitTests.Helpers;

namespace DrawboardPOS.UnitTests
{    
    public class PointOfSaleTerminalTests
    {
        [Fact]
        public void PointOfSaleTerminal_Discounts_IsNull_ExceptionThrown()
        {
            // Arrange + Act + Assert
            Assert.Throws<ArgumentNullException>(() => new PointOfSaleTerminal(null));
        }

        [Fact]
        public void PointOfSaleTerminal_AddProducts_CheckProductsCounts()
        {
            PointOfSaleTerminal_AddProducts_CheckProductsCount(2);
            PointOfSaleTerminal_AddProducts_CheckProductsCount(8);
            PointOfSaleTerminal_AddProducts_CheckProductsCount(9);
        }

        private void PointOfSaleTerminal_AddProducts_CheckProductsCount(int productsToCreate)
        {
            // Arrange             
            var PointOfSaleTerminal = new PointOfSaleTerminal(new List<IDiscount>());
            PointOfSaleTerminal.AddProducts(ProductQuantityHelper.CreateProducts(productsToCreate));

            // Act
            var result = PointOfSaleTerminal.ProductCount;

            // Assert
            Assert.Equal(result, productsToCreate);   
        }

        [Fact]        
        public void PointOfSaleTerminal_CheckCalculateSubTotal_WithNoDiscounts()
        {
            // Arrange             
            var PointOfSaleTerminal = new PointOfSaleTerminal(new List<IDiscount>());
            PointOfSaleTerminal.AddProducts(ProductQuantityHelper.CreateProducts());

            // Act
            var result = PointOfSaleTerminal.SubTotal;

            // Assert
            Assert.Equal(result, 3.23m);
        }

        [Fact]
        public void PointOfSaleTerminal_CheckCalculateTotalPrice_WithNoDiscounts()
        {
            // Arrange             
            var PointOfSaleTerminal = new PointOfSaleTerminal(new List<IDiscount>());
            PointOfSaleTerminal.AddProducts(ProductQuantityHelper.CreateProducts());

            // Act
            var result = PointOfSaleTerminal.SubTotal - PointOfSaleTerminal.GetBasketDiscounts().Sum(item => item.Amount);

            // Assert
            Assert.Equal(result, 3.23m);
        }

        [Fact]
        public void PointOfSaleTerminal_CheckCalculateTotalPrice_WithPercentageDiscount()
        {
            // Arrange
            var percentageDiscount = new Mock<IDiscount>();

            percentageDiscount.Setup(mock => mock.DiscountsApplicable(It.IsAny<IEnumerable<ProductQuantity>>()))
                .Returns(DiscountHelper.CreatePercentageAppliedDiscount());

            var PointOfSaleTerminal = new PointOfSaleTerminal(new List<IDiscount>{ percentageDiscount.Object});            
            PointOfSaleTerminal.AddProducts(ProductQuantityHelper.CreateProducts());

            // Act
            var discountsTotal = PointOfSaleTerminal.GetBasketDiscounts().Sum(item => item.Amount);
            var result = PointOfSaleTerminal.SubTotal - discountsTotal;

            // Assert
            Assert.Equal(result, 3.20m);
        }

        [Fact]
        public void PointOfSaleTerminal_CheckCalculateTotalPrice_WithHalfPriceDiscount()
        {
            // Arrange         
            var halfPriceDiscount = new Mock<IDiscount>();

            halfPriceDiscount.Setup(mock => mock.DiscountsApplicable(It.IsAny<IEnumerable<ProductQuantity>>()))
                .Returns(DiscountHelper.CreateHalfPriceAppliedDiscount);

            var PointOfSaleTerminal = new PointOfSaleTerminal(new List<IDiscount> { halfPriceDiscount.Object });
            PointOfSaleTerminal.AddProducts(ProductQuantityHelper.CreateProducts());

            // Act
            var discountsTotal = PointOfSaleTerminal.GetBasketDiscounts().Sum(item => item.Amount);
            var result = PointOfSaleTerminal.SubTotal - discountsTotal;

            // Assert
            Assert.Equal(result, 2.83m);
        }

        [Fact]
        public void PointOfSaleTerminal_CheckCalculateTotalPrice_WitMultipleDiscounts()
        {
            // Arrange    
            var multlpleDiscounts = new Mock<IDiscount>();

            multlpleDiscounts.Setup(mock => mock.DiscountsApplicable(It.IsAny<IEnumerable<ProductQuantity>>()))
                .Returns(DiscountHelper.CreateMultipleAppliedDiscounts());

            var PointOfSaleTerminal = new PointOfSaleTerminal(new List<IDiscount> { multlpleDiscounts.Object });
            PointOfSaleTerminal.AddProducts(ProductQuantityHelper.CreateProducts());

            // Act
            var discountsTotal = PointOfSaleTerminal.GetBasketDiscounts().Sum(item => item.Amount);
            var result = PointOfSaleTerminal.SubTotal - discountsTotal;

            // Assert
            Assert.Equal(result, 2.80m);
        }

        [Fact]
        public void PointOfSaleTerminal_GetBasketDiscounts_WithNoDiscountApplied()
        {
            // Arrange
            var discounts = new Mock<IDiscount>();

            discounts.Setup(mock => mock.DiscountsApplicable(It.IsAny<IEnumerable<ProductQuantity>>()))
                .Returns(DiscountHelper.CreateNoDiscountApplied);

            var PointOfSaleTerminal = new PointOfSaleTerminal(new List<IDiscount> { discounts.Object });
            PointOfSaleTerminal.AddProducts(ProductQuantityHelper.CreateProducts());

            // Act
            var result = PointOfSaleTerminal.GetBasketDiscounts().ToArray();
            
            // Assert
            Assert.Equal(result.Any(), true);
            Assert.Equal(result[0].Type, DiscountType.None);
            Assert.Equal(result[0].Amount, 0.00m);
            Assert.Equal(result[0].Text, "(No offers available)");
        }
    }
}
