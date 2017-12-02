using SharpDX.Windows;
using System.Diagnostics;
using System.Windows.Forms;

namespace Renderer
{
    public partial class Form1 : Form
    {
        //Renderer2 newRenderer = new Renderer2();

        MainRenderer renderer = new MainRenderer();

        

        public Form1()
        {
            InitializeComponent();

            GraphicsDevice.Instance.Initialise(pictureBox1.Handle, pictureBox1.ClientRectangle);
            
            renderer.Initialise();

            //newRenderer.Initialise(pictureBox1, pictureBox1.Handle);
            //newRenderer.Initialise(pictureBox1, pictureBox1.Handle);
            timer1.Start();

            pictureBox1.Resize += PictureBox1_Resize;
        }

        private void PictureBox1_Resize(object sender, System.EventArgs e)
        {
            GraphicsDevice.Instance.ResizeRenderTarget(pictureBox1.ClientSize.Width, pictureBox1.ClientSize.Height);
        }

        private void timer1_Tick(object sender, System.EventArgs e)
        {
            //newRenderer.render();
            renderer.Render();
        }
    }
}
