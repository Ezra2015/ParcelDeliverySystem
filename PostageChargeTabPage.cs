using System;
using System.Drawing;
using System.ComponentModel;
using System.Windows.Forms;
using System.IO;
using formPostage = ParcelDeliverySystem.fmPCR;

public class PostageChargeTabPage : TabPage
{
    private const double maxWeight = 10000;
    private Button calBtn, rstBtn;
    private TextBox txtDeliveryTime, txtweight, txtOrigCountry, txtDestCountry, txtInterCourier;
    private ComboBox DeliveryServiceCombo, CurrencyCombo;
    private Label lbledtc, lblweight, lblDeliveryService, lblOrigCountry, lblDestCountry, lblImmediateCourier, 
        lblCurrency, lblNote;
    private PostageChargeCalculator pcc, icc;
    private IntermediateCourierCalcResult iccr;
    private String EstimatedDeliveryTimeTxtPath = "estimatedDeliveryTime.txt";
    private String currenciesTxtPath = "currencies.txt";
    private String intermediateCouriersTxtPath = "intermediate-couriers.txt";
    private String postageChargeTxtPath = "tempPostageCharge.txt";
    private String intermediateCourierPath = "intermediateCourier.txt";

    public PostageChargeTabPage()
    {
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
        lblNote = new Label();
        txtDeliveryTime = new TextBox();
        txtweight = new TextBox();
        txtOrigCountry = new TextBox();
        txtDestCountry = new TextBox();
        txtInterCourier = new TextBox();
        this.Text = "Postage Charge";
        if (File.Exists(EstimatedDeliveryTimeTxtPath) && File.Exists(currenciesTxtPath)
            && File.Exists(intermediateCouriersTxtPath))
        {
            calBtn.Text = "Calculate";
            calBtn.Size = new Size(70, 23);
            calBtn.Location = new Point(6, 200);
            calBtn.Click += new EventHandler(calBtn_Click);

            rstBtn.Text = "Reset";
            rstBtn.Size = new Size(70, 23);
            rstBtn.Location = new Point(135, 200);
            rstBtn.Click +=new EventHandler(rstBtn_Click);

            lblNote.Text = "Note: Correct details will be shown after you calculate estimated delivery time or intermediate courier.\n" +
                "Original currency type will be USD.";
            lblNote.Location = new Point(6, 250);
            lblNote.Size = new Size(600, 30);

            lbledtc.Text = "Estimated Delivery Time:";
            lbledtc.Location = new Point(6, 41);
            txtDeliveryTime.Text = "";
            txtDeliveryTime.ReadOnly = true;
            txtDeliveryTime.Location = new Point(135, 41);

            lblweight.Text = "Weight in grams:";
            lblweight.Location = new Point(6, 78);
            txtweight.Location = new Point(135, 78);

            lblDeliveryService.Text = "Delivery Service";
            lblDeliveryService.Location = new Point(6, 117);
            DeliveryServiceCombo.Location = new Point(135, 117);
            DeliveryServiceCombo.SelectedIndexChanged += new EventHandler(DeliveryServiceCombo_SelectedIndexChanged);

            lblOrigCountry.Text = "Origin Country:";
            lblOrigCountry.Location = new Point(300, 41);
            txtOrigCountry.ReadOnly = true;
            txtOrigCountry.Location = new Point(410, 41);
            txtOrigCountry.Text = null;

            lblDestCountry.Text = "Destination Country:";
            lblDestCountry.Size = new Size(110, 23);
            lblDestCountry.Location = new Point(300, 78);
            txtDestCountry.ReadOnly = true;
            txtDestCountry.Location = new Point(410, 78);
            txtDestCountry.Text = null;

            lblImmediateCourier.Text = "Intermediate Courier:";
            lblImmediateCourier.Size = new Size(110, 23);
            lblImmediateCourier.Location = new Point(300, 117);
            txtInterCourier.ReadOnly = true;
            txtInterCourier.Location = new Point(410, 117);

            lblCurrency.Text = "Currency";
            lblCurrency.Location = new Point(200, 160);
            CurrencyCombo.Location = new Point(300, 160);

            this.Enter +=new EventHandler(PostageChargeTabPage_Enter);

            this.Controls.Add(calBtn);
            this.Controls.Add(rstBtn);
            this.Controls.Add(lblNote);
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
                ") are missing. Feature disabled.\n";
            lblError.Size = new Size(500, 60);
            lblError.Location = new Point(50, 130);
            this.Controls.Add(lblError);
        }
    }

    void DeliveryServiceCombo_SelectedIndexChanged(object sender, EventArgs e)
    {
        Environment.SpecialFolder mydocument = Environment.SpecialFolder.MyDocuments;
        String folderLocation = Environment.GetFolderPath(mydocument);

        this.txtOrigCountry.Text = pcc.getOriginCountry(this.DeliveryServiceCombo.SelectedItem.ToString());
        this.txtDestCountry.Text = pcc.getDestinationCountry(this.DeliveryServiceCombo.SelectedItem.ToString());
        this.txtDeliveryTime.Text = pcc.getEstimatedDays(this.DeliveryServiceCombo.SelectedItem.ToString());

        if (pcc.getEstimatedDays(this.DeliveryServiceCombo.SelectedItem.ToString()).Equals("Not Available"))
        {
            if (File.Exists(folderLocation + "\\" + this.intermediateCourierPath))
            {
                icc = new PostageChargeCalculator(folderLocation + "\\" + this.intermediateCourierPath);
                this.txtInterCourier.Text = icc.getInterCourierName();
                this.txtDeliveryTime.Text = icc.getDeliveryTime().ToString() + " Days";
                this.txtweight.ReadOnly = true;
                this.txtweight.Text = icc.getComputedWeight().ToString();
                this.rstBtn.Enabled = true;
                this.calBtn.Enabled = true;
                this.CurrencyCombo.Items.Clear();
                this.CurrencyCombo.Items.Add(icc.getCurrencyType());
                this.CurrencyCombo.SelectedIndex = 0;
                this.CurrencyCombo.Enabled = true;
            }
            else
            {
                MessageBox.Show("Please calculate Intermediate Courier as the start delivery in" +
                " this selected delivery service is not available.");
                this.calBtn.Enabled = false;
                this.rstBtn.Enabled = false;
                this.txtweight.ReadOnly = true;
                this.txtweight.Text = "";
                this.CurrencyCombo.Enabled = false;
            }
        }
        else
        {
            this.txtDeliveryTime.Text = pcc.getEstimatedDays(this.DeliveryServiceCombo.SelectedItem.ToString()) +
                " Days";
            this.txtInterCourier.Text = "Not Applicable";
            this.txtweight.ReadOnly = false;
            this.txtweight.Text = "";
            this.DeliveryServiceCombo.Enabled = true;
            this.rstBtn.Enabled = true;
            this.calBtn.Enabled = true;
            pcc.InitCurrencyComboBox(this.CurrencyCombo);
            this.CurrencyCombo.Enabled = true;
            this.txtweight.Enabled = true;
        }
    }

    void PostageChargeTabPage_Enter(object sender, EventArgs e)
    {
        Environment.SpecialFolder mydocument = Environment.SpecialFolder.MyDocuments;
        String folderLocation = Environment.GetFolderPath(mydocument);

        if (File.Exists(folderLocation + "\\" + this.postageChargeTxtPath))
        {
            pcc = new PostageChargeCalculator(this.EstimatedDeliveryTimeTxtPath, this.currenciesTxtPath,
                this.intermediateCouriersTxtPath, folderLocation + "\\" + this.postageChargeTxtPath);

            pcc.InitDeliveryServiceComboBox(this.DeliveryServiceCombo);
            pcc.InitCurrencyComboBox(this.CurrencyCombo);

            this.txtOrigCountry.Text = pcc.getOriginCountry(this.DeliveryServiceCombo.SelectedItem.ToString());
            this.txtDestCountry.Text = pcc.getDestinationCountry(this.DeliveryServiceCombo.SelectedItem.ToString());

            if (pcc.getEstimatedDays(this.DeliveryServiceCombo.SelectedItem.ToString()).Equals("Not Available"))
            {
                if (File.Exists(folderLocation + "\\" + this.intermediateCourierPath))
                {
                    icc = new PostageChargeCalculator(folderLocation + "\\" + this.intermediateCourierPath);
                    this.txtInterCourier.Text = icc.getInterCourierName();
                    this.txtDeliveryTime.Text = icc.getDeliveryTime().ToString() + " Days";
                    this.txtweight.ReadOnly = true;
                    this.txtweight.Text = icc.getComputedWeight().ToString();
                    this.rstBtn.Enabled = true;
                    this.calBtn.Enabled = true;
                    this.CurrencyCombo.Items.Clear();
                    this.CurrencyCombo.Items.Add(icc.getCurrencyType());
                    this.CurrencyCombo.SelectedIndex = 0;
                    this.CurrencyCombo.Enabled = true;
                }
                else
                {
                    MessageBox.Show("Please calculate Intermediate Courier as the start delivery in" +
                    " this selected delivery service is not available.");
                    this.calBtn.Enabled = false;
                    this.rstBtn.Enabled = false;
                    this.txtweight.ReadOnly = true;
                    this.txtweight.Text = "";
                    this.CurrencyCombo.Enabled = false;
                }
            }
            else
            {
                this.txtDeliveryTime.Text = pcc.getEstimatedDays(this.DeliveryServiceCombo.SelectedItem.ToString()) +
                    " Days";
                this.txtInterCourier.Text = "Not Applicable";
                this.txtweight.ReadOnly = false;
                this.txtweight.Text = "";
                this.DeliveryServiceCombo.Enabled = true;
                this.rstBtn.Enabled = true;
                this.calBtn.Enabled = true;
                this.CurrencyCombo.Enabled = true;
                this.txtweight.Enabled = true;
            }
        }
        else
        { 
            MessageBox.Show("You need to compute Estimated Delivery Time first" 
            + " before you procced this.");
            this.calBtn.Enabled = false;
            this.rstBtn.Enabled = false;
            this.DeliveryServiceCombo.Enabled = false;
            this.CurrencyCombo.Enabled = false;
            this.txtweight.Enabled = false;
        }
    }

    private void rstBtn_Click(object sender, EventArgs e)
    {
        Environment.SpecialFolder mydocument = Environment.SpecialFolder.MyDocuments;
        String folderLocation = Environment.GetFolderPath(mydocument);

        this.DeliveryServiceCombo.SelectedIndex = 0;
        if (pcc.getEstimatedDays(this.DeliveryServiceCombo.SelectedItem.ToString()).Equals("Not Available"))
        {
            icc = new PostageChargeCalculator(folderLocation + "\\" + this.intermediateCourierPath);
            this.txtInterCourier.Text = icc.getInterCourierName();
            this.txtDeliveryTime.Text = icc.getDeliveryTime().ToString() + " Days";
            this.txtweight.ReadOnly = true;
            this.txtweight.Text = icc.getComputedWeight().ToString();
        }
        else
        {
            this.txtDeliveryTime.Text = pcc.getEstimatedDays(this.DeliveryServiceCombo.SelectedItem.ToString()) +
                " Days";
            this.txtweight.ReadOnly = false;
            this.txtweight.Text = "";
        }
        this.CurrencyCombo.SelectedIndex = 0;
    }

    private void calBtn_Click(object sender, EventArgs e)
    {
        Environment.SpecialFolder mydocument = Environment.SpecialFolder.MyDocuments;
        String folderLocation = Environment.GetFolderPath(mydocument);

        pcc = new PostageChargeCalculator(this.EstimatedDeliveryTimeTxtPath, this.currenciesTxtPath,
            this.intermediateCouriersTxtPath, folderLocation + "\\" + this.postageChargeTxtPath);
        icc = new PostageChargeCalculator(folderLocation + "\\" + this.intermediateCourierPath);
        formPostage formResult = new formPostage();
        formResult.OriginCountry.Text = this.txtOrigCountry.Text;
        formResult.OriginCountry.Font = new Font(formResult.OriginCountry.Font, FontStyle.Bold);
        formResult.DestCountry.Text = this.txtDestCountry.Text;
        formResult.DestCountry.Font = new Font(formResult.DestCountry.Font, FontStyle.Bold);
        formResult.InterCourier.Text = this.txtInterCourier.Text;
        formResult.InterCourier.Font = new Font(formResult.InterCourier.Font, FontStyle.Bold);
        formResult.EstimatedDaysToDeliver.Text = this.txtDeliveryTime.Text;
        formResult.EstimatedDaysToDeliver.Font = new Font(formResult.EstimatedDaysToDeliver.Font, FontStyle.Bold);
        formResult.Weight.Text = this.txtweight.Text;
        formResult.Weight.Font = new Font(formResult.Weight.Font, FontStyle.Bold);
        formResult.StartDate.Text = pcc.getStartDate(this.DeliveryServiceCombo.SelectedItem.ToString());
        formResult.StartDate.Font = new Font(formResult.StartDate.Font, FontStyle.Bold);
        formResult.Currency.Text = this.CurrencyCombo.SelectedItem.ToString();
        formResult.Currency.Font = new Font(formResult.Currency.Font, FontStyle.Bold);
        if (formResult.InterCourier.Text.Equals("Not Applicable"))
        {
            formResult.ExpectedDate.Text = pcc.getExpectedDate(this.DeliveryServiceCombo.SelectedItem.ToString());
            formResult.ExpectedDate.Font = new Font(formResult.ExpectedDate.Font, FontStyle.Bold);
            formResult.DeliveryService.Text = this.DeliveryServiceCombo.SelectedItem.ToString();
            formResult.DeliveryService.Font = new Font(formResult.DeliveryService.Font, FontStyle.Bold);

            try
            {
                double weight = double.Parse(this.txtweight.Text);

                if (weight > maxWeight)
                {
                    MessageBox.Show("Cannot input over the max weight of 10,000 grams!");
                }
                else
                {
                    formResult.TotalCost.Text = pcc.computeTotalCost(this.txtweight.Text, formResult.DeliveryService.Text, 
                        this.CurrencyCombo.SelectedItem.ToString()).ToString("0.00");
                    formResult.TotalCost.Font = new Font(formResult.TotalCost.Font, FontStyle.Bold);
                    formResult.ShowDialog();
                }
            }
            catch (FormatException ex)
            {
                MessageBox.Show("Please input the weight in number only for direct courier");
            }
        }
        else
        {
            formResult.DeliveryService.Text = "Not Applicable";
            formResult.DeliveryService.Font = new Font(formResult.DeliveryService.Font, FontStyle.Bold);
            formResult.ExpectedDate.Text = icc.getExpectedDateInterCourier(formResult.StartDate.Text);
            formResult.ExpectedDate.Font = new Font(formResult.ExpectedDate.Font, FontStyle.Bold);
            formResult.TotalCost.Text = String.Format("{0:0.00}", icc.getTotalCost());
            formResult.TotalCost.Font = new Font(formResult.TotalCost.Font, FontStyle.Bold);
            formResult.ShowDialog();
        }
    }
}