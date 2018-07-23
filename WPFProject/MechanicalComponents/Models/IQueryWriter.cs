using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MechanicalComponents.Models
{
    public interface INodeQueryWriter
    {
        string GetByParentId(int? ParentId);
        string GetById(int Id);
        string SetNode(NodeModel n, int? ParentId);
        string DeleteById(int Id);
    }
}
