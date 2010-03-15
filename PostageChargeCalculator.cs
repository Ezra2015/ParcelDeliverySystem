using System;
using System.Collections;
using System.Windows.Forms;
using System.Drawing;
using System.ComponentModel;

public class PostageChargeCalculator
{
    private String currencyPath;
    private String estimatedDeliveryPath;
    private String intermediatecourier;
    private ArrayList entries;
    private CurrencyConverter cc;
    private EstimatedDeliveryTimeCalculator edtc;
    private IntermediateCourier ic;

    public PostageChargeCalculator(String estimatedDeliveryPath, String currencyPath, String intermediatecourier)
    {
        this.estimatedDeliveryPath = estimatedDeliveryPath;
        this.currencyPath = currencyPath;
        this.intermediatecourier = intermediatecourier;
        this.edtc = new EstimatedDeliveryTimeCalculator(this.estimatedDeliveryPath);
        this.cc = new CurrencyConverter(this.currencyPath);
        //this.ic = new IntermediateCourier();


    }
}
