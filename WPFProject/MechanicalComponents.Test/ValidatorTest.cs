using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MechanicalComponents.Models;
using System.Data.SqlClient;
using System.Data;
using System.IO;

namespace MechanicalComponents.Test
{
    [TestClass]
    public class NameValidatorTest
    {
        [TestMethod]
        public void NameNotNull()
        {
            var validator = new NameValidator();
            var errors = validator.Validate(new NodeModel(null, "", ""));

            Assert.AreEqual(1, errors.Count);
            Assert.AreEqual("Name not valid!", errors[0]);
        }

        [TestMethod]
        public void NameNotEmpty()
        {
            var validator = new NameValidator();
            var errors = validator.Validate(new NodeModel("", "", ""));

            Assert.AreEqual(1, errors.Count);
            Assert.AreEqual("Name not valid!", errors[0]);
        }

        [TestMethod]
        public void NameNotBlanks()
        {
            var validator = new NameValidator();
            var errors = validator.Validate(new NodeModel("   ", "", ""));

            Assert.AreEqual(1, errors.Count);
            Assert.AreEqual("Name not valid!", errors[0]);
        }

        [TestMethod]
        public void NameValid()
        {
            var validator = new NameValidator();
            var errors = validator.Validate(new NodeModel("node", "", ""));

            Assert.AreEqual(0, errors.Count);
        }
    }

    [TestClass]
    public class SerialCodeValidatorTest
    {
        private IDatabase _database { get { return newDatabase(); } }
        IDatabase newDatabase()
        {
            Database database = new Database();
            database.SetConnectionString(File.ReadAllText("DatabaseConnectionString.txt"));
            return database;
        }

        [TestMethod]
        public void SerialCodeNotNull()
        {
            var validator = new SerialCodeValidator(_database);
            var errors = validator.Validate(new NodeModel("", null, ""));

            Assert.AreEqual(1, errors.Count);
            Assert.AreEqual("SerialCode not valid!", errors[0]);
        }

        [TestMethod]
        public void SerialCodeNotEmpty()
        {
            var validator = new SerialCodeValidator(_database);
            var errors = validator.Validate(new NodeModel("", "", ""));

            Assert.AreEqual(1, errors.Count);
            Assert.AreEqual("SerialCode not valid!", errors[0]);
        }

        [TestMethod]
        public void SerialCodeNotBlanks()
        {
            var validator = new SerialCodeValidator(_database);
            var errors = validator.Validate(new NodeModel("", "      ", ""));

            Assert.AreEqual(1, errors.Count);
            Assert.AreEqual("SerialCode not valid!", errors[0]);
        }

        [TestMethod]
        public void SerialCodeTooLong()
        {
            var validator = new SerialCodeValidator(_database);
            var errors = validator.Validate(new NodeModel("", "12345678910", ""));

            Assert.AreEqual(1, errors.Count);
            Assert.AreEqual("SerialCode not valid!", errors[0]);
        }

        [TestMethod]
        public void SerialCodeTooShort()
        {
            var validator = new SerialCodeValidator(_database);
            var errors = validator.Validate(new NodeModel("", "1234567", ""));

            Assert.AreEqual(1, errors.Count);
            Assert.AreEqual("SerialCode not valid!", errors[0]);
        }

        [TestMethod]
        public void SerialCodeValid()
        {
            var validator = new SerialCodeValidator(_database);
            var errors = validator.Validate(new NodeModel("", "1234567891", ""));

            Assert.AreEqual(0, errors.Count);
        }
    }
}
