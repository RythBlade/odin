using Renderer.Buffers;
using Renderer.Geometry;
using Renderer.Lighting;
using SharpDX;
using SharpDX.D3DCompiler;
using SharpDX.Direct3D;
using SharpDX.Direct3D11;
using SharpDX.DXGI;
using System;
using System.Collections.Generic;

namespace Renderer
{
    public class MainRenderer : IDisposable
    {
        private RasterizerState rasterizerStateFill = null;
        private RasterizerState rasterizerStateWireFrame = null;
        private RasterizerStateDescription rasterizerStateDescFill = new RasterizerStateDescription();
        private RasterizerStateDescription rasterizerStateDescWireframe = new RasterizerStateDescription();


        VertexShader vertexShader = null;
        PixelShader pixelShader = null;

        InputLayout inputLayout = null;

        SharpDX.Direct3D11.Buffer perObjectConstantBuffer = null;
        SharpDX.Direct3D11.Buffer perRenderConstantBuffer = null;
        
        public Cameras.FirstPersonCamera Camera { get; }

        public List<RenderInstance> InstanceList { get; }

        public MeshManager Meshes { get; }

        public LightSettings LightSettings { get; set; }

        public MainRenderer()
        {
            LightSettings = new LightSettings();
            Meshes = new MeshManager();
            InstanceList = new List<RenderInstance>();
            Camera = new Cameras.FirstPersonCamera();

            rasterizerStateDescFill.FillMode = FillMode.Solid;
            rasterizerStateDescFill.CullMode = CullMode.Back;

            rasterizerStateDescWireframe.FillMode = FillMode.Wireframe;
            rasterizerStateDescWireframe.CullMode = CullMode.None;

            // Setup new projection matrix with correct aspect ratio
            Camera.CameraPosition = new Vector3(0.0f, 0.0f, 0.0f);
            Camera.NearClipPlane = 0.1f;
            Camera.FarClipPlane = 100.0f;
            Camera.FieldOfView = (float)Math.PI / 4.0f;
            Camera.BackBufferResolution = new Vector2(GraphicsDevice.Instance.m_viewport.Width, GraphicsDevice.Instance.m_viewport.Height);

            Camera.SetMatrices();
        }

        public void Initialise()
        {
            rasterizerStateFill = new RasterizerState(GraphicsDevice.Instance.Device, rasterizerStateDescFill);
            rasterizerStateWireFrame = new RasterizerState(GraphicsDevice.Instance.Device, rasterizerStateDescWireframe);

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
            // Clear the views
            GraphicsDevice.Instance.ClearAndSetMainRenderTarget();
            GraphicsDevice.Instance.SetRenderTargetViews();

            Camera.BackBufferResolution = new Vector2(GraphicsDevice.Instance.m_viewport.Width, GraphicsDevice.Instance.m_viewport.Height);

            Camera.SetMatrices();

            DeviceContext deviceContext = GraphicsDevice.Instance.Context;

            PerRenderConstantBuffer perRenderConstantBufferData = new PerRenderConstantBuffer();
            perRenderConstantBufferData.ViewProject = Matrix.Multiply(Camera.ViewMatrix, Camera.ProjectionMatrix);
            perRenderConstantBufferData.ViewPosition.X = Camera.CameraPosition.X;
            perRenderConstantBufferData.ViewPosition.Y = Camera.CameraPosition.Y;
            perRenderConstantBufferData.ViewPosition.Z = Camera.CameraPosition.Z;

            perRenderConstantBufferData.ViewProject.Transpose();
            perRenderConstantBufferData.LightColour = LightSettings.LightColour;
            perRenderConstantBufferData.LightDirection = LightSettings.LightDirection;
            deviceContext.UpdateSubresource(ref perRenderConstantBufferData, perRenderConstantBuffer);

            deviceContext.InputAssembler.InputLayout = inputLayout;
            deviceContext.InputAssembler.PrimitiveTopology = PrimitiveTopology.TriangleList;

            deviceContext.VertexShader.SetConstantBuffer(0, perRenderConstantBuffer);
            deviceContext.VertexShader.SetConstantBuffer(1, perObjectConstantBuffer);
            deviceContext.VertexShader.Set(vertexShader);

            deviceContext.PixelShader.SetConstantBuffer(0, perRenderConstantBuffer);
            deviceContext.PixelShader.SetConstantBuffer(1, perObjectConstantBuffer);
            deviceContext.PixelShader.Set(pixelShader);

            foreach (RenderInstance instance in InstanceList)
            {
                Mesh nextMesh = Meshes.GetMeshInstanceAt(instance.MeshId);
                deviceContext.InputAssembler.SetVertexBuffers(0, nextMesh.vertexBufferBinding);

                switch (instance.Fill)
                {
                    case RenderInstance.FillMode.eWireFrame:
                        deviceContext.Rasterizer.State = rasterizerStateWireFrame;
                        break;
                    case RenderInstance.FillMode.eFill:
                    default:
                        deviceContext.Rasterizer.State = rasterizerStateFill;
                        break;
                }

                // Update world matrix
                PerObjectConstantbuffer constantBuffer = new PerObjectConstantbuffer();
                constantBuffer.worldMatrix = instance.WorldMatrix;
                constantBuffer.worldMatrix.Transpose();
                constantBuffer.ColourTint = instance.Material.ColourTint;
                constantBuffer.AmbientLightStrength = instance.Material.AmbientLightStrength;
                constantBuffer.DiffuseLightStrength = instance.Material.DiffuseLightStrength;
                constantBuffer.SpecularLightStrength = instance.Material.SpecularLightStrength;
                constantBuffer.SpecularShininess = instance.Material.SpecularShininess;
                constantBuffer.userDataValue = instance.UserDataValue;

                deviceContext.UpdateSubresource(ref constantBuffer, perObjectConstantBuffer);

                // Draw the mesh
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
