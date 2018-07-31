using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MechanicalComponents.Models
{
    class NodeQueryWriter : INodeQueryWriter
    {
        public string GetByParentId(int? parentId)
        {
            switch(parentId)
            {
                case null:
                    return $"select * from Nodes " +
                        $"where ParentId is NULL ";
                default:
                    return $"select * from Nodes " +
                    $"where ParentId = {parentId} ";
            }
        }

        public string GetById(int id)
        {
            return "select * from Nodes " +
                $"where Id = {id}";
        }

        public string GetProperties(int id)
        {
            throw new NotImplementedException();
        }

        public string GetSerialCodes()
        {
            return "select SerialCode from Nodes";
        }

        public string SetNode(NodeModel node, int? parentId)
        {
            var query = $"insert into Nodes " +
                        $"(Name, SerialCode, ParentId, Type) " +
                        $"OUTPUT Inserted.Id " +
                        $"values ";


            switch (parentId)
            {
                case null:
                    return query += $"('{node.Name}', '{node.SerialCode}', NULL, '{node.Type}') ";
                default:
                    return  query += $"('{node.Name}', '{node.SerialCode}', {parentId}, '{node.Type}') ";
            }
        }

        public string DeleteById(int id)
        {
            return $"delete from Nodes where Id = {id}";
        }

        public string UpdateParentId(int id, int parentId)
        {
            return "update Nodes " +
                $"set ParentId = {parentId} " +
                $"where Id = {id}";
        }

        public string UpdateProperties(int id)
        {
            throw new NotImplementedException();
        }
    }
}
