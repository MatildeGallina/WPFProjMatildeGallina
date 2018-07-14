using System.Collections.Generic;
using System.Data.SqlClient;

namespace MechanicalComponents.Models
{
    public interface IDatabase
    {
        void SetConnectionString(string connectionString);
        SqlConnection CreateConnection();

        List<Node> GetNodes(int? Id);
        void SetNode(Node node);
    }
}
