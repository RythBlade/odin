using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace Renderer
{
    public partial class DirectXControl : UserControl
    {
        MainRenderer renderer = new MainRenderer();

        public DirectXControl()
        {
            InitializeComponent();

            if(LicenseManager.UsageMode == LicenseUsageMode.Runtime)
            {
                GraphicsDevice.Instance.Initialise(Handle, ClientRectangle);
                renderer.Initialise();
                timer.Start();

                Resize += DirectXControl_Resize;
            }
        }

        private void DirectXControl_Resize(object sender, EventArgs e)
        {
            if (LicenseManager.UsageMode == LicenseUsageMode.Runtime)
            {
                GraphicsDevice.Instance.ResizeRenderTarget(ClientSize.Width, ClientSize.Height);
            }
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            if (LicenseManager.UsageMode == LicenseUsageMode.Runtime)
            {
                renderer.Render();
            }
        }
    }
}
