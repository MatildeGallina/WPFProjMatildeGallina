using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using MechanicalComponents.Models;

namespace MechanicalComponents
{
    public partial class MainWindow : Window
    {
        private IDatabase _database;

        public MainWindow()
        {
            _database = ConnectionToDatabase();
            InitializeComponent();
            InitializeTreeview();            
        }

        public Database ConnectionToDatabase()
        {
            Database database = new Database();
            database.SetConnectionString(File.ReadAllText("DatabaseConnectionString.txt"));
            return database;
        }

        public void InitializeTreeview()
        {
            var engines = _database.GetNodes(null);
            foreach(Node n in engines)
                n._database = _database;

            EnginesTreeView.ItemsSource = engines;
        }

        private void EnginesTreeView_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            var selectedNode = RetriveCastedNode();

            VisibilitySpecifications(selectedNode);
            
            NodeName.Text = selectedNode.Name;
            NodeSerialCode.Text = selectedNode.SerialCode;

            ShowPropertiesValues(selectedNode);
        }

        private void AddEngine_Click(object sender, RoutedEventArgs e)
        {
            NewEngineWindow engineWindow = new NewEngineWindow();
            engineWindow.ShowDialog();

            InitializeTreeview();
        }

        private void AddNewChild_Click(object sender, RoutedEventArgs e)
        {
            NewChildWindow childWindow = new NewChildWindow();
            childWindow.ShowDialog();
            
            var newChild = childWindow.child;
            //INode selectedNode = (INode)EnginesTreeView.SelectedItem;

            var selectedNode = RetriveCastedNode(); 

            _database.UpdateParentId(newChild.Id, selectedNode.Id);
            VisibilitySpecifications(selectedNode);
        }

        private void Refresh_Click(object sender, RoutedEventArgs e)
        {
            var selectedNode = RetriveCastedNode();
            // come fare il refresh di un ramo del treeview

            switch (RetriveCastedNode().GetType().Name)
            {
                case "MultiChildrenNode":
                    var multiNode = (MultiChildrenNode)EnginesTreeView.SelectedItem;
                    var refreshedChildren = _database.GetNodes(multiNode.Id);
                    foreach (Node c in refreshedChildren)
                        c._database = _database;
                    multiNode.Children = new ObservableCollection<INode>(refreshedChildren);
                    break;
                case "SingleChildrenNode":
                    var singleNode = (SingleChildrenNode)EnginesTreeView.SelectedItem;
                    var refreshedChild = _database.GetNodes(singleNode.Id);
                    foreach (Node c in refreshedChild)
                        c._database = _database;
                    singleNode.Children = new ObservableCollection<INode>(refreshedChild);
                    break;
                default:
                    break;
            }
            
            
            InitializeTreeview();

            //throw new NotImplementedException();
        }

        private void AlterProperties_Click(object sender, RoutedEventArgs e)
        {
            SetPropertiesWindow setProperties = new SetPropertiesWindow(RetriveCastedNode());
            setProperties.ShowDialog();
        }
        
        private void VisibilitySpecifications(INode selectedNode)
        {
            CommonProperites.Visibility = Visibility.Visible;

            switch (RetriveCastedNode().GetType().Name)
            {
                case "MultiChildrenNode" :
                    MultiChildrenNodeProperties.Visibility = Visibility.Visible;
                    SingleChildrenNodeProperties.Visibility = Visibility.Hidden;
                    NullChildrenNodeProperties.Visibility = Visibility.Hidden;
                    break;
                case "SingleChildrenNode":
                    MultiChildrenNodeProperties.Visibility = Visibility.Hidden;
                    SingleChildrenNodeProperties.Visibility = Visibility.Visible;
                    NullChildrenNodeProperties.Visibility = Visibility.Hidden;
                    break;
                case "NullChildrenNode":
                    MultiChildrenNodeProperties.Visibility = Visibility.Hidden;
                    SingleChildrenNodeProperties.Visibility = Visibility.Hidden;
                    NullChildrenNodeProperties.Visibility = Visibility.Visible;
                    break;
                default:
                    throw new NullReferenceException("Unknown type");
            }

            if (selectedNode.CanHaveChild())
                AddNewChild.Visibility = Visibility.Visible;
            else
                AddNewChild.Visibility = Visibility.Hidden;
            Refresh.IsEnabled = true;

            Informations.Visibility = Visibility.Visible;

            SelectedItemButtons.Visibility = Visibility.Visible;

            ShowPropertiesValues(selectedNode);
        }

        private void ShowPropertiesValues(INode selectedNode)
        {            
            switch (RetriveCastedNode().GetType().Name)
            {
                case "MultiChildrenNode":
                    var multiNode = (MultiChildrenNode)selectedNode;
                    NodeBrand.Text = multiNode.properties.Brand;
                    NodeModel.Text = multiNode.properties.Model;
                    NodePrice.Text = multiNode.properties.Price.ToString();
                    Maintenance.Text = multiNode.properties.FreeMaintenance.ToString();
                    break;
                case "SingleChildrenNode":
                    var singleNode = (SingleChildrenNode)selectedNode;
                    NodeBrand.Text = singleNode.properties.Brand;
                    NodeModel.Text = singleNode.properties.Model;
                    NodePrice.Text = singleNode.properties.Price.ToString();
                    Warranty.Text = singleNode.properties.WarrantyPeriod.ToString();
                    break;
                case "NullChildrenNode":
                    var nullNode = (NullChildrenNode)selectedNode;
                    NodeBrand.Text = nullNode.properties.Brand;
                    NodeModel.Text = nullNode.properties.Model;
                    NodePrice.Text = nullNode.properties.Price.ToString();
                    Material.Text = nullNode.properties.Material;
                    Year.Text = nullNode.properties.Year.ToString();
                    break;
                default:
                    throw new NullReferenceException("Unknown type");
            }
        }

        private INode RetriveCastedNode()
        {
            var nodeType = EnginesTreeView.SelectedItem.GetType().Name;
            switch (nodeType)
            {
                case "MultiChildrenNode":
                    return (MultiChildrenNode)EnginesTreeView.SelectedItem;
                case "SingleChildrenNode":
                    return (SingleChildrenNode)EnginesTreeView.SelectedItem;
                case "NullChildrenNode":
                    return (NullChildrenNode)EnginesTreeView.SelectedItem;
                default:
                    throw new ArgumentException("Type not found");
            }
        }
    }
}
