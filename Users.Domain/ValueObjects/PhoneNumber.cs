using System.Text.RegularExpressions;

namespace Users.Domain.ValueObjects
{
    public record PhoneNumber
    {
        public string Value { get; }
        public PhoneNumber(string Value)
        {
            this.Value = Value;
        }

        public static PhoneNumber Of(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentException("Phone number cannot be empty.", nameof(value));

            value = value.Trim();

            if (!Regex.IsMatch(value, @"^01[0125]\d{8}$"))
                throw new ArgumentException("Invalid Egyptian phone number.", nameof(value));

            return new PhoneNumber(value);
        }
    }
}
