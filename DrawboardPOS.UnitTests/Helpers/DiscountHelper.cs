using System.Collections.Generic;
using System.Linq;
using DrawboardPOS.Builders;
using DrawboardPOS.Discounts;
using DrawboardPOS.Domain;
using DrawboardPOS.Enums;

namespace DrawboardPOS.UnitTests.Helpers
{
    public class DiscountHelper
    {
        public static IEnumerable<IDiscount> CreatePercentageDiscount()
        {
            return new List<IDiscount>
            {
                new PercentageDiscount(new DiscountedProduct
                                        {
                                            ProductId = 1,
                                            Name = "Apples"
                                        },
                                        0.10m)
            };
        }

        public static IEnumerable<IDiscount> CreateMultipleDiscounts()
        {
            var productBuilder = new ProductBuilder();

            return new List<IDiscount>
            {
               
                new PacksDiscount(
                    new ProductQuantity
                    {
                        Product = productBuilder.Create("A")
                                            .WithProductId(1),
                        Quantity = 3

                    },
                    new DiscountedProduct()
                    {
                        ProductId = 1,
                        Name = "A"
                    }),
                 new PercentageDiscount(
                        new DiscountedProduct
                        {
                            ProductId = 1,
                            Name = "B"
                        },
                    0.10m)

            };
        }

        public static IEnumerable<AppliedDiscount> CreatePercentageAppliedDiscount()
        {
            return new List<AppliedDiscount>
            {
                new AppliedDiscount
                {
                    Type = DiscountType.Percentage,
                    Text = "Apples 10% OFF: - 3p",
                    Amount = 0.03m
                }
            };
        }

        public static IEnumerable<AppliedDiscount> CreateHalfPriceAppliedDiscount()
        {
            return new List<AppliedDiscount>
            {
                new AppliedDiscount
                {
                    Type = DiscountType.HalfPrice,
                    Text = "Bread 50% OFF: - 40p",
                    Amount = 0.40m
                }
            };
        }

        public static IEnumerable<AppliedDiscount> CreateNoDiscountApplied()
        {
            return new List<AppliedDiscount>
            {
                new AppliedDiscount
                {
                    Type = DiscountType.None,
                    Text = "(No offers available)",
                    Amount = 0.00m
                }
            };
        }

        public static IEnumerable<AppliedDiscount> CreateMultipleAppliedDiscounts()
        {
            return CreatePercentageAppliedDiscount().Concat(CreateHalfPriceAppliedDiscount());
        }

        public static DiscountedProduct CreateDiscountedProducts()
        {
            return new DiscountedProduct
            {
                ProductId = 1,
                Name = "A"
            };
        }
    }
}
