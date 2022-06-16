using System;
using Xunit;
using Entities;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tests
{
    public class EntitiesTests
    {
        [Fact]
        public void CorrectInitialization_ContactInformation()
        {
            //Arrange:
            int id = 1;
            string mail = "patrickquinn@mail.com";
            string phoneNumber = "35612891";

            //Act:
            ContactInformations contactInformations = new(id, mail, phoneNumber);

            //Assert:
            Assert.True(contactInformations.Id > 0);
            Assert.Contains("@", mail);
            int length = phoneNumber.Length;
            Assert.True(length == 8);
    }
        [Fact]
        public void CorrectMutation_ContactInformation()
        {
            //Arrange:
            int id = 1;
            string mail = "patrickquinn@mail.com";
            string phoneNumber = "35612891";
            ContactInformations contactInformations = new(id, mail, phoneNumber);


            //Act:
            contactInformations.PhoneNumber = "45871234";
            contactInformations.Mail = "quinnpatrick@mail.dk";
            //Assert:
            Assert.Equal("45871234", contactInformations.PhoneNumber);
            Assert.Equal("quinnpatrick@mail.dk", contactInformations.Mail);

        }
        
    }
}
