namespace OOP_Assignmet_1
{
    internal class Customer
    {
        private const decimal VipDiscountRate = 0.10m;

        public int Id { get; private set; }
        public string Name { get; private set; }
        public string Email { get; private set; }
        public string City { get; private set; }
        public bool IsVip { get; private set; }

        public Customer(int id, string name, string email, string city, bool isVip = false)
        {
            if (id <= 0)
                throw new ArgumentException("Id must be a positive integer.", nameof(id));
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Name cannot be null or whitespace.", nameof(name));
            if (string.IsNullOrWhiteSpace(email))
                throw new ArgumentException("Email cannot be null or whitespace.", nameof(email));
            if (string.IsNullOrWhiteSpace(city))
                throw new ArgumentException("City cannot be null or whitespace.", nameof(city));

            Id = id;
            Name = name;
            Email = email;
            City = city;
            IsVip = isVip;
        }

        // The customer knows its own discount rule; Order just asks.
        public decimal ApplyDiscount(decimal amount)
        {
            return IsVip ? amount * (1 - VipDiscountRate) : amount;
        }

        public override string ToString()
        {
            return $"#{Id}  {Name}  <{Email}>  {City}  vip={(IsVip ? "yes" : "no")}";
        }
    }
}
