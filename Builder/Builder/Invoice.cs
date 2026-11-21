using System;
using System.Collections.Generic;
using System.Text;

namespace Builder
{
    internal class Invoice
    {

        public int InvoiceId { get; set; }
        public string CustomerName { get; set; }
        public string CustomerEmail { get; set; }
        public string CustomerPhone { get; set; }

       


        public string ShippingStreet { get; set; }
        public string ShippingCity { get; set; }
        public string ShippingState { get; set; }
        public string ShippingZipCode { get; set; }
        public string ShippingCountry { get; set; }

        public BilingAddress BillingAddress { get; set; }
        public DateTime OrderDate { get; set; }
        public string PaymentMethod { get; set; }
        public string Currency { get; set; }
        public decimal SubTotal { get; set; }
        public decimal DiscountAmount { get; set; }
        public decimal TaxAmount { get; set; }
        public decimal TotalAmount { get; set; }

        public Invoice(int invoiceId, string customerName, string customerEmail,
                      string customerPhone, BilingAddress billingAddress, string shippingStreet, string shippingCity, string shippingState, string shippingZipCode,
                      string shippingCountry, DateTime orderDate, string paymentMethod,
                      string currency, decimal subTotal, decimal discountAmount, decimal taxAmount, decimal totalAmount)
        {
            InvoiceId = invoiceId;
            CustomerName = customerName;
            CustomerEmail = customerEmail;
            CustomerPhone = customerPhone;
            BillingAddress = billingAddress;
            ShippingStreet = shippingStreet;
            ShippingCity = shippingCity;
            ShippingState = shippingState;
            ShippingZipCode = shippingZipCode;
            ShippingCountry = shippingCountry;
            OrderDate = orderDate;
            PaymentMethod = paymentMethod;
            Currency = currency;
            SubTotal = subTotal;
            DiscountAmount = discountAmount;
            TaxAmount = taxAmount;
            TotalAmount = totalAmount;
        }
        public void PrintInvoice()
        {
            Console.WriteLine($"Invoice ID: {InvoiceId}");
            Console.WriteLine($"Customer Name: {CustomerName}");
            Console.WriteLine($"Customer Email: {CustomerEmail}");
            Console.WriteLine($"Customer Phone: {CustomerPhone}");

            if (BillingAddress != null)
            {
                Console.WriteLine($"Billing Address: {BillingAddress.BillingStreet}, {BillingAddress.BillingCity}, {BillingAddress.BillingState}, {BillingAddress.BillingZipCode}, {BillingAddress.BillingCountry}");
            }

            Console.WriteLine($"Shipping Address: {ShippingStreet}, {ShippingCity}, {ShippingState}, {ShippingZipCode}, {ShippingCountry}");

            Console.WriteLine($"Order Date: {OrderDate}");
            Console.WriteLine($"Payment Method: {PaymentMethod}");
            Console.WriteLine($"Currency: {Currency}");
            
            Console.WriteLine($"Discount: {DiscountAmount}");
          
            Console.WriteLine($"Total Amount: {TotalAmount}");
        }
    }
    internal class InvoiceBuilder
    {
        private int _invoiceId;
        private string _customerName;
        private string _customerEmail;
        private string _customerPhone;

        private BilingAddress _billingAddress;

        private string _shippingStreet;
        private string _shippingCity;
        private string _shippingState;
        private string _shippingZipCode;
        private string _shippingCountry;
        private DateTime _orderDate;
        private string _paymentMethod;
        private string _currency;
      
        private decimal _discountAmount;
        private decimal _orderAmount;
        private decimal _totalAmount => _orderAmount - _discountAmount;

        public InvoiceBuilder(int invoiceId, string customerName , string customerEmail , string currency , decimal orderAmount)
        {
            _invoiceId = invoiceId;
            _customerName = customerName;
            _customerEmail = customerEmail;
            _currency = currency;
            _orderAmount = orderAmount;
        }
        public InvoiceBuilder SetCustomerPhone(string customerPhone)
        {
            _customerPhone = customerPhone;
            return this;
        }
      public InvoiceBuilder SetBillingAddress(BilingAddress billingAddress)
        {
            _billingAddress = billingAddress;
            return this;
        }
        public InvoiceBuilder SetShippingAddress(string street, string city, string state, string zipCode, string country)
        {
            _shippingStreet = street;
            _shippingCity = city;
            _shippingState = state;
            _shippingZipCode = zipCode;
            _shippingCountry = country;
            return this;
        }
        public InvoiceBuilder SetOrderDate(DateTime orderDate)
        {
            _orderDate = orderDate;
            return this;
        }
        public InvoiceBuilder SetPaymentMethod(string paymentMethod)
        {
            _paymentMethod = paymentMethod;
            return this;
        }
        public InvoiceBuilder SetDiscountAmount(decimal discountAmount)
        {
            _discountAmount = discountAmount;
            return this;
        }
        public Invoice Build()
        {
            return new Invoice(_invoiceId, _customerName, _customerEmail, _customerPhone, _billingAddress,
                               _shippingStreet, _shippingCity, _shippingState, _shippingZipCode, _shippingCountry,
                               _orderDate, _paymentMethod, _currency, _orderAmount, _discountAmount,
                               0m, _totalAmount);
        }
    }
}
