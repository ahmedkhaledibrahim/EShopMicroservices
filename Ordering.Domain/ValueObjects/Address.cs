namespace Ordering.Domain.ValueObjects
{
   public record Address
    {
        public string AddressLine { get; }
        public string City { get;  }
        public string State { get; }
        public string PostalCode { get; }


        private Address(string AddressLine, string City, string State, string PostalCode) {
            this.AddressLine = AddressLine;
            this.City = City;
            this.State = State;
            this.PostalCode = PostalCode;
        }
        public static Address Of(string AddressLine, string City, string State, string PostalCode) {
            return new Address(AddressLine, City, State, PostalCode);
        }
    }
}
