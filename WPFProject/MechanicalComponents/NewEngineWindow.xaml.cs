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

        public NewEngineWindow()
        {
            _engine = new NodeModel();

            InitializeComponent();

            ChildrenListBox.ItemsSource = _engine.Children;
        }

        private void AddChild_Click(object sender, RoutedEventArgs e)
        {
            var childWindow = new NewChildWindow();
            childWindow.ShowDialog();

            var newChild = childWindow.child;
            newChild.ParentId = _engine.Id; // forse questo passaggio lo devo fare nella query di inserimento
            _engine.Children.Add(newChild);
        }

        private void Remove_Click(object sender, RoutedEventArgs e)
        {
            var child = (NodeModel)ChildrenListBox.SelectedItem;
            _engine.Children.Remove(child);
        }

        private void SetProperties_Click(object sender, RoutedEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            var savableEngine = new NodeModel(EngineName.Text, EngineSerialCode.Text, "MultiChildrenNode");
            var validator = new Validator();
            var errors = validator.ValidateNode(savableEngine);

            if(errors.Count == 0)
            {
                _engine.Name = savableEngine.Name;
                _engine.SerialCode = savableEngine.SerialCode;
                _engine.ParentId = null;
                _engine.Type = savableEngine.Type;
                _engine.Savable = true;

                Cleaning();
                this.Close();
            }
            else
            {
                MessageBox.Show("Invalid name or serialCode value!");
            }
        }

        private void Undo_Click(object sender, RoutedEventArgs e)
        {
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
