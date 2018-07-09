using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MechanicalComponents.Models
{
    public class DatabaseConnection
    {
        public DatabaseConnection(string connenctionString)
        {
            _connenctionString = connenctionString;
        }

        private QueryWriter _query { get; set; }
        private string _connenctionString { get; set; }


        public SqlConnection CreateConnection()
        {
            var conn = new SqlConnection(_connenctionString);
            conn.Open();

            return conn;
        }
    }
}
