using Renderer.Buffers;
using SharpDX;

namespace Renderer.Geometry
{
    public class MeshVertices
    {
        public static Vertex[] s_cube = new Vertex[]
        {
            new Vertex( new Vector4(-1.0f, -1.0f, -1.0f, 1.0f), new Vector4(1.0f, 0.0f, 0.0f, 1.0f) ), // Front
            new Vertex( new Vector4(-1.0f,  1.0f, -1.0f, 1.0f), new Vector4(1.0f, 0.0f, 0.0f, 1.0f) ),
            new Vertex( new Vector4( 1.0f,  1.0f, -1.0f, 1.0f), new Vector4(1.0f, 0.0f, 0.0f, 1.0f) ),
            new Vertex( new Vector4(-1.0f, -1.0f, -1.0f, 1.0f), new Vector4(1.0f, 0.0f, 0.0f, 1.0f) ),
            new Vertex( new Vector4( 1.0f,  1.0f, -1.0f, 1.0f), new Vector4(1.0f, 0.0f, 0.0f, 1.0f) ),
            new Vertex( new Vector4( 1.0f, -1.0f, -1.0f, 1.0f), new Vector4(1.0f, 0.0f, 0.0f, 1.0f) ),

            new Vertex( new Vector4(-1.0f, -1.0f,  1.0f, 1.0f), new Vector4(0.0f, 1.0f, 0.0f, 1.0f) ), // BACK
            new Vertex( new Vector4( 1.0f,  1.0f,  1.0f, 1.0f), new Vector4(0.0f, 1.0f, 0.0f, 1.0f) ),
            new Vertex( new Vector4(-1.0f,  1.0f,  1.0f, 1.0f), new Vector4(0.0f, 1.0f, 0.0f, 1.0f) ),
            new Vertex( new Vector4(-1.0f, -1.0f,  1.0f, 1.0f), new Vector4(0.0f, 1.0f, 0.0f, 1.0f) ),
            new Vertex( new Vector4( 1.0f, -1.0f,  1.0f, 1.0f), new Vector4(0.0f, 1.0f, 0.0f, 1.0f) ),
            new Vertex( new Vector4( 1.0f,  1.0f,  1.0f, 1.0f), new Vector4(0.0f, 1.0f, 0.0f, 1.0f) ),

            new Vertex( new Vector4(-1.0f, 1.0f, -1.0f,  1.0f), new Vector4(0.0f, 0.0f, 1.0f, 1.0f) ), // Top
            new Vertex( new Vector4(-1.0f, 1.0f,  1.0f,  1.0f), new Vector4(0.0f, 0.0f, 1.0f, 1.0f) ),
            new Vertex( new Vector4( 1.0f, 1.0f,  1.0f,  1.0f), new Vector4(0.0f, 0.0f, 1.0f, 1.0f) ),
            new Vertex( new Vector4(-1.0f, 1.0f, -1.0f,  1.0f), new Vector4(0.0f, 0.0f, 1.0f, 1.0f) ),
            new Vertex( new Vector4( 1.0f, 1.0f,  1.0f,  1.0f), new Vector4(0.0f, 0.0f, 1.0f, 1.0f) ),
            new Vertex( new Vector4( 1.0f, 1.0f, -1.0f,  1.0f), new Vector4(0.0f, 0.0f, 1.0f, 1.0f) ),

            new Vertex( new Vector4(-1.0f,-1.0f, -1.0f,  1.0f), new Vector4(1.0f, 1.0f, 0.0f, 1.0f) ), // Bottom
            new Vertex( new Vector4( 1.0f,-1.0f,  1.0f,  1.0f), new Vector4(1.0f, 1.0f, 0.0f, 1.0f) ),
            new Vertex( new Vector4(-1.0f,-1.0f,  1.0f,  1.0f), new Vector4(1.0f, 1.0f, 0.0f, 1.0f) ),
            new Vertex( new Vector4(-1.0f,-1.0f, -1.0f,  1.0f), new Vector4(1.0f, 1.0f, 0.0f, 1.0f) ),
            new Vertex( new Vector4( 1.0f,-1.0f, -1.0f,  1.0f), new Vector4(1.0f, 1.0f, 0.0f, 1.0f) ),
            new Vertex( new Vector4( 1.0f,-1.0f,  1.0f,  1.0f), new Vector4(1.0f, 1.0f, 0.0f, 1.0f) ),

            new Vertex( new Vector4(-1.0f, -1.0f, -1.0f, 1.0f), new Vector4(1.0f, 0.0f, 1.0f, 1.0f) ), // Left
            new Vertex( new Vector4(-1.0f, -1.0f,  1.0f, 1.0f), new Vector4(1.0f, 0.0f, 1.0f, 1.0f) ),
            new Vertex( new Vector4(-1.0f,  1.0f,  1.0f, 1.0f), new Vector4(1.0f, 0.0f, 1.0f, 1.0f) ),
            new Vertex( new Vector4(-1.0f, -1.0f, -1.0f, 1.0f), new Vector4(1.0f, 0.0f, 1.0f, 1.0f) ),
            new Vertex( new Vector4(-1.0f,  1.0f,  1.0f, 1.0f), new Vector4(1.0f, 0.0f, 1.0f, 1.0f) ),
            new Vertex( new Vector4(-1.0f,  1.0f, -1.0f, 1.0f), new Vector4(1.0f, 0.0f, 1.0f, 1.0f) ),

            new Vertex( new Vector4( 1.0f, -1.0f, -1.0f, 1.0f), new Vector4(0.0f, 1.0f, 1.0f, 1.0f) ), // Right
            new Vertex( new Vector4( 1.0f,  1.0f,  1.0f, 1.0f), new Vector4(0.0f, 1.0f, 1.0f, 1.0f) ),
            new Vertex( new Vector4( 1.0f, -1.0f,  1.0f, 1.0f), new Vector4(0.0f, 1.0f, 1.0f, 1.0f) ),
            new Vertex( new Vector4( 1.0f, -1.0f, -1.0f, 1.0f), new Vector4(0.0f, 1.0f, 1.0f, 1.0f) ),
            new Vertex( new Vector4( 1.0f,  1.0f, -1.0f, 1.0f), new Vector4(0.0f, 1.0f, 1.0f, 1.0f) ),
            new Vertex( new Vector4( 1.0f,  1.0f,  1.0f, 1.0f), new Vector4(0.0f, 1.0f, 1.0f, 1.0f) ),
        };

        public static Vertex[] s_tetrahedron = new Vertex[]
        {
            new Vertex( new Vector4(-1.0f, -1.0f, -1.0f, 1.0f), Vector4.Normalize(new Vector4(-1.0f,  1.0f, -1.0f, 1.0f)) ),
            new Vertex( new Vector4(-1.0f,  1.0f,  1.0f, 1.0f), Vector4.Normalize(new Vector4(-1.0f,  1.0f, -1.0f, 1.0f)) ),
            new Vertex( new Vector4( 1.0f,  1.0f, -1.0f, 1.0f), Vector4.Normalize(new Vector4(-1.0f,  1.0f, -1.0f, 1.0f)) ),

            new Vertex( new Vector4(-1.0f, -1.0f, -1.0f, 1.0f), Vector4.Normalize(new Vector4( 1.0f, -1.0f, -1.0f, 1.0f)) ),
            new Vertex( new Vector4( 1.0f,  1.0f, -1.0f, 1.0f), Vector4.Normalize(new Vector4( 1.0f, -1.0f, -1.0f, 1.0f)) ),
            new Vertex( new Vector4( 1.0f, -1.0f,  1.0f, 1.0f), Vector4.Normalize(new Vector4( 1.0f, -1.0f, -1.0f, 1.0f)) ),

            new Vertex( new Vector4( 1.0f,  1.0f, -1.0f, 1.0f), Vector4.Normalize(new Vector4( 1.0f,  1.0f,  1.0f, 1.0f)) ),
            new Vertex( new Vector4(-1.0f,  1.0f,  1.0f, 1.0f), Vector4.Normalize(new Vector4( 1.0f,  1.0f,  1.0f, 1.0f)) ),
            new Vertex( new Vector4( 1.0f, -1.0f,  1.0f, 1.0f), Vector4.Normalize(new Vector4( 1.0f,  1.0f,  1.0f, 1.0f)) ),

            new Vertex( new Vector4(-1.0f, -1.0f, -1.0f, 1.0f), Vector4.Normalize(new Vector4(-1.0f, -1.0f,  1.0f, 1.0f)) ),
            new Vertex( new Vector4( 1.0f, -1.0f,  1.0f, 1.0f), Vector4.Normalize(new Vector4(-1.0f, -1.0f,  1.0f, 1.0f)) ),
            new Vertex( new Vector4(-1.0f,  1.0f,  1.0f, 1.0f), Vector4.Normalize(new Vector4(-1.0f, -1.0f,  1.0f, 1.0f)) ),
        };
    }
}
