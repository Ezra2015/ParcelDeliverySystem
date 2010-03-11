using System;
using System.ComponentModel;
using System.IO;
using System.Windows.Forms;
using System.Drawing;

public class CurrencyConverterTabPage : TabPage
{
    private ComboBox fromCurrency, toCurrency;
    private Label fromLabel, toLabel;
    private TextBox fromValue;
    private Button convertBtn;
    private CurrencyConverter cc;
    private String currenciesTxtPath = "currencies.txt";

    public CurrencyConverterTabPage()
    {
        this.Text = "Currency Converter";
        if (File.Exists(this.currenciesTxtPath))
        {
            cc = new CurrencyConverter(this.currenciesTxtPath);
            fromCurrency = new ComboBox();
            toCurrency = new ComboBox();
            fromLabel = new Label();
            toLabel = new Label();
            fromValue = new TextBox();
            convertBtn = new Button();

            fromLabel.Text = "Convert from";

            toLabel.Text = "To";

            fromLabel.Location = new Point(0, 30);
            toLabel.Location = new Point(150, 30);

            fromCurrency.Location = new Point(0, 50);
            toCurrency.Location = new Point(200, 50);

            fromValue.Location = new Point(0, 90);

            cc.InitComboBoxes(new ComboBox[] { fromCurrency, toCurrency });

            convertBtn.Size = new Size(70, 40);
            convertBtn.Location = new Point(129, 130);
            convertBtn.Text = "Convert";
            this.Controls.Add(convertBtn);
            convertBtn.Click += new EventHandler(convertBtn_Click);

            this.Controls.Add(fromCurrency);
            this.Controls.Add(toCurrency);
            this.Controls.Add(fromLabel);
            this.Controls.Add(toLabel);
            this.Controls.Add(fromValue);
        }
        else
        {
            Label lblError = new Label();
            lblError.Text = this.currenciesTxtPath + " is not found alongside with this program. Feature disabled.";
            lblError.Size = new Size(500, 30);
            lblError.Location = new Point(50, 130);
            this.Controls.Add(lblError);
        }
    }

    private void convertBtn_Click(object sender, EventArgs e)
    {
        try
        {
            double from = Double.Parse(fromValue.Text);
            MessageBox.Show("Convert from " + fromCurrency.SelectedItem + " "
                + from + " to " + toCurrency.SelectedItem + " is \n\n" +
                cc.Convert((String)fromCurrency.SelectedItem, from, (String)toCurrency.SelectedItem)
                );
        }
        catch (FormatException fe)
        {
            MessageBox.Show("Unable to convert. Please use numbers and/or a dot only.");
        }
    }
}