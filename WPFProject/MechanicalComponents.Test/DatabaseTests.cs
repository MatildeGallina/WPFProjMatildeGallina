using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MechanicalComponents.Models;
using System.Data.SqlClient;
using System.Data;
using System.IO;

namespace MechanicalComponents.Test
{
    [TestClass]
    public class DatabaseTests
    {
        [TestMethod]
        public void GetNode_with_right_connection_string()
        {
            Database database = new Database();
            database.SetConnectionString(File.ReadAllText("DatabaseConnectionString.txt"));
            database.GetNodes(1);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void GetNode_with_null_connection_string()
        {
            Database database = new Database();
            database.SetConnectionString(null);
            database.GetNodes(1);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void GetNode_with_empty_connection_string()
        {
            Database database = new Database();
            database.SetConnectionString("");
            database.GetNodes(1);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void GetNode_with_blanks_connection_string()
        {
            Database database = new Database();
            database.SetConnectionString("   ");
            database.GetNodes(1);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void GetNode_with_wrong_connection_string()
        {
            Database database = new Database();
            database.SetConnectionString("wrong/connection/string");
            database.GetNodes(1);
        }

        [TestMethod]
        public void GetNodes_update_database_list()
        {
            Database database = new Database();
            database.SetConnectionString(File.ReadAllText("DatabaseConnectionString.txt"));
            var engines = database.GetNodes(null);

            Assert.AreEqual(3, engines.Count);
        }

        [TestMethod]
        public void GetNodes_charge_rigth_values()
        {
            Database database = new Database();
            database.SetConnectionString(File.ReadAllText("DatabaseConnectionString.txt"));
            var engines = database.GetNodes(null);

            Assert.AreEqual(1, engines[0].Id);
            Assert.AreEqual("29CV 2 cilindri", engines[0].Name);
            Assert.AreEqual("1020304056", engines[0].SerialCode);
            Assert.AreEqual(null, engines[0].ParentId);
        }

        [TestMethod]
        public void SetNode_insert_values_in_correct_columns()
        {
            var mockNode = new NodeModel("mockNode", "00001111AB", "SingleChildrenNode");

            Database database = new Database();
            database.SetConnectionString(File.ReadAllText("DatabaseConnectionString.txt"));
            database.SetNode(mockNode, null);

            var node = database.GetNodeById(mockNode.Id);
            Assert.AreEqual("mockNode", node.Name);
            Assert.AreEqual("00001111AB", node.SerialCode);

            database.DeleteNode(mockNode.Id);
        }
    }
}
