using System.Globalization;

namespace OOP_Assignmet_1
{
    internal class Product
    {
        public int Id { get; private set; }
        public string Name { get; private set; }
        public decimal Price { get; private set; }
        public int Stock { get; private set; }

        public Product(int id, string name, decimal price, int stock)
        {
            if (id <= 0)
                throw new ArgumentException("Id must be a positive integer.", nameof(id));
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Name cannot be null or whitespace.", nameof(name));
            if (price <= 0)
                throw new ArgumentException("Price must be a positive number.", nameof(price));
            if (stock < 0)
                throw new ArgumentException("Stock cannot be negative.", nameof(stock));

            Id = id;
            Name = name;
            Price = price;
            Stock = stock;
        }

        // Stock rules live here, not in whoever happens to need stock.
        public void RemoveStock(int quantity)
        {
            if (quantity <= 0)
                throw new ArgumentException("Quantity must be positive.", nameof(quantity));
            if (Stock < quantity)
                throw new InvalidOperationException($"Not enough stock for product #{Id}.");
            Stock -= quantity;
        }

        public override string ToString()
        {
            return string.Create(CultureInfo.InvariantCulture,
                $"#{Id}  {Name}  price={Price:F2}  stock={Stock}");
        }
    }
}
