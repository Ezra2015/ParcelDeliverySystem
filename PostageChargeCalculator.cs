using System;
using System.Collections;
using System.Windows.Forms;
using System.Drawing;
using System.IO;

public class PostageChargeCalculator
{
    private String currencyPath;
    private String estimatedDeliveryPath;
    private String intermediatecourier;
    private String postageChargeTxtPath;
    private String intermediateCourierPath;
    private String stringbuilder;
    private String currencyTxtPath = "currencies.txt";
    private String currentCurrencyType = "USD";
    String[] getWeightRateString;
    String[] getRateCostString;
    public Object intercourier;
    private ArrayList edpEntries, icEntries, cEntries, pcEntries, icpEntries;

    public PostageChargeCalculator()
    {
    }
    
    public PostageChargeCalculator(String intermediateCourierPath)
    {
        this.intermediateCourierPath = intermediateCourierPath;

        ReadicpEntries();
    }

    public PostageChargeCalculator(String estimatedDeliveryPath, String currencyPath, String intermediatecourier,
        String postageChargeTxtPath)
    {
        this.estimatedDeliveryPath = estimatedDeliveryPath;
        this.currencyPath = currencyPath;
        this.intermediatecourier = intermediatecourier;
        this.postageChargeTxtPath = postageChargeTxtPath;

        ReadedpEntries();
        ReadicEntries();
        ReadpcEntries();
        ReadcEntries();
    }

    private void ReadedpEntries()
    {
        this.edpEntries = new ArrayList();
        DatabaseManager dm = new DatabaseManager(this.estimatedDeliveryPath);
        for (int i = 0; i < dm.Count; ++i)
        {
            String[] s = ((String)dm.GetObject(i)).Split('\t');

            this.edpEntries.Add(new EstimatedDeliveryTime(s[0], s[1], s[2], s[3],
                s[4], s[5], s[6]));
        }
    }

    private void ReadicEntries()
    {
        this.icEntries = new ArrayList();
        DatabaseManager dm = new DatabaseManager(this.intermediatecourier);
        for (int i = 0; i < dm.Count; ++i)
        {
            String[] s = ((String)dm.GetObject(i)).Split('\t');
            this.icEntries.Add(new IntermediateCourier(s[0], s[1], s[2], Double.Parse(s[3]),
                Int32.Parse(s[4]), s[5]));
        }
    }

    private void ReadpcEntries()
    {
        this.pcEntries = new ArrayList();
        DatabaseManager dm = new DatabaseManager(this.postageChargeTxtPath);
        for (int i = 0; i < dm.Count; ++i)
        {
            String[] s = ((String)dm.GetObject(i)).Split('\t');
            this.pcEntries.Add(new PostageCharge(s[0], s[1], s[2], s[3],
                s[4], s[5], s[6], s[7]));
        }
    }

    private void ReadcEntries()
    {
        this.cEntries = new ArrayList();
        DatabaseManager dm = new DatabaseManager(this.currencyPath);
        // Console.WriteLine("{0} lines in {1}", dm.Count, thePath);
        for (int i = 0; i < dm.Count; ++i)
        {
            String[] s = ((String)dm.GetObject(i)).Split('\t');
            this.cEntries.Add(new Currency(s[0], Double.Parse(s[1])));
        }
    }

    private void ReadicpEntries()
    {
        this.icpEntries = new ArrayList();
        DatabaseManager dm = new DatabaseManager(this.intermediateCourierPath);
        // Console.WriteLine("{0} lines in {1}", dm.Count, thePath);
        for (int i = 0; i < dm.Count; ++i)
        {
            String[] s = ((String)dm.GetObject(i)).Split('\t');
            this.icpEntries.Add(new InterCourCurrency(s[0], s[1], s[2], Int32.Parse(s[3]), s[4],
                Int32.Parse(s[5]), s[6], double.Parse(s[7]), double.Parse(s[8])));
        }
    }

    public void InitDeliveryServiceComboBox(ComboBox cb)
    {
        cb.Items.Clear();
        cb.DropDownStyle = ComboBoxStyle.DropDownList;

        // Get unique entries for the Origin Countries
        ArrayList unique = new ArrayList();
        bool exists = false;
        foreach (PostageCharge cds in this.pcEntries)
        {
            foreach (String s in unique)
            {
                if (s.Equals(cds.DeliveryService, StringComparison.OrdinalIgnoreCase))
                {
                    exists = true;
                    break;
                }
            }

            if (!exists)
            {
                unique.Add(cds.DeliveryService);
            }
            exists = false;
        }

        foreach (String s in unique)
        {
            cb.Items.Add(s);
        }
        cb.SelectedIndex = 0;
    }

    public void InitCurrencyComboBox(ComboBox cbs)
    {
        cbs.Items.Clear();
        cbs.DropDownStyle = ComboBoxStyle.DropDownList;

        // Get unique entries for the Origin Countries
        ArrayList unique = new ArrayList();
        bool exists = false;
        foreach (Currency cds in this.cEntries)
        {
            foreach (String s in unique)
            {
                if (s.Equals(cds.Type, StringComparison.OrdinalIgnoreCase))
                {
                    exists = true;
                    break;
                }
            }

            if (!exists)
            {
                unique.Add(cds.Type);
            }
            exists = false;
        }

        foreach (String s in unique)
        {
            cbs.Items.Add(s);
        }
        cbs.SelectedIndex = 0;
    }

    public String getOriginCountry(String selectedDeliveryService)
    {
        PostageCharge DeliveryNames = null;
        foreach (PostageCharge e in this.pcEntries)
        {
            if (DeliveryNames != null)
                break;

            if (DeliveryNames == null)
                if (e.DeliveryService.Equals(selectedDeliveryService, System.StringComparison.OrdinalIgnoreCase))
                {
                    DeliveryNames = e;
                }
        }

        if (DeliveryNames != null)
        {
            foreach (PostageCharge e in this.pcEntries)
            {
                if (e.DeliveryService.Equals(selectedDeliveryService, System.StringComparison.OrdinalIgnoreCase))
                {
                    return e.OriginCountryName;
                }
            }
        }
        return null;
    }

    public String getDestinationCountry(String selectedDeliveryService)
    {
        PostageCharge DeliveryNames = null;
        foreach (PostageCharge e in this.pcEntries)
        {
            if (DeliveryNames != null)
                break;

            if (DeliveryNames == null)
                if (e.DeliveryService.Equals(selectedDeliveryService, System.StringComparison.OrdinalIgnoreCase))
                {
                    DeliveryNames = e;
                }
        }

        if (DeliveryNames != null)
        {
            foreach (PostageCharge e in this.pcEntries)
            {
                if (e.DeliveryService.Equals(selectedDeliveryService, System.StringComparison.OrdinalIgnoreCase))
                {
                    return e.DestCountryName;
                }
            }
        }
        return null;
    }

    public String getEstimatedDays(String selectedDeliveryName)
    {
        PostageCharge DeliveryNames = null;
        foreach (PostageCharge e in this.pcEntries)
        {
            if (DeliveryNames == null)
                if (e.DeliveryService.Equals(selectedDeliveryName, System.StringComparison.OrdinalIgnoreCase))
                {
                    DeliveryNames = e;
                }
        }

        if ( DeliveryNames != null)
        {
            foreach (PostageCharge e in this.pcEntries)
            {
                if (e.DeliveryService.Equals(selectedDeliveryName, System.StringComparison.OrdinalIgnoreCase))
                {
                    return e.EstimatedDaysToDeliver.ToString();
                }
            }
        }
        return null;
    }

    public void writeToCourierText(IntermediateCourier courier, Currency currency, double mass)
    {
        //Store in mydocuments as temporary in local drive...
        Environment.SpecialFolder mydocument = Environment.SpecialFolder.MyDocuments;
        String folderLocation = Environment.GetFolderPath(mydocument);
        if (courier != null && currency != null)
        {
            this.stringbuilder = courier.CompanyName + "\t";
            this.stringbuilder += courier.ToLocation.ToString() + "\t";
            this.stringbuilder += courier.CurrencyType.ToString() + "\t";
            this.stringbuilder += courier.EstimatedDaysToDeliver.ToString() + "\t";
            this.stringbuilder += courier.AvailableDaysToDeliver.ToString() + "\t";
            this.stringbuilder += courier.CostPer100g.ToString() + "\t";
            this.stringbuilder += currency.Type.ToString() + "\t";
            this.stringbuilder += currency.ExchangeRate.ToString() + "\t";
            this.stringbuilder += mass.ToString() + "\t";
        }
        try
        {
            if (File.Exists(folderLocation + "\\intermediateCourier.txt"))
            {
                System.GC.Collect();
                System.GC.WaitForPendingFinalizers();
                File.Delete(folderLocation + "\\intermediateCourier.txt");
            }
            String createtext = folderLocation + "\\intermediateCourier.txt";
            FileInfo file = new FileInfo(createtext);
            StreamWriter writer = file.CreateText();

            writer.Write(this.stringbuilder);
            writer.Close();
            writer.Dispose();
        }
        catch (IOException fe)
        {
            MessageBox.Show("Error immediate courier output");
        }
    }

    public String getInterCourierName()
    {
        foreach (InterCourCurrency e in this.icpEntries)
        {
            return e.CompanyName;
        }
        return null;
    }

    public int getDeliveryTime()
    {
        foreach (InterCourCurrency e in this.icpEntries)
        {
            return e.EstimatedDays;
        }
        return -1;
    }

    public double getComputedWeight()
    {
        foreach (InterCourCurrency e in this.icpEntries)
        {
            return e.Mass;
        }
        return -1;
    }

    public String getCurrencyType()
    {
        foreach (InterCourCurrency e in this.icpEntries)
        {
            return e.Type;
        }
        return null;
    }

    public String getStartDate(String selectedDeliveryName)
    {
        PostageCharge DeliveryNames = null;
        foreach (PostageCharge e in this.pcEntries)
        {
            if (DeliveryNames == null)
                if (e.DeliveryService.Equals(selectedDeliveryName, System.StringComparison.OrdinalIgnoreCase))
                {
                    DeliveryNames = e;
                }
        }

        if (DeliveryNames != null)
        {
            foreach (PostageCharge e in this.pcEntries)
            {
                if (e.DeliveryService.Equals(selectedDeliveryName, System.StringComparison.OrdinalIgnoreCase))
                {
                    return e.StartDate;
                }
            }
        }
        return null;
    }

    public String getExpectedDate(String selectedDeliveryName)
    {
        PostageCharge DeliveryNames = null;
        foreach (PostageCharge e in this.pcEntries)
        {
            if (DeliveryNames == null)
                if (e.DeliveryService.Equals(selectedDeliveryName, System.StringComparison.OrdinalIgnoreCase))
                {
                    DeliveryNames = e;
                }
        }

        if (DeliveryNames != null)
        {
            foreach (PostageCharge e in this.pcEntries)
            {
                if (e.DeliveryService.Equals(selectedDeliveryName, System.StringComparison.OrdinalIgnoreCase))
                {
                    return e.EndDate;
                }
            }
        }
        return null;
    }

    public String getExpectedDateInterCourier(String startDate)
    {
        double estimatedDays;

        DateTime selectedDate = new DateTime();
        selectedDate = Convert.ToDateTime(startDate);
        foreach (InterCourCurrency e in this.icpEntries)
        {
            estimatedDays = e.EstimatedDays;
            return Convert.ToString(selectedDate.AddDays(estimatedDays)).Substring(0,10);

        }
        return null; 
    }

    public double getTotalCost()
    {
        foreach (InterCourCurrency e in this.icpEntries)
        {
            return e.ExchangeRate ;
        }
        return -1; 
    }

    public double computeTotalCost(String weight, String selectedDeliveryService, String currencyType)
    {
        double[] getWeightRate;
        double[] getRateCost;
        Boolean exist=false;
        PostageCharge DeliveryNames = null;
        CurrencyConverter cc = new CurrencyConverter(currencyTxtPath);

        foreach (PostageCharge e in this.pcEntries)
        {
            if (DeliveryNames == null)
                if (e.DeliveryService.Equals(selectedDeliveryService, System.StringComparison.OrdinalIgnoreCase))
                {
                    DeliveryNames = e;
                }
        }

        if (DeliveryNames != null)
        {
            foreach (PostageCharge e in this.pcEntries)
            {
                if (e.DeliveryService.Equals(selectedDeliveryService, System.StringComparison.OrdinalIgnoreCase))
                {
                    this.getWeightRateString = e.Weight.Split(',');
                    this.getRateCostString = e.RateCost.Split(',');
                }
            }
            getWeightRate = new double[this.getWeightRateString.Length];
            getRateCost = new double[this.getRateCostString.Length];
            for (int i = 0; i < this.getWeightRateString.Length; i++)
            {
                getWeightRate[i] = double.Parse(this.getWeightRateString[i]);
            }
            for (int i = 0; i < this.getRateCostString.Length; i++)
            {
                getRateCost[i] = double.Parse(this.getRateCostString[i]);
            }
            for (int i = 0; i < getWeightRate.Length; i++)
            {
                if (double.Parse(weight) <= getWeightRate[i])
                {
                    exist = true;
                    return cc.Convert(currentCurrencyType, double.Parse(weight) * getWeightRate[i], currencyType);
                }
            }
            if (!exist)
            {
                return cc.Convert(currentCurrencyType,
                    ((double.Parse(weight) / 100) * getWeightRate[getWeightRate.Length - 1]), currencyType); 
            }
            exist = false;
        }
        return -1;        
    }

    public void removeAllTxts()
    {
        System.GC.Collect();
        System.GC.WaitForPendingFinalizers();
        //Store in mydocuments as temporary in local drive...
        Environment.SpecialFolder mydocument = Environment.SpecialFolder.MyDocuments;
        String folderLocation = Environment.GetFolderPath(mydocument);
        try
        {
            if (File.Exists(folderLocation + "\\intermediateCourier.txt"))
            {
                File.Delete(folderLocation + "\\intermediateCourier.txt");
            }
            if (File.Exists(folderLocation + "\\tempPostageCharge.txt"))
            {
                File.Delete(folderLocation + "\\tempPostageCharge.txt");
            }
        }
        catch (IOException ex)
        {
        }
    }

    public void removeInterCourierFile()
    {
        System.GC.Collect();
        System.GC.WaitForPendingFinalizers();
        //Store in mydocuments as temporary in local drive...
        Environment.SpecialFolder mydocument = Environment.SpecialFolder.MyDocuments;
        String folderLocation = Environment.GetFolderPath(mydocument);
        try
        {
            if (File.Exists(folderLocation + "\\intermediateCourier.txt"))
            {
                File.Delete(folderLocation + "\\intermediateCourier.txt");
            }
        }
        catch (IOException ex)
        {
        }
    }
}
