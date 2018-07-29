using System;
using System.Collections.Generic;
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
        public MainWindow()
        {
            InitializeComponent();

            var db = ConnectionToDatabase();
            InitializeTreeview(db);            
        }

        public Database ConnectionToDatabase()
        {
            Database database = new Database();
            database.SetConnectionString(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=MockMechanicalComponentsDatabase;");
            return database;
        }

        public void InitializeTreeview(Database db)
        {
            var engines = db.GetNodes(null);
            foreach(Node n in engines)
                n._database = db;

            EnginesTreeView.ItemsSource = engines;
        }

        private void EnginesTreeView_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            VisibilitySpecifications();

            INode selectedNode = (INode)EnginesTreeView.SelectedItem;

            NodeName.Text = selectedNode.Name;
            NodeSerialCode.Text = selectedNode.SerialCode;
        }

        private void AddEngine_Click(object sender, RoutedEventArgs e)
        {
            NewEngineWindow newEngine = new NewEngineWindow();
            newEngine.ShowDialog();
        }

        private void Refresh_Click(object sender, RoutedEventArgs e)
        {
            INode selectedNode = (INode)EnginesTreeView.SelectedItem;
            // come fare il refresh di un ramo del treeview
        }

        private void AddNewChild_Click(object sender, RoutedEventArgs e)
        {
            NewChildWindow newChild = new NewChildWindow();
            newChild.ShowDialog();
        }

        private void AlterProperties_Click(object sender, RoutedEventArgs e)
        {
            SetPropertiesWindow setProperties = new SetPropertiesWindow();
            setProperties.ShowDialog();
        }
        
        private void VisibilitySpecifications()
        {
            INode selectedNode = (INode)EnginesTreeView.SelectedItem;

            AddNewChild.IsEnabled = selectedNode.CanHaveChild();
            Refresh.IsEnabled = true;
            Informations.Visibility = Visibility.Visible;
            SelectedItemButtons.Visibility = Visibility.Visible;
        }
    }
}
