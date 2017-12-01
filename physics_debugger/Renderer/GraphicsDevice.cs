using SharpDX;
using SharpDX.Direct3D;
using SharpDX.Direct3D11;
using SharpDX.DXGI;
using System;

namespace Renderer
{
    public class GraphicsDevice : IDisposable
    {
        private static GraphicsDevice s_singleton = new GraphicsDevice();

        public static GraphicsDevice Instance
        {
            get { return s_singleton; }
        }

        private DeviceContext deviceContext = null;
        private SharpDX.Direct3D11.Device graphicsDevice = null;
        private SwapChain swapChain = null;

        private Texture2D backBuffer = null;
        private RenderTargetView renderTargetView = null;

        private Texture2D depthBuffer = null;
        private DepthStencilView depthView = null;
        
        public Viewport m_viewport = new Viewport(0, 0, 1, 1);

        private Texture2DDescription depthBufferDesc = new Texture2DDescription();
        private SwapChainDescription description = new SwapChainDescription();

        private bool m_isInitialised = false;

        public bool IsInitialised
        {
            get { return m_isInitialised; }
        }

        public DeviceContext Context
        {
            get { return deviceContext; }
        }

        public SharpDX.Direct3D11.Device Device
        {
            get { return graphicsDevice; }
        }

        private GraphicsDevice()
        {
            // swap chain description defaults
            description.BufferCount = 1;
            description.IsWindowed = true;
            description.OutputHandle = IntPtr.Zero;
            description.SampleDescription = new SampleDescription(1, 0);
            description.SwapEffect = SwapEffect.Discard;
            description.Usage = Usage.RenderTargetOutput;

            // depth buffer description defaults
            depthBufferDesc.Format = Format.D32_Float_S8X24_UInt;
            depthBufferDesc.ArraySize = 1;
            depthBufferDesc.MipLevels = 1;
            depthBufferDesc.Width = 1;
            depthBufferDesc.Height = 1;
            depthBufferDesc.SampleDescription = new SampleDescription(1, 0);
            depthBufferDesc.Usage = ResourceUsage.Default;
            depthBufferDesc.BindFlags = BindFlags.DepthStencil;
            depthBufferDesc.CpuAccessFlags = CpuAccessFlags.None;
            depthBufferDesc.OptionFlags = ResourceOptionFlags.None;
        }

        public void Initialise(IntPtr parentWindowHandle, System.Drawing.Rectangle viewport )
        {
            // create a swap chain description
            description.ModeDescription = new ModeDescription(viewport.Width, viewport.Height, new Rational(60, 1), Format.R8G8B8A8_UNorm);
            description.OutputHandle = parentWindowHandle;

            depthBufferDesc.Width = viewport.Width;
            depthBufferDesc.Height = viewport.Height;

            SharpDX.Direct3D11.Device.CreateWithSwapChain(
                DriverType.Hardware
                , DeviceCreationFlags.None
                , description
                , out graphicsDevice
                , out swapChain);

            // we're going to need the context a lot so cache a reference
            deviceContext = graphicsDevice.ImmediateContext;

            // don't allow alt-enter etc as the parent control will manage it
            Factory factory = swapChain.GetParent<Factory>();
            factory.MakeWindowAssociation(parentWindowHandle, WindowAssociationFlags.IgnoreAll); 
            
            // Get the backbuffer from the swapchain
            backBuffer = Texture2D.FromSwapChain<Texture2D>(swapChain, 0);
            renderTargetView = new RenderTargetView(graphicsDevice, backBuffer);
            
            depthBuffer = new Texture2D(graphicsDevice, depthBufferDesc);

            // Create the depth buffer view
            depthView = new DepthStencilView(graphicsDevice, depthBuffer);
            
            // Setup targets and viewport for rendering
            m_viewport = new Viewport(
                viewport.X
                , viewport.Y
                , viewport.Width
                , viewport.Height
                , 0.0f
                , 1.0f);

            m_isInitialised = true;
        }

        public void ResizeRenderTarget(int width, int height)
        {
            // Dispose all previous allocated resources that we're resizing
            Utilities.Dispose(ref backBuffer);
            Utilities.Dispose(ref renderTargetView);
            Utilities.Dispose(ref depthBuffer);
            Utilities.Dispose(ref depthView);

            // Resize the backbuffer
            swapChain.ResizeBuffers(description.BufferCount, width, height, Format.Unknown, SwapChainFlags.None);

            // Get the backbuffer from the swapchain
            backBuffer = Texture2D.FromSwapChain<Texture2D>(swapChain, 0);

            // Renderview on the backbuffer
            renderTargetView = new RenderTargetView(graphicsDevice, backBuffer);

            depthBufferDesc.Width = width;
            depthBufferDesc.Height = height;

            // Create the depth buffer
            depthBuffer = new Texture2D(graphicsDevice, depthBufferDesc);

            // Create the depth buffer view
            depthView = new DepthStencilView(graphicsDevice, depthBuffer);

            // Setup targets and viewport for rendering
            m_viewport.Width = width;
            m_viewport.Height = height;
        }

        public void ClearAndSetMainRenderTarget()
        {
            deviceContext.ClearDepthStencilView(depthView, DepthStencilClearFlags.Depth, 1.0f, 0);
            deviceContext.ClearRenderTargetView(renderTargetView, Color.OrangeRed);
            deviceContext.Rasterizer.SetViewport(m_viewport);
            deviceContext.OutputMerger.SetTargets(depthView, renderTargetView);
        }

        public void Present()
        {
            // Present!
            swapChain.Present(0, PresentFlags.None);
        }

        public void Dispose()
        {
            depthBuffer.Dispose();
            depthView.Dispose();
            renderTargetView.Dispose();
            backBuffer.Dispose();
            swapChain.Dispose();
            deviceContext.Dispose();
            graphicsDevice.Dispose();
        }
    }
}
