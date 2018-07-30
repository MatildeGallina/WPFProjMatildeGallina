using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MechanicalComponents.Models
{
    public interface INode
    {
        int Id { get; set; }
        string Name { get; set; }
        string SerialCode { get; set; }
        int? ParentId { get; set; }
        string Icon { get; set; }
        List<INode> Children { get; }
        IProperties properties { get; set; }

        bool CanHaveChild();
    }
}
