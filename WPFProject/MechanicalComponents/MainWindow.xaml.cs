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
            db.SetConnectionString(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=MechanicalComponentsDatabase;Integrated Security=True;Connect Timeout=15;");
            db.CreateConnection();

            var engines = db.GetNodes(null);
            EnginesTreeView.ItemsSource = engines;
        }

        private void AddEngine_Click(object sender, RoutedEventArgs e)
        {

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

        private void EnginesTreeView_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {

        }
    }
}
