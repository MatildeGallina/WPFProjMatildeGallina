using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MechanicalComponents.Models
{
    public class Database : IDatabase
    {
        public Database(string connenctionString)
        {
            _connenctionString = connenctionString;
        }

        private string _connenctionString { get; set; }


        public SqlConnection CreateConnection()
        {
            var conn = new SqlConnection(_connenctionString);
            conn.Open();

            return conn;
        }
        
        public void RetriveData(SqlConnection sc, Node n)
        {
            SqlCommand comm = sc.CreateCommand();

            comm.CommandType = CommandType.Text;
            comm.CommandText = n.QueryWriter.RetriveDataQuery(n);

            SqlDataReader reader = comm.ExecuteReader();
            n.DataReader.ReadNodeData(reader);

        }

        public void FindChildren(SqlConnection sc, Node n)
        {
            SqlCommand comm = sc.CreateCommand();

            comm.CommandType = CommandType.Text;
            comm.CommandText = n.QueryWriter.FindChildrenComponentsQuery(n);

            SqlDataReader reader = comm.ExecuteReader();
            n.DataReader.ReadListOfChild(reader);
        }

        public void AddChild(SqlConnection sc, Node n, string name, string serialcode)
        {
            SqlCommand comm = sc.CreateCommand();

            comm.CommandType = CommandType.Text;
            comm.CommandText = n.QueryWriter.AddChildComponentsQuery(n, name, serialcode);

            SqlDataReader reader = comm.ExecuteReader();
        }

        public void AddProrperties(SqlConnection sc, Node n)
        {
            SqlCommand comm = sc.CreateCommand();

            comm.CommandType = CommandType.Text;
            comm.CommandText = n.QueryWriter.AddPropertiesQuery(n);

            SqlDataReader reader = comm.ExecuteReader();
        }

        public void ChangeProrperties(SqlConnection sc, Node n)
        {
            SqlCommand comm = sc.CreateCommand();

            comm.CommandType = CommandType.Text;
            comm.CommandText = n.QueryWriter.ChangePropertiesQuery(n);

            SqlDataReader reader = comm.ExecuteReader();
        }
    }
}
