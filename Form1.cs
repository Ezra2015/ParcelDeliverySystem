using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace ParcelDeliverySystem
{
    public partial class PDS:Form
    {
        public PDS()
        {
            InitializeComponent();
        }
        public void PDS_Load(object sender, EventArgs e)
        {
        }
        private void closeBtn_Click(object sender, EventArgs e)
        {
            PostageChargeCalculator pcc = new PostageChargeCalculator();
            pcc.removeAllTxts();        
            this.Close();
        }

        private void rateCost_Click(object sender, EventArgs e)
        {
            new PostageRatesDialog().Show();
        }

        private void PDS_FormClosed(object sender, EventArgs e)
        {
            PostageChargeCalculator pcc = new PostageChargeCalculator();
            pcc.removeAllTxts();
        }
    }
}
