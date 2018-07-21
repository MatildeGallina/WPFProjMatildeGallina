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
        public void GetNode_with_rigth_connection_string()
        {
            Database database = new Database();
            database.SetConnectionString(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=MockMechanicalComponentsDatabase;");
            database.GetNodes(1);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void GetNode_with_null_connection_string()
        {
            Database database = new Database();
            database.SetConnectionString(null);
            database.GetNodes(1);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void GetNode_with_empty_connection_string()
        {
            Database database = new Database();
            database.SetConnectionString("");
            database.GetNodes(1);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
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
            database.SetConnectionString(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=MockMechanicalComponentsDatabase;");
            database.GetNodes(null);

            Assert.AreEqual(3, database._nodes);
        }

        [TestMethod]
        public void GetNodes_charge_rigth_values()
        {
            throw new NotImplementedException();
        }

        [TestMethod]
        public void SetNode_update_table_Count()
        {
            throw new NotImplementedException();
        }

        [TestMethod]
        public void SetNode_insert_values_in_correct_columns()
        {
            throw new NotImplementedException();
        }

        [TestMethod]
        public void LazyInitialization_children_list() // non è proprio un test della classe Database ma della classe Node
        {
            throw new NotImplementedException();
        }

        [TestMethod]
        public void query_of_Alter_changes_values() // cambiare il nome !!!!!!!!!
        {
            throw new NotImplementedException();
        }
    }
}
