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
        void SetConnectionString(string connectionString);
        SqlConnection CreateConnection();

        List<Node> GetNodes();
        Node GetInformations();
        void SetNode(); // vuole in input un Node n
    }
}
