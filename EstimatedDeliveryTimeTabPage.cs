using System;
using System.Drawing;
using System.ComponentModel;
using System.Windows.Forms;
using System.IO;
using ParcelDeliverySystem;
public class EstimatedDeliveryTimeTabPage : TabPage
{
    private Button calBtn, rstBtn;
    private ComboBox OrigCountryCombo, DestCountryCombo;
    private Label lblOrigCountry, lblDestCountry, lblDate, lblError;
    private DateTimePicker datePicker;
    private EstimatedDeliveryTimeCalculator edtc;
    private String EstimatedDeliveryTimeTxtPath = "estimatedDeliveryTime.txt";

    public EstimatedDeliveryTimeTabPage()
    {
        this.Text = "Estimated Delivery Time";

        if (File.Exists(this.EstimatedDeliveryTimeTxtPath))
        {
            edtc = new EstimatedDeliveryTimeCalculator(
                this.EstimatedDeliveryTimeTxtPath);
            calBtn = new Button();
            rstBtn = new Button();
            OrigCountryCombo = new ComboBox();
            DestCountryCombo = new ComboBox();
            lblOrigCountry = new Label();
            lblDestCountry = new Label();
            lblDate = new Label();
            lblError = new Label();
            datePicker = new DateTimePicker();

            calBtn.Text = "Calculate";
            calBtn.Size = new Size(75, 23);

            rstBtn.Text = "Reset";
            rstBtn.Size = new Size(75, 23);
            this.rstBtn.Click += new EventHandler(rstBtn_Click);

            edtc.InitOriginCountriesComboBox(OrigCountryCombo);
            OrigCountryCombo.SelectedIndex = 0;
            // init the locations box.
            OrigCountryCombo_SelectedIndexChanged(null, null);

            lblOrigCountry.Text = "Origin Country: ";
            lblOrigCountry.Location = new Point(6, 41);
            OrigCountryCombo.Location = new Point(135, 41);

            lblDestCountry.Text = "Destination Country: ";
            lblDestCountry.Size = new Size(110,23);
            lblDestCountry.Location = new Point(6, 78);
            DestCountryCombo.Location = new Point(135, 78);

            lblDate.Text = "Date(DD/MM/YYYY)";
            lblDate.Location = new Point(6, 117);
            lblDate.Size = new Size(114, 18);

            lblError.Text = "";
            lblError.ForeColor = Color.Red;
            lblError.Location = new Point(6, 250);
            lblError.Size = new Size(400, 18);

            datePicker.Location = new Point(135, 117);
            datePicker.Name = "dateTimePicker";
            datePicker.Size = new Size(140, 20);
            datePicker.CustomFormat = "dd/MM/yyyy";
            datePicker.Format = DateTimePickerFormat.Custom;
            datePicker.MaxDate = new DateTime(2020, 12, 31, 0, 0, 0, 0);
            datePicker.MinDate = new DateTime(2010, 3, 1, 0, 0, 0, 0);
            datePicker.ValueChanged += new EventHandler(dateTimePicker_ValueChanged);           

            calBtn.Location = new Point(6, 195);
            calBtn.Size = new Size(75, 23);
            rstBtn.Location = new Point(135, 195);
            rstBtn.Size = new Size(75, 23);

            OrigCountryCombo.SelectedIndexChanged += new EventHandler(OrigCountryCombo_SelectedIndexChanged);
            DestCountryCombo.SelectedIndexChanged += new EventHandler(DestCountryCombo_SelectedIndexChanged);
            calBtn.Click += new EventHandler(calBtn_Click);
            rstBtn.Click += new EventHandler(rstBtn_Click);

            this.Controls.Add(calBtn);
            this.Controls.Add(rstBtn);
            this.Controls.Add(datePicker);
            this.Controls.Add(OrigCountryCombo);
            this.Controls.Add(DestCountryCombo);
            this.Controls.Add(lblOrigCountry);
            this.Controls.Add(lblDestCountry);
            this.Controls.Add(lblDate);
            this.Controls.Add(lblError);
        }
        else
        {
            Label lblError = new Label();
            lblError.Text = this.EstimatedDeliveryTimeTxtPath +
                " is not found alongside with this program. Feature disabled.";
            lblError.Size = new Size(500, 30);
            lblError.Location = new Point(50, 130);
            this.Controls.Add(lblError);
        }
    }

    private void calBtn_Click(object sender, EventArgs e)
    {
        EDTresult result = new EDTresult();
        String stringBuilder;
        String[] deliveryServiceName = new String[edtc.DeliveryServiceCount()];

        if (this.lblError.Text.Equals(""))
        {
            for(int i=0; i<OrigCountryCombo.Items.Count;i++)
            {
                if(OrigCountryCombo.SelectedIndex == i){
                    for (int j = 0; j < DestCountryCombo.Items.Count; j++)
                    {
                        if (DestCountryCombo.SelectedIndex == j)
                        {
                            for (int k = 0; k < edtc.DeliveryServiceCount(); k++)
                            {
                                stringBuilder = "\n------------------------------";// about 30 chars of '-'
                                stringBuilder += "------------------------------";// about 30 chars of '-'
                                stringBuilder += "------------------------------";// about 30 chars of '-'
                                stringBuilder += "------------------------------";// about 30 chars of '-'
                                stringBuilder += "------------------------------";// about 30 chars of '-'
                                stringBuilder += "------------------------------";// about 30 chars of '-'
                                stringBuilder += "------------------------------\n";// about 30 chars of '-'
                                stringBuilder += edtc.GetDeliveryServiceNames()[k].PadRight(30);
                                stringBuilder += OrigCountryCombo.SelectedItem.ToString().PadRight(30);
                                stringBuilder += DestCountryCombo.SelectedItem.ToString().PadRight(30);
                                stringBuilder += (String.Format("{0:00}", this.datePicker.Value.Day) + "/" +
                                        String.Format("{0:00}", this.datePicker.Value.Month) + "/" +
                                        String.Format("{0:0000}", this.datePicker.Value.Year)).PadRight(30);
                                String[] getAvailableDays = edtc.GetAvailableDays(OrigCountryCombo.SelectedItem.ToString(),
                                    DestCountryCombo.SelectedItem.ToString(), edtc.GetDeliveryServiceNames()[k]);
                                int getEstimatedDays = edtc.GetEstimatedDays(OrigCountryCombo.SelectedItem.ToString(),
                                    DestCountryCombo.SelectedItem.ToString(), edtc.GetDeliveryServiceNames()[k]);
                                Boolean exist = false;

                                for (int l = 0; l < getAvailableDays.Length; l++)
                                {
                                    if (this.datePicker.Value.ToString("ddd") == getAvailableDays[l])
                                    {
                                        stringBuilder += this.datePicker.Value.AddHours(getEstimatedDays * 24)
                                            .ToString("dd/MM/yyyy").PadRight(30);
                                        exist = true;
                                        break;
                                    }
                                }
                                if(!exist)
                                {
                                    stringBuilder += "Not Available".PadRight(30);
                                }
                                stringBuilder += getEstimatedDays;
                                stringBuilder += "\n------------------------------";// about 30 chars of '-'
                                stringBuilder += "------------------------------";// about 30 chars of '-'
                                stringBuilder += "------------------------------";// about 30 chars of '-'
                                stringBuilder += "------------------------------";// about 30 chars of '-'
                                stringBuilder += "------------------------------";// about 30 chars of '-'
                                stringBuilder += "------------------------------";// about 30 chars of '-'
                                stringBuilder += "------------------------------\n";// about 30 chars of '-'
                                result.Labeltest.Text += stringBuilder;
                            }
                        }
                    }
                }
            }
            result.Show();
        }
        else
        {
            MessageBox.Show("Please choose the present day or later!");
        }
    }

    private void rstBtn_Click(object sender, EventArgs e)
    {
        this.OrigCountryCombo.SelectedIndex = 0;
        this.DestCountryCombo.SelectedIndex = 0;
        this.datePicker.Value = DateTime.Today;
        this.lblError.Text = "";
    }

    private void OrigCountryCombo_SelectedIndexChanged(object sender, EventArgs e)
    {
        this.edtc.InitDestCountriesComboBox((String)this.OrigCountryCombo.SelectedItem, this.DestCountryCombo);
        DestCountryCombo_SelectedIndexChanged(null, null);
    }

    private void DestCountryCombo_SelectedIndexChanged(object sender, EventArgs e)
    {
        EstimatedDeliveryTime edt = this.edtc.GetEstimatedDeliveryTime(
            (String)this.OrigCountryCombo.SelectedItem,
            (String)this.DestCountryCombo.SelectedItem);
    }

    private void dateTimePicker_ValueChanged(object sender, EventArgs e)
    {
        int YearNow = DateTime.Now.Year;
        int MonthNow = DateTime.Now.Month;
        int DayNow = DateTime.Now.Day;

        if (this.datePicker.Value.Year >= YearNow)
        {
            if (this.datePicker.Value.Year == YearNow)
            {
                this.lblError.Text = "";
                if (this.datePicker.Value.Month >= MonthNow)
                {
                    if (this.datePicker.Value.Month == MonthNow)
                    {
                        if (this.datePicker.Value.Day >= DayNow)
                        {
                            this.lblError.Text = "";
                        }
                        else
                        {
                            MessageBox.Show("Please choose the present day or later!", "Error", MessageBoxButtons.OK,
                                MessageBoxIcon.Error);
                            this.lblError.Text = "Please choose the present day or later!";
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Please choose the present month or later!", "Error", MessageBoxButtons.OK, 
                        MessageBoxIcon.Error);
                    this.lblError.Text = "Please choose the present day or later!";
                }
            }
        }
        else
        {
            MessageBox.Show("Please choose the present year or later!", "Error",MessageBoxButtons.OK, MessageBoxIcon.Error);
            this.lblError.Text = "Please choose the present day or later!";
        }
    }
}