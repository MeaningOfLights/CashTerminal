using Xunit;
using System;
using System.Linq;
using DrawboardPOS.Domain;
using DrawboardPOS.Enums;
using DrawboardPOS.UnitTests.Helpers;
using DrawboardPOS.Discounts;

namespace DrawboardPOS.UnitTests
{
    public class PacksDiscountTests
    {
        [Fact]
        public void PacksDiscount_ProductsThatQualifyBasketforDiscount_IsNull_ExceptionThrown()
        {
            // Arrange + Act + Assert
            Assert.Throws<ArgumentNullException>(() => new PacksDiscount(null, new DiscountedProduct()));
        }

        [Fact]
        public void PacksDiscount_DiscountedProduct_IsNull_ExceptionThrown()
        {
            // Arrange + Act + Assert
            Assert.Throws<ArgumentNullException>(() => new PacksDiscount(new ProductQuantity(), null));
        }
        
        [Fact]
        public void PacksDiscount_CalculateAppliedDiscount_WithQuantity_3rMore()
        {
            // Arrange
            var percentageDiscount = new PacksDiscount(new ProductQuantity
                                                            {
                                                                Product = new Product
                                                                {
                                                                    ProductId = 1,
                                                                    Name = "A",
                                                                    UnitPrice= 0.50m,
                                                                    PackPrice = 0.50m
                                                                },
                                                                Quantity = 3

                                                            },
                                                            new DiscountedProduct()
                                                            {
                                                                ProductId = 1,
                                                                Name = "A"
                                                            });
  
            // Act
            var result = percentageDiscount.DiscountsApplicable(ProductQuantityHelper.CreateProductsForPacksDiscounts()).ToArray();

            // Assert
            Assert.Equal(result.Any(), true);
            Assert.Equal(result[0].Type, DiscountType.Packs);
            Assert.Equal(result[0].Amount, 1.00m);
            Assert.Equal(result[0].Text, "3 @ A's for 50cents");
        }

        [Fact]
        public void PacksDiscount_CalculateAppliedDiscount_WithQuantity_4rMore()
        {
            // Arrange
            var percentageDiscount = new PacksDiscount(new ProductQuantity
                {
                    Product = new Product
                    {
                        ProductId = 2,
                        Name = "C",
                        UnitPrice = 0.50m,
                        PackPrice = 1.50m
                    },
                    Quantity = 4

                },
                new DiscountedProduct()
                {
                    ProductId = 3,
                    Name = "C"
                });

            // Act
            var result = percentageDiscount.DiscountsApplicable(ProductQuantityHelper.CreateProductsForPacksDiscounts()).ToArray();

            // Assert
            Assert.Equal(result.Any(), true);
            Assert.Equal(result[0].Type, DiscountType.Packs);
            Assert.Equal(result[0].Amount, 0.50m);
            Assert.Equal(result[0].Text, "4 @ C's for $1.50");
        }

        [Fact]
        public void PacksDiscount_NoDiscountApplied()
        {
            // Arrange
            var percentageDiscount = new PacksDiscount(new ProductQuantity
                {
                    Product = new Product
                    {
                        ProductId = 2,
                        Name = "B"
                    },
                    Quantity = 2

                },
                new DiscountedProduct()
                {
                    ProductId = 4,
                    Name = "D"
                });

            // Act
            var result = percentageDiscount.DiscountsApplicable(ProductQuantityHelper.CreateProducts()).ToArray();

            // Assert
            Assert.Equal(result.Any(), false);
        }
    }
}
