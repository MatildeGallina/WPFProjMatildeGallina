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

            var db = new Database();
            db.SetConnectionString(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=MechanicalComponentsDatabase;");
            SqlConnection conn = db.CreateConnection();

            InitializeTreeview(db);            
        }

        public void InitializeTreeview(Database db)
        {
            var engines = db.GetNodes(null);
            EnginesTreeView.ItemsSource = engines;
        }

        private void EnginesTreeView_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            BasicInformations.Visibility = Visibility.Visible;
            Node thisNode = (Node)EnginesTreeView.SelectedItem;
            NodeName.Text = thisNode.Name;
            NodeSerialCode.Text = thisNode.SerialCode;

            AddNewChild.Visibility = Visibility.Visible;
        }

        private void EnginesTreeView_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            Window window = new Window();
            window.Show();
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
