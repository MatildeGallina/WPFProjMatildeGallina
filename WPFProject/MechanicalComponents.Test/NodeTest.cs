using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MechanicalComponents.Models;
using System.Data.SqlClient;
using System.Data;

namespace MechanicalComponents.Test
{
    [TestClass]
    public class NodeTest
    {
        Database newDatabase()
        {
            Database database = new Database();
            database.SetConnectionString(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=MockMechanicalComponentsDatabase;");
            return database;
        }
            
        [TestMethod]
        public void MultiChildrenNode_can_have_child()
        {
            var mcn = new MultiChildrenNode(newDatabase());
            Assert.AreEqual(true, mcn.CanHaveChild());
        }

        [TestMethod]
        public void MultiChildrenNode_lazy_initialization_list_of_children()
        {
            var mcn = new MultiChildrenNode(newDatabase())
            {
                Id = 1
            };
            
            Assert.AreEqual(false, mcn.WereChildrenLoaded());
            
            Assert.AreEqual(3, mcn.Children.Count);
            Assert.AreEqual(true, mcn.WereChildrenLoaded());
        }

        [TestMethod]
        public void SingleChildNode_can_have_child()
        {
            var scn = new SingleChildrenNode(newDatabase())
            {
                Id = 4
            };
            Assert.AreEqual(true, scn.CanHaveChild());
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void SingleChildNode_can_have_only_one_child()
        {
            var scn = new SingleChildrenNode(newDatabase())
            {
                Id = 4
            };

            var child1 = new NodeModel("child 1", "1234AA1234", "MultiChildrenNode");
            scn.AddChild(child1);

            var child2 = new NodeModel("child 2", "5678BB5678", "SingleChildrenNode");
            scn.AddChild(child2);
        }

        [TestMethod]
        public void NullChildrenNode_cannot_have_child()
        {
            var ncn = new NullChildrenNode(newDatabase());
            Assert.AreEqual(false, ncn.CanHaveChild());
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void NullChildrenNode_throw_exception_if_I_try_to_add_a_child()
        {
            var ncn = new NullChildrenNode(newDatabase())
            {
                Id = 6
            };
            var child = new NodeModel("child", "2584CCC2584", "NullChildrenNode");

            ncn.AddChild(child);
        }
    }
}
