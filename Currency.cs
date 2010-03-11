using System;

public class Currency
{
    private String currencyType;

    public Currency(String type, double rate)
    {
        this.currencyType = type;
        this.exchangeRate = rate;
    }

    public String Type
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

    private double exchangeRate;

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
}