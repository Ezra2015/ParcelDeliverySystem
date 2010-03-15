using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ParcelDeliverySystem
{
    public partial class EDTresult : Form
    {
        public EDTresult()
        {
            InitializeComponent();
        }

        public Label Labeltest
        {
            get { return this.lbltest; }
            set { this.lbltest = value; }

        }

        private void EDTresult_Load(object sender, EventArgs e)
        {

        }
    }
}
