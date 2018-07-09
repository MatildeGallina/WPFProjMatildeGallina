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
        public void create_and_open_connection_succesfully()
        {
            var dbConn = new DatabaseConnection(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=MechanicalComponentsDatabase;Integrated Security=True;Connect Timeout=15;");
            var conn = dbConn.CreateConnection();

            Assert.AreEqual(conn.State, ConnectionState.Open);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void connection_string_wrong_throw_exception()
        {
            var dbConn = new DatabaseConnection("");
            var conn = dbConn.CreateConnection();
        }
    }
}
