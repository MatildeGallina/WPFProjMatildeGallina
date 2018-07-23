using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MechanicalComponents.Models
{
    class NodeQueryWriter : INodeQueryWriter
    {
        public string GetByParentId(int? ParentId)
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

        public string GetById(int Id)
        {
            return "select * from Nodes " +
                $"where Id = {Id}";
        }

        public string SetNode(NodeModel node, int? ParentId)
        {
            string ParentIdValue;

            switch (ParentId)
            {
                case null:
                    ParentIdValue = "NULL";
                    break;
                default:
                    ParentIdValue = ParentId.ToString();
                    break;
            }

            return $"insert into Nodes " +
                $"(Name, SerialCode, ParentId, Type) " +
                $"OUTPUT Inserted.ID " +
                $"values " +
                $"('{node.Name}', '{node.SerialCode}', {ParentIdValue}, '{node.Type}') "; /* {node.ParentId},*/
        }

        public string DeleteById(int Id)
        {
            return $"delete from Nodes where Id = {Id}";
        }
    }
}
