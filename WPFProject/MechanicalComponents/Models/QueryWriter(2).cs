using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MechanicalComponents.Models
{
    public abstract class QueryWriter : IQueryWriter
    {
        public string RetriveDataQuery(Node n)
        {
            return $"select * from {GetTableName()} where Id = {n.Id}";
        }

        public string FindChildrenComponentsQuery(Node n)
        {
            return $"select c.Name " +
                $"from Components as c " +
                FilterData(n);
        }

        public string AddChildComponentsQuery(Node n, string childName, string childSerialCode)
        {
            Node child = new Node()
            {
                Name = childName,
                SerialCode = childSerialCode,
                QueryWriter = new ComponentQueryWriter()
            };

            return "insert into Components " +
                "(Name, SerialCode, EngineId, IconId, ComponentId) " +
                "values " +
                InsertValues(n, child);
        }

        public abstract string RetriveProperiesQuery(Node n);

        public abstract string AddPropertiesQuery(Node n);

        internal abstract object GetTableName();

        public abstract string ChangePropertiesQuery(Node n);

        internal abstract string FilterData(Node n);

        internal abstract string InsertValues(Node n, Node child);
    }

    class EngineQueryWriter : QueryWriter
    {
        internal override object GetTableName()
        {
            return "Engines ";
        }

        internal override string FilterData(Node n)
        {
            return $"where {n.Id} = c.EngineId";
        }

        internal override string InsertValues(Node n, Node child)
        {
            return $"('{child.Name}', '{child.SerialCode}', {n.Id}, 1, NULL)";
        }

        public override string RetriveProperiesQuery(Node n)
        {
            throw new Exception("An engine has no properties");
        }

        public override string AddPropertiesQuery(Node n)
        {
            throw new Exception("An engine has no properties");
        }

        public override string ChangePropertiesQuery(Node n)
        {
            throw new Exception("An engine has no properties");
        }
    }

    class ComponentQueryWriter : QueryWriter
    { 
        internal override object GetTableName()
        {
            return "Components ";
        }

        internal override string FilterData(Node n)
        {
            return $"where {n.Id} = c.ComponentId";
        }

        internal override string InsertValues(Node n, Node child)
        {
            return $"('{child.Name}', '{child.SerialCode}', NULL, 1, {n.Id})";
        }

        public override string RetriveProperiesQuery(Node n)
        {
            return $"select * from Properties where ComponentId = {n.Id}";
        }

        public override string AddPropertiesQuery(Node n)
        {
            throw new NotImplementedException();

            // implementare usando il pattern null object
        }

        public override string ChangePropertiesQuery(Node n)
        {
            throw new NotImplementedException();

            // implementare usando il pattern null object
        }
    }
}
