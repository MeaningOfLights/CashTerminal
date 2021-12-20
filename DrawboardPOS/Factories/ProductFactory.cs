namespace DrawboardPOS.Factories
{
    public class ProductFactory
    {
        public Product CreateProduct(string productName)
        {
            switch (productName)
            {
                case "A":
                    return new Product
                    {
                        ProductId = 1,
                        Name = "A",
                        UnitPrice = 1.25m
                    };
                case "B":
                    return new Product
                    {
                        ProductId = 2,
                        Name = "B",
                        UnitPrice = 4.25m
                    };
                case "C":
                    return new Product
                    {
                        ProductId = 3,
                        Name = "C",
                        UnitPrice = 1.00m
                    };
                case "D":
                    return new Product
                    {
                        ProductId = 4,
                        Name = "D",
                        UnitPrice = 0.75m
                    };
                default:
                    throw new NotSupportedException($"Unrecognized product name : {productName.ToLower()}");
            }
        }
    }
}
