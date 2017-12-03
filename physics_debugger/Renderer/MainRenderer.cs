using Renderer.Geometry;
using SharpDX;
using SharpDX.D3DCompiler;
using SharpDX.Direct3D;
using SharpDX.Direct3D11;
using SharpDX.DXGI;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Renderer
{
    public class MainRenderer : IDisposable
    {
        private RasterizerState rasterizerState = null;
        private RasterizerStateDescription state = new RasterizerStateDescription();

        VertexShader vertexShader = null;
        PixelShader pixelShader = null;

        InputLayout inputLayout = null;

        SharpDX.Direct3D11.Buffer vertexBuffer = null;
        SharpDX.Direct3D11.Buffer perObjectConstantBuffer = null;
        SharpDX.Direct3D11.Buffer perRenderConstantBuffer = null;

        Stopwatch clock = new Stopwatch();

        MeshManager meshManager = new MeshManager();
        List<RenderInstance> renderInstanceList = new List<RenderInstance>();

        Cameras.FirstPersonCamera camera = new Cameras.FirstPersonCamera();

        public Cameras.FirstPersonCamera Camera
        {
            get { return camera; }
        }

        public MainRenderer()
        {
            state.FillMode = FillMode.Solid;
            state.CullMode = CullMode.Back;

            // Setup new projection matrix with correct aspect ratio
            camera.CameraPosition = new Vector3(0.0f, 10.0f, 15.0f);
            camera.NearClipPlane = 0.1f;
            camera.FarClipPlane = 100.0f;
            camera.FieldOfView = (float)Math.PI / 4.0f;
            camera.BackBufferResolution = new Vector2(GraphicsDevice.Instance.m_viewport.Width, GraphicsDevice.Instance.m_viewport.Height);

            camera.SetMatrices();
        }

        public void Initialise()
        {
            clock.Start();

            DeviceContext deviceContext = GraphicsDevice.Instance.Context;
            rasterizerState = new RasterizerState(GraphicsDevice.Instance.Device, state);

            CompilationResult vertexShaderByteCode = ShaderBytecode.CompileFromFile("Shaders\\MiniCube.fx", "VS", "vs_4_0");
            vertexShader = new VertexShader(GraphicsDevice.Instance.Device, vertexShaderByteCode);

            CompilationResult pixelShaderByteCode = ShaderBytecode.CompileFromFile("Shaders\\MiniCube.fx", "PS", "ps_4_0");
            pixelShader = new PixelShader(GraphicsDevice.Instance.Device, pixelShaderByteCode);

            ShaderSignature signature = ShaderSignature.GetInputSignature(vertexShaderByteCode);

            InputElement[] inputElements = new[]
            {
                new InputElement("POSITION",  0, Format.R32G32B32A32_Float, 0, 0)
                , new InputElement("NORMAL",  0, Format.R32G32B32A32_Float, 16, 0)
            };

            inputLayout = new InputLayout(GraphicsDevice.Instance.Device, signature, inputElements);

            int meshInstance = meshManager.AddCubeMesh();

            renderInstanceList.Add(new RenderInstance(Matrix.Translation(5.0f, 0.0f, 5.0f), meshInstance));
            renderInstanceList.Add(new RenderInstance(Matrix.Translation(5.0f, 0.0f, -5.0f), meshInstance));
            renderInstanceList.Add(new RenderInstance(Matrix.Translation(-5.0f, 0.0f, 5.0f), meshInstance));
            renderInstanceList.Add(new RenderInstance(Matrix.Translation(-5.0f, 0.0f, -5.0f), meshInstance));
            renderInstanceList.Add(new RenderInstance(Matrix.Translation(0.0f, 0.0f, -5.0f), meshInstance));
            renderInstanceList.Add(new RenderInstance(Matrix.Translation(-5.0f, 0.0f, 0.0f), meshInstance));
            renderInstanceList.Add(new RenderInstance(Matrix.Translation(5.0f, 0.0f, 0.0f), meshInstance));
            renderInstanceList.Add(new RenderInstance(Matrix.Translation(0.0f, 0.0f, 5.0f), meshInstance));
            renderInstanceList.Add(new RenderInstance(Matrix.Translation(0.0f, 0.0f, 0.0f), meshInstance));

            vertexBuffer = SharpDX.Direct3D11.Buffer.Create(GraphicsDevice.Instance.Device, BindFlags.VertexBuffer, MeshVertices.s_cube);

            perObjectConstantBuffer = new SharpDX.Direct3D11.Buffer(
                GraphicsDevice.Instance.Device
                , Utilities.SizeOf<Renderer.Buffers.PerObjectConstantbuffer>()
                , ResourceUsage.Default
                , BindFlags.ConstantBuffer
                , CpuAccessFlags.None
                , ResourceOptionFlags.None
                , 0);

            perRenderConstantBuffer = new SharpDX.Direct3D11.Buffer(
                GraphicsDevice.Instance.Device
                , Utilities.SizeOf<Renderer.Buffers.PerRenderConstantBuffer>()
                , ResourceUsage.Default
                , BindFlags.ConstantBuffer
                , CpuAccessFlags.None
                , ResourceOptionFlags.None
                , 0);
        }

        public void Render()
        {
            var time = clock.ElapsedMilliseconds / 1000.0f;

            for(int i = 0; i < renderInstanceList.Count; ++i)
            {
                Matrix translationMatrix = Matrix.Translation(
                    renderInstanceList[i].WorldMatrix.Row4.X
                    , renderInstanceList[i].WorldMatrix.Row4.Y
                    , renderInstanceList[i].WorldMatrix.Row4.Z);

                // Update world matrix
                renderInstanceList[i].WorldMatrix = Matrix.RotationX(time) * Matrix.RotationY(time * 2) * Matrix.RotationZ(time * .7f) * translationMatrix;
            }
            
            long duration = (1000 * 3);
            long animFrameTime = clock.ElapsedMilliseconds % duration;

            float ratio = (float)animFrameTime / (float)duration;

            camera.Pitch = (MathUtil.Pi / 8.0f) * ratio - (MathUtil.Pi / 4.0f);
            camera.Roll =  (MathUtil.Pi / 8.0f) * ratio;
            camera.Yaw =   (MathUtil.Pi / 8.0f) * ratio;





            // Clear the views
            GraphicsDevice.Instance.ClearAndSetMainRenderTarget();
            
            camera.BackBufferResolution = new Vector2(GraphicsDevice.Instance.m_viewport.Width, GraphicsDevice.Instance.m_viewport.Height);
            
            camera.SetMatrices();
            
            Matrix viewProj = Matrix.Multiply(camera.ViewMatrix, camera.ProjectionMatrix);

            DeviceContext deviceContext = GraphicsDevice.Instance.Context;
            deviceContext.Rasterizer.State = rasterizerState;

            deviceContext.InputAssembler.InputLayout = inputLayout;
            deviceContext.InputAssembler.PrimitiveTopology = PrimitiveTopology.TriangleList;

            deviceContext.VertexShader.SetConstantBuffer(0, perRenderConstantBuffer);
            deviceContext.VertexShader.SetConstantBuffer(1, perObjectConstantBuffer);
            deviceContext.VertexShader.Set(vertexShader);
            deviceContext.PixelShader.Set(pixelShader);

            viewProj.Transpose();
            deviceContext.UpdateSubresource(ref viewProj, perRenderConstantBuffer);

            foreach (RenderInstance instance in renderInstanceList)
            {
                Mesh nextMesh = meshManager.GetMeshInstanceAt(instance.MeshId);
                deviceContext.InputAssembler.SetVertexBuffers(0, nextMesh.vertexBufferBinding);

                // Update world matrix
                Matrix world = instance.WorldMatrix;
                world.Transpose();
                deviceContext.UpdateSubresource(ref world, perObjectConstantBuffer);

                // Draw the cube
                deviceContext.Draw(nextMesh.numberOfVertices, 0);
            }
            
            GraphicsDevice.Instance.Present();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}
