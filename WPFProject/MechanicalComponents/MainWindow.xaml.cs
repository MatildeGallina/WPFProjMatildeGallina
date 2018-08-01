﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.SqlClient;
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
            database.SetConnectionString(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=MockMechanicalComponentsDatabase;");
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

            throw new NotImplementedException();
        }

        private void AlterProperties_Click(object sender, RoutedEventArgs e)
        {
            SetPropertiesWindow setProperties = new SetPropertiesWindow(RetriveCastedNode());
            setProperties.ShowDialog();

            // riassegrare i valori inviati dalla setPropertyWindow all'item selezionato
            // forse serve aggiornare

            //throw new NotImplementedException();
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
        }

        private void ShowPropertiesValues(INode selectedNode)
        {
            NodeBrand.Text = selectedNode.properties.Brand;
            NodeModel.Text = selectedNode.properties.Model;
            NodePrice.Text = selectedNode.properties.Price.ToString();
            
            switch (RetriveCastedNode().GetType().Name)
            {
                case "MultiChildrenNode":
                    Maintenance.Text = selectedNode.properties.FreeMaintenance.ToString();
                    break;
                case "SingleChildrenNode":
                    Warranty.Text = selectedNode.properties.WarrantyPeriod.ToString();
                    break;
                case "NullChildrenNode":
                    Material.Text = selectedNode.properties.Material;
                    Year.Text = selectedNode.properties.Year.ToString();
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
