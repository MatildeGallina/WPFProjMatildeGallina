using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MechanicalComponents.Models
{
    public abstract class Node : INode
    {
        /*protected*/public Node(IDatabase database)
        {
            _Children = new Lazy<List<Node>>(LoadChildren);
            _database = database;
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string SerialCode { get; set; }
        public int? ParentId { get; set; }
        public string Icon { get; set; }

        public List<Node> Children
        {
            get { return _Children.Value; }
        }
        private readonly Lazy<List<Node>> _Children;
        private IDatabase _database;

        private List<Node> LoadChildren()
        {
            return _database.GetNodes(this.Id);
        }

        public bool WereChildrenLoaded()
        {
            return _Children.IsValueCreated;
        }

        private void AddChild(NodeModel child)
        {
            _database.SetNode(child);
        }

        internal abstract bool CanHaveChild();
    }

    public class MultiChildrenNode : Node
    {
        internal MultiChildrenNode (IDatabase database)
            : base(database)
        {
            Icon = "MultiChildrenIcon";
        }

        public readonly string Icon;

        internal override bool CanHaveChild()
        {
            return true;
        }
    }

    public class SingleChildrenNode : Node
    {
        internal SingleChildrenNode (IDatabase database)
            : base(database)
        {
            Icon = "SingleChildrenIcon";
        }

        public readonly string Icon;

        internal override bool CanHaveChild()
        {
            if (Children.Count > 0)
                return false;

            return true;
        }
    }

    public class NullChildrenNode : Node
    {
        internal NullChildrenNode (IDatabase database)
            : base(database)
        {
            Icon = "NullChildrenIcon";
        }

        public readonly string Icon;

        internal override bool CanHaveChild()
        {
            return false;
        }
    }
    
    public class NodeModel
    {
        public NodeModel(string name, string serialCode)
        {
            Name = name;
            SerialCode = serialCode;
        }

        internal string Name { get; set; }
        internal string SerialCode { get; set; }
        internal int? ParentId { get; set; }  
        // !!
        // dovrebbe avere una proprietà stringa che indica il tipo di nodo (MultiChildrenNode, SingleChildrenNode ...)
        // magari visto che la scelta verrà fatta nella finestra mettere un menu a tendina o un radio buttton con gia un check
        // che seleziona un tipo
        // !!
        private List<NodeModel> Children { get; set; }
        // aggiungere un metodo per creare un nuovo NodeModel da aggiungere alla lista di figli
    }
}
