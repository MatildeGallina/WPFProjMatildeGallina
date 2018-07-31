using System.Collections.Generic;
using System;
using System.Data;
using System.Data.SqlClient;

namespace MechanicalComponents.Models
{
    public class Database : IDatabase
    {
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

        public List<INode> GetNodes(int? parentId)
        {
            var nodes = new List<INode>();

            using (var conn = this.CreateConnection())
            using (var comm = conn.CreateCommand())
            {
                conn.Open();

                comm.CommandType = CommandType.Text;
                comm.CommandText = _queryWriter.GetByParentId(parentId);
                UpdateList(comm, nodes);
            }
            return nodes;
        }

        public INode GetNodeById(int id)
        {
            using (var conn = this.CreateConnection())
            using (var comm = conn.CreateCommand())
            {
                conn.Open();

                comm.CommandType = CommandType.Text;
                comm.CommandText = _queryWriter.GetById(id);

                var reader = comm.ExecuteReader();
                reader.Read();
                var node = AddValuesToNode(reader);
                    
                return node;
            }
        }

        public void GetProperties(int id)
        {
            throw new NotImplementedException();
        }

        public List<string> GetSerialCodes()
        {
            var serialCodes = new List<string>();

            using (var conn = this.CreateConnection())
            using (var comm = conn.CreateCommand())
            {
                conn.Open();

                comm.CommandType = CommandType.Text;
                comm.CommandText = _queryWriter.GetSerialCodes();

                var reader = comm.ExecuteReader();
                while (reader.Read())
                {
                    var value = (string)reader["SerialCode"];

                    serialCodes.Add(value);
                }
                return serialCodes;
            }
        }

        public void SetNode(NodeModel node, int? parentId)
        {
            using (var conn = this.CreateConnection())
            using (var comm = conn.CreateCommand())
            {
                conn.Open();

                comm.CommandType = CommandType.Text;
                comm.CommandText = _queryWriter.SetNode(node, parentId);

                node.Id = (int)comm.ExecuteScalar();

                // quando un engine aveva una lista di figli dovevo aggiornargli il parentid
                //foreach(var child in node.Children)
                //{
                //    UpdateParentId(child.Id, node.Id);
                //}
            }
        }
        
        public void DeleteNode(int id)
        {
            using (var conn = this.CreateConnection())
            using (var comm = conn.CreateCommand())
            {
                conn.Open();

                comm.CommandType = CommandType.Text;
                comm.CommandText = _queryWriter.DeleteById(id);

                comm.ExecuteNonQuery();
            }
        }

        public void UpdateParentId(int id, int parentId)
        {
            using (var conn = this.CreateConnection())
            using (var comm = conn.CreateCommand())
            {
                conn.Open();

                comm.CommandType = CommandType.Text;
                comm.CommandText = _queryWriter.UpdateParentId(id, parentId);

                comm.ExecuteNonQuery();
            }
        }

        public void UpdateProperties(int id)
        {
            using (var conn = this.CreateConnection())
            using (var comm = conn.CreateCommand())
            {
                conn.Open();

                comm.CommandType = CommandType.Text;
                comm.CommandText = _queryWriter.UpdateProperties(id);

                comm.ExecuteNonQuery();
            }
        }

        //private void SetChildToSingleChildrenNode(int id, SingleChildrenNode parentNode)
        //{
        //    using (var conn = this.CreateConnection())
        //    using (var comm = conn.CreateCommand())
        //    {
        //        conn.Open();

        //        comm.CommandType = CommandType.Text;
        //        comm.CommandText = _queryWriter.GetById(id);

        //        var reader = comm.ExecuteReader();
        //        reader.Read();
        //        var node = AddValuesToNode(reader);

        //        parentNode.Children = node;
        //    }
        //}

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
