using System;
using System.Collections;


public class EstimatedDeliveryTime
{
    private String originCountryName;
    private String destCountryName;
    private String deliveryService;
    private String weight;
    private String rateCost;
    private String availableDaysToDeliver;
    private int estimatedDaysToDeliver;

    public EstimatedDeliveryTime(String originCountryName, String destCountryName, String deliveryService,         
        String weight, String rateCost, String availableDaysToDeliver, int estimatedDaysToDeliver)
    {
        this.originCountryName = originCountryName;
        this.destCountryName = destCountryName;
        this.deliveryService = deliveryService;
        this.weight = weight;
        this.rateCost = rateCost;
        this.availableDaysToDeliver = availableDaysToDeliver;
        this.estimatedDaysToDeliver = estimatedDaysToDeliver;
    }

    public String OriginCountryName
    {
        get
        {
            return this.originCountryName;
        }

        set
        {
            this.originCountryName = value;
        }
    }

    public String DestCountryName
    {
        get
        {
            return this.destCountryName;
        }

        set
        {
            this.destCountryName = value;
        }
    }

    public String DeliveryService
    {
        get
        {
            return this.deliveryService;
        }

        set
        {
            this.deliveryService = value;
        }
    }

    public String RateCost
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

    public String Weight
    {
        get
        {
            return this.weight;
        }

        set
        {
            this.weight = value;
        }
    }

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