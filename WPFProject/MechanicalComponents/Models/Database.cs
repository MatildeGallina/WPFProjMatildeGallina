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
        public void SetConnectionString(string connectionString)
        {
            _connectionString = connectionString;
        }
        private string _connectionString { get; set; }

        private List<Node> _nodes;

        public SqlConnection CreateConnection()
        {
            return new SqlConnection(_connectionString);
        }

        public List<Node> GetNodes()
        {
            throw new NotImplementedException();
        }

        public Node GetInformations()
        {
            throw new NotImplementedException();
        }

        public void SetNode()
        {
            throw new NotImplementedException();
        }

        private  Database() { }

        static Database()
        {
            Instance = new Database();
        }
        private static Database Instance { get; }
    }
}
