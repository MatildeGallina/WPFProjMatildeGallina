using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MechanicalComponents.Models
{
    public abstract class Node : INode
    {
        public Node(IDatabase database)
        {
            _database = database;
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string SerialCode { get; set; }
        public int? ParentId { get; set; }
        public string Icon { get; set; }

        public IProperties properties { get; set; }

        public abstract List<INode> Children { get; }

        internal IDatabase _database;

        public List<INode> LoadChildren()
        {
            var list = _database.GetNodes(this.Id);
            foreach (Node n in list)
                n._database = this._database;

            return list;
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
        public MultiChildrenNode(IDatabase database)
            : base(database)
        {
            Icon = @"Icons\MultiChildrenNode.jpg";
            _Children = new Lazy<List<INode>>(LoadChildren);
            properties = new MultiChildrenNodeProperties();
        }

        public override List<INode> Children
        {
            get { return _Children.Value; }
        }

        private readonly Lazy<List<INode>> _Children;
        
        public bool WereChildrenLoaded()
        {
            return _Children.IsValueCreated;
        }

        public override bool CanHaveChild()
        {
            return true;
        }
    }

    public class SingleChildrenNode : Node
    {
        public SingleChildrenNode(IDatabase database)
            : base(database)
        {
            Icon = @"Icons\SingleChildrenNode.jpg";
            _Children = new Lazy<List<INode>>(LoadChildren);
            properties = new SingleChildrenNodeProperties();
        }

        public override List<INode> Children
        {
            get { return _Children.Value; }
        }

        private readonly Lazy<List<INode>> _Children;

        public bool WereChildrenLoaded()
        {
            return _Children.IsValueCreated;
        }

        public override bool CanHaveChild()
        {
            if (Children.Count > 0)
                return false;
            else
                return true;
        }
    }

    public class NullChildrenNode : Node
    {
        public NullChildrenNode(IDatabase database)
            : base(database)
        {
            Icon = @"Icons\NullChildrenNode.jpg";
            properties = new NullChildrenNodeProperties();
        }

        public override List<INode> Children { get { return null; } }

        public override bool CanHaveChild()
        {
            return false;
        }
    }
    
    public class NodeModel
    {
        public NodeModel()
        {
            Children = new ObservableCollection<NodeModel>();
        }

        public NodeModel(string name, string serialCode, string type)
        {
            Name = name;
            SerialCode = serialCode;
            Type = type;
            Children = new ObservableCollection<NodeModel>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        internal string SerialCode { get; set; }
        internal int? ParentId { get; set; }  
        internal string Type { get; set; }
        
        internal ObservableCollection<NodeModel> Children { get; set; }
    }
}
