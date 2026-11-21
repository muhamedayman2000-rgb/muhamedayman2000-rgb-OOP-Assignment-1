namespace Builder
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var billingAddress = new BilingAddressBuilder("Egypt")
                .SetBillingStreet("123 Main St")
                .SetBillingCity("NasrCity")
                .SetBillingState("Cairo")
                .SetBillingZipCode("12345")
                .Build();

            var invoice1 = new InvoiceBuilder(1 , "Mohamed Ayman", "mohamed@example.com", "EGP", 1000m)
                .SetCustomerPhone("123-456-7890")
                .SetBillingAddress(billingAddress)
                .SetShippingAddress("456 Oak Ave", "Somewhere", "State", "67890", "Country")
                .SetOrderDate(DateTime.Now)
                .SetPaymentMethod("Credit Card")
                .SetDiscountAmount(100m)
                .Build();
            
            invoice1.PrintInvoice();
        }
    }
}
