using System.Collections.Generic;
using System.Data.SqlClient;

namespace MechanicalComponents.Models
{
    public interface IDatabase
    {
        void SetConnectionString(string connectionString);
        SqlConnection CreateConnection();

        List<INode> GetNodes(int? Id);
        INode GetNodeById(int Id);
        void SetNode(NodeModel node, int? ParentId);
        void DeleteNode(int Id);
    }
}
