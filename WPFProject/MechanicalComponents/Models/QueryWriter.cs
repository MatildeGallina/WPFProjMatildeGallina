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

        public string GetProperties(INode node)
        {
            var nodeType = node.GetType().Name;
            switch (nodeType)
            {
                case "MultiChildrenNode":
                    return $"select Brand, Model, Price, FreeMaintenance " +
                        $"from Nodes " +
                        $"where Id = ${node.Id}";
                case "SingleChildrenNode":
                    return $"select Brand, Model, Price, WarrantyPeriod " +
                        $"from Nodes " +
                        $"where Id = ${node.Id}";
                case "NullChildrenNode":
                    return $"select Brand, Model, Price, Material, Year " +
                        $"from Nodes " +
                        $"where Id = ${node.Id}";
                default:
                    throw new ArgumentException("Type not found");
            }
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

        public string DeleteByParentId(int parentId)
        {
            return $"delete from Nodes where ParentId = {parentId}";
        }

        public string UpdateParentId(int id, int parentId)
        {
            return "update Nodes " +
                $"set ParentId = {parentId} " +
                $"where Id = {id}";
        }

        public string UpdateProperties(INode node)
        {
            var nodeType = node.GetType().Name;
            switch (nodeType)
            {
                case "MultiChildrenNode":
                    var multiNode = (MultiChildrenNode)node;
                    return "update Nodes " +
                        $"set Brand = '{IsStringEmprty(multiNode.properties.Brand)}', " +
                        $"Model = '{IsStringEmprty(multiNode.properties.Model)}', " +
                        $"Price = {IsDoubleNullValue(multiNode.properties.Price)}, " +
                        $"FreeMaintenance = {IsIntNullValue(multiNode.properties.FreeMaintenance)} " +
                        $"where Id = {multiNode.Id}";
                case "SingleChildrenNode":
                    var singleNode = (SingleChildrenNode)node;
                    return "update Nodes " +
                        $"set Brand = '{IsStringEmprty(singleNode.properties.Brand)}', " +
                        $"Model = '{(singleNode.properties.Model)}', " +
                        $"Price = {IsDoubleNullValue(singleNode.properties.Price)}, " +
                        $"WarrantyPeriod = {IsIntNullValue(singleNode.properties.WarrantyPeriod)} " +
                        $"where Id = {singleNode.Id}";
                case "NullChildrenNode":
                    var nullNode = (NullChildrenNode)node;
                    return "update Nodes " +
                        $"set Brand = '{IsStringEmprty(nullNode.properties.Brand)}', " +
                        $"Model = '{IsStringEmprty(nullNode.properties.Model)}', " +
                        $"Price = {IsDoubleNullValue(nullNode.properties.Price)}, " +
                        $"Material = {IsStringEmprty(nullNode.properties.Material)}, " +
                        $"Year = {IsIntNullValue(nullNode.properties.Year)} " +
                        $"where Id = {nullNode.Id}";
                default:
                    throw new ArgumentException("Type not found");
            }
        }

        private string IsIntNullValue(int? i)
        {
            if (i == null)
                return "NULL";

            return i.ToString();
        }

        private string IsDoubleNullValue(double? d)
        {
            if (d == null)
                return "NULL";

            return d.ToString();
        }

        private string IsStringEmprty(string s)
        {
            if (string.IsNullOrWhiteSpace(s))
                return "NULL";

            return s;
        }
    }
}
