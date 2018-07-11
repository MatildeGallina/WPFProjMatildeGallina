using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MechanicalComponents.Models
{
    abstract class MyDataReader : IDataReader
    {
        public void ReadListOfChild(SqlDataReader reader)
        {
            if (reader.HasRows)
            {
                List<Node> nodes = new List<Node>();
                while (reader.Read())
                {
                    Node child = new Node()
                    {
                        Id = reader.GetInt32(0),
                        Name = reader.GetString(1),
                        SerialCode = reader.GetString(2),
                        IconId = reader.GetInt32(4)
                    };

                    nodes.Add(child);
                }
            }
        }

        public void ReadNodeData(SqlDataReader reader)
        {
            throw new NotImplementedException();
        }
    }

    class EnginesDataReader : MyDataReader
    {
    }

    class ComponentsDataReader : MyDataReader
    {
    }
}
