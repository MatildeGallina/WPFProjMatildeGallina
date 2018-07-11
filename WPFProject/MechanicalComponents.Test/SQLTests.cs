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
        public void create_and_open_Connection_succesfully()
        {
            var dbConn = new Database("");
            var conn = dbConn.CreateConnection();

            Assert.AreEqual(conn.State, ConnectionState.Open);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void Connection_string_wrong_throw_Exception()
        {
            var dbConn = new Database("");
            var conn = dbConn.CreateConnection();
        }

        [TestMethod]
        public void query_of_Insert_count_of_row_in_table_update()
        {
            throw new Exception();
        }

        [TestMethod]
        public void checking_the_Insert_values()
        {
            throw new Exception();
        }

        [TestMethod]
        public void query_of_Select_return_the_correct_Id()
        {
            var dbConn = new Database("");
            var conn = dbConn.CreateConnection();

            //Node n = new Node("29CV 2 cilindri", "1020304050");

            //string dbConn.NewQuery(conn, n) = n.QueryWriter.RetriveDataQuery(n);

        }

        [TestMethod]
        public void query_of_Update_update_values()
        {
            throw new Exception();
        }

        [TestMethod]
        public void query_of_Alter_changes_values()
        {
            throw new Exception();
        }
    }
}
