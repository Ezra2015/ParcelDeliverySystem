using System;
using System.Collections;
using System.IO;
using System.Windows.Forms;
using System.Drawing;
using System.ComponentModel;


public class CurrencyConverter
{
    private ArrayList currencies = new ArrayList();
    private String thePath;

    public CurrencyConverter(String path)
    {
        this.thePath = path;
        ReadCurrencies();
    }

    public double Convert(String fromType, double fromValue,
        String toType)
    {
        ReadCurrencies();

        // Find fromType and toType.
        Currency from = null, to = null;
        foreach (Currency c in currencies)
        {
            if (from != null && to != null)
                break;

            if (from == null)
                if (c.Type.Equals(fromType, System.StringComparison.OrdinalIgnoreCase))
                {
                    from = c;
                }

            if (to == null)
                if (c.Type.Equals(toType, System.StringComparison.OrdinalIgnoreCase))
                {
                    to = c;
                }
        }

        if (from != null && to != null)
        {
            double result =
                fromValue / from.ExchangeRate * to.ExchangeRate;

            return result;
        }

        return -1;
    }

    private void ReadCurrencies()
    {
        currencies = new ArrayList();
        DatabaseManager dm = new DatabaseManager(thePath);
        // Console.WriteLine("{0} lines in {1}", dm.Count, thePath);
        for (int i = 0; i < dm.Count; ++i)
        {
            String[] s = ((String)dm.GetObject(i)).Split('\t');
            currencies.Add(new Currency(s[0], Double.Parse(s[1])));
        }
    }

    public void PrintOutCurrencies()
    {
        foreach (Currency c in currencies)
        {
            Console.WriteLine("Currency {0} {1}", c.Type, c.ExchangeRate);
        }
    }

    public void InitComboBoxes(ComboBox[] cbs)
    {
        foreach (ComboBox cb in cbs)
        {
            cb.Items.Clear();

            foreach (Currency c in this.currencies)
            {
                cb.Items.Add(c.Type);
            }

            cb.DropDownStyle = ComboBoxStyle.DropDownList;
            cb.SelectedIndex = 0;
        }
    }

}