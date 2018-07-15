using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MechanicalComponents.Models;
using System.Data.SqlClient;
using System.Data;

namespace MechanicalComponents.Test
{
    [TestClass]
    public class SQLTests
    {
        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void create_and_open_Connection_succesfully()
        {
            var mockDB = new MockDatabase();
            mockDB.connectionOpen = false;
            mockDB.SetConnectionString("correctString");
            SqlConnection conn = mockDB.CreateConnection();
            
            
            Assert.AreEqual(true, mockDB.connectionOpen);
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void Connection_string_wrong_throw_Exception()
        {
            var mockDB = new MockDatabase();
            mockDB.connectionOpen = false;
            mockDB.SetConnectionString("a wrong connection string");
            SqlConnection conn = mockDB.CreateConnection();
        }

        [TestMethod]
        public void SetNode_update_list_of_mockdatabase_Count()
        {
            var mockDB = new MockDatabase();

            int count = mockDB._models.Count;

            NodeModel node = new NodeModel("SetNode test Method", "A12354678B", 1);
            mockDB.SetNode(node);

            Assert.AreEqual(count + 1, mockDB._models.Count);
        }

        [TestMethod]
        public void checking_the_Insert_values()
        {
            var mockDB = new MockDatabase();

            NodeModel node = new NodeModel("SetNode 2 test Method", "B12354678D", 1);
            mockDB.SetNode(node);

            int count = mockDB._models.Count;

            Assert.AreEqual("SetNode 2 test Method", mockDB._models[count - 1].Name);
            Assert.AreEqual("B12354678D", mockDB._models[count - 1].SerialCode);
            Assert.AreEqual(1, mockDB._models[count - 1].ParentId);
        }

        [TestMethod]
        public void GetNodes_list_has_correct_Count()
        {
            var mockDB = new MockDatabase();
            var engines = mockDB.GetNodes(null);

            Assert.AreEqual(2, engines.Count);
        }

        [TestMethod]
        public void LazyInitialization_children_list()
        {
            var mockDB = new MockDatabase();
            var engines = mockDB.GetNodes(null);

            Assert.AreEqual(false, engines[0].LazyInitializationTest());

            var children = engines[0].Children;

            Assert.AreEqual(true, engines[0].LazyInitializationTest());
            Assert.AreEqual(2, children.Count);
        }

        [TestMethod]
        public void query_of_Alter_changes_values()
        {
            throw new Exception();
        }
    }
}
