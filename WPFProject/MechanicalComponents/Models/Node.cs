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
        
        public IProperties properties { get; set; }

        public List<INode> Children
        {
            get { return _Children.Value; }
        }

        private readonly Lazy<List<INode>> _Children;
        internal IDatabase _database;

        public List<INode> LoadChildren()
        {
            var list =_database.GetNodes(this.Id);
            foreach (Node n in list)
                n._database = this._database;

            return list;
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
        public MultiChildrenNode(IDatabase database)
            : base(database)
        {
            Icon = @"/WPFProject/MechanicalComponents;Icons/MultiChildrenNode.jpg";
            properties = new MultiChildrenNodeProperties();
        }
        //public new readonly string Icon;

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
            properties = new SingleChildrenNodeProperties();
        }

        public override bool CanHaveChild()
        {
            if (this.Children.Count > 0)
                return false;
            else
                return true;
        }
    }

    public class NullChildrenNode : Node
    {
        public NullChildrenNode (IDatabase database)
            : base(database)
        {
            Icon = "NullChildrenIcon.jpg";
            properties = new NullChildrenNodeProperties();
        }

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
            Savable = false;
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

        internal bool Savable { get; set; }

        internal ObservableCollection<NodeModel> Children { get; set; }
    }
}
