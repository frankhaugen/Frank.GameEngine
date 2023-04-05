using Frank.GameEngine.Primitives;
using Microsoft.Xna.Framework;

namespace Frank.GameEngine.Types;

public class PolygonFactory
{
    public static IPolygon CreateTriangle(Vector3 A, Vector3 B, Vector3 C) => new Triangle(new Vertex(A), new Vertex(B), new Vertex(C));

    public static IPolygon CreateTetragon(Vector3 A, Vector3 B, Vector3 C, Vector3 D) => new Tetragon(new Vertex(A), new Vertex(B), new Vertex(C), new Vertex(D));
    
    public static IPolygon CreateHexagon(Vector3 A, Vector3 B, Vector3 C, Vector3 D, Vector3 E, Vector3 F) => new Hexagon(new Vertex(A), new Vertex(B), new Vertex(C), new Vertex(D), new Vertex(E), new Vertex(F));

    public static IPolygon CreateCube(Vector3 vector3, int i) => new Cube(new Vertex(vector3), i);
    
    public static IPolygon CreateSquare(Vector3 vector3, int i) => new Square(new Vertex(vector3), i);
    
    public static IPolygon CreateSphere(Vector3 vector3, int i, int j) => new Sphere(new Vertex(vector3), i, j);
    
    public static IPolygon CreateMesh(IEnumerable<Vertex> vertices) => new Mesh(vertices);
    
    public static IPolygon CreateTriDecahedron(Vector3 vector3, int i) => new TriDecahedron(new Vertex(vector3), i);
    
    public static IPolygon CreateTorus(Vector3 vector3, int i, int j) => new Torus(vector3, i, j, j);

    public static IPolygon CreatePyramid(Vertex vertex, Vertex vertex1, Vertex vertex2, Vertex vertex3) => new Pyramid(vertex, vertex1, vertex2, vertex3);
}