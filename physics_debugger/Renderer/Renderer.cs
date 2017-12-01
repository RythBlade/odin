using SharpDX;
using SharpDX.D3DCompiler;
using SharpDX.Direct3D;
using SharpDX.Direct3D11;
using SharpDX.DXGI;
using System;
using System.Diagnostics;

namespace Renderer
{
    public class Renderer : IDisposable
    {
        private RasterizerState rasterizerState = null;
        private RasterizerStateDescription state = new RasterizerStateDescription();

        VertexShader vertexShader = null;
        PixelShader pixelShader = null;

        InputLayout inputLayout = null;

        SharpDX.Direct3D11.Buffer vertexBuffer = null;
        SharpDX.Direct3D11.Buffer constantBuffer = null;

        Stopwatch clock = new Stopwatch();

        public Renderer()
        {
            state.FillMode = FillMode.Solid;
            state.CullMode = CullMode.Back;
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
                , new InputElement("COLOR",  0, Format.R32G32B32A32_Float, 16, 0)
            };

            inputLayout = new InputLayout(GraphicsDevice.Instance.Device, signature, inputElements);
            
            vertexBuffer = SharpDX.Direct3D11.Buffer.Create(GraphicsDevice.Instance.Device, BindFlags.VertexBuffer, new[]
            {
                new Vector4(-1.0f, -1.0f, -1.0f, 1.0f), new Vector4(1.0f, 0.0f, 0.0f, 1.0f), // Front
                new Vector4(-1.0f,  1.0f, -1.0f, 1.0f), new Vector4(1.0f, 0.0f, 0.0f, 1.0f),
                new Vector4( 1.0f,  1.0f, -1.0f, 1.0f), new Vector4(1.0f, 0.0f, 0.0f, 1.0f),
                new Vector4(-1.0f, -1.0f, -1.0f, 1.0f), new Vector4(1.0f, 0.0f, 0.0f, 1.0f),
                new Vector4( 1.0f,  1.0f, -1.0f, 1.0f), new Vector4(1.0f, 0.0f, 0.0f, 1.0f),
                new Vector4( 1.0f, -1.0f, -1.0f, 1.0f), new Vector4(1.0f, 0.0f, 0.0f, 1.0f),

                new Vector4(-1.0f, -1.0f,  1.0f, 1.0f), new Vector4(0.0f, 1.0f, 0.0f, 1.0f), // BACK
                new Vector4( 1.0f,  1.0f,  1.0f, 1.0f), new Vector4(0.0f, 1.0f, 0.0f, 1.0f),
                new Vector4(-1.0f,  1.0f,  1.0f, 1.0f), new Vector4(0.0f, 1.0f, 0.0f, 1.0f),
                new Vector4(-1.0f, -1.0f,  1.0f, 1.0f), new Vector4(0.0f, 1.0f, 0.0f, 1.0f),
                new Vector4( 1.0f, -1.0f,  1.0f, 1.0f), new Vector4(0.0f, 1.0f, 0.0f, 1.0f),
                new Vector4( 1.0f,  1.0f,  1.0f, 1.0f), new Vector4(0.0f, 1.0f, 0.0f, 1.0f),

                new Vector4(-1.0f, 1.0f, -1.0f,  1.0f), new Vector4(0.0f, 0.0f, 1.0f, 1.0f), // Top
                new Vector4(-1.0f, 1.0f,  1.0f,  1.0f), new Vector4(0.0f, 0.0f, 1.0f, 1.0f),
                new Vector4( 1.0f, 1.0f,  1.0f,  1.0f), new Vector4(0.0f, 0.0f, 1.0f, 1.0f),
                new Vector4(-1.0f, 1.0f, -1.0f,  1.0f), new Vector4(0.0f, 0.0f, 1.0f, 1.0f),
                new Vector4( 1.0f, 1.0f,  1.0f,  1.0f), new Vector4(0.0f, 0.0f, 1.0f, 1.0f),
                new Vector4( 1.0f, 1.0f, -1.0f,  1.0f), new Vector4(0.0f, 0.0f, 1.0f, 1.0f),

                new Vector4(-1.0f,-1.0f, -1.0f,  1.0f), new Vector4(1.0f, 1.0f, 0.0f, 1.0f), // Bottom
                new Vector4( 1.0f,-1.0f,  1.0f,  1.0f), new Vector4(1.0f, 1.0f, 0.0f, 1.0f),
                new Vector4(-1.0f,-1.0f,  1.0f,  1.0f), new Vector4(1.0f, 1.0f, 0.0f, 1.0f),
                new Vector4(-1.0f,-1.0f, -1.0f,  1.0f), new Vector4(1.0f, 1.0f, 0.0f, 1.0f),
                new Vector4( 1.0f,-1.0f, -1.0f,  1.0f), new Vector4(1.0f, 1.0f, 0.0f, 1.0f),
                new Vector4( 1.0f,-1.0f,  1.0f,  1.0f), new Vector4(1.0f, 1.0f, 0.0f, 1.0f),

                new Vector4(-1.0f, -1.0f, -1.0f, 1.0f), new Vector4(1.0f, 0.0f, 1.0f, 1.0f), // Left
                new Vector4(-1.0f, -1.0f,  1.0f, 1.0f), new Vector4(1.0f, 0.0f, 1.0f, 1.0f),
                new Vector4(-1.0f,  1.0f,  1.0f, 1.0f), new Vector4(1.0f, 0.0f, 1.0f, 1.0f),
                new Vector4(-1.0f, -1.0f, -1.0f, 1.0f), new Vector4(1.0f, 0.0f, 1.0f, 1.0f),
                new Vector4(-1.0f,  1.0f,  1.0f, 1.0f), new Vector4(1.0f, 0.0f, 1.0f, 1.0f),
                new Vector4(-1.0f,  1.0f, -1.0f, 1.0f), new Vector4(1.0f, 0.0f, 1.0f, 1.0f),

                new Vector4( 1.0f, -1.0f, -1.0f, 1.0f), new Vector4(0.0f, 1.0f, 1.0f, 1.0f), // Right
                new Vector4( 1.0f,  1.0f,  1.0f, 1.0f), new Vector4(0.0f, 1.0f, 1.0f, 1.0f),
                new Vector4( 1.0f, -1.0f,  1.0f, 1.0f), new Vector4(0.0f, 1.0f, 1.0f, 1.0f),
                new Vector4( 1.0f, -1.0f, -1.0f, 1.0f), new Vector4(0.0f, 1.0f, 1.0f, 1.0f),
                new Vector4( 1.0f,  1.0f, -1.0f, 1.0f), new Vector4(0.0f, 1.0f, 1.0f, 1.0f),
                new Vector4( 1.0f, 1.0f, 1.0f, 1.0f), new Vector4(0.0f, 1.0f, 1.0f, 1.0f),
            });

            constantBuffer = new SharpDX.Direct3D11.Buffer(
                GraphicsDevice.Instance.Device
                , Utilities.SizeOf<Matrix>()
                , ResourceUsage.Default
                , BindFlags.ConstantBuffer
                , CpuAccessFlags.None
                , ResourceOptionFlags.None
                , 0);
        }

        public void Render()
        {
            var time = clock.ElapsedMilliseconds / 1000.0f;

            // Clear the views
            GraphicsDevice.Instance.ClearAndSetMainRenderTarget();

            Matrix viewMatrix = Matrix.LookAtLH(new Vector3(0.0f, 0.0f, -5.0f), new Vector3(0.0f, 0.0f, 0.0f), Vector3.UnitY);
            // Setup new projection matrix with correct aspect ratio
            Matrix projectionMatrix = Matrix.PerspectiveFovLH((float)Math.PI / 4.0f, GraphicsDevice.Instance.m_viewport.Width / (float)GraphicsDevice.Instance.m_viewport.Height, 0.1f, 100.0f);
            Matrix viewProj = Matrix.Multiply(viewMatrix, projectionMatrix);

            DeviceContext deviceContext = GraphicsDevice.Instance.Context;
            deviceContext.Rasterizer.State = rasterizerState;

            deviceContext.InputAssembler.InputLayout = inputLayout;
            deviceContext.InputAssembler.PrimitiveTopology = PrimitiveTopology.TriangleList;
            deviceContext.InputAssembler.SetVertexBuffers(0, new VertexBufferBinding(vertexBuffer, Utilities.SizeOf<Vector4>() * 2, 0));
            deviceContext.VertexShader.SetConstantBuffer(0, constantBuffer);
            deviceContext.VertexShader.Set(vertexShader);
            deviceContext.PixelShader.Set(pixelShader);

            // Update WorldViewProj Matrix
            Matrix worldViewProj = Matrix.RotationX(time) * Matrix.RotationY(time * 2) * Matrix.RotationZ(time * .7f) * viewProj;
            worldViewProj.Transpose();
            deviceContext.UpdateSubresource(ref worldViewProj, constantBuffer);

            // Draw the cube
            deviceContext.Draw(36, 0);

            GraphicsDevice.Instance.Present();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}
