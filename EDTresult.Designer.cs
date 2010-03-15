namespace ParcelDeliverySystem
{
    partial class EDTresult
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
            this.lbltest = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lbltest
            // 
            this.lbltest.AutoSize = true;
            this.lbltest.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbltest.Location = new System.Drawing.Point(0, 0);
            this.lbltest.Margin = new System.Windows.Forms.Padding(5);
            this.lbltest.Name = "lbltest";
            this.lbltest.Size = new System.Drawing.Size(673, 13);
            this.lbltest.TabIndex = 0;
            this.lbltest.Text = "Delivery Service         Origin Country           Destination Country      Start " +
                "Delivery Time      Expected Delivery Time   Estimated Working Days   \n";
            // 
            // EDTresult
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(284, 262);
            this.Controls.Add(this.lbltest);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "EDTresult";
            this.Text = "Estimated Delivery Time Result";
            this.Load += new System.EventHandler(this.EDTresult_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lbltest;
    }
}