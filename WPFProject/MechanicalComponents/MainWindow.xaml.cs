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
            EnginesTreeView.ItemsSource = engines;
        }

        private void EnginesTreeView_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            BasicInformations.Visibility = Visibility.Visible;
            INode thisNode = (INode)EnginesTreeView.SelectedItem;
            NodeName.Text = thisNode.Name;
            NodeSerialCode.Text = thisNode.SerialCode;

            if(thisNode.CanHaveChild())
                AddNewChild.Visibility = Visibility.Visible;
        }

        private void EnginesTreeView_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            INode thisNode = (INode)EnginesTreeView.SelectedItem;
        }
        
        private void AddEngine_Click(object sender, RoutedEventArgs e)
        {
            NewEngine newEngine = new NewEngine();
            newEngine.ShowDialog();
        }

        private void AlterProperties_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Refresh_Click(object sender, RoutedEventArgs e)
        {

        }

        private void AddNewChild_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
