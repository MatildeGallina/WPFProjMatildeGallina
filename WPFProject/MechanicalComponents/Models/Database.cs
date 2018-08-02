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
        
        public Database()
        {
            _queryWriter = new NodeQueryWriter();
        }

        static Database()
        {
            Instance = new Database();
        }

        public static Database Instance { get; }

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

        public INode GetProperties(INode node)
        {
            using (var conn = this.CreateConnection())
            using (var comm = conn.CreateCommand())
            {
                conn.Open();

                comm.CommandType = CommandType.Text;
                comm.CommandText = _queryWriter.GetProperties(node);

                var reader = comm.ExecuteReader();
                reader.Read();
                node = AddPropertiesToNode(reader, node);

                return node;
            }
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

        public void DeleteNodeByParentId(int parentId)
        {
            using (var conn = this.CreateConnection())
            using (var comm = conn.CreateCommand())
            {
                conn.Open();

                comm.CommandType = CommandType.Text;
                comm.CommandText = _queryWriter.DeleteByParentId(parentId);

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

        public void UpdateProperties(INode node)
        {
            using (var conn = this.CreateConnection())
            using (var comm = conn.CreateCommand())
            {
                conn.Open();

                comm.CommandType = CommandType.Text;
                comm.CommandText = _queryWriter.UpdateProperties(node);

                comm.ExecuteNonQuery();
            }
        }

        private void UpdateList(SqlCommand comm, List<INode> nodes)
        {
            using (var reader = comm.ExecuteReader())
            {
                while (reader.Read())
                {
                    var node = AddPropertiesToNode(reader, AddValuesToNode(reader));

                    nodes.Add(node);
                }
            }
        }

        internal static INode AddValuesToNode(SqlDataReader reader)
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

        internal INode AddPropertiesToNode(SqlDataReader reader, INode node)
        {
            var nodeType = node.GetType().Name;
            switch (nodeType)
            {
                case "MultiChildrenNode":
                    ReaderPropertiesOnMultiChildrenNode((MultiChildrenNode)node, reader);
                    break;
                case "SingleChildrenNode":
                    ReaderPropertiesOnSingleChildrenNode((SingleChildrenNode)node, reader);
                    break;
                case "NullChildrenNode":
                    ReaderPropertiesOnNullChildrenNode((NullChildrenNode)node, reader);
                    break;
                default:
                    throw new ArgumentException("Type not found");
            }

            return node;
        }

        private void ReaderPropertiesOnMultiChildrenNode(MultiChildrenNode node, SqlDataReader reader)
        {
            if (reader["Brand"] == DBNull.Value)
                node.properties.Brand = null;
            else
                node.properties.Brand = (string)reader["Brand"];

            if (reader["Model"] == DBNull.Value)
                node.properties.Model = null;
            else
                node.properties.Model = (string)reader["Model"];

            if (reader["Price"] == DBNull.Value)
                node.properties.Price = null;
            else
                node.properties.Price = (double?)reader["Price"];

            if (reader["FreeMaintenance"] == DBNull.Value)
                node.properties.FreeMaintenance = null;
            else
                node.properties.FreeMaintenance = (int?)reader["FreeMaintenance"];
        }

        private void ReaderPropertiesOnSingleChildrenNode(SingleChildrenNode node, SqlDataReader reader)
        {
            if (reader["Brand"] == DBNull.Value)
                node.properties.Brand = null;
            else
                node.properties.Brand = (string)reader["Brand"];

            if (reader["Model"] == DBNull.Value)
                node.properties.Model = null;
            else
                node.properties.Model = (string)reader["Model"];

            if (reader["Price"] == DBNull.Value)
                node.properties.Price = null;
            else
                node.properties.Price = (double?)reader["Price"];

            if (reader["WarrantyPeriod"] == DBNull.Value)
                node.properties.WarrantyPeriod = null;
            else
                node.properties.WarrantyPeriod = (int?)reader["WarrantyPeriod"];
        }

        private void ReaderPropertiesOnNullChildrenNode(NullChildrenNode node, SqlDataReader reader)
        {
            if (reader["Brand"] == DBNull.Value)
                node.properties.Brand = null;
            else
                node.properties.Brand = (string)reader["Brand"];

            if (reader["Model"] == DBNull.Value)
                node.properties.Model = null;
            else
                node.properties.Model = (string)reader["Model"];

            if (reader["Price"] == DBNull.Value)
                node.properties.Price = null;
            else
                node.properties.Price = (double?)reader["Price"];

            if (reader["Material"] == DBNull.Value)
                node.properties.Material = "";
            else
                node.properties.Material = (string)reader["Material"];

            if (reader["Year"] == DBNull.Value)
                node.properties.Year = null;
            else
                node.properties.Year = (int?)reader["Year"];
        }
    }
}
