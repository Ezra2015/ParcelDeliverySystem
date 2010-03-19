using System;
using System.Collections;

public class PostageCharge
{
    private String originCountryName;
    private String destCountryName;
    private String deliveryService;
    private String weight;
    private String rateCost;
    private String startdate;
    private String enddate;
    private String estimatedDaysToDeliver;

    public PostageCharge(String deliveryService, String originCountryName, String destCountryName, String weight,
        String rateCost, String startdate, String enddate, String estimatedDaysToDeliver)
    {
        this.deliveryService = deliveryService;
        this.originCountryName = originCountryName;
        this.destCountryName = destCountryName;
        this.weight = weight;
        this.rateCost = rateCost;
        this.startdate = startdate;
        this.enddate = enddate;
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

    public String StartDate
    {
        get
        {
            return this.startdate;
        }

        set
        {
            this.startdate = value;
        }
    }

    public String EndDate
    {
        get
        {
            return this.enddate;
        }

        set
        {
            this.enddate = value;
        }
    }

    public String EstimatedDaysToDeliver
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