using System;

public class InterCourCurrency
{
    private String companyName;
    private String toLocation;
    private String currency;
    private Int32 estimatedDays;
    private String availableDays;
    private Int32 rateCost;
    private String type;
    private double exchangeRate;
    private double mass;

    public InterCourCurrency(String companyName, String toLocation, String currency, Int32 estimatedDays, 
        String availableDays, Int32 rateCost, String type, double exchangeRate, double mass)
    {
        this.companyName = companyName;
        this.toLocation = toLocation;
        this.currency = currency;
        this.estimatedDays = estimatedDays;
        this.availableDays = availableDays;
        this.rateCost = rateCost;
        this.type = type;
        this.exchangeRate = exchangeRate;
        this.mass = mass;
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

    public String Currency
    {
        get
        {
            return this.currency;
        }

        set
        {
            this.currency = value;
        }
    }

    public Int32 EstimatedDays
    {
        get
        {
            return this.estimatedDays;
        }

        set
        {
            this.estimatedDays = value;
        }
    }

    public String AvailableDays
    {
        get
        {
            return this.availableDays;
        }

        set
        {
            this.availableDays = value;
        }
    }

    public Int32 RateCost
    {
        get
        {
            return this.rateCost;
        }

        set
        {
            this.rateCost = value;
        }
    }

    public String Type
    {
        get
        {
            return this.type;
        }

        set
        {
            this.type = value;
        }
    }

    public double ExchangeRate
    {
        get
        {
            return this.exchangeRate;
        }

        set
        {
            this.exchangeRate = value;
        }
    }

    public double Mass
    {
        get
        {
            return this.mass;
        }

        set
        {
            this.mass = value;
        }
    }
}