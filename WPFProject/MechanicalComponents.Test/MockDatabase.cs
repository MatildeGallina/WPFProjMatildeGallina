using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MechanicalComponents.Models;

namespace MechanicalComponents.Test
{
    class MockDatabase : IDatabase
    {
        public MockDatabase()
        {
            _models = new List<Node>
            {
                new Node(this)
                {
                    Id = 1,
                    Name = "Engine mock 1",
                    SerialCode = "AAABBB1234",
                    ParentId = null
                },
                new Node(this)
                {
                    Id = 2,
                    Name = "Engine mock 2",
                    SerialCode = "BBBCCC5678",
                    ParentId = null
                },
                new Node(this)
                {
                    Id = 3,
                    Name = "Component mock 1",
                    SerialCode = "AAA1234BBB",
                    ParentId = 1
                },
                new Node(this)
                {
                    Id = 4,
                    Name = "Component mock 2",
                    SerialCode = "BBB5678CCC",
                    ParentId = 1
                },
                new Node(this)
                {
                    Id = 5,
                    Name = "Component mock 3",
                    SerialCode = "AAA5678BBB",
                    ParentId = 3
                },
                new Node(this)
                {
                    Id = 6,
                    Name = "Component mock 4",
                    SerialCode = "BBB1234CCC",
                    ParentId = 3
                },
                new Node(this)
                {
                    Id = 7,
                    Name = "Component mock 5",
                    SerialCode = "AAA4321BBB",
                    ParentId = 3
                },
                new Node(this)
                {
                    Id = 8,
                    Name = "Component mock 6",
                    SerialCode = "BBB8765CCC",
                    ParentId = 4
                }
            };
        }

        internal List<Node> _models;
        internal bool connectionOpen;

        public void SetConnectionString(string connectionString)
        {
            _connectionString = connectionString;
        }
        private string _connectionString;

        public SqlConnection CreateConnection()
        {
            switch (_connectionString)
            {
                case "correctString":
                    SqlConnection conn = new SqlConnection();
                    conn.Open();
                    connectionOpen = true;
                    return conn;
                default:
                    throw new Exception();
            }
        }

        public List<Node> GetNodes(int? ParentId)
        {
            return _models
                .Select(x => x)
                .Where(n => n.ParentId == ParentId)
                .ToList();
        }

        public void SetNode(NodeModel node)
        {
            Node newNode = new Node(this)
            {
                Name = node.Name,
                SerialCode = node.SerialCode,
                ParentId = node.ParentId
            };
            _models.Add(newNode);
        }
    }
}
