using System;
using Xunit;
using MVCAssignmentTwo.Models;
using System.Collections.Generic;

namespace MVCAssignmentTwo.Tests
{
    public class InMemoryPeopleRepoTests
    {
        [Fact]
        public void PeopleAllListWorks()
        {
            //Arrange
            InMemoryPeopleRepo inMemoryPeopleRepo = new InMemoryPeopleRepo();

            //Act
            Person testPerson = inMemoryPeopleRepo.Create("TestName", "949529", "TestCity");
            var resultList = inMemoryPeopleRepo.Read();

            //Assert
            Assert.IsType<List<Person>>( resultList);
            Assert.True(resultList.Count > 0);
            Assert.Contains(testPerson, resultList);

        }
        [Theory]
        [InlineData("TestName1", "9495291", "TestCity1")]
        [InlineData("TestName2", "9495292", "TestCity2")]
        [InlineData("TestName3", "9495293", "TestCity3")]
        public void CreatePerson(string name, string phone, string city)
        {
            //Arrange
            InMemoryPeopleRepo inMemoryPeopleRepo = new InMemoryPeopleRepo();

            //Actw
            Person testPerson = inMemoryPeopleRepo.Create(name, phone, city);
            var resultList = inMemoryPeopleRepo.Read();

            //Assert
            Assert.Equal(name, testPerson.Name);
            Assert.Equal(phone, testPerson.PhoneNumber);
            Assert.Equal(city, testPerson.City);
            Assert.Contains(testPerson, resultList);
        }
    }
}
