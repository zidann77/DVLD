using BusinessLogic_DVLD;
using DVLDProject.Global_Classes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLDProject.Applications.Application_Types
{
    public partial class EditApplicationTypes : Form
    {
        private int ID = -1; // initilizing 
        private clsApplicationType Application;
        public EditApplicationTypes(int id)
        {
            InitializeComponent();
            ID = id;
        }

        void SetInfo()
        {
            lbID.Text = ID.ToString();
            txtFees.Text = Application.Fees.ToString();
            txtTitle.Text = Application.Title.ToString();
        }

        private void EditApplicationTypes_Load(object sender, EventArgs e)
        {
            Application = clsApplicationType.Find(ID);

            if(Application == null)
            {
                MessageBox.Show("No Application Type With ID = "+ID , "in This System" , MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
                return;
            }
            else
            {
                SetInfo();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (!this.ValidateChildren())
            {
                MessageBox.Show("Some fileds are not valide!, put the mouse over the red icon(s) to see the erro", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            else
            {
                Application.Title = txtTitle.Text.Trim();
                Application.Fees = Convert.ToSingle(txtFees.Text.Trim());

                if (Application.Save())
                {
                    MessageBox.Show("Data Saved Successfully.", "Saved", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                    MessageBox.Show("Error: Data Is not Saved Successfully.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtTitle_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(txtTitle.Text.Trim()))
            { 
                e.Cancel = true;
                errorProvider1.SetError(txtTitle, "Title cannot be empty!");
            }
            else
            {
                errorProvider1.SetError(txtTitle, null);
            }

        }

        private void txtFees_Validating(object sender, CancelEventArgs e)
        {
            if(string.IsNullOrEmpty(txtFees.Text.Trim()))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtFees, "Title cannot be empty!");
            }
            else
            {
                errorProvider1.SetError(txtFees, null);
            }

            if (!clsValidation.IsNumber(txtFees.Text.Trim()))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtFees, "Invalid Number.");
            }
            else
            {
                errorProvider1.SetError(txtFees, null);
            }

        }
    }
}
