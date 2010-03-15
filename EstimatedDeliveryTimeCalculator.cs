using System;
using System.Collections;
using System.Windows.Forms;
using System.Drawing;
using System.ComponentModel;

class EstimatedDeliveryTimeCalculator
{
    enum Days { Mon = 0, Tue, Wed, Thu, Fri, Sat, Sun };
    private String thePath;
    private ArrayList entries;

    public EstimatedDeliveryTimeCalculator(String thePath)
    {
        this.thePath = thePath;
        ReadEntries();
    }

    private void ReadEntries()
    {
        this.entries = new ArrayList();
        DatabaseManager dm = new DatabaseManager(this.thePath);
        for (int i = 0; i < dm.Count; ++i)
        {
            String[] s = ((String)dm.GetObject(i)).Split('\t');

            this.entries.Add(new EstimatedDeliveryTime(s[0], s[1], s[2], s[3],
                s[4], s[5], Int32.Parse(s[6])));
        }
    }

    public void InitOriginCountriesComboBox(ComboBox cb)
    {
        cb.Items.Clear();
        cb.DropDownStyle = ComboBoxStyle.DropDownList;

        // Get unique entries for the Origin Countries
        ArrayList unique = new ArrayList();
        bool exists = false;
        foreach (EstimatedDeliveryTime cds in this.entries)
        {
            foreach (String s in unique)
            {
                if (s.Equals(cds.OriginCountryName, StringComparison.OrdinalIgnoreCase))
                {
                    exists = true;
                    break;
                }
            }

            if (!exists)
            {
                unique.Add(cds.OriginCountryName);
            }
            exists = false;
        }

        foreach (String s in unique)
        {
            cb.Items.Add(s);
        }
        cb.SelectedIndex = 0;
    }

    public void InitDestCountriesComboBox(String origCountryName, ComboBox cb)
    {
        cb.Items.Clear();
        cb.DropDownStyle = ComboBoxStyle.DropDownList;

        // Get unique entries for the Origin Countries
        ArrayList unique = new ArrayList();
        bool exists = false;
        foreach (EstimatedDeliveryTime cds in this.entries)
        {
            foreach (String s in unique)
            {
                if (s.Equals(cds.DestCountryName, StringComparison.OrdinalIgnoreCase) ||
                    cds.OriginCountryName.Equals(origCountryName, StringComparison.OrdinalIgnoreCase))
                {
                    exists = true;
                    break;
                }
            }

            if (!exists)
            {
                unique.Add(cds.DestCountryName);
            }
            exists = false;
        }
        foreach (String s in unique)
        {
            cb.Items.Add(s);
        }
        cb.SelectedIndex = 0;
    }

    public EstimatedDeliveryTime GetEstimatedDeliveryTime(String originCountryName,
        String destCountryName)
    {
        foreach (EstimatedDeliveryTime edt in this.entries)
        {
            if (originCountryName.Equals(edt.OriginCountryName, StringComparison.OrdinalIgnoreCase)
                && destCountryName.Equals(edt.DestCountryName, StringComparison.OrdinalIgnoreCase))
            {
                return edt;
            }
        }

        return null;
    }

    public String[] GetDeliveryServiceNames()
    {
        ArrayList unique = new ArrayList();
        bool exists = false;
        String deliveryServices = "";
        foreach (EstimatedDeliveryTime cds in this.entries)
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
            deliveryServices +=s + "\t";
        }
        String[] alldeliveryServices = deliveryServices.Split('\t');
        return alldeliveryServices;
    }

    public int DeliveryServiceCount()
    {
        ArrayList unique = new ArrayList();
        bool exists = false;
        foreach (EstimatedDeliveryTime cds in this.entries)
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

        return unique.Count;
    }

    public String[] GetAvailableDays(String origCountry, String destCountry, String deliveryNames)
    {
        String[] AvailableDays;
        ReadEntries();

        EstimatedDeliveryTime OrigCountry = null, DestCountry = null, DeliveryNames = null;
        foreach (EstimatedDeliveryTime e in this.entries)
        {
            if (OrigCountry != null && DestCountry != null && DeliveryNames != null)
                break;

            if (OrigCountry == null)
                if (e.OriginCountryName.Equals(origCountry, System.StringComparison.OrdinalIgnoreCase))
                {
                    OrigCountry = e;
                }

            if (DestCountry == null)
                if (e.DestCountryName.Equals(destCountry, System.StringComparison.OrdinalIgnoreCase))
                {
                    DestCountry = e;
                }

            if (DeliveryNames == null)
                if (e.DeliveryService.Equals(deliveryNames, System.StringComparison.OrdinalIgnoreCase))
                {
                    DeliveryNames = e;
                }
        }

        if (OrigCountry != null && DestCountry != null && DeliveryNames != null)
        {
            foreach (EstimatedDeliveryTime e in this.entries)
            {
                if (e.OriginCountryName.Equals(origCountry, System.StringComparison.OrdinalIgnoreCase) &&
                    e.DestCountryName.Equals(destCountry, System.StringComparison.OrdinalIgnoreCase) &&
                    e.DeliveryService.Equals(deliveryNames, System.StringComparison.OrdinalIgnoreCase))
                {
                    AvailableDays = e.AvailableDaysToDeliver.Split(',');
                    for (int i = 0; i < AvailableDays.Length; i++)
                    {
                        switch (int.Parse(AvailableDays[i]))
                        {
                            case (int)Days.Mon:
                                AvailableDays[i] = Days.Mon.ToString();
                                break;
                            case (int)Days.Tue:
                                AvailableDays[i] = Days.Tue.ToString();
                                break;
                            case (int)Days.Wed:
                                AvailableDays[i] = Days.Wed.ToString();
                                break;
                            case (int)Days.Thu:
                                AvailableDays[i] = Days.Thu.ToString();
                                break;
                            case (int)Days.Fri:
                                AvailableDays[i] = Days.Fri.ToString();
                                break;
                            case (int)Days.Sat:
                                AvailableDays[i] = Days.Sat.ToString();
                                break;
                            case (int)Days.Sun:
                                AvailableDays[i] = Days.Sun.ToString();
                                break;
                        }                       
                    } 
                    return AvailableDays;
                }
            }
        }
        return null;
    }

    public int GetEstimatedDays(String origCountry, String destCountry, String deliveryNames)
    {
        ReadEntries();

        EstimatedDeliveryTime OrigCountry = null, DestCountry = null, DeliveryNames = null;
        foreach (EstimatedDeliveryTime e in this.entries)
        {
            if (OrigCountry != null && DestCountry != null && DeliveryNames !=null)
                break;

            if (OrigCountry == null)
                if (e.OriginCountryName.Equals(origCountry, System.StringComparison.OrdinalIgnoreCase))
                {
                    OrigCountry = e;
                }

            if (DestCountry == null)
                if (e.DestCountryName.Equals(destCountry, System.StringComparison.OrdinalIgnoreCase))
                {
                    DestCountry = e;
                }

            if (DeliveryNames == null)
                if (e.DeliveryService.Equals(deliveryNames, System.StringComparison.OrdinalIgnoreCase))
                {
                    DeliveryNames = e;
                }
        }

        if (OrigCountry != null && DestCountry != null && DeliveryNames != null)
        {
            foreach (EstimatedDeliveryTime e in this.entries)
            {
                if (e.OriginCountryName.Equals(origCountry, System.StringComparison.OrdinalIgnoreCase) &&
                    e.DestCountryName.Equals(destCountry, System.StringComparison.OrdinalIgnoreCase) &&
                    e.DeliveryService.Equals(deliveryNames, System.StringComparison.OrdinalIgnoreCase))
                {
                    return e.EstimatedDaysToDeliver;
                }
            }
        }
        return -1;
    }
}