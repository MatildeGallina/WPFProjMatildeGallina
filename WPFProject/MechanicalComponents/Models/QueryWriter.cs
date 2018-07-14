using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MechanicalComponents.Models
{
    class QueryWriter : IQueryWriter
    {
        public string GetNodesQuery(int? Id)
        {
            return $"select * from Nodes " +
                $"where Id = {Id} ";
        }

        public string SetNodeQuery(Node node)
        {
            return $"insert into Nodes " +
                $"(Name, SerialCode, ParentId) " +
                //$"OUTPUT Inserted.ID " +
                $"values " +
                $"('{node.Name}', '{node.SerialCode}', {node.ParentId}) ";
        }
    }
}
