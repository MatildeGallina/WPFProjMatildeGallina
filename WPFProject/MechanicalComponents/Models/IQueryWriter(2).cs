using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MechanicalComponents.Models
{
    public interface IQueryWriter
    {
        string RetriveDataQuery(Node n);
        string FindChildrenComponentsQuery(Node n);
        string AddChildComponentsQuery(Node n, string childName, string childSerialCode);
        string RetriveProperiesQuery(Node n);
        string AddPropertiesQuery(Node n);
        string ChangePropertiesQuery(Node n);
    }
}
