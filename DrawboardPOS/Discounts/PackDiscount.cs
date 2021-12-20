using DrawboardPOS.Enums;
using DrawboardPOS.Helpers;

namespace DrawboardPOS.Discounts
{
    public class PacksDiscount : IDiscount
    {
        private readonly ProductQuantity _productsThatQualifyBasketforDiscount;
        private readonly DiscountedProduct _discountProduct;

        public PacksDiscount(ProductQuantity productsThatQualifyBasketforDiscount, DiscountedProduct discountProduct)
        {            
            _productsThatQualifyBasketforDiscount = productsThatQualifyBasketforDiscount ?? throw new ArgumentNullException(nameof(productsThatQualifyBasketforDiscount));
            _discountProduct = discountProduct ?? throw new ArgumentNullException(nameof(discountProduct));
        }
        private decimal ApplyDiscount(ProductQuantity item) => Math.Round((item.Product.UnitPrice * item.Quantity) - item.Product.PackPrice, 2);

        public IEnumerable<AppliedDiscount> DiscountsApplicable(IEnumerable<ProductQuantity> items)
        {
            var itemsArray = items as ProductQuantity[] ?? items.ToArray();

            var discountsApplied = new List<AppliedDiscount>();

            foreach (var item in itemsArray)
            {
                if (item.Product.ProductId == _productsThatQualifyBasketforDiscount.Product.ProductId && item.Quantity >= _productsThatQualifyBasketforDiscount.Quantity)
                {
                    var packItems = itemsArray
                        .Where(packItem => packItem.Product.ProductId == _discountProduct.ProductId)
                        .ToArray();

                    if (packItems.Length > 0)
                    {
                        var discount = ApplyDiscount(_productsThatQualifyBasketforDiscount);

                        discountsApplied.Add(new AppliedDiscount
                        {
                            Type = DiscountType.Packs,                           
                            Text = $"{_productsThatQualifyBasketforDiscount.Quantity} @ {_discountProduct.Name}'s for {_productsThatQualifyBasketforDiscount.Product.PackPrice.ToCurrencyString()}",
                            Amount = discount
                        });
                    }
                }
            }

            return discountsApplied;
        }
    }
}
