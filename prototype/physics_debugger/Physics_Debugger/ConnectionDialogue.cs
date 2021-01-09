using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace physics_debugger
{
    public partial class ConnectionDialogue : Form
    {
        public string HostName
        {
            get { return targetTextBox.Text; }
        }

        public int Port
        {
            get
            {
                int result = 0;
                if( int.TryParse(portTextBox.Text, out result) )
                {
                    return result;
                }

                return 0;
            }
        }

        public ConnectionDialogue(string target, int portId)
        {
            InitializeComponent();

            targetTextBox.Text = target;
            portTextBox.Text = portId.ToString();
        }

        private void okButton_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            Close();
        }

        private void thisPcButton_Click(object sender, EventArgs e)
        {
            targetTextBox.Text = "localhost";
        }
    }
}
