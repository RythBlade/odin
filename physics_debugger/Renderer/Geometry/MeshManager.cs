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

        public int AddMesh(List<Vertex> triangleList)
        {
            return AddMesh(triangleList.ToArray());
        }

        public int AddCubeMesh()
        {
            return AddMesh(MeshVertices.s_cube);
        }

        public int AddTetrahedron()
        {
            return AddMesh(MeshVertices.s_tetrahedron);
        }

        public int AddPlane()
        {
            return AddMesh(MeshVertices.s_plane);
        }

        public int AddMesh(Vertex[] triangleList)
        {
            Mesh newMesh = new Mesh();
            newMesh.numberOfVertices = triangleList.Length;
            newMesh.vertexBuffer = SharpDX.Direct3D11.Buffer.Create(GraphicsDevice.Instance.Device, BindFlags.VertexBuffer, triangleList);
            newMesh.vertexBufferBinding = new VertexBufferBinding(newMesh.vertexBuffer, Utilities.SizeOf<Vertex>(), 0);

            m_meshes.Add(newMesh);

            return m_meshes.Count > 0 ? m_meshes.Count - 1 : -1;
        }
    }
}
