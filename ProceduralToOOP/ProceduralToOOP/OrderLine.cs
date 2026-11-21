using System.Globalization;

namespace OOP_Assignmet_1
{
    internal class OrderLine
    {
        public Product Product { get; private set; }
        public int Quantity { get; private set; }
        // Price is captured when the line is created, so later price changes don't rewrite history.
        public decimal UnitPrice { get; private set; }
        public decimal LineTotal => UnitPrice * Quantity;

        public OrderLine(Product product, int quantity)
        {
            ArgumentNullException.ThrowIfNull(product);
            if (quantity <= 0)
                throw new ArgumentException("Quantity must be positive.", nameof(quantity));

            Product = product;
            Quantity = quantity;
            UnitPrice = product.Price;
        }

        public override string ToString()
        {
            return string.Create(CultureInfo.InvariantCulture,
                $"{Product.Name}  x{Quantity}  @{UnitPrice:F2}  = {LineTotal:F2}");
        }
    }
}
