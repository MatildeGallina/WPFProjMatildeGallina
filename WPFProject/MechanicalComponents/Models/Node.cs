using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
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

        //public IProperties properties { get; set; }

        public IDatabase _database;

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
            _Children = new Lazy<ObservableCollection<INode>>(LoadChildren);
            properties = new MultiChildrenNodeProperties();
        }
        public MultiChildrenNodeProperties properties { get; set; }

        public ObservableCollection<INode> Children
        {
            get { return _Children.Value; }
            set { }
        }

        private readonly Lazy<ObservableCollection<INode>> _Children;

        public ObservableCollection<INode> LoadChildren()
        {
            var list = _database.GetNodes(this.Id);
            foreach (Node n in list)
                n._database = this._database;

            var oc = new ObservableCollection<INode>(list);

            return oc;
        }
        
        public bool WereChildrenLoaded()
        {
            return _Children.IsValueCreated;
        }

        public override bool CanHaveChild()
        {
            return true;
        }
    }

    public class SingleChildrenNode : Node, INotifyPropertyChanged
    {
        public SingleChildrenNode(IDatabase database)
            : base(database)
        {
            Icon = @"Icons\SingleChildrenNode.jpg";
            _Children = new Lazy<ObservableCollection<INode>>(LoadChildren);
            //_Children = LoadChildren();
            properties = new SingleChildrenNodeProperties();
        }

        public SingleChildrenNodeProperties properties { get; set; }
        //public List<INode> Children
        //{
        //    get { return _Children.Value; }
        //}

        //private readonly Lazy<List<INode>> _Children;

        //public List<INode> LoadChildren()
        //{
        //    var list = _database.GetNodes(this.Id);
        //    foreach (Node n in list)
        //        n._database = this._database;

        //    return list;
        //}

        #region prop Children is not a list
        public ObservableCollection<INode> Children
        {
            get { return _Children.Value; }
            set { }
        }

        private readonly Lazy<ObservableCollection<INode>> _Children;

        public event PropertyChangedEventHandler PropertyChanged;

        public ObservableCollection<INode> LoadChildren()
        {
            var list = _database.GetNodes(this.Id);
            foreach (Node n in list)
                n._database = this._database;

            var oc = new ObservableCollection<INode>(list);

            return oc;
        }
        #endregion

        public override bool CanHaveChild()
        {
            //if (LoadChildren().Count > 0)
            //    return false;
            if (LoadChildren().Count > 0)
                return false;
            else
                return true;
        }
    }

    //public class SingleChildrenNode : Node
    //{
    //    public SingleChildrenNode(IDatabase database)
    //        : base(database)
    //    {
    //        Icon = @"Icons\SingleChildrenNode.jpg";
    //        _Children = LoadChildren();
    //        properties = new SingleChildrenNodeProperties();
    //    }

    //    public INode Children
    //    {
    //        get { return _Children; }
    //    }

    //    private readonly INode _Children;

    //    private INode LoadChildren()
    //    {
    //        if (_database == null)
    //            return this;

    //        var child = _database.GetNodes(this.Id);

    //        return child.FirstOrDefault();
    //    }

    //    public override bool CanHaveChild()
    //    {
    //        if (Children != null)
    //            return false;
    //        else
    //            return true;
    //    }
    //}

    public class NullChildrenNode : Node
    {
        public NullChildrenNode(IDatabase database)
            : base(database)
        {
            Icon = @"Icons\NullChildrenNode.jpg";
            properties = new NullChildrenNodeProperties();
        }

        public NullChildrenNodeProperties properties { get; set; }

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
