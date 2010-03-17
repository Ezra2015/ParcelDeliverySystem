using System;
using System.Drawing;
using System.ComponentModel;
using System.Windows.Forms;

public class PostageRatesDialog : Form
{
    public PostageRatesDialog()
    {
        this.Text = "Postage Rates Table";
        this.Size = new Size(700, 500);
        Panel p = new Panel();
        p.AutoScroll = true;
        p.Controls.Add(new PostageRatesTable());
        p.Size = new Size(650, 450);
        this.Controls.Add(p);
    }
}