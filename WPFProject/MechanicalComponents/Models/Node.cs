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
        public Node(string name, string serialCode)
        {
            Name = name;
            SerialCode = serialCode;
        }

        internal int Id { get; set; }
        internal string Name { get; set; }
        internal string SerialCode { get; set; }
        internal int ParentId { get; set; }
        internal int IconId { get; set; }
        internal string Query { get; set; }
        
        protected List<Node> Components { get; set; }

        public IQueryWriter QueryWriter { get; set; }
        public IDataReader DataReader { get; set; }
    }
}
