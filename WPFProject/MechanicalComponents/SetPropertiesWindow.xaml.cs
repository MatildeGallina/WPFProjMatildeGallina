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
    public partial class SetPropertiesWindow : Window
    {
        internal IProperties _properties { get; set; }
        public INode node { get; set; }

        public SetPropertiesWindow(INode node)
        {
            this.node = node;
            InitializeComponent();
            SetVisibility();
        }

        private void SetVisibility()
        {
            switch (node.GetType().ToString())
            {
                case "MechanicalComponents.Models.MultiChildrenNode" :
                    MultiChildrenNode.Visibility = Visibility.Visible;
                    break;
                case "MechanicalComponents.Models.SingleChildrenNode":
                    SingleChildrenNode.Visibility = Visibility.Visible;
                    break;
                case "MechanicalComponents.Models.NullChildrenNode":
                    NullChildrenNode.Visibility = Visibility.Visible;
                    break;
                default:
                    throw new NullReferenceException("Unkonown type");
            }
        }

        private void SaveNewChild_Click(object sender, RoutedEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void Undo_Click(object sender, RoutedEventArgs e)
        {
            Cleaning();
            this.Close();
        }

        private void Cleaning()
        {
            NodeBrand.Clear();
            NodeModel.Clear();
            NodePrice.Clear();
            Maintenance.Clear();
            Warranty.Clear();
            Material.Clear();
            Year.Clear();
        }
    }
}
