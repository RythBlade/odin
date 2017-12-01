using SharpDX;
using SharpDX.D3DCompiler;
using SharpDX.Direct3D;
using SharpDX.Direct3D11;
using SharpDX.DXGI;
using SharpDX.Windows;
using System;
using System.Diagnostics;
using System.Windows.Forms;

namespace Renderer
{
    public class Renderer3
    {
        public Renderer3()
        {
        }

        public void Initialise(Form parentForm, IntPtr parentWindowHandle, int width, int height)
        {
            //////////////////////////////////////////////////////////////////////
            //////////////////////////////////////////////////////////////////////
            RenderForm renderForm = new RenderForm("My Test");

            parentWindowHandle = renderForm.Handle;
            parentForm = renderForm;
            width = renderForm.ClientSize.Width;
            height = renderForm.ClientSize.Height;

            RenderControl control = new RenderControl();

            //////////////////////////////////////////////////////////////////////
            //////////////////////////////////////////////////////////////////////

            //SwapChainDescription
            SwapChainDescription description = new SwapChainDescription();
            description.BufferCount = 1;
            description.ModeDescription = new ModeDescription(width, height, new Rational(60, 1), Format.R8G8B8A8_UNorm);
            description.IsWindowed = true;
            description.OutputHandle = parentWindowHandle;
            description.SampleDescription = new SampleDescription(1, 0);
            description.SwapEffect = SwapEffect.Discard;
            description.Usage = Usage.RenderTargetOutput;

            SharpDX.Direct3D11.Device graphicsDevice = null;
            SwapChain swapChain = null;

            SharpDX.Direct3D11.Device.CreateWithSwapChain(
                DriverType.Hardware
                , DeviceCreationFlags.None
                , description
                , out graphicsDevice
                , out swapChain);

            DeviceContext deviceContext = graphicsDevice.ImmediateContext;

            Factory factory = swapChain.GetParent<Factory>();
            factory.MakeWindowAssociation(parentWindowHandle, WindowAssociationFlags.IgnoreAll);

            CompilationResult vertexShaderByteCode = ShaderBytecode.CompileFromFile("Shaders\\MiniCube.fx", "VS", "vs_4_0");
            VertexShader vertexShader = new VertexShader(graphicsDevice, vertexShaderByteCode);

            CompilationResult pixelShaderByteCode = ShaderBytecode.CompileFromFile("Shaders\\MiniCube.fx", "PS", "ps_4_0");
            PixelShader pixelShader = new PixelShader(graphicsDevice, pixelShaderByteCode);

            ShaderSignature signature = ShaderSignature.GetInputSignature(vertexShaderByteCode);

            InputElement[] inputElements = new[]
            {
                new InputElement("POSITION",  0, Format.R32G32B32A32_Float, 0, 0)
                , new InputElement("COLOR",  0, Format.R32G32B32A32_Float, 16, 0)
            };

            InputLayout inputLayout = new InputLayout(graphicsDevice, signature, inputElements);

            SharpDX.Direct3D11.Buffer vertexBuffer = SharpDX.Direct3D11.Buffer.Create(graphicsDevice, BindFlags.VertexBuffer, new[]
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

            SharpDX.Direct3D11.Buffer constantBuffer = new SharpDX.Direct3D11.Buffer(
                graphicsDevice
                , Utilities.SizeOf<Matrix>()
                , ResourceUsage.Default
                , BindFlags.ConstantBuffer
                , CpuAccessFlags.None
                , ResourceOptionFlags.None
                , 0);

            deviceContext.InputAssembler.InputLayout = inputLayout;
            deviceContext.InputAssembler.PrimitiveTopology = PrimitiveTopology.TriangleList;
            deviceContext.InputAssembler.SetVertexBuffers(0, new VertexBufferBinding(vertexBuffer, Utilities.SizeOf<Vector4>() * 2, 0));
            deviceContext.VertexShader.SetConstantBuffer(0, constantBuffer);
            deviceContext.VertexShader.Set(vertexShader);
            deviceContext.PixelShader.Set(pixelShader);

            Matrix viewMatrix = Matrix.LookAtLH(new Vector3(0.0f, 0.0f, -5.0f), new Vector3(0.0f, 0.0f, 0.0f), Vector3.UnitY);
            Matrix projectionMatrix = Matrix.Identity;


            //////////////////////////////////////////////////////////////////////
            //////////////////////////////////////////////////////////////////////
            // Use clock
            var clock = new Stopwatch();
            clock.Start();

            // Declare texture for rendering
            bool userResized = true;
            Texture2D backBuffer = null;
            RenderTargetView renderView = null;
            Texture2D depthBuffer = null;
            DepthStencilView depthView = null;

            // Setup handler on resize form
            renderForm.UserResized += (sender, args) => userResized = true;

            // Setup full screen mode change F5 (Full) F4 (Window)
            renderForm.KeyUp += (sender, args) =>
            {
                if (args.KeyCode == Keys.F5)
                {
                    swapChain.SetFullscreenState(true, null);
                }
                else if (args.KeyCode == Keys.F4)
                {
                    swapChain.SetFullscreenState(false, null);
                }
                else if (args.KeyCode == Keys.Escape)
                {
                    renderForm.Close();
                }
            };

            // Main loop
            RenderLoop.Run(renderForm, () =>
            {
                // If Form resized
                if (userResized)
                {
                    // Dispose all previous allocated resources
                    Utilities.Dispose(ref backBuffer);
                    Utilities.Dispose(ref renderView);
                    Utilities.Dispose(ref depthBuffer);
                    Utilities.Dispose(ref depthView);

                    // Resize the backbuffer
                    swapChain.ResizeBuffers(description.BufferCount, renderForm.ClientSize.Width, renderForm.ClientSize.Height, Format.Unknown, SwapChainFlags.None);

                    // Get the backbuffer from the swapchain
                    backBuffer = Texture2D.FromSwapChain<Texture2D>(swapChain, 0);

                    // Renderview on the backbuffer
                    renderView = new RenderTargetView(graphicsDevice, backBuffer);

                    // Create the depth buffer
                    depthBuffer = new Texture2D(graphicsDevice, new Texture2DDescription()
                    {
                        Format = Format.D32_Float_S8X24_UInt,
                        ArraySize = 1,
                        MipLevels = 1,
                        Width = renderForm.ClientSize.Width,
                        Height = renderForm.ClientSize.Height,
                        SampleDescription = new SampleDescription(1, 0),
                        Usage = ResourceUsage.Default,
                        BindFlags = BindFlags.DepthStencil,
                        CpuAccessFlags = CpuAccessFlags.None,
                        OptionFlags = ResourceOptionFlags.None
                    });

                    // Create the depth buffer view
                    depthView = new DepthStencilView(graphicsDevice, depthBuffer);

                    // Setup targets and viewport for rendering
                    Viewport viewport = new Viewport(0, 0, renderForm.ClientSize.Width, renderForm.ClientSize.Height, 0.0f, 1.0f);
                    deviceContext.Rasterizer.SetViewport(viewport);
                    deviceContext.OutputMerger.SetTargets(depthView, renderView);

                    // Setup new projection matrix with correct aspect ratio
                    projectionMatrix = Matrix.PerspectiveFovLH((float)Math.PI / 4.0f, renderForm.ClientSize.Width / (float)renderForm.ClientSize.Height, 0.1f, 100.0f);

                    // We are done resizing
                    userResized = false;
                }

                var time = clock.ElapsedMilliseconds / 1000.0f;

                var viewProj = Matrix.Multiply(viewMatrix, projectionMatrix);

                // Clear views
                deviceContext.ClearDepthStencilView(depthView, DepthStencilClearFlags.Depth, 1.0f, 0);
                deviceContext.ClearRenderTargetView(renderView, Color.Black);

                // Update WorldViewProj Matrix
                var worldViewProj = Matrix.RotationX(time) * Matrix.RotationY(time * 2) * Matrix.RotationZ(time * .7f) * viewProj;
                worldViewProj.Transpose();
                deviceContext.UpdateSubresource(ref worldViewProj, constantBuffer);

                // Draw the cube
                deviceContext.Draw(36, 0);

                // Present!
                swapChain.Present(0, PresentFlags.None);
            });

            // Release all resources
            signature.Dispose();
            vertexShaderByteCode.Dispose();
            vertexShader.Dispose();
            pixelShaderByteCode.Dispose();
            pixelShader.Dispose();
            vertexBuffer.Dispose();
            inputLayout.Dispose();
            constantBuffer.Dispose();
            depthBuffer.Dispose();
            depthView.Dispose();
            renderView.Dispose();
            backBuffer.Dispose();
            deviceContext.ClearState();
            deviceContext.Flush();
            graphicsDevice.Dispose();
            deviceContext.Dispose();
            swapChain.Dispose();
            factory.Dispose();

            //////////////////////////////////////////////////////////////////////
            //////////////////////////////////////////////////////////////////////
        }
    }
}
