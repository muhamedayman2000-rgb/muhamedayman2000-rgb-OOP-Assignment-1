using System.Globalization;

namespace OOP_Assignmet_1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Object-Oriented Order System");
            Console.WriteLine("Seed sample data, show a demo, then open the menu.");

            var system = new OrderSystem();
            SeedSampleData(system);
            RunDemoScenario(system);

            var menu = new ConsoleMenu(system);
            menu.PrintCustomers();
            menu.PrintProducts();
            menu.PrintAllOrders();
            Console.WriteLine();
            Console.Write("Paid sales total after demo: ");
            Console.WriteLine(system.TotalSalesPaidOnly().ToString("F2", CultureInfo.InvariantCulture));

            menu.Run();
        }

        private static void SeedSampleData(OrderSystem system)
        {
            system.AddCustomer(1, "Mona Ali", "mona@example.com", "Cairo", true);
            system.AddCustomer(2, "Omar Hassan", "omar@example.com", "Alexandria", false);
            system.AddCustomer(3, "Sara Nabil", "sara@example.com", "Giza", false);

            system.AddProduct(101, "USB Cable", 50m, 100);
            system.AddProduct(102, "Wireless Mouse", 250m, 40);
            system.AddProduct(103, "Mechanical Keyboard", 1200m, 15);
            system.AddProduct(104, "Laptop Stand", 400m, 25);
        }

        private static void RunDemoScenario(OrderSystem system)
        {
            system.CreateOrder(1001, 1, new DateTime(2026, 9, 15));
            system.AddLineToOrder(1001, 101, 2);
            system.AddLineToOrder(1001, 102, 1);
            system.MarkOrderPaid(1001);

            system.CreateOrder(1002, 2, new DateTime(2026, 9, 15));
            system.AddLineToOrder(1002, 103, 1);
            system.AddLineToOrder(1002, 104, 1);

            system.CreateOrder(1003, 3, new DateTime(2026, 9, 16));
            system.AddLineToOrder(1003, 101, 5);
            system.MarkOrderPaid(1003);
        }
    }
}
