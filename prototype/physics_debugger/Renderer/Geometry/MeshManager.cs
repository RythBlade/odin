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

        public int AddPlane(int width, int depth, Vector3 minCorner, Vector3 maxCorner)
        {
            List<Vertex> vertexList = new List<Vertex>(width * depth * 6);

            float xStepSize = (maxCorner.X - minCorner.X) / (float)width;
            float zStepSize = (maxCorner.Z - minCorner.Z) / (float)depth;

            for (int x = 0; x < width; ++x)
            {
                for (int z = 0; z < depth; ++z)
                {
                    float minX = minCorner.X + (float)x * xStepSize;
                    float maxX = minCorner.X + ((float)x + 1.0f) * xStepSize;
                    float minZ = minCorner.Z + (float)z * xStepSize;
                    float maxZ = minCorner.Z + ((float)z + 1.0f) * zStepSize;

                    vertexList.Add(new Vertex(new Vector4(minX, 0.0f, minZ, 1.0f), new Vector4(0.0f, 0.0f, 0.0f, 1.0f)));
                    vertexList.Add(new Vertex(new Vector4(minX, 0.0f, maxZ, 1.0f), new Vector4(0.0f, 0.0f, 0.0f, 1.0f)));
                    vertexList.Add(new Vertex(new Vector4(maxX, 0.0f, maxZ, 1.0f), new Vector4(0.0f, 0.0f, 0.0f, 1.0f)));
                    vertexList.Add(new Vertex(new Vector4(minX, 0.0f, minZ, 1.0f), new Vector4(0.0f, 0.0f, 0.0f, 1.0f)));
                    vertexList.Add(new Vertex(new Vector4(maxX, 0.0f, maxZ, 1.0f), new Vector4(0.0f, 0.0f, 0.0f, 1.0f)));
                    vertexList.Add(new Vertex(new Vector4(maxX, 0.0f, minZ, 1.0f), new Vector4(0.0f, 0.0f, 0.0f, 1.0f)));
                }
            }

            return AddMesh(vertexList);
        }

        public int AddMesh(Vertex[] triangleList)
        {
            CalculateMeshNormals(triangleList);

            Mesh newMesh = new Mesh();
            newMesh.numberOfVertices = triangleList.Length;
            newMesh.vertexBuffer = SharpDX.Direct3D11.Buffer.Create(GraphicsDevice.Instance.Device, BindFlags.VertexBuffer, triangleList);
            newMesh.vertexBufferBinding = new VertexBufferBinding(newMesh.vertexBuffer, Utilities.SizeOf<Vertex>(), 0);

            m_meshes.Add(newMesh);

            return m_meshes.Count > 0 ? m_meshes.Count - 1 : -1;
        }

        private void CalculateMeshNormals(Vertex[] triangleList)
        {
            for( int i = 0; i < triangleList.Length; i += 3)
            {
                Vector4 vertA = triangleList[i].Position;
                Vector4 vertB = triangleList[i + 1].Position;
                Vector4 vertC = triangleList[i + 2].Position;

                Vector4 AtoB = (vertB - vertA);
                Vector4 AtoC = (vertC - vertA);

                Vector3 AtoBVec3 = new Vector3(AtoB.X, AtoB.Y, AtoB.Z);
                Vector3 AtoCVec3 = new Vector3(AtoC.X, AtoC.Y, AtoC.Z);

                Vector3 normal;

                Vector3.Cross(ref AtoBVec3, ref AtoCVec3, out normal);

                normal.Normalize();

                triangleList[i].SetNormal(normal);
                triangleList[i + 1].SetNormal(normal);
                triangleList[i + 2].SetNormal(normal);
            }
        }
    }
}
