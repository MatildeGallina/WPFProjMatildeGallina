using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MechanicalComponents.Models
{
    public class Node
    {
        public Node(IDatabase database)
        {
            _database = database;
            _Children = new Lazy<List<Node>>(LoadChildren);
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string SerialCode { get; set; }
        public int? ParentId { get; set; }
        internal int? IconId { get; set; }

        public List<Node> Children
        {
            get { return _Children.Value; }
        }
        private Lazy<List<Node>> _Children;

        public bool LazyInitializationTest()
        {
            return _Children.IsValueCreated;
        }

        private List<Node> LoadChildren()
        {
            return _database.GetNodes(this.Id);
        }

        private void AddChild(NodeModel child)
        {
            _database.SetNode(child);
        }

        private IDatabase _database;
    }
    
    public class NodeModel
    {
        public NodeModel(string name, string serialCode, int? parentId)
        {
            Name = name;
            SerialCode = serialCode;
            ParentId = parentId;
        }

        public string Name { get; set; }
        public string SerialCode { get; set; }
        public int? ParentId { get; set; }
        public List<NodeModel> Children { get; set; }
    }
}
