using Entities;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class Repository
    {
        private readonly string connectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=UniDb;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

        public Repository()
        {
            SqlConnection connection = new(connectionString);
            connection.Open();
            connection.Close();
        }
        public List<ContactInformations> GetContactInformation()
        {
            List<ContactInformations> contactInformations = new();
            SqlConnection connection = new(connectionString);
            connection.Open();

            string sql = "SELECT * FROM ContactInformations";

            SqlCommand command = new(sql, connection);

            SqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                int id = (int)reader[0];
                string phone = (string)reader[1];
                string mail = (string)reader[2];

                ContactInformations ci = new()
                {
                    Id = id,
                    PhoneNumber = phone,
                    Mail = mail
                };
                contactInformations.Add(ci);
            }

            connection.Close();

            return contactInformations;
        }
        public List<Address> GetAllAddresses()
        {
            // A list to hold all addresses. This is the list this method will return:
            List<Address> addresses = new();

            // Get all persons from the database:
            List<Person> persons = GetAllPersons();

            // Get all addresses from the database:
            SqlConnection connection = new(connectionString);
            connection.Open();
            string sql = "SELECT * FROM Addresses";
            SqlCommand command = new(sql, connection);
            SqlDataReader reader = command.ExecuteReader();

            // Handle data:
            while (reader.Read())
            {
                int addressId = (int)reader[0];
                string streetName = (string)reader[1];
                string streetNumber = (string)reader[2];
                string zip = (string)reader[3];
                string city = (string)reader[4];
                string country = (string)reader[5];

                Address a = new()
                {
                    Id = addressId,
                    StreetName = streetName,
                    StreetNumber = streetNumber,
                    Zip = zip,
                    City = city,
                    Country = country,
                    People = new()
                };

                addresses.Add(a);
            }
            connection.Close();

            Aggregate(persons, addresses);

            // Return the list of addresses:
            return addresses;
        }

        private void Aggregate(List<Person> persons, List<Address> addresses)
        {
            // Loop over all persons:
            for (int i = 0; i < persons.Count; i++)
            {
                // Check whether or not the person has an address:
                if (persons[i].AddressFK != 0)
                {
                    // If true, then loop over all addresses:
                    for (int j = 0; j < addresses.Count; j++)
                    {
                        // Find the right match between person and address.
                        // NOTE: there could be more than perons living at the
                        // address, therefore we shall not break out of the inner loop:
                        if (persons[i].AddressFK == addresses[j].Id)
                        {
                            // Add the person to the list of poeple living at the address:
                            addresses[j].People.Add(persons[i]);
                        }
                    }
                }
            }
        }
        public void AddNewContactInformation(ContactInformations contactInformationToAdd)
        {
            SqlConnection connection = new(connectionString);
            connection.Open();

            string sql = $"INSERT INTO ContactInformations (PhoneNumber, Mail) VALUES('{contactInformationToAdd.Mail}', '{contactInformationToAdd.PhoneNumber}')";

            SqlCommand command = new(sql, connection);

            command.ExecuteNonQuery();

            connection.Close();
        }
        public void UpdateContactInformation(ContactInformations contactInformationToUpdate)
        {
            SqlConnection connection = new(connectionString);
            connection.Open();

            string sql = $"UPDATE ContactInformations SET PhoneNumber = '' Mail = ''";

            SqlCommand command = new(sql, connection);
            command.ExecuteNonQuery();
            connection.Close();
        }
        public void DeleteContactInformation(ContactInformations contactInformationToDelete)
        {
            SqlConnection connection = new(connectionString);
            connection.Open();

            string sql = $"DELETE FROM ContactInformations";

            SqlCommand command = new(sql, connection);
            command.ExecuteNonQuery();
            connection.Close();
        }
        public List<Person> GetAllPersons()
        {
            // First, get all the contact infos, because they are aggregated from person:
            List<ContactInformations> contactInformations = GetContactInformation();

            // Make the list that the method returns:
            List<Person> persons = new();

            // Make a connection to the DB and open it:
            SqlConnection connection = new(connectionString);
            connection.Open();

            // Make the SQl query:
            string sql = "SELECT * FROM Persons";

            // Make the command object:
            SqlCommand command = new(sql, connection);

            // Execute query and save the returned data in a variable:
            SqlDataReader reader = command.ExecuteReader();

            // Convert reader data to C# objects. For each row in the reader:
            while (reader.Read())
            {
                int id = (int)reader[0];
                string firstname = (string)reader[1];
                string lastname = (string)reader[2];

                // This is the FK
                int contactInformation_FKid = (int)reader[3];

                // The aggregated contact information object. Initialize to null:
                ContactInformations contactInformation = null;

                // Loop through all the contact information objects in the contact informations list, that we got before from the database:
                for (int i = 0; i < contactInformations.Count; i++)
                {
                    // If there is a match in the retrieved person row's FK value,
                    if (contactInformation_FKid == contactInformations[i].Id)
                    {
                        // then assign the object from the list, to the property on the person object, thereby making the OOP aggregation:
                        contactInformation = contactInformations[i];

                        // Break out of the loop, because there' no reason to continue:
                        break;
                    }
                }

                // Assign all the retrieved values to the person object:
                Person p = new()
                {
                    Id = id,
                    Firstname = firstname,
                    Lastname = lastname,
                    ContactInformation = contactInformation
                };

                // Add the retrieved person to the list of persons:
                persons.Add(p);
            }

            // Return the list of Persons:
            return persons;
        }
    }
}
