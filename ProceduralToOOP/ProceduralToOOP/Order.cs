using System.Globalization;
using System.Text;

namespace OOP_Assignmet_1
{
    internal class Order
    {
        public const int MaxLines = 20;

        private readonly List<OrderLine> _lines = new();

        public int Id { get; private set; }
        public Customer Customer { get; private set; }
        public DateTime Date { get; private set; }
        public bool IsPaid { get; private set; }
        public IReadOnlyList<OrderLine> Lines => _lines;

        public decimal Total
        {
            get
            {
                decimal subtotal = _lines.Sum(l => l.LineTotal);
                return Customer.ApplyDiscount(subtotal);
            }
        }

        public Order(int id, Customer customer, DateTime date)
        {
            if (id <= 0)
                throw new ArgumentException("Id must be a positive integer.", nameof(id));
            ArgumentNullException.ThrowIfNull(customer);

            Id = id;
            Customer = customer;
            Date = date;
        }

        public void AddLine(Product product, int quantity)
        {
            if (IsPaid)
                throw new InvalidOperationException("Cannot change a paid order.");
            if (_lines.Count >= MaxLines)
                throw new InvalidOperationException("Order has too many lines.");

            var line = new OrderLine(product, quantity); // validates quantity
            product.RemoveStock(quantity);               // validates stock; throws before the line is added
            _lines.Add(line);
        }

        public void MarkPaid()
        {
            if (_lines.Count == 0)
                throw new InvalidOperationException("Cannot pay an empty order.");
            IsPaid = true;
        }

        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.AppendLine($"=== ORDER #{Id} ===");
            sb.AppendLine("Date: " + Date.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture));
            sb.AppendLine($"Customer: {Customer.Name} (#{Customer.Id})");
            sb.AppendLine($"Paid: {(IsPaid ? "yes" : "no")}");
            sb.AppendLine("Lines:");
            foreach (var line in _lines)
                sb.AppendLine($"  - {line}");
            sb.Append(string.Create(CultureInfo.InvariantCulture, $"TOTAL: {Total:F2}"));
            return sb.ToString();
        }
    }
}
