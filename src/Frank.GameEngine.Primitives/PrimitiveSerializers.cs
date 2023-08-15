using System.Numerics;
using System.Text;

namespace Frank.GameEngine.Primitives;

public static class PrimitiveSerializers
{
    public static class Vector3Serializer
    {
        public static Vector3 Deserialize(string vector)
        {
            var vectorParts = vector.Split('|');
            return new Vector3(
                float.Parse(vectorParts[0].Replace("(", "")),
                float.Parse(vectorParts[1]),
                float.Parse(vectorParts[2].Replace(")", "")));
        }
    
        public static string Serialize(Vector3 vector, string format = "F") 
            => $"({vector.X.ToString(format)}|{vector.Y.ToString(format)}|{vector.Z.ToString(format)})";
    }
    
    public static Vector3 DeserializeVector3(string vector) 
        => Vector3Serializer.Deserialize(vector);
    
    public static string SerializeVector3(Vector3 vector, string format = "F")
        => Vector3Serializer.Serialize(vector, format);

    public static string SerializeVector3s(IEnumerable<Vector3> vectors, string format = "F") 
        => SerializeList(vectors.Select(vector => SerializeVector3(vector, format)), "Vectors");

    public static class EdgeSerializer
    {
        public static Edge Deserialize(string edge)
        {
            var edgeParts = edge.Split('=');
            return new Edge(
                Vector3Serializer.Deserialize(edgeParts[0].Replace("<", "")),
                Vector3Serializer.Deserialize(edgeParts[1].Replace(">", "")));
        }
    
        public static string Serialize(Edge edge, string format = "F") 
            => $"<{Vector3Serializer.Serialize(edge.A, format)}={Vector3Serializer.Serialize(edge.B, format)}>";
    }
    
    public static Edge DeserializeEdge(string edge) 
        => EdgeSerializer.Deserialize(edge);
    
    public static string SerializeEdge(Edge edge, string format = "F")
        => EdgeSerializer.Serialize(edge, format);
    
    public static string SerializeEdges(IEnumerable<Edge> edges, string format = "F") 
        => SerializeList(edges.Select(edge => SerializeEdge(edge, format)), "Edges");

    public static string SerializePolygon(Polygon polygon, string format = "F")
    {
        return "";
    }
    
    
    
    
    
    
    private static string SerializeList(IEnumerable<string> list, string title, string indent = "    ")
    {
        var sb = new StringBuilder();
        sb.AppendLine(title);
        foreach (var item in list) sb.AppendLine($"{indent}{item}");
        return sb.ToString();
    }
    
}