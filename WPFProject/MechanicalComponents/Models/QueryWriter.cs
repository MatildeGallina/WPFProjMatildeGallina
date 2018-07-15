using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MechanicalComponents.Models
{
    class QueryWriter : IQueryWriter
    {
        public string GetNodesQuery(int? ParentId)
        {
            switch(ParentId)
            {
                case null:
                    return $"select * from Nodes " +
                        $"where ParentId is NULL ";
                default:
                    return $"select * from Nodes " +
                    $"where ParentId = {ParentId} ";
            }

            
        }

        public string SetNodeQuery(NodeModel node)
        {
            return $"insert into Nodes " +
                $"(Name, SerialCode, ParentId) " +
                //$"OUTPUT Inserted.ID " +
                $"values " +
                $"('{node.Name}', '{node.SerialCode}', {node.ParentId}) ";
        }
    }
}
