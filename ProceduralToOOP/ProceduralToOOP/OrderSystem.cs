namespace OOP_Assignmet_1
{
    // Owns all state (replaces the static Service class and the C++ globals).
    // Create an instance and pass it where it's needed.
    internal class OrderSystem
    {
        private const int MaxCustomers = 50;
        private const int MaxProducts = 50;
        private const int MaxOrders = 100;

        private readonly List<Customer> _customers = new();
        private readonly List<Product> _products = new();
        private readonly List<Order> _orders = new();

        public IReadOnlyList<Customer> Customers => _customers;
        public IReadOnlyList<Product> Products => _products;
        public IReadOnlyList<Order> Orders => _orders;

        public Customer FindCustomer(int id) =>
            _customers.FirstOrDefault(c => c.Id == id)
            ?? throw new InvalidOperationException($"Customer id {id} not found.");

        public Product FindProduct(int id) =>
            _products.FirstOrDefault(p => p.Id == id)
            ?? throw new InvalidOperationException($"Product id {id} not found.");

        public Order FindOrder(int id) =>
            _orders.FirstOrDefault(o => o.Id == id)
            ?? throw new InvalidOperationException($"Order id {id} not found.");

        public Customer AddCustomer(int id, string name, string email, string city, bool isVip)
        {
            if (_customers.Count >= MaxCustomers)
                throw new InvalidOperationException("Customer list is full.");
            if (_customers.Any(c => c.Id == id))
                throw new InvalidOperationException($"Customer id {id} already exists.");

            var customer = new Customer(id, name, email, city, isVip);
            _customers.Add(customer);
            return customer;
        }

        public Product AddProduct(int id, string name, decimal price, int stock)
        {
            if (_products.Count >= MaxProducts)
                throw new InvalidOperationException("Product list is full.");
            if (_products.Any(p => p.Id == id))
                throw new InvalidOperationException($"Product id {id} already exists.");

            var product = new Product(id, name, price, stock);
            _products.Add(product);
            return product;
        }

        public Order CreateOrder(int orderId, int customerId, DateTime date)
        {
            if (_orders.Count >= MaxOrders)
                throw new InvalidOperationException("Order list is full.");
            if (_orders.Any(o => o.Id == orderId))
                throw new InvalidOperationException($"Order id {orderId} already exists.");

            var customer = FindCustomer(customerId);
            var order = new Order(orderId, customer, date);
            _orders.Add(order);
            return order;
        }

        public void AddLineToOrder(int orderId, int productId, int quantity)
        {
            var order = FindOrder(orderId);
            var product = FindProduct(productId);
            order.AddLine(product, quantity);
        }

        public void MarkOrderPaid(int orderId) => FindOrder(orderId).MarkPaid();

        public decimal TotalSalesPaidOnly() =>
            _orders.Where(o => o.IsPaid).Sum(o => o.Total);
    }
}
