namespace ParcelDeliverySystem
{
    partial class PDS
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PDS));
            this.rateCost = new System.Windows.Forms.Button();
            this.datePicker = new System.Windows.Forms.DateTimePicker();
            this.tabControl = new System.Windows.Forms.TabControl();
            this.estimatedDeliveryTabPage1 = new EstimatedDeliveryTimeTabPage();
            this.intermediateCourierTabPage1 = new IntermediateCourierTabPage();
            this.currencyConverterTabPage1 = new CurrencyConverterTabPage();
            this.postageChargeTabPage1 = new PostageChargeTabPage();
            this.closeBtn = new System.Windows.Forms.Button();
            this.tabControl.SuspendLayout();
            this.SuspendLayout();
            // 
            // rateCost
            // 
            this.rateCost.Location = new System.Drawing.Point(442, 9);
            this.rateCost.Name = "rateCost";
            this.rateCost.Size = new System.Drawing.Size(166, 23);
            this.rateCost.TabIndex = 1;
            this.rateCost.Text = "Local/Overseas Postage Rates";
            this.rateCost.UseVisualStyleBackColor = true;
            // 
            // tabControl
            // 
            this.tabControl.Controls.Add(this.estimatedDeliveryTabPage1);
            this.tabControl.Controls.Add(this.intermediateCourierTabPage1);
            this.tabControl.Controls.Add(this.currencyConverterTabPage1);
            this.tabControl.Controls.Add(this.postageChargeTabPage1);
            this.tabControl.Location = new System.Drawing.Point(12, 16);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(600, 350);
            this.tabControl.TabIndex = 0;
            // 
            // estimatedDeliveryTabPage1
            // 
            this.estimatedDeliveryTabPage1.Location = new System.Drawing.Point(4, 22);
            this.estimatedDeliveryTabPage1.Name = "estimatedDeliveryTabPage1";
            this.estimatedDeliveryTabPage1.Size = new System.Drawing.Size(592, 324);
            this.estimatedDeliveryTabPage1.TabIndex = 1;
            this.estimatedDeliveryTabPage1.Text = "Estimated Delivery Time";
            this.estimatedDeliveryTabPage1.Visible = false;
            // 
            // intermediateCourierTabPage1
            // 
            this.intermediateCourierTabPage1.Location = new System.Drawing.Point(4, 22);
            this.intermediateCourierTabPage1.Name = "intermediateCourierTabPage1";
            this.intermediateCourierTabPage1.Size = new System.Drawing.Size(592, 324);
            this.intermediateCourierTabPage1.TabIndex = 2;
            this.intermediateCourierTabPage1.Text = "Intermediate Courier";
            this.intermediateCourierTabPage1.Visible = false;
            // 
            // currencyConverterTabPage1
            // 
            this.currencyConverterTabPage1.Location = new System.Drawing.Point(4, 22);
            this.currencyConverterTabPage1.Name = "currencyConverterTabPage1";
            this.currencyConverterTabPage1.Size = new System.Drawing.Size(592, 324);
            this.currencyConverterTabPage1.TabIndex = 3;
            this.currencyConverterTabPage1.Text = "Currency Converter";
            this.currencyConverterTabPage1.Visible = false;
            // 
            // postageChargeTabPage1
            // 
            this.postageChargeTabPage1.Location = new System.Drawing.Point(4, 22);
            this.postageChargeTabPage1.Name = "postageChargeTabPage1";
            this.postageChargeTabPage1.Size = new System.Drawing.Size(592, 324);
            this.postageChargeTabPage1.TabIndex = 4;
            this.postageChargeTabPage1.Text = "Postage Charge";
            this.postageChargeTabPage1.Visible = false;
            // 
            // closeBtn
            // 
            this.closeBtn.Location = new System.Drawing.Point(260, 372);
            this.closeBtn.Name = "closeBtn";
            this.closeBtn.Size = new System.Drawing.Size(75, 23);
            this.closeBtn.TabIndex = 2;
            this.closeBtn.Text = "Close";
            this.closeBtn.Click += new System.EventHandler(this.closeBtn_Click);
            // 
            // PDS
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(634, 402);
            this.Controls.Add(this.closeBtn);
            this.Controls.Add(this.rateCost);
            this.Controls.Add(this.tabControl);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximumSize = new System.Drawing.Size(800, 600);
            this.MinimumSize = new System.Drawing.Size(640, 400);
            this.Name = "PDS";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Parcel Delivery System";
            this.Load += new System.EventHandler(this.PDS_Load);
            this.tabControl.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button rateCost;
        private System.Windows.Forms.TabControl tabControl;
        private System.Windows.Forms.DateTimePicker datePicker;
        private EstimatedDeliveryTimeTabPage estimatedDeliveryTabPage1;
        private IntermediateCourierTabPage intermediateCourierTabPage1;
        private CurrencyConverterTabPage currencyConverterTabPage1;
        private PostageChargeTabPage postageChargeTabPage1;
        private System.Windows.Forms.Button closeBtn;
    }
}

