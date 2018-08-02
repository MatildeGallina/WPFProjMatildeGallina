using System;
using System.Collections.Generic;
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
using System.Windows.Shapes;
using MechanicalComponents.Models;

namespace MechanicalComponents
{
    public partial class SetPropertiesWindow : Window
    {
        private IDatabase _database;
        public INode node { get; set; }

        public SetPropertiesWindow(INode inputNode)
        {
            _database = ConnectionToDatabase();
            node = RetriveCastedNode(inputNode);
            InitializeComponent();
            SetVisibility(node);
            SetValues(node);
        }

        public Database ConnectionToDatabase()
        {
            Database database = new Database();
            database.SetConnectionString(File.ReadAllText("DatabaseConnectionString.txt"));
            return database;
        }

        private void SetVisibility(INode node)
        {
            switch (node.GetType().ToString())
            {
                case "MechanicalComponents.Models.MultiChildrenNode" :
                    MultiChildrenNode.Visibility = Visibility.Visible;
                    break;
                case "MechanicalComponents.Models.SingleChildrenNode":
                    SingleChildrenNode.Visibility = Visibility.Visible;
                    break;
                case "MechanicalComponents.Models.NullChildrenNode":
                    NullChildrenNode.Visibility = Visibility.Visible;
                    break;
                default:
                    throw new NullReferenceException("Unkonown type");
            }
        }

        private void SetValues(INode node)
        {
            var nodeType = node.GetType().Name;
            switch (nodeType)
            {
                case "MultiChildrenNode":
                    var multiNode = (MultiChildrenNode)node;
                    NodeBrand.Text = multiNode.properties.Brand;
                    NodeModel.Text = multiNode.properties.Model;
                    NodePrice.Text = multiNode.properties.Price.ToString();
                    Maintenance.Text = multiNode.properties.FreeMaintenance.ToString();
                    break;
                case "SingleChildrenNode":
                    var singleNode = (SingleChildrenNode)node;
                    NodeBrand.Text = singleNode.properties.Brand;
                    NodeModel.Text = singleNode.properties.Model;
                    NodePrice.Text = singleNode.properties.Price.ToString();
                    Warranty.Text = singleNode.properties.WarrantyPeriod.ToString();
                    break;
                case "NullChildrenNode":
                    var nullNode = (NullChildrenNode)node;
                    NodeBrand.Text = nullNode.properties.Brand;
                    NodeModel.Text = nullNode.properties.Model;
                    NodePrice.Text = nullNode.properties.Price.ToString();
                    Material.Text = nullNode.properties.Material;
                    Year.Text = nullNode.properties.Year.ToString();
                    break;
                default:
                    throw new ArgumentException("Type not found");
            }
        }

        private void SaveNewChild_Click(object sender, RoutedEventArgs e)
        {
            // riassegnare i valori al nodo
            switch (node.GetType().Name)
            {
                case "MultiChildrenNode" :
                    var multiNode = new MultiChildrenNode(_database);
                    multiNode.Id = node.Id;

                    multiNode.properties.Brand = NodeBrand.Text;
                    multiNode.properties.Model = NodeModel.Text;

                    if (NodePrice.Text.Length > 0 && decimal.TryParse(NodePrice.Text, out decimal mPrice))
                        multiNode.properties.Price = mPrice;

                    if (Maintenance.Text.Length > 0 && int.TryParse(Maintenance.Text, out int maintenance))
                    multiNode.properties.FreeMaintenance = maintenance;

                    _database.UpdateProperties(multiNode);
                    break;

                case "SingleChildrenNode":
                    var singleNode = new SingleChildrenNode(_database);
                    singleNode.Id = node.Id;

                    singleNode.properties.Brand = NodeBrand.Text;
                    singleNode.properties.Model = NodeModel.Text;

                    if (NodePrice.Text.Length > 0 && decimal.TryParse(NodePrice.Text, out decimal sPrice))
                        singleNode.properties.Price = sPrice;

                    if (Maintenance.Text.Length > 0 && int.TryParse(Warranty.Text, out int warranty))
                        singleNode.properties.WarrantyPeriod = warranty;

                    _database.UpdateProperties(singleNode);
                    break;

                case "NullChildrenNode":
                    var nullNode = new NullChildrenNode(_database);
                    nullNode.Id = node.Id;

                    nullNode.properties.Brand = NodeBrand.Text;
                    nullNode.properties.Model = NodeModel.Text;

                    if (NodePrice.Text.Length > 0 && decimal.TryParse(NodePrice.Text, out decimal nPrice))
                        nullNode.properties.Price = nPrice;

                    nullNode.properties.Material = Material.Text;

                    if (Maintenance.Text.Length > 0 && int.TryParse(Warranty.Text, out int year))
                        nullNode.properties.Year = year;

                    _database.UpdateProperties(nullNode);
                    break;
                    
                default:
                    throw new ArgumentException("Unknown type!");
            }
            // chiamare il database che salvi dandogli il nodo
            Cleaning();
            this.Close();
        }

        private void Undo_Click(object sender, RoutedEventArgs e)
        {
            Cleaning();
            this.Close();
        }

        private void Cleaning()
        {
            NodeBrand.Clear();
            NodeModel.Clear();
            NodePrice.Clear();
            Maintenance.Clear();
            Warranty.Clear();
            Material.Clear();
            Year.Clear();
        }

        private INode RetriveCastedNode(INode inputNode)
        {
            var nodeType = inputNode.GetType().Name;
            switch (nodeType)
            {
                case "MultiChildrenNode":
                    return (MultiChildrenNode)inputNode;
                case "SingleChildrenNode":
                    return (SingleChildrenNode)inputNode;
                case "NullChildrenNode":
                    return (NullChildrenNode)inputNode;
                default:
                    throw new ArgumentException("Type not found");
            }
        }
    }
}
