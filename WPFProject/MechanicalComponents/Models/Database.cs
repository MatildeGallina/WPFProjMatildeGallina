using System.Collections.Generic;
using System;
using System.Data;
using System.Data.SqlClient;

namespace MechanicalComponents.Models
{
    //public class Database : IDatabase
    //{
    //    private List<Node> _nodes { get; set; }
    //    private QueryWriter _queryWriter { get; set; }

    //    public void SetConnectionString(string connectionString)
    //    {
    //        _connectionString = connectionString;
    //    }
    //    private string _connectionString;

    //    public SqlConnection CreateConnection()
    //    {
    //        SqlConnection conn = new SqlConnection(_connectionString);
    //        conn.Open();
    //        return conn;
    //    }

    //    public List<Node> GetNodes(int? ParentId)
    //    {
    //        using (var conn = this.CreateConnection())
    //        using (var comm = conn.CreateCommand())
    //        {
    //            comm.CommandType = CommandType.Text;
    //            comm.CommandText = _queryWriter.GetNodesQuery(ParentId);
    //            UpdateList(comm);
    //        }

    //        return _nodes;
    //    }

    //    public void SetNode(NodeModel node)
    //    {
    //        using (var conn = this.CreateConnection())
    //        using(var comm = this.CreateConnection().CreateCommand())
    //        {
    //            comm.CommandType = CommandType.Text;
    //            comm.CommandText = _queryWriter.SetNodeQuery(node);

    //            comm.ExecuteNonQuery();
    //            //node.Id = (int)comm.ExecuteScalar();
    //        }
    //    }

    //    private void UpdateList(SqlCommand comm)
    //    {
    //        SqlDataReader reader = comm.ExecuteReader();

    //        while (reader.Read())
    //        {
    //            //INode node;
    //            ///*= new INode(this)*/
    //            //{
    //            //    Id = (int)reader["Id"],
    //            //    Name = (string)reader["Name"],
    //            //    SerialCode = (string)reader["SerialCode"],
    //            //    //ParentId = (int?)reader["ParentId"]
    //            //    //IconId 
    //            //};
    //            //_nodes.Add(node);
    //        }
    //    }

    //    public  Database()
    //    {
    //        _nodes = new List<Node>();
    //        _queryWriter = new QueryWriter();
    //    }

    //    static Database()
    //    {
    //        Instance = new Database();
    //    }

    //    private static Database Instance { get; }
    //}

    public class Database : IDatabase
    {
        //internal List<Node> _nodes { get; set; }
        private NodeQueryWriter _queryWriter { get; set; }

        public void SetConnectionString(string connectionString)
        {
            _connectionString = connectionString;
        }
        private string _connectionString;

        public SqlConnection CreateConnection()
        {
            return new SqlConnection(_connectionString);
        }

        #region Singleton
        public Database()
        {
            _queryWriter = new NodeQueryWriter();
        }

        static Database()
        {
            Instance = new Database();
        }

        public static Database Instance { get; }
        #endregion

        public List<INode> GetNodes(int? ParentId)
        {
            var nodes = new List<INode>();
            //try
            //{
                using (var conn = this.CreateConnection())
                using (var comm = conn.CreateCommand())
                {
                    conn.Open();

                    comm.CommandType = CommandType.Text;
                    comm.CommandText = _queryWriter.GetByParentId(ParentId);
                    UpdateList(comm, nodes);

                    conn.Dispose();
                }
                return nodes;
            //}
            //catch(Exception)
            //{
            //    // connessione non riuscita per quanlche motivo
            //    throw new ArgumentException("Connection failed");
            //}
        }

        public INode GetNodeById(int Id)
        {
            try
            {
                using (var conn = this.CreateConnection())
                using (var comm = conn.CreateCommand())
                {
                    conn.Open();

                    comm.CommandType = CommandType.Text;
                    comm.CommandText = _queryWriter.GetById(Id);

                    var reader = comm.ExecuteReader();
                    reader.Read();
                    var node = AddValuesToNode(reader);

                    conn.Dispose();
                    return node;
                }
            }
            catch (Exception)
            {
                // connessione non riuscita per quanlche motivo
                throw new ArgumentException("Connection failed");
            }
}

        public void SetNode(NodeModel node, int? ParentId)
        {
            try
            {
                using (var conn = this.CreateConnection())
                using (var comm = conn.CreateCommand())
                {
                    conn.Open();

                    comm.CommandType = CommandType.Text;
                    comm.CommandText = _queryWriter.SetNode(node, ParentId);

                    node.Id = (int)comm.ExecuteScalar();

                    conn.Dispose();
                }
            }
            catch
            {
                // connessione non riuscita per quanlche motivo
                throw new ArgumentException("Connection failed");
            }
        }
        
        public void DeleteNode(int Id)
        {
            try
            {
                using (var conn = this.CreateConnection())
                using (var comm = conn.CreateCommand())
                {
                    conn.Open();

                    comm.CommandType = CommandType.Text;
                    comm.CommandText = _queryWriter.DeleteById(Id);

                    comm.ExecuteNonQuery();

                    conn.Dispose();
                }
            }
            catch
            {
                // connessione non riuscita per quanlche motivo
                throw new ArgumentException("Connection failed");
            }
        }

        private static void UpdateList(SqlCommand comm, List<INode> nodes)
        {
            using (var reader = comm.ExecuteReader())
            {
                while (reader.Read())
                {
                    INode node = AddValuesToNode(reader);

                    nodes.Add(node);
                }
            }
        }

        private static INode AddValuesToNode(SqlDataReader reader)
        {
            var node = CreateNode(reader);
            node.Id = (int)reader["Id"];
            node.Name = (string)reader["Name"];
            node.SerialCode = (string)reader["SerialCode"];
            return node;
        }

        private static INode CreateNode(SqlDataReader reader)
        {
            switch ((string)reader["Type"])
            {
                case "MultiChildrenNode":
                    return new MultiChildrenNode(Instance);
                case "SingleChildrenNode":
                    return new SingleChildrenNode(Instance);
                case "NullChildrenNode":
                    return new NullChildrenNode(Instance);
                default:
                    throw new ArgumentException("Node type not found");
            }
        }
    }
}
