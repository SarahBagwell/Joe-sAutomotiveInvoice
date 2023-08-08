// Programmer: S. Bagwell
// Class: Application Development Summer - OL1
// Program Purpose: Displays the total(s) for a customer's visit to Joe's Automotive




using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.Drawing.Text;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Joe_sAutomotive_BagwellS
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        // Declaring constant values
        public const double OIL_CHANGE = 26.00;
        public const double LUBE_JOB = 18.00;
        public const double RAD_FLUSH = 30.00;
        public const double TRANS_FLUSH = 80.00;
        public const double INSPECTION = 15.00;
        public const double MUFF_REPLACEMENT = 100.00;
        public const double TIRE_ROTATION = 20.00;
        public const double TAX_RATE = 0.06;
        public const double HOURLY_RATE = 20.00;

        // Declaring variables to hold values
        double laborServiceCost = 0.00;
        double oilAndLubeCharges = 0.00;
        double flushCharges = 0.00;
        double miscCharges = 0.00;
        double partsCost = 0.00;
        double laborCost = 0.0;
        double taxOnParts = 0.00;
        double totalFees = 0.00;

        // Method to clear check boxes in "Oil and Lube" GroupBox
        private void ClearOilLube ()
        {
            oilChangeCheckBox.Checked = false;
            lubeJobCheckBox.Checked = false;
            oilAndLubeCharges = 0.00;
        }

        // Method to clear check boxes in "Flushes" GroupBox
        private void ClearFlushes ()
        {
            transFlushCheckBox.Checked = false;
            radiatorFlushCheckBox.Checked = false;
            flushCharges = 0.00;
        }

        // Method to clear check boxes in "Misc" GroupBox
        private void ClearMisc ()
        {
            inspectionCheckBox.Checked = false;
            replaceMufflerCheckBox.Checked = false;
            tireRotationCheckBox.Checked = false;
            miscCharges = 0.00;
        }

        // Method to clear text boxes in "Parts and Labor" GroupBox
        private void ClearOther ()
        {
            laborTextBox.Text = "";
            partsTextBox.Text = "";
            partsCost = 0.00;
            laborCost = 0.00;
        }

        // Method to clear labels in "Summary" GroupBox
        private void ClearFees ()
        {
            serviceAndLaborFinalOutputLabel.Text = "";
            partsFinalOutputLabel.Text = "";
            partsTaxFinalOutputLabel.Text = "";
            feesFinalOutputLabel.Text = "";
        }

        // Method to return the charges for items in the "Oil and Lube" GroupBox
        private double OilLubeCharges()
        {
            if (oilChangeCheckBox.Checked)
                {
                    oilAndLubeCharges += OIL_CHANGE;
                }
            if (lubeJobCheckBox.Checked)
                {
                    oilAndLubeCharges += LUBE_JOB;
                }
            else
                {
                    oilAndLubeCharges += 0.00; 
                }
            return oilAndLubeCharges;
         }

        // Method to return the charges for items in the "Flushes" GroupBox
        private double FlushCharges ()
        {
            if (radiatorFlushCheckBox.Checked)
                {
                    flushCharges += RAD_FLUSH;
                }
            if (transFlushCheckBox.Checked)
                {
                    flushCharges += TRANS_FLUSH;
                }
            else
                {
                    flushCharges += 0.00;
                }
            return flushCharges;
        }

        // Method to return the charges for items in the "Misc" GroupBox
        private double MiscCharges ()
        {
            if (inspectionCheckBox.Checked)
                {
                    miscCharges += INSPECTION;
                }
            if (replaceMufflerCheckBox.Checked)
                {
                    miscCharges += MUFF_REPLACEMENT;
                }
            if (tireRotationCheckBox.Checked)
                {
                    miscCharges += TIRE_ROTATION;
                }
            else
                {
                    miscCharges += 0.00;
                }
            return miscCharges;
        }

        // Method to return the charges for items in the "Parts and Labor" GroupBox
        private double OtherCharges ()
        {
            // Parts
            if (partsTextBox.Text == "")
                {
                    partsTextBox.Text = "0.00";
                }
            try
            {
                string temp = partsTextBox.Text.ToString();

                if (!double.TryParse(temp, out partsCost))
                {
                    throw (new FormatException()); 
                }
                partsCost = Convert.ToDouble(temp);
            }
            catch (FormatException)
                {
                    MessageBox.Show("Parts Cost Format Error");

                    partsCost = 0.00;
                }
            finally
                {
                    partsFinalOutputLabel.Text = partsCost.ToString("c");
                }

            // Labor
            if (laborTextBox.Text == "")
            {
                laborTextBox.Text = "0.00";
            }
            try
            {
                string temp = laborTextBox.Text.ToString();

                if (!double.TryParse(temp, out laborCost))
                {
                    throw (new FormatException());
                }
                laborCost = Convert.ToDouble(temp);
            }
            catch (FormatException)
                {
                    MessageBox.Show("Labor Cost Format Error");

                    laborCost = 0.00;
                }
            return laborCost;
        }

        // Method to calculate tax on parts
        private double TaxCharges ()
        {
            try
            {
                if (partsCost != 0)
                {
                    taxOnParts = partsCost * TAX_RATE;
                    partsTaxFinalOutputLabel.Text = taxOnParts.ToString("c");
                }
                else
                {
                    taxOnParts = 0;
                    partsTaxFinalOutputLabel.Text = taxOnParts.ToString("c");
                }
            }
            catch
                {
                    MessageBox.Show("Parts Cost Format Error" );
                }
           
            return partsCost;
        }

        // Method to add and display the total charges in "Total Fees" - including the "Service and Labor" Label
         private double TotalCharges ()
        {

            // "Service and Labor" Label
            laborServiceCost = oilAndLubeCharges + flushCharges + miscCharges + laborCost;

            serviceAndLaborFinalOutputLabel.Text = laborServiceCost.ToString("c");

            // "Total Fees" Label
            totalFees = oilAndLubeCharges + flushCharges + miscCharges 
            + partsCost + laborCost + taxOnParts;

            feesFinalOutputLabel.Text = totalFees.ToString("c");

            return totalFees;
        }
        
        private void exitButton_Click(object sender, EventArgs e)
        {
            // Close the form
            this.Close();
        }

        private void clearButton_Click(object sender, EventArgs e)
        { 
            // Executing methods to clear form components
            ClearOilLube ();
            ClearFlushes ();
            ClearMisc ();   
            ClearOther ();
            ClearFees ();
        }

        private void calcButton_Click(object sender, EventArgs e)
        {
            // Executing methods to return charges, tax, and the total bill amount
           OilLubeCharges();
           FlushCharges ();
           MiscCharges ();
           OtherCharges ();
           TaxCharges();
           TotalCharges ();
        }
    }
}
