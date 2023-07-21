using System.Numerics;

namespace Frank.GameEngine.Primitives;

public static class EdgeExtensions
{
    /// <summary>
    /// Gets the midpoint of the edge.
    /// </summary>
    /// <param name="edge"></param>
    /// <returns></returns>
    public static Vector3 GetMidpoint(this Edge edge) => new((edge.A.X + edge.B.X) / 2, (edge.A.Y + edge.B.Y) / 2, (edge.A.Z + edge.B.Z) / 2);
    
    /// <summary>
    /// Gets the direction of the edge. The direction is a vector that points from the start of the edge to the end of the edge.
    /// </summary>
    /// <param name="edge"></param>
    /// <returns></returns>
    public static Vector3 GetDirection(this Edge edge) => new(edge.B.X - edge.A.X, edge.B.Y - edge.A.Y, edge.B.Z - edge.A.Z);
    
    /// <summary>
    /// Gets the length of the edge (the distance between the start and end points).
    /// </summary>
    /// <param name="edge"></param>
    /// <returns></returns>
    public static float GetLength(this Edge edge) => Vector3.Distance(edge.A, edge.B);
    
    /// <summary>
    /// Gets the angle of the edge. The angle is the angle between the start and end points.
    /// </summary>
    /// <param name="edge"></param>
    /// <returns></returns>
    public static float GetAngle(this Edge edge) => MathF.Atan2(edge.B.Y - edge.A.Y, edge.B.X - edge.A.X);
    
    /// <summary>
    /// Gets the characteristic length of the edge. The characteristic length is the average length of the edge.
    /// </summary>
    /// <param name="edges"></param>
    /// <returns></returns>
    public static float GetCharacteristicLength(this IEnumerable<Edge> edges)
    {
        var edgeArray = edges as Edge[] ?? edges.ToArray();
        var sumOfSquaredLengths = edgeArray.Sum(edge => edge.GetLength() * edge.GetLength());
        return MathF.Sqrt(sumOfSquaredLengths / edgeArray.Length);
    }
    
    /// <summary>
    /// Gets the normal of the edge. The normal is a vector that is perpendicular to the edge.
    /// </summary>
    /// <param name="edge"></param>
    /// <returns></returns>
    public static Vector3 GetNormal(this Edge edge) => Vector3.Normalize(Vector3.Cross(edge.GetDirection(), Vector3.UnitZ));
    
    /// <summary>
    /// Gets the tangent of the edge. The tangent is a vector that is perpendicular to the normal of the edge.
    /// </summary>
    /// <param name="edge"></param>
    /// <returns></returns>
    public static Vector3 GetTangent(this Edge edge) => Vector3.Normalize(Vector3.Cross(edge.GetDirection(), Vector3.UnitZ));

    /// <summary>
    /// Gets the intersection point of two edges.
    /// </summary>
    /// <param name="edge"></param>
    /// <param name="otherEdges"></param>
    /// <returns></returns>
    public static bool Intersect(this Edge edge, IEnumerable<Edge> otherEdges)
    {
        var isIntersecting = false;
        Parallel.ForEach(otherEdges, otherEdge =>
        {
            if (edge.Intersect(otherEdge, out _))
            {
                isIntersecting = true;
            }
        });
        
        return isIntersecting;
    }
    
    /// <summary>
    /// Gets the intersection point of two edges.
    /// </summary>
    /// <param name="edge"></param>
    /// <param name="otherEdges"></param>
    /// <returns></returns>
    public static IEnumerable<Vector3> GetIntersectionPoints(this Edge edge, IEnumerable<Edge> otherEdges)
    {
        List<Vector3> intersectionPoints = new();
        
        Parallel.ForEach(otherEdges, otherEdge =>
        {
            if (edge.Intersect(otherEdge, out var point) && point.HasValue) intersectionPoints.Add(point.Value);
        });
        
        return intersectionPoints;
    }
    
    /// <summary>
    /// Gets the intersection point of two edges. If the edges do not intersect, the intersection point is null.
    /// </summary>
    /// <param name="edge"></param>
    /// <param name="other"></param>
    /// <param name="intersectionPoint"></param>
    /// <returns></returns>
    public static bool Intersect(this Edge edge, Edge other, out Vector3? intersectionPoint)
    {
        intersectionPoint = new Vector3(0, 0, 0);

        var da = new Vector3(edge.B.X - edge.A.X, edge.B.Y - edge.A.Y, edge.B.Z - edge.A.Z);
        var db = new Vector3(other.B.X - other.A.X, other.B.Y - other.A.Y, other.B.Z - other.A.Z);
        var dc = new Vector3(edge.A.X - other.A.X, edge.A.Y - other.A.Y, edge.A.Z - other.A.Z);

        if (Vector3.Dot(Vector3.Cross(da, db), dc) != 0) // lines are not coplanar
            return false;

        var nda = Vector3.Normalize(da);
        var ndb = Vector3.Normalize(db);

        var dp = Vector3.Dot(nda, ndb);
        if (Math.Abs(dp - 1f) < 0.0001f || Math.Abs(dp + 1f) < 0.0001f)// lines are parallel
            return false;

        var c = Vector3.Cross(da, db);
        var t1 = Vector3.Dot(Vector3.Cross(dc, db), c) / Vector3.Dot(c, c);
        if (t1 < 0 || t1 > 1) // intersection not within first line segment
            return false;

        float t2;
        if (Math.Abs(dp - 1f) < 0.0001f || Math.Abs(dp + 1f) < 0.0001f)
            t2 = (Vector3.Dot(dc, nda) - Vector3.Dot(Vector3.Multiply(other.A - edge.A, ndb), nda)) / (1 - dp * dp);
        else
            t2 = Vector3.Dot(Vector3.Cross(dc, da), c) / Vector3.Dot(c, c);
        if (t2 < 0 || t2 > 1) // intersection not within second line segment
            return false;

        intersectionPoint = new Vector3(edge.A.X + t1 * da.X, edge.A.Y + t1 * da.Y, edge.A.Z + t1 * da.Z);

        return true;
    }
}