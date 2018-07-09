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
        public abstract string AddForeignKeyValue(int ElementId);
        public abstract string InsertNewElement(string Name, string SerialCode);
        public abstract string RetriveFromDatabase();
    }

    class EngineQueryWriter : QueryWriter
    {
        public override string AddForeignKeyValue(int ElementId)
        {
            throw new NotImplementedException();
        }

        public override string InsertNewElement(string Name, string SerialCode)
        {
            throw new NotImplementedException();
        }

        public override string RetriveFromDatabase()
        {
            throw new NotImplementedException();
        }
    }

    class ComponentQueryWriter : QueryWriter
    {
        public override string AddForeignKeyValue(int ElementId)
        {
            throw new NotImplementedException();
        }

        public override string InsertNewElement(string Name, string SerialCode)
        {
            throw new NotImplementedException();
        }

        public override string RetriveFromDatabase()
        {
            throw new NotImplementedException();
        }
    }

    class ChildQueryWriter : QueryWriter
    {
        public override string AddForeignKeyValue(int ElementId)
        {
            throw new NotImplementedException();
        }

        public override string InsertNewElement(string Name, string SerialCode)
        {
            throw new NotImplementedException();
        }

        public override string RetriveFromDatabase()
        {
            throw new NotImplementedException();
        }
    }
}
