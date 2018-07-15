using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace MechanicalComponents.Models
{
    public class Database : IDatabase
    {
        private List<Node> _nodes { get; set; }
        private QueryWriter _queryWriter { get; set; }

        public void SetConnectionString(string connectionString)
        {
            _connectionString = connectionString;
        }
        private string _connectionString;
        
        public SqlConnection CreateConnection()
        {
            SqlConnection conn = new SqlConnection(_connectionString);
            conn.Open();
            return conn;
        }

        public List<Node> GetNodes(int? ParentId)
        {
            using (var conn = this.CreateConnection())
            using (var comm = conn.CreateCommand())
            {
                comm.CommandType = CommandType.Text;
                comm.CommandText = _queryWriter.GetNodesQuery(ParentId);
                UpdateList(comm);
            }

            return _nodes;
        }
        
        public void SetNode(NodeModel node)
        {
            using (var conn = this.CreateConnection())
            using(var comm = this.CreateConnection().CreateCommand())
            {
                comm.CommandType = CommandType.Text;
                comm.CommandText = _queryWriter.SetNodeQuery(node);

                comm.ExecuteNonQuery();
                //node.Id = (int)comm.ExecuteScalar();
            }
        }

        private void UpdateList(SqlCommand comm)
        {
            SqlDataReader reader = comm.ExecuteReader();

            while (reader.Read())
            {
                Node node = new Node(this)
                {
                    Id = (int)reader["Id"],
                    Name = (string)reader["Name"],
                    SerialCode = (string)reader["SerialCode"],
                    //ParentId = (int?)reader["ParentId"]
                    //IconId 
                };
                _nodes.Add(node);
            }
        }

        public  Database()
        {
            _nodes = new List<Node>();
            _queryWriter = new QueryWriter();
        }

        static Database()
        {
            Instance = new Database();
        }
        
        private static Database Instance { get; }
    }
}
