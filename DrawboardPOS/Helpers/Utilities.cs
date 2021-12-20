using DrawboardPOS.Builders;
using DrawboardPOS.Discounts;
using DrawboardPOS.Factories;

namespace DrawboardPOS.Helpers
{

    public static class Utilities
    {
        public static string ToCurrencyString(this decimal value)
        {
            return value < 1 ? $"{(int) (value * 100)}cents" : $"{value:C}";            
        }

        // The products and discounts would be read from a config or database so not to change the code 
        // everytime a discount is added/amended/removed
        public static IEnumerable<IDiscount> CreateDiscounts()
        {
            var productBuilder = new ProductBuilder();

            return new List<IDiscount>
            {
                 new PacksDiscount(
                    new ProductQuantity
                    {
                        Product = productBuilder.Create("A")
                                            .WithProductId(1)
                                            .WithUnitPrice(1.25m)
                                            .WithPackPrice(3),
                        Quantity = 3

                    },
                    new DiscountedProduct()
                    {
                        ProductId = 1,
                        Name = "A"

                    }),
                new PacksDiscount(
                    new ProductQuantity
                    {
                        Product = productBuilder.Create("C")
                                            .WithProductId(3)
                                            .WithUnitPrice(1)
                                            .WithPackPrice(5),
                        Quantity = 6
                        },
                    new DiscountedProduct()
                    {
                        ProductId = 3,
                        Name = "C"
                    })
            };
        }

        public static IEnumerable<ProductQuantity> CreateProducts(IEnumerable<string> products)
        {
            var productsAndQuantities = new List<ProductQuantity>();

            var productFactory = new ProductFactory();

            foreach (var product in products)
            {
                var existProduct = productsAndQuantities.SingleOrDefault(item =>
                    string.Equals(item.Product.Name, product, StringComparison.CurrentCultureIgnoreCase));

                if (existProduct == null)
                {
                    productsAndQuantities.Add(new ProductQuantity
                    {
                        Product = productFactory.CreateProduct(product),
                        Quantity = 1
                    });
                }

                if (existProduct != null)
                    existProduct.Quantity += 1;
            }

            return productsAndQuantities;            
        }
    }
}
