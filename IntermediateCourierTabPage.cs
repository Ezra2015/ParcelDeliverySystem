using System;
using System.Drawing;
using System.ComponentModel;
using System.Windows.Forms;
using System.IO;

public class IntermediateCourierTabPage : TabPage
{
    Button calculateBtn;
    ComboBox couriers, locations, currencies;
    Label lblCourier, lblLocation, lblCurrency, lblMass;
    IntermediateCourierCalculator icc;
    TextBox mass;
    Label lblCurrencyType, lblCurrencyTypeValue;
    Label lblCostPer100g, lblCostPer100gValue;
    Label lblEstimatedDaysToDeliver, lblEstimatedDaysToDeliverValue;
    String intermediateCouriersTxtPath = "intermediate-couriers.txt";
    String currenciesTxtPath = "currencies.txt";

    public IntermediateCourierTabPage()
    {
        this.Text = "Intermediate Courier";

        if (File.Exists(this.intermediateCouriersTxtPath))
        {
            icc = new IntermediateCourierCalculator(
                this.intermediateCouriersTxtPath,
                this.currenciesTxtPath);
            calculateBtn = new Button();
            couriers = new ComboBox();
            locations = new ComboBox();
            currencies = new ComboBox();
            lblCourier = new Label();
            lblLocation = new Label();
            lblCurrency = new Label();
            lblMass = new Label();
            mass = new TextBox();
            lblCurrencyType = new Label();
            lblCurrencyTypeValue = new Label();
            lblCostPer100g = new Label();
            lblCostPer100gValue = new Label();
            lblEstimatedDaysToDeliver = new Label();
            lblEstimatedDaysToDeliverValue = new Label();

            calculateBtn.Text = "Calculate";
            calculateBtn.Size = new Size(70, 40);

            icc.InitCouriersComboBox(couriers);
            icc.InitCurrencyComboBox(currencies);
            couriers.SelectedIndex = 0;
            // init the locations box.
            couriers_SelectedIndexChanged(null, null);

            lblCurrencyType.Text = "Currency: ";
            lblCurrencyType.Location = new Point(190, 20);
            lblCurrencyTypeValue.Location = new Point(300, 20);

            lblCostPer100g.Text = "Cost Per 100g";
            lblCostPer100g.Location = new Point(190, 50);
            lblCostPer100gValue.Location = new Point(300, 50);

            lblEstimatedDaysToDeliver.Text = "Estimated Days To Deliver";
            lblEstimatedDaysToDeliver.Size = new Size(200, 20);
            lblEstimatedDaysToDeliver.Location = new Point(190, 80);
            lblEstimatedDaysToDeliverValue.Location = new Point(190, 100);

            lblCourier.Text = "Intermediate Courier";
            lblCourier.Size = new Size(150, 20);
            lblCourier.Location = new Point(20, 20);
            couriers.Location = new Point(20, 50);

            lblLocation.Text = "To Location";
            lblLocation.Location = new Point(20, 80);
            locations.Location = new Point(20, 110);

            lblCurrency.Text = "Currency To Show In";
            lblCurrency.Size = new Size(150, 20);
            lblCurrency.Location = new Point(20, 140);
            currencies.Location = new Point(20, 170);

            lblMass.Text = "Parcel Mass (grams)";
            lblMass.Location = new Point(20, 200);
            lblMass.Size = new Size(150, 20);
            mass.Location = new Point(20, 230);

            calculateBtn.Location = new Point(20, 270);

            couriers.SelectedIndexChanged += new EventHandler(couriers_SelectedIndexChanged);
            calculateBtn.Click += new EventHandler(calculateBtn_Click);
            locations.SelectedIndexChanged += new EventHandler(locations_SelectedIndexChanged);

            this.Controls.Add(calculateBtn);
            this.Controls.Add(couriers);
            this.Controls.Add(locations);
            this.Controls.Add(currencies);
            this.Controls.Add(lblCourier);
            this.Controls.Add(lblLocation);
            this.Controls.Add(lblCurrency);
            this.Controls.Add(lblMass);
            this.Controls.Add(mass);
            this.Controls.Add(lblCurrencyType);
            this.Controls.Add(lblCurrencyTypeValue);
            this.Controls.Add(lblCostPer100g);
            this.Controls.Add(lblCostPer100gValue);
            this.Controls.Add(lblEstimatedDaysToDeliver);
            this.Controls.Add(lblEstimatedDaysToDeliverValue);
        }
        else
        {
            Label lblError = new Label();
            lblError.Text = "Some or all of the required database files (" +
                this.intermediateCouriersTxtPath + ", " + this.currenciesTxtPath +
                ") are missing. Feature disabled.";
            lblError.Size = new Size(500, 30);
            lblError.Location = new Point(50, 130);
            this.Controls.Add(lblError);
        }
    }

    private void calculateBtn_Click(object sender, EventArgs e)
    {
        IntermediateCourier ic = this.icc.GetIntermediateCourier(
            (String)this.couriers.SelectedItem,
            (String)this.locations.SelectedItem);
        try
        {
            if (ic != null)
            {
                double mass = Double.Parse((String)this.mass.Text);
                double total = this.icc.CalculateCharge(ic.CompanyName,
                    ic.ToLocation, mass, (String)this.currencies.SelectedItem);

                MessageBox.Show("The total cost is " + ((String)this.currencies.SelectedItem) +
                    " " + total + " and it takes an estimated " + ic.EstimatedDaysToDeliver +
                    (ic.EstimatedDaysToDeliver == 1 ? " day" : " days") + " to deliver.");
            }
            else
            {
                MessageBox.Show("Unable to calculate. No such possibility.");
            }
        }
        catch (FormatException fe)
        {
            MessageBox.Show("Cannot accept non-numbers. Only numbers and/or a dot are allowed.");
        }
    }

    private void couriers_SelectedIndexChanged(object sender, EventArgs e)
    {
        this.icc.InitLocationComboBox((String)this.couriers.SelectedItem, this.locations);
        locations_SelectedIndexChanged(null, null);
    }

    private void locations_SelectedIndexChanged(object sender, EventArgs e)
    {
        IntermediateCourier ic = this.icc.GetIntermediateCourier(
            (String) this.couriers.SelectedItem,
            (String) this.locations.SelectedItem);

        this.lblCurrencyTypeValue.Text = ic.CurrencyType;
        this.lblCostPer100gValue.Text = ic.CostPer100g.ToString();
        this.lblEstimatedDaysToDeliverValue.Text = "" + ic.EstimatedDaysToDeliver;
    }
}