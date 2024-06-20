using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuokkaDevVSIXTemplates
{
    public partial class NewAggregateForm : Form
    {
        public bool IncludeIRepo{ get; private set; }
        public bool IncludeEvents { get; private set; }
        public bool IncludeFactory { get; private set; }
        public bool IncludeSpec { get; private set; }

        public NewAggregateForm()
        {
            InitializeComponent();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            IncludeIRepo = chkIncludeIRepo.Checked;
            IncludeEvents = chkIncludeEvents.Checked;
            IncludeFactory = chkIncludeFactory.Checked;
            IncludeSpec = chkIncludeSpec.Checked;
            DialogResult = DialogResult.OK;
            Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}
