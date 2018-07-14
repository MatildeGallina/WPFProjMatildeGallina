using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MechanicalComponents.Models
{
    class Query : IQueryFactory
    {
        public string GetNodesQuery(int? Id)
        {
            return $"select * from Nodes where Id = {Id}";
        }

        public string SetNodeQuery(Node node)
        {
            return $"insert into Node" +
                $"(Name, SerialCode, ParentId)" +
                $"values" +
                $"('{node.Name}', '{node.SerialCode}', {node.ParentId})";
        }
    }
}
