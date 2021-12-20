namespace DrawboardPOS.Discounts
{
    public interface IDiscount
    {
        IEnumerable<AppliedDiscount> DiscountsApplicable(IEnumerable<ProductQuantity> items);
    }
}
