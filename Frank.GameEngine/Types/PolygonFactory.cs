using Frank.GameEngine.Primitives;
using Microsoft.Xna.Framework;

namespace Frank.GameEngine.Types;

public class PolygonFactory
{
    public static IPolygon CreateTriangle(Vector3 A, Vector3 B, Vector3 C) => new Triangle(new Vertex(A), new Vertex(B), new Vertex(C));

    public static IPolygon CreateTetragon(Vector3 A, Vector3 B, Vector3 C, Vector3 D) => new Tetragon(new Vertex(A), new Vertex(B), new Vertex(C), new Vertex(D));
    
    public static IPolygon CreatePentagon(Vector3 A, Vector3 B, Vector3 C, Vector3 D, Vector3 E) => new Pentagon(new Vertex(A), new Vertex(B), new Vertex(C), new Vertex(D), new Vertex(E));
    
    public static IPolygon CreateHexagon(Vector3 A, Vector3 B, Vector3 C, Vector3 D, Vector3 E, Vector3 F) => new Hexagon(new Vertex(A), new Vertex(B), new Vertex(C), new Vertex(D), new Vertex(E), new Vertex(F));
    
    public static IPolygon CreateHeptagon(Vector3 A, Vector3 B, Vector3 C, Vector3 D, Vector3 E, Vector3 F, Vector3 G) => new Heptagon(new Vertex(A), new Vertex(B), new Vertex(C), new Vertex(D), new Vertex(E), new Vertex(F), new Vertex(G));
    
    public static IPolygon CreateOctagon(Vector3 A, Vector3 B, Vector3 C, Vector3 D, Vector3 E, Vector3 F, Vector3 G, Vector3 H) => new Octagon(new Vertex(A), new Vertex(B), new Vertex(C), new Vertex(D), new Vertex(E), new Vertex(F), new Vertex(G), new Vertex(H));
    
    public static IPolygon CreateNonagon(Vector3 A, Vector3 B, Vector3 C, Vector3 D, Vector3 E, Vector3 F, Vector3 G, Vector3 H, Vector3 I) => new Nonagon(new Vertex(A), new Vertex(B), new Vertex(C), new Vertex(D), new Vertex(E), new Vertex(F), new Vertex(G), new Vertex(H), new Vertex(I));
    
    public static IPolygon CreateDecagon(Vector3 A, Vector3 B, Vector3 C, Vector3 D, Vector3 E, Vector3 F, Vector3 G, Vector3 H, Vector3 I, Vector3 J) => new Decagon(new Vertex(A), new Vertex(B), new Vertex(C), new Vertex(D), new Vertex(E), new Vertex(F), new Vertex(G), new Vertex(H), new Vertex(I), new Vertex(J));
    
    public static IPolygon CreateHendecagon(Vector3 A, Vector3 B, Vector3 C, Vector3 D, Vector3 E, Vector3 F, Vector3 G, Vector3 H, Vector3 I, Vector3 J, Vector3 K) => new Hendecagon(new Vertex(A), new Vertex(B), new Vertex(C), new Vertex(D), new Vertex(E), new Vertex(F), new Vertex(G), new Vertex(H), new Vertex(I), new Vertex(J), new Vertex(K));
    
    public static IPolygon CreateDodecagon(Vector3 A, Vector3 B, Vector3 C, Vector3 D, Vector3 E, Vector3 F, Vector3 G, Vector3 H, Vector3 I, Vector3 J, Vector3 K, Vector3 L) => new Dodecagon(new Vertex(A), new Vertex(B), new Vertex(C), new Vertex(D), new Vertex(E), new Vertex(F), new Vertex(G), new Vertex(H), new Vertex(I), new Vertex(J), new Vertex(K), new Vertex(L));
    
    public static IPolygon CreateTriskaidecagon(Vector3 A, Vector3 B, Vector3 C, Vector3 D, Vector3 E, Vector3 F, Vector3 G, Vector3 H, Vector3 I, Vector3 J, Vector3 K, Vector3 L, Vector3 M) => new Triskaidecagon(new Vertex(A), new Vertex(B), new Vertex(C), new Vertex(D), new Vertex(E), new Vertex(F), new Vertex(G), new Vertex(H), new Vertex(I), new Vertex(J), new Vertex(K), new Vertex(L), new Vertex(M));
}