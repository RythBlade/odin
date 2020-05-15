using Renderer.Buffers;
using SharpDX;
using SharpDX.Direct3D11;
using System.Collections.Generic;

namespace Renderer.Geometry
{
    public struct Mesh
    {
        public SharpDX.Direct3D11.Buffer vertexBuffer;
        public VertexBufferBinding vertexBufferBinding;
        public int numberOfVertices;
    }

    public class MeshManager
    {
        List<Mesh> m_meshes = new List<Mesh>();

        public MeshManager()
        {
        }

        public Mesh GetMeshInstanceAt(int index)
        {
            return m_meshes[index];
        }

        public int AddCubeMesh()
        {
            Mesh newMesh = new Mesh();
            newMesh.numberOfVertices = MeshVertices.s_cube.Length;
            newMesh.vertexBuffer = SharpDX.Direct3D11.Buffer.Create(GraphicsDevice.Instance.Device, BindFlags.VertexBuffer, MeshVertices.s_cube);
            newMesh.vertexBufferBinding = new VertexBufferBinding(newMesh.vertexBuffer, Utilities.SizeOf<Vertex>(), 0);

            m_meshes.Add(newMesh);

            return m_meshes.Count > 0 ? m_meshes.Count - 1 : -1;
        }

        public int AddTetrahedron()
        {
            Mesh newMesh = new Mesh();
            newMesh.numberOfVertices = MeshVertices.s_tetrahedron.Length;
            newMesh.vertexBuffer = SharpDX.Direct3D11.Buffer.Create(GraphicsDevice.Instance.Device, BindFlags.VertexBuffer, MeshVertices.s_tetrahedron);
            newMesh.vertexBufferBinding = new VertexBufferBinding(newMesh.vertexBuffer, Utilities.SizeOf<Vertex>(), 0);

            m_meshes.Add(newMesh);

            return m_meshes.Count > 0 ? m_meshes.Count - 1 : -1;
        }
    }
}
