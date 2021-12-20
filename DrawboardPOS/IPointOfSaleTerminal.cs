namespace DrawboardPOS
{
    using System.Collections.Generic;
    using Domain;

    public interface IPointOfSaleTerminal
    {
        void AddProducts(IEnumerable<ProductQuantity> items);
        int ProductCount { get; }
        decimal SubTotal { get; }
        IEnumerable<AppliedDiscount> GetBasketDiscounts();
    }
}
