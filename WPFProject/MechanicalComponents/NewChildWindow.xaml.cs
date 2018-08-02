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
    public partial class NewChildWindow : Window
    {
        internal NodeModel child { get; set; }
        private IDatabase _database;

        public NewChildWindow()
        {
            child = new NodeModel();
            _database = ConnectionToDatabase();
            InitializeComponent();
        }

        public Database ConnectionToDatabase()
        {
            Database database = new Database();
            database.SetConnectionString(File.ReadAllText("DatabaseConnectionString.txt"));
            return database;
        }

        private void SaveNewChild_Click(object sender, RoutedEventArgs e)
        {
            child.Name = NewChildName.Text;
            child.SerialCode = NewChildSN.Text;
            child.Type = ReturnRadioType();
            
            var validator = new Validator(_database);
            var errors = validator.ValidateNode(child);

            if (errors.Count == 0)
            {
                _database.SetNode(child, null);

                Cleaning();
                this.Close();
            }
            else
            {
                MessageBox.Show("Operation failed! Invalid name or serialCode value!");
            }
        }

        private void Undo_Click(object sender, RoutedEventArgs e)
        {
            Cleaning();
            this.Close();
        }

        private void Cleaning()
        {
            NewChildName.Clear();
            NewChildSN.Clear();
        }

        private string ReturnRadioType()
        {
            if (Null.IsChecked == true)
                return "NullChildrenNode";

            else if (Single.IsChecked == true)
                return "SingleChildrenNode";

            else
                return "MultiChildrenNode";
        }
    }
}
