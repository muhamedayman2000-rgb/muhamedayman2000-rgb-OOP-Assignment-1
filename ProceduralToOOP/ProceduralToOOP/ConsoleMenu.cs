using System.Globalization;

namespace OOP_Assignmet_1
{
    // All console input/output lives here; business classes never touch Console.
    internal class ConsoleMenu
    {
        private readonly OrderSystem _system;

        public ConsoleMenu(OrderSystem system)
        {
            _system = system ?? throw new ArgumentNullException(nameof(system));
        }

        public void Run()
        {
            int choice = -1;
            while (choice != 0)
            {
                PrintMenu();
                choice = ReadInt(null);
                try
                {
                    Handle(choice);
                }
                catch (Exception ex) when (ex is ArgumentException or InvalidOperationException)
                {
                    Console.WriteLine($"ERROR: {ex.Message}");
                }
            }
        }

        private void Handle(int choice)
        {
            switch (choice)
            {
                case 1: PrintCustomers(); break;
                case 2: PrintProducts(); break;
                case 3: PrintAllOrders(); break;
                case 4: Console.WriteLine(); Console.WriteLine(_system.FindOrder(ReadInt("Order id: "))); break;
                case 5:
                    _system.CreateOrder(ReadInt("Order id: "), ReadInt("Customer id: "), ReadDate("Date (YYYY-MM-DD): "));
                    break;
                case 6:
                    _system.AddLineToOrder(ReadInt("Order id: "), ReadInt("Product id: "), ReadInt("Quantity: "));
                    break;
                case 7: _system.MarkOrderPaid(ReadInt("Order id: ")); break;
                case 8: PrintPaidSales(); break;
                case 0: Console.WriteLine("Bye."); break;
                default: Console.WriteLine("Unknown choice."); break;
            }
        }

        public void PrintCustomers()
        {
            Console.WriteLine($"\n=== CUSTOMERS ({_system.Customers.Count}) ===");
            foreach (var c in _system.Customers)
                Console.WriteLine(c);
        }

        public void PrintProducts()
        {
            Console.WriteLine($"\n=== PRODUCTS ({_system.Products.Count}) ===");
            foreach (var p in _system.Products)
                Console.WriteLine(p);
        }

        public void PrintAllOrders()
        {
            Console.WriteLine($"\n=== ALL ORDERS ({_system.Orders.Count}) ===");
            foreach (var o in _system.Orders)
            {
                Console.WriteLine();
                Console.WriteLine(o);
            }
        }

        public void PrintPaidSales()
        {
            Console.WriteLine(string.Create(CultureInfo.InvariantCulture,
                $"Paid sales total: {_system.TotalSalesPaidOnly():F2}"));
        }

        private static void PrintMenu()
        {
            Console.WriteLine("\n---------- MENU ----------");
            Console.WriteLine("1) Print customers");
            Console.WriteLine("2) Print products");
            Console.WriteLine("3) Print all orders");
            Console.WriteLine("4) Print one order by id");
            Console.WriteLine("5) Create order");
            Console.WriteLine("6) Add line to order");
            Console.WriteLine("7) Mark order paid");
            Console.WriteLine("8) Show paid sales total");
            Console.WriteLine("0) Exit");
            Console.Write("Choice: ");
        }

        // Re-prompts on bad input (the C++ version loops forever on non-numeric input).
        // Returns 0 (exit) if input is closed, so Run() ends instead of spinning.
        private static int ReadInt(string? prompt)
        {
            while (true)
            {
                if (prompt != null) Console.Write(prompt);
                string? line = Console.ReadLine();
                if (line == null) return 0;
                if (int.TryParse(line.Trim(), NumberStyles.Integer, CultureInfo.InvariantCulture, out int value))
                    return value;
                Console.WriteLine("Please enter a whole number.");
                if (prompt == null) Console.Write("Choice: ");
            }
        }

        private static DateTime ReadDate(string prompt)
        {
            while (true)
            {
                Console.Write(prompt);
                string? line = Console.ReadLine();
                if (line == null) throw new InvalidOperationException("No input available.");
                if (DateTime.TryParseExact(line.Trim(), "yyyy-MM-dd", CultureInfo.InvariantCulture,
                        DateTimeStyles.None, out DateTime date))
                    return date;
                Console.WriteLine("Please use the format YYYY-MM-DD.");
            }
        }
    }
}
