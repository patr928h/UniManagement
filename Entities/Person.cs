using System;

namespace Entities
{
    public class Person
    {
        public int Id { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public ContactInformations ContactInformation { get; set; }
        public Address Address { get; set; }
        public int AddressFK { get; set; }
    }
}
