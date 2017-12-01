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
    public class Renderer2
    {
        DeviceContext deviceContext;
        SharpDX.Direct3D11.Device graphicsDevice = null;
        SwapChain swapChain = null;
        VertexShader vertexShader;
        PixelShader pixelShader;
        InputLayout inputLayout;
        SharpDX.Direct3D11.Buffer vertexBuffer;
        SharpDX.Direct3D11.Buffer constantBuffer;

        Texture2D backBuffer = null;
        RenderTargetView renderView = null;
        Texture2D depthBuffer = null;
        DepthStencilView depthView = null;
        bool userResized;

        Stopwatch clock = new Stopwatch();

        Control m_parentControl;
        SwapChainDescription description = new SwapChainDescription();

        Viewport viewport;

        public Renderer2()
        {
        }

        public void Initialise(Control parentControl, IntPtr parentWindowHandle)
        {
            m_parentControl = parentControl;

            







            

            

            

            

            


            //////////////////////////////////////////////////////////////////////
            //////////////////////////////////////////////////////////////////////
            // Use clock
            
            clock.Start();

            // Declare texture for rendering
            userResized = true;
            

            // Setup handler on resize form
            parentControl.Resize += (sender, args) => userResized = true;
            

            //////////////////////////////////////////////////////////////////////
            //////////////////////////////////////////////////////////////////////
        }

        public void render()
        {
            

            // If Form resized
            if (userResized)
            {
                
                
                // We are done resizing
                userResized = false;
            }

            var time = clock.ElapsedMilliseconds / 1000.0f;
            
        }
    }
}
