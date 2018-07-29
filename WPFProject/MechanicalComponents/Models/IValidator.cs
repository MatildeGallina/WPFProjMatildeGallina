using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MechanicalComponents.Models
{
    interface IValidator
    {
        List<string> Validate(NodeModel model);
    }
}
