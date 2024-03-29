﻿namespace DrawboardPOS.UnitTests.Helpers
{
    using System.Collections.Generic;
    using DrawboardPOS.Builders;
    using DrawboardPOS.Domain;

    public class ProductQuantityHelper
    {
        public static IEnumerable<ProductQuantity> CreateProducts(int numberOfProductsToCreate)
        {
            var products = new List<ProductQuantity>();

            var productBuilder = new ProductBuilder();

            for (int i = 0; i < numberOfProductsToCreate; i++)
            {
                products.Add(new ProductQuantity
                {
                    Product = productBuilder.Create("TestProduct")
                                            .WithProductId(i)
                                            .WithUnitPrice(1.00m),
                    Quantity = 1
                });
            }

            return products;
        }

        public static IEnumerable<ProductQuantity> CreateProducts()
        {
            var productBuilder = new ProductBuilder();

            return new List<ProductQuantity>
            {
                new ProductQuantity
                {
                    Product = productBuilder.Create("A")
                                            .WithProductId(1)
                                            .WithUnitPrice(0.33m),
                    Quantity = 1
                },
                new ProductQuantity
                {
                    Product = productBuilder.Create("B")
                                            .WithProductId(2)
                                            .WithUnitPrice(0.65m),
                    Quantity = 2
                },
                new ProductQuantity
                {
                    Product = productBuilder.Create("C")
                                            .WithProductId(3)
                                            .WithUnitPrice(0.80m),
                    Quantity = 2
                }
            };
        }

        public static IEnumerable<ProductQuantity> CreateProductsForPacksDiscounts()
        {
            var productBuilder = new ProductBuilder();

            return new List<ProductQuantity>
            {
                new ProductQuantity
                {
                    Product = productBuilder.Create("A")
                                            .WithProductId(1)
                                            .WithUnitPrice(0.33m)
                                            .WithPackPrice(.50m),
                    Quantity = 3
                },
                new ProductQuantity
                {
                    Product = productBuilder.Create("B")
                                            .WithProductId(2)
                                            .WithUnitPrice(0.65m),
                    Quantity = 4
                },
                new ProductQuantity
                {
                    Product = productBuilder.Create("C")
                                            .WithProductId(3)
                                            .WithUnitPrice(0.80m),
                    Quantity = 2
                }
            };
        }
    }
}
