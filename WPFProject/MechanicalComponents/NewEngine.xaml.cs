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
    /// <summary>
    /// Logica di interazione per NewEngine.xaml
    /// </summary>
    public partial class NewEngine : Window
    {
        public NewEngine()
        {
            InitializeComponent();
        }

        private void AddChild_Click(object sender, RoutedEventArgs e)
        {
            PreviewChild.Visibility = Visibility.Visible;
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Undo_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void SaveChild_Click(object sender, RoutedEventArgs e)
        {
            NodeModel child = new NodeModel(ChildName.ToString(), ChildSerialCode.ToString()/*, id del engine*/);
        }

        private void PreSaveChild_Click(object sender, RoutedEventArgs e)
        {

        }

        private void UndoChild_Click(object sender, RoutedEventArgs e)
        {
            ChildName.Clear();
            ChildSerialCode.Clear();

            PreviewChild.Visibility = Visibility.Hidden;
        }
    }
}
