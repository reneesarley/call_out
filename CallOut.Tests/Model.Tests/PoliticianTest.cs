using Microsoft.VisualStudio.TestTools.UnitTesting;
using CallOut.Models;
using System;
using System.Collections.Generic;

namespace CallOut.Tests
{
    [TestClass]
    public class PoliticianTests : IDisposable
    {
        public void Dispose()
        {
            Politician.DeleteAll();
        }

        public PoliticianTests()
        {
            DBConfiguration.ConnectionString = "server=localhost;user id=root;password=root;port=8889;database=call_out_test;";
        }


        [TestMethod]
        public void GetAll_DBStartsEmpty_0()
        {
            //Arrange
            //Act
            int result = Politician.GetAll().Count;

            //Assert
            Assert.AreEqual(0, result);
        }

        [TestMethod]
        public void Save_SavesToDatabase_PoliticianList()
        {
            //Arrange
            Politician testPolitician = new Politician("Sarah", "Jones");

            //Act
            testPolitician.Save();
            List<Politician> result = Politician.GetAll();
            List<Politician> testList = new List<Politician>() { testPolitician };

            //Assert
            CollectionAssert.AreEqual(result, testList);
        }
    }
}