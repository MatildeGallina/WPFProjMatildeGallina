using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MechanicalComponents.Models
{
    public class Node
    {
        protected int Id { get; set; }
        protected string Name { get; set; }
        protected string SerialCode { get; set; }
        protected List<Node> Components { get; set; }
        protected IQueryWriter QueryWriter { get; set; }
    }
}
