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
            _database = database;
            _Children = new Lazy<List<INode>>(LoadChildren);
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string SerialCode { get; set; }
        public int? ParentId { get; set; }
        public string Icon { get; set; }

        public List<INode> Children
        {
            get { return _Children.Value; }
        }
        private readonly Lazy<List<INode>> _Children;
        private IDatabase _database;

        public List<INode> LoadChildren()
        {
            if (!CanHaveChild())
                throw new ArgumentException("This node can not have children!");

            return _database.GetNodes(this.Id);
        }

        public bool WereChildrenLoaded()
        {
            return _Children.IsValueCreated;
        }

        public void AddChild(NodeModel child)
        {
            if (!CanHaveChild())
                throw new ArgumentException("This node can not have a new child!");

            _database.SetNode(child, this.Id);
        }

        public abstract bool CanHaveChild();
    }

    public class MultiChildrenNode : Node
    {
        public MultiChildrenNode (IDatabase database)
            : base(database)
        {
            Icon = "MultiChildrenIcon.jpg";
        }
        public new readonly string Icon;

        public override bool CanHaveChild()
        {
            return true;
        }
    }

    public class SingleChildrenNode : Node
    {
        public SingleChildrenNode (IDatabase database)
            : base(database)
        {
            Icon = "SingleChildrenIcon.jpg";
        }
        public new readonly string Icon;

        public override bool CanHaveChild()
        {
            if (Children.Count > 0)
                return false;

            return true;
        }
    }

    public class NullChildrenNode : Node
    {
        public NullChildrenNode (IDatabase database)
            : base(database)
        {
            Icon = "NullChildrenIcon.jpg";
        }
        public new readonly string Icon;

        public override bool CanHaveChild()
        {
            return false;
        }
    }
    
    public class NodeModel
    {
        public NodeModel(string name, string serialCode, string type)
        {
            Name = name;
            SerialCode = serialCode;
            Type = type;
        }

        public int Id { get; set; }
        internal string Name { get; set; }
        internal string SerialCode { get; set; }
        internal int? ParentId { get; set; }  
        internal string Type { get; set; }
        // !!
        // dovrebbe avere una proprietà stringa che indica il tipo di nodo (MultiChildrenNode, SingleChildrenNode ...)
        // magari visto che la scelta verrà fatta nella finestra mettere un menu a tendina o un radio buttton con gia un check
        // che seleziona un tipo
        // !!
        private List<NodeModel> Children { get; set; }
        // aggiungere un metodo per creare un nuovo NodeModel da aggiungere alla lista di figli
    }
}
