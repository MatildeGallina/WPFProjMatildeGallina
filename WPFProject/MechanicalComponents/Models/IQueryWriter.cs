using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MechanicalComponents.Models
{
    public interface IQueryWriter
    {
        string GetNodesQuery(int? Id);
        string SetNodeQuery(NodeModel n);
    }
}
