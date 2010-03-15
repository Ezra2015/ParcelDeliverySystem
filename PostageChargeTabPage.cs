using System;
using System.Drawing;
using System.ComponentModel;
using System.Windows.Forms;
using System.IO;

public class PostageChargeTabPage : TabPage
{
    private Button calBtn, rstBtn;
    private TextBox txtDeliveryTime, txtweight, txtOrigCountry, txtDestCountry, txtInterCourier;
    private ComboBox DeliveryServiceCombo, CurrencyCombo;
    private Label lbledtc, lblweight, lblDeliveryService, lblOrigCountry, lblDestCountry, lblImmediateCourier, lblCurrency;
    private PostageChargeCalculator pcc;
    private CurrencyConverter cc;
    private EstimatedDeliveryTimeCalculator edtc;
    private IntermediateCourier ic;
    private String EstimatedDeliveryTimeTxtPath = "estimatedDeliveryTime.txt";
    private String currenciesTxtPath = "currencies.txt";
    private String intermediateCouriersTxtPath = "intermediate-couriers.txt";

    public PostageChargeTabPage()
    {
        this.Text = "Postage Charge";
        if (File.Exists(EstimatedDeliveryTimeTxtPath) && File.Exists(currenciesTxtPath)
            && File.Exists(intermediateCouriersTxtPath))
        {
            pcc = new PostageChargeCalculator(this.EstimatedDeliveryTimeTxtPath, this.currenciesTxtPath,
                this.intermediateCouriersTxtPath);
            calBtn = new Button();
            rstBtn = new Button();
            DeliveryServiceCombo = new ComboBox();
            CurrencyCombo = new ComboBox();
            lbledtc = new Label();
            lblweight = new Label();
            lblDeliveryService = new Label();
            lblOrigCountry = new Label();
            lblDestCountry = new Label();
            lblImmediateCourier = new Label();
            lblCurrency = new Label();
            txtDeliveryTime = new TextBox();
            txtweight = new TextBox();
            txtOrigCountry = new TextBox();
            txtDestCountry = new TextBox();
            txtInterCourier = new TextBox();

            calBtn.Text = "Calculate";
            calBtn.Size = new Size(70, 23);
            calBtn.Location = new Point(6, 250);

            rstBtn.Text = "Reset";
            rstBtn.Size = new Size(70,23);
            rstBtn.Location = new Point(135, 250);

            lbledtc.Text = "Estimated Delivery Time:";
            lbledtc.Location = new Point(6, 41);
            txtDeliveryTime.Text = "";
            txtDeliveryTime.ReadOnly = true;
            txtDeliveryTime.Location = new Point(135, 41);

            lblweight.Text = "Weight in grams:";
            lblweight.Location = new Point(6, 78);
            txtweight.Location = new Point(135, 78);

            lblDeliveryService.Text = "Delivery Service";
            lblDeliveryService.Location = new Point(6,117);
            DeliveryServiceCombo.Text = "DHL";
            DeliveryServiceCombo.Location = new Point(135, 117);
            //DeliveryServiceCombo.SelectedIndex = 0;

            lblOrigCountry.Text = "Origin Country:";
            lblOrigCountry.Location = new Point(300, 41);
            txtOrigCountry.ReadOnly = true;
            txtOrigCountry.Location = new Point(410,41);

            lblDestCountry.Text = "Destination Country:";
            lblDestCountry.Size = new Size(110, 23);
            lblDestCountry.Location = new Point(300, 78);
            txtDestCountry.ReadOnly = true;
            txtDestCountry.Location = new Point(410, 78);

            lblImmediateCourier.Text = "Intermediate Courier:";
            lblImmediateCourier.Size = new Size(110, 23);
            lblImmediateCourier.Location = new Point(300, 117);
            txtInterCourier.ReadOnly = true;
            txtInterCourier.Location = new Point(410, 117);

            lblCurrency.Text = "Currency";
            lblCurrency.Location = new Point(200, 160);
            CurrencyCombo.Text = "US";
            CurrencyCombo.Location = new Point(300, 160);
            //CurrencyCombo.SelectedIndex = 0;

            //couriers.SelectedIndexChanged += new EventHandler(couriers_SelectedIndexChanged);
            //calculateBtn.Click += new EventHandler(calculateBtn_Click);
            //locations.SelectedIndexChanged += new EventHandler(locations_SelectedIndexChanged);

            this.Controls.Add(calBtn);
            this.Controls.Add(rstBtn);
            this.Controls.Add(lbledtc);
            this.Controls.Add(txtDeliveryTime);
            this.Controls.Add(lblweight);
            this.Controls.Add(txtweight);
            this.Controls.Add(lblDeliveryService);
            this.Controls.Add(DeliveryServiceCombo);
            this.Controls.Add(lblOrigCountry);
            this.Controls.Add(txtOrigCountry);
            this.Controls.Add(lblDestCountry);
            this.Controls.Add(txtDestCountry);
            this.Controls.Add(lblImmediateCourier);
            this.Controls.Add(txtInterCourier);
            this.Controls.Add(lblCurrency);
            this.Controls.Add(CurrencyCombo);
        }
        else
        {
            Label lblError = new Label();
            lblError.Text = "Some or all of the required database files (" +
                this.EstimatedDeliveryTimeTxtPath + ", " +
                this.intermediateCouriersTxtPath + ", " + this.currenciesTxtPath +
                ") are missing. Feature disabled.";
            lblError.Size = new Size(500, 30);
            lblError.Location = new Point(50, 130);
            this.Controls.Add(lblError);
        }
    }

    private void rstBtn_Click(object sender, EventArgs e)
    {

    }
}