using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class ContactInformations
    {
        private int id;
        private string mail;
        private string phoneNumber;
        public ContactInformations()
        {

        }
        public ContactInformations(int id, string mail, string phoneNumber)
        {
            Id = id;
            Mail = mail;
            PhoneNumber = phoneNumber;

            
        }

        public int Id
        {
            get => id; 
            set
            {
                if(value < 0)
                {
                    throw new ArgumentOutOfRangeException();
                }
                id = value;
            }
        }
        public string Mail
        {
            get => mail; 
            set
            {
                mail = value;
            }
        }
        public string PhoneNumber
        {
            get => phoneNumber; 
            set
            {
                phoneNumber = value;
            }
        }
    }
}
