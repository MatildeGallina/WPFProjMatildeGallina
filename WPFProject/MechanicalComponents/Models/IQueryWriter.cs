using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MechanicalComponents.Models
{
    interface IQueryFactory
    {
        string GetNodesQuery(int? Id);
        string SetNodeQuery(Node n);
    }
}
