using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MechanicalComponents.Models
{
    public interface IDataReader
    {
        void ReadNodeData(SqlDataReader reader);
        void ReadListOfChild(SqlDataReader reader);
    }
}
