global using DrawboardPOS.Domain;
using DrawboardPOS.Helpers;
namespace DrawboardPOS
{
    public class Program
    {
        private static void Main(string[] args)
        {
            // The products and discounts would be read from a config or database so not to change the code 
            // everytime a discount is added/amended/removed
            var discounts = Utilities.CreateDiscounts();


            while (true)
            {
                Console.WriteLine("Point of Sale Terminal");
                Console.WriteLine("Please Enter Products, eg: ABCD");
                Console.WriteLine("==========================================");

                var inputLine = Console.ReadLine();
                inputLine = inputLine?.Replace(" ", "");

                if (string.IsNullOrEmpty(inputLine))
                {
                    Console.WriteLine("No products passed in!");

                    Console.WriteLine("Press <Enter> to exit...");
                    if (Console.ReadKey().Key == ConsoleKey.Enter)
                        break;

                    continue;
                }
                else if (!string.IsNullOrEmpty(inputLine.Replace("A","").Replace("B","").Replace("C","").Replace("D","")))
                {
                    Console.WriteLine($"Invalid product passed in {inputLine}. Please only use Products A, B, C, D.");

                    Console.WriteLine("Press <Enter> to exit...");
                    if (Console.ReadKey().Key == ConsoleKey.Enter)
                        break;

                    continue;
                }

                var products = new List<string>();

                foreach (var arg in inputLine)
                {
                    products.Add(arg.ToString().ToUpper());
                }

                var posTerminal = new PointOfSaleTerminal(discounts);

                posTerminal.AddProducts(Utilities.CreateProducts(products));

                var subTotal = posTerminal.SubTotal;

                var discountsApplied = posTerminal.GetBasketDiscounts().ToArray();

                var totalPrice = subTotal - discountsApplied.Sum(item => item.Amount);

                Console.WriteLine("SubTotal : " + $"{subTotal.ToCurrencyString()}");

                foreach (var discount in discountsApplied)
                {
                    Console.WriteLine(discount.Text);
                }

                Console.WriteLine("Total Price : " + $"{totalPrice.ToCurrencyString()}");
                Console.ReadLine();

            }
        }
    }
}
