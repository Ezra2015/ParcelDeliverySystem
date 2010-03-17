// Given situation that Singapore unable to mail to Korea, but mailing to Malaysia
// first, then use Malaysia courier can mail to Korea.
//
// Need to calculate the Malaysia's courier charges and add it to existing charge.
//

using System;
using System.Collections;

public class IntermediateCourier
{
    private String companyName;
    private String toLocation;
    private double costPer100g;
    private String currencyType;
    private int estimatedDaysToDeliver;
    private String availableDaysToDeliver;

    public String AvailableDaysToDeliver
    {
        get
        {
            return this.availableDaysToDeliver;
        }
        set
        {
            this.availableDaysToDeliver = value;
        }
    }

    public IntermediateCourier(String companyName, String toLocation, String currencyType, double costPer100g,
        int estimatedDaysToDeliver, String availableDaysToDeliver)
    {
        this.companyName = companyName;
        this.toLocation = toLocation;
        this.costPer100g = costPer100g;
        this.currencyType = currencyType;
        this.estimatedDaysToDeliver = estimatedDaysToDeliver;
        this.availableDaysToDeliver = availableDaysToDeliver;
    }

    public String CompanyName
    {
        get
        {
            return this.companyName;
        }

        set
        {
            this.companyName = value;
        }
    }

    public String ToLocation
    {
        get
        {
            return this.toLocation;
        }

        set
        {
            this.toLocation = value;
        }
    }

    public double CostPer100g
    {
        get
        {
            return this.costPer100g;
        }

        set
        {
            this.costPer100g = value;
        }
    }

    public String CurrencyType
    {
        get
        {
            return this.currencyType;
        }

        set
        {
            this.currencyType = value;
        }
    }

    public int EstimatedDaysToDeliver
    {
        get
        {
            return this.estimatedDaysToDeliver;
        }

        set
        {
            this.estimatedDaysToDeliver = value;
        }
    }
}