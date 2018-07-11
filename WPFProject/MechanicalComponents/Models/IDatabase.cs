using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MechanicalComponents.Models
{
    interface IDatabase
    {
        SqlConnection CreateConnection();
        void RetriveData(SqlConnection sc, Node n);
        void FindChildren(SqlConnection sc, Node n);
        void AddChild(SqlConnection sc, Node n, string name, string serialcode);
        void AddProrperties(SqlConnection sc, Node n);
        void ChangeProrperties(SqlConnection sc, Node n);
    }
}
