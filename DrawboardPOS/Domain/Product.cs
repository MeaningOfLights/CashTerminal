namespace DrawboardPOS.Domain
{
    public class Product
    {
        public int ProductId { get; set; }

        public string Name { get; set; }

        public decimal UnitPrice { get; set; }

        public decimal PackPrice { get; set; }
    }
}
