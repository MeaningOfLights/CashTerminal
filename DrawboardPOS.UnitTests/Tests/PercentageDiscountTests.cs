using Xunit;
using System;
using System.Linq;
using DrawboardPOS.Discounts;
using DrawboardPOS.Domain;
using DrawboardPOS.Enums;
using DrawboardPOS.UnitTests.Helpers;
namespace DrawboardPOS.UnitTests
{

    
    public class PercentageDiscountTests
    {
        [Fact]
        public void PercentageDiscount_DiscountedItems_IsNull_ExceptionThrown()
        {
            // Arrange + Act + Assert
            Assert.Throws<ArgumentNullException>(() => new PercentageDiscount(null, 0.10m));
        }

        [Fact]
        public void PercentageDiscount_NoDiscountApplied_With10PercentDiscount()
        {
            // Arrange
            var percentageDiscount = new PercentageDiscount(new DiscountedProduct
            {
                ProductId = 4,
                Name = "D"
            }, 0.10m);

            // Act
            var result = percentageDiscount.DiscountsApplicable(ProductQuantityHelper.CreateProducts()).ToArray();

            // Assert
            Assert.Equal(result.Any(), false);            
        }

        [Fact]
        public void PercentageDiscount_CalculateAppliedDiscount_With10PercentDiscount()
        {
            // Arrange
            var percentageDiscount = new PercentageDiscount(DiscountHelper.CreateDiscountedProducts(), 0.10m);            

            // Act
            var result = percentageDiscount.DiscountsApplicable(ProductQuantityHelper.CreateProducts()).ToArray();

            // Assert
            Assert.Equal(result.Any(), true);
            Assert.Equal(result[0].Type, DiscountType.Percentage);
            Assert.Equal(result[0].Amount, 0.03m);
            Assert.Equal(result[0].Text, "A 10% OFF: - $3");
        }

        [Fact]
        public void PercentageDiscount_CalculateAppliedDiscount_With50PercentDiscount()
        {
            // Arrange
            var percentageDiscount = new PercentageDiscount(DiscountHelper.CreateDiscountedProducts(), 0.50m);

            // Act
            var result = percentageDiscount.DiscountsApplicable(ProductQuantityHelper.CreateProducts()).ToArray();

            // Assert
            Assert.Equal(result.Any(), true);
            Assert.Equal(result[0].Type, DiscountType.Percentage);
            Assert.Equal(result[0].Amount, 0.16m);
            Assert.Equal(result[0].Text, "A 50% OFF: - 16cents");
        }
    }
}
