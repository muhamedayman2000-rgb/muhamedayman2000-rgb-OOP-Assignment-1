using System;
using System.Collections.Generic;
using System.Text;

namespace Builder
{
    internal class BilingAddress
    {
        public string BillingStreet { get; set; }
        public string BillingCity { get; set; }
        public string BillingState { get; set; }
        public string BillingZipCode { get; set; }
        public string BillingCountry { get; set; }
    }
    internal class BilingAddressBuilder
    {
        private string _billingStreet;
        private string _billingCity;
        private string _billingState;
        private string _billingZipCode;
        private string _billingCountry;
        public BilingAddressBuilder (string Country)
        {
            _billingCountry = Country;
        }
        public BilingAddressBuilder SetBillingStreet(string street)
        {
            _billingStreet = street;
            return this;
        }
        public BilingAddressBuilder SetBillingCity(string city)
        {
            _billingCity = city;
            return this;
        }
        public BilingAddressBuilder SetBillingState(string state)
        {
            _billingState = state;
            return this;
        }
        public BilingAddressBuilder SetBillingZipCode(string zipCode)
        {
            _billingZipCode = zipCode;
            return this;
        }
        public BilingAddress Build()
        {
            return new BilingAddress
            {
                BillingStreet = _billingStreet,
                BillingCity = _billingCity,
                BillingState = _billingState,
                BillingZipCode = _billingZipCode,
                BillingCountry = _billingCountry
            };
        }
    }


}

