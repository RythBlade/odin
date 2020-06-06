using System;
using System.Windows.Forms;

namespace physics_debugger.FrameControl
{
    public partial class FrameSelectDialogue : Form
    {
        public int MinimumFrameId { get { return (int)frameSelectUpDown.Minimum; } }
        public int MaximumFrameId { get { return (int)frameSelectUpDown.Maximum; } }

        public int SelectedFrameId 
        {
            get { return (int)frameSelectUpDown.Value; }
            set { frameSelectUpDown.Value = value; }
        }

        public FrameSelectDialogue(int minimumFrameId, int maximumFrameId)
        {
            InitializeComponent();

            frameSelectUpDown.Minimum = minimumFrameId;
            frameSelectUpDown.Maximum = maximumFrameId;

            SelectedFrameId = minimumFrameId;
        }

        private void okButton_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }

        private void FrameSelectDialogue_Shown(object sender, EventArgs e)
        {
            // select all of the text so people who open the dialog can start typing a valid frame number immediately, to overwrite  the default value.
            frameSelectUpDown.Select(0, int.MaxValue);
        }
    }
}
