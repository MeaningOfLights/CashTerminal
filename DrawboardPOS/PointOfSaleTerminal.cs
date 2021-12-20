using DrawboardPOS.Discounts;
using DrawboardPOS.Enums;

namespace DrawboardPOS
{
    public class PointOfSaleTerminal : IPointOfSaleTerminal
    {
        private readonly IEnumerable<IDiscount> _discounts;
        private readonly List<ProductQuantity> _productItems;

        public PointOfSaleTerminal(IEnumerable<IDiscount> discounts)
        {
            _discounts = discounts ?? throw new ArgumentNullException(nameof(discounts));
            _productItems = new List<ProductQuantity>();
        }

        public void AddProducts(IEnumerable<ProductQuantity> productItems)
        {
            _productItems.AddRange(productItems);
        }

        public int ProductCount => _productItems.Count;

        public decimal SubTotal =>  _productItems.Sum(item => item.Product.UnitPrice * item.Quantity);

        public IEnumerable<AppliedDiscount> GetBasketDiscounts()
        {
            var discounts = new List<AppliedDiscount>();

            foreach (var discount in _discounts)
            {
                discounts.AddRange(discount.DiscountsApplicable(_productItems));
            }

            if (!discounts.Any())
                discounts.Add(new AppliedDiscount
                {
                    Type = DiscountType.None,
                    Text = "(No offers available)",
                    Amount = 0.00m
                });           

            return discounts;
        }
    }
}
