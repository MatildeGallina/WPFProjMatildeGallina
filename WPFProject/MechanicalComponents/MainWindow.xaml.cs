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

            var dbConn = new Database(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=MechanicalComponentsDatabase;Integrated Security=True;Connect Timeout=15;");
            var conn = dbConn.CreateConnection();

            EnginesTreeView.ItemsSource = EngineBranches(conn);

            }

        private List<Node> EngineBranches(SqlConnection conn)
        {
            SqlCommand comm = conn.CreateCommand();

            comm.CommandType = CommandType.Text;
            comm.CommandText = "select * from Engines";

            SqlDataReader reader = comm.ExecuteReader();

            List<Node> engines = new List<Node>();

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    Node e = new Node
                    {
                        Id = reader.GetInt32(0),
                        Name = reader.GetString(1),
                        SerialCode = reader.GetString(2)
                    };


                    engines.Add(e);
                }
            }

            return engines;
        }
    }
}
