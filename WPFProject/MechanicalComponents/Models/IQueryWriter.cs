using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MechanicalComponents.Models
{
    public interface IQueryWriter
    {
        string InsertNewElement(string Name, string SerialCode);
        string AddForeignKeyValue(int ElementId);
        string RetriveFromDatabase();
    }
}
