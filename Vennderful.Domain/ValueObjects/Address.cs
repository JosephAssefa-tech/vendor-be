using System.Collections.Generic;
using Vennderful.Domain.Common;

namespace Vennderful.Domain.ValueObjects
{
    public class Address : ValueObject
    {
        public string? Street { get;  set; }
        public string? City { get;  set; }
        public string? State { get;  set; }
        public string? Country { get;  set; }
        public string? ZipCode { get;  set; }

        public Address() { }

        public Address(string? street, string? city, string? state, string? country, string? zipcode)
        {
            Street = street;
            City = city;
            State = state;
            Country = country;
            ZipCode = zipcode;
        }

        public override string ToString()
        {
            return $"{Street}, {City}, {State} {ZipCode}";
        }

        protected override IEnumerable<object> GetAtomicValues()
        {
            // Using a yield return statement to return each element one at a time
            yield return Street;
            yield return City;
            yield return State;
            yield return Country;
            yield return ZipCode;
        }
    }
}
