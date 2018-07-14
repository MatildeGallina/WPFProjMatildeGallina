using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MechanicalComponents.Models
{
    public class Node
    {
        public Node(Database database)
        {
            _database = database;
            _Children = new Lazy<List<Node>>(LoadChildren);
        }

        internal int Id { get; set; }
        public string Name { get; set; }
        public string SerialCode { get; set; }
        internal int? ParentId { get; set; }
        internal int? IconId { get; set; }

        public List<Node> Children
        {
            get { return _Children.Value; }
        }
        private Lazy<List<Node>> _Children;

        private List<Node> LoadChildren()
        {
            return _database.GetNodes(this.Id);
        }

        private Database _database;
    }
}
