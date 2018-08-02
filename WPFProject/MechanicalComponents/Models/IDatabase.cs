using System.Collections.Generic;
using System.Data.SqlClient;

namespace MechanicalComponents.Models
{
    public interface IDatabase
    {
        void SetConnectionString(string connectionString);
        SqlConnection CreateConnection();

        List<INode> GetNodes(int? id);
        INode GetNodeById(int id);
        INode GetProperties(INode node);
        List<string> GetSerialCodes();
        void SetNode(NodeModel node, int? parentId);
        void DeleteNode(int id);
        void DeleteNodeByParentId(int parentId);
        void UpdateParentId(int id, int parentId);
        void UpdateProperties(INode node);
    }
}
