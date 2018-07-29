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
    public class User
    {
        public string Name { get; set; }
    }
    public partial class NewEngineWindow : Window
    {
        private NodeModel _engine { get; set; }
        private ObservableCollection<User> users = new ObservableCollection<User>();


        public NewEngineWindow()
        {
            _engine = new NodeModel();

            InitializeComponent();
            ChildrenListBox.ItemsSource = users;
        }

        private void AddChild_Click(object sender, RoutedEventArgs e)
        {
            var childWindow = new NewChildWindow();
            childWindow.ShowDialog();

            var newChild = childWindow.child;
            newChild.ParentId = _engine.Id; // forse questo passaggio lo devo fare nella query di inserimento
            _engine.Children.Add(newChild);
        }

        private void RemoveChild_Click(object sender, RoutedEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void SetProperties_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            throw new NotImplementedException();
            // come condividere la connessione al database tra la MainWindow e questa finestra per permettere di
            // salvare il NodeModel con la stessa stringa di connessione al database
        }

        private void Undo_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
