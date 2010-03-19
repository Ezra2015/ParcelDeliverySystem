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
    public partial class fmPCR : Form
    {
        public fmPCR()
        {
            InitializeComponent();
        }

        public Label OriginCountry
        {
            get
            {
                return this.lblOriginCountry;
            }

            set
            {
                this.lblOriginCountry = value;
            }
        }

        public Label DestCountry
        {
            get
            {
                return this.lblDestCountry;
            }

            set
            {
                this.lblDestCountry = value;
            }
        }

        public Label InterCourier
        {
            get
            {
                return this.lblInterCourier;
            }

            set
            {
                this.lblInterCourier = value;
            }
        }

        public Label DeliveryService
        {
            get
            {
                return this.lblDelService;
            }

            set
            {
                this.lblDelService = value;
            }
        }

        public Label EstimatedDaysToDeliver
        {
            get
            {
                return this.lblEdt;
            }

            set
            {
                this.lblEdt = value;
            }
        }

        public Label Weight
        {
            get
            {
                return this.lblWeight ;
            }

            set
            {
                this.lblWeight = value;
            }
        }

        public Label StartDate
        {
            get
            {
                return this.lblStartDate;
            }

            set
            {
                this.lblStartDate = value;
            }
        }

        public Label ExpectedDate
        {
            get
            {
                return this.lblExpectedDate;
            }

            set
            {
                this.lblExpectedDate = value;
            }
        }

        public Label Currency
        {
            get
            {
                return this.lblCurrency;
            }

            set
            {
                this.lblCurrency = value;
            }
        }

        public Label TotalCost
        {
            get
            {
                return this.lblTotalCost;
            }

            set
            {
                this.lblTotalCost = value;
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Dispose(true);
        }
    }
}
