using System;
using System.Drawing;
using System.ComponentModel;
using System.Windows.Forms;

public class PostageRatesTable : TableLayoutPanel
{
    // origin, destination, delivery service, weight, charges (4 parts), available days, estimated delivery time (working days)
    private TextBox lblOrigin;
    private TextBox lblDestination;
    private TextBox lblDeliveryService;
    private TextBox lblWeight;
    private TextBox lblCharges;
    private TextBox lblAvailableDays;
    private TextBox lblEstimatedDeliveryTime;

    private TextBox MakeNewTextBox()
    {
        TextBox tb = new TextBox();
        tb.Size = new Size(70, 50);
        tb.WordWrap = true;
        tb.ReadOnly = true;
        tb.Multiline = true;
        tb.BorderStyle = BorderStyle.None;
        return tb;
    }

    public PostageRatesTable()
    {
        lblOrigin = MakeNewTextBox();
        lblDestination = MakeNewTextBox();
        lblDeliveryService = MakeNewTextBox();
        lblWeight = MakeNewTextBox();
        lblCharges = MakeNewTextBox();
        lblAvailableDays = MakeNewTextBox();
        lblEstimatedDeliveryTime = MakeNewTextBox();

        lblOrigin.Text = "Origin";
        lblDestination.Text = "Destination";
        lblDeliveryService.Text = "Delivery Service";
        lblWeight.Text = "Weight";
        lblCharges.Text = "Charges (20g, 50g, 100g, per additional 100g)";
        lblAvailableDays.Text = "Available Days";
        lblEstimatedDeliveryTime.Text = "Estimated Delivery Time";

        this.Size = new Size(700, 500);
        this.ColumnCount = 7;
        this.RowCount = 0;
        this.CellBorderStyle = TableLayoutPanelCellBorderStyle.OutsetDouble;
        this.AutoSize = true;

        this.Controls.Add(lblOrigin);
        this.Controls.Add(lblDestination);
        this.Controls.Add(lblDeliveryService);
        this.Controls.Add(lblWeight);
        this.Controls.Add(lblCharges);
        this.Controls.Add(lblAvailableDays);
        this.Controls.Add(lblEstimatedDeliveryTime);

        PopulateTable();
    }

    private void PopulateTable()
    {
        DatabaseManager dm = new DatabaseManager("estimatedDeliveryTime.txt");
        for (int i = 0; i < dm.Count; ++i)
        {
            String s = (String) dm.GetObject(i);
            String[] arr = s.Split('\t');
            foreach (String st in arr)
            {
                TextBox tb = MakeNewTextBox();
                tb.Text = st;
                this.Controls.Add(tb);
            }
        }
    }
}