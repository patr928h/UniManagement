using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using DataAccess;
using System.Data.SqlClient;
using Entities;

namespace Tests
{
    public class RepositoryTests
    {
        [Fact]
        public void GetRepositoryDataTest()
        {
            //Arrange:
            Repository repository;


            //Act:
            repository = new();
            //Assert:
            Assert.NotNull(repository);
        }

            [Fact]
            public void GetDatabaseTableData()
        {
           // Arrange:
            Repository repository = new();

            // Act:
            List<Person> people = repository.GetAllPersons();

            // Assert:
            Assert.True(people.Count > 0);

            
        }

     
    }
}
