using System;
using System.Collections.Generic;
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

        public NewChildWindow()
        {
            child = new NodeModel();
            InitializeComponent();
        }

        private void SaveNewChild_Click(object sender, RoutedEventArgs e)
        {
            var savableChild = new NodeModel(NewChildName.Text, NewChildSN.Text, ReturnRadioType());
            var validator = new Validator();
            var errors = validator.ValidateNode(savableChild);

            if (errors.Count == 0)
            {
                child.Name = savableChild.Name;
                child.SerialCode = savableChild.SerialCode;
                child.Type = savableChild.Type;
                child.Savable = true;

                Cleaning();
                this.Close();
            }
            else
            {
                MessageBox.Show("Invalid set values!");
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
