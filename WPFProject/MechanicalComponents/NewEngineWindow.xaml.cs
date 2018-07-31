using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    public partial class NewEngineWindow : Window
    {
        internal NodeModel _engine = new NodeModel();
        private IDatabase _database;

        public NewEngineWindow()
        {
            _engine = new NodeModel();
            _database = ConnectionToDatabase();
            InitializeComponent();
        }

        public Database ConnectionToDatabase()
        {
            Database database = new Database();
            database.SetConnectionString(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=MockMechanicalComponentsDatabase;");
            return database;
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            _engine.Name = EngineName.Text;
            _engine.SerialCode = EngineSerialCode.Text;
            _engine.Type = "MultiChildrenNode";
            var validator = new Validator(_database);
            var errors = validator.ValidateNode(_engine);

            if(errors.Count == 0)
            {
                _database.SetNode(_engine, null);

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
            foreach (var child in _engine.Children)
                _database.DeleteNode(child.Id);

            Cleaning();
            this.Close();
        }

        private void Cleaning()
        {
            EngineName.Clear();
            EngineSerialCode.Clear();
        }
    }
}
