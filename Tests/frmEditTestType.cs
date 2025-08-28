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

namespace DVLDProject.Tests
{
    public partial class frmEditTestType : Form
    {
        int ID = -1;
        clsTestType Test;
        public frmEditTestType(int id)
        {
            InitializeComponent();
            ID = id;
        }

        void _LoadInfo()
        {
            lbID.Text = Test.ID.ToString();
            txtTitle.Text = Test.Title.ToString();
            txtDescription.Text = Test.Description.ToString();
            txtFees.Text = Test.Fees.ToString();
        }


        private void frmEditTestType_Load(object sender, EventArgs e)
        {
            clsTestType.enTestType Type = (clsTestType.enTestType)ID;
            Test = clsTestType.Find(Type);

            if (Test != null)
            {
                _LoadInfo();
            }
            else
            {
                MessageBox.Show("Could not find Test Type with id = " + ID, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                btnClose.PerformClick();
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
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
            };
        }

        private void txtDescription_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(txtDescription.Text.Trim()))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtDescription, "Description cannot be empty!");
            }
            else
            {
                errorProvider1.SetError(txtDescription, null);
            };
        }

        private void txtFees_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(txtFees.Text.Trim()))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtFees, "Fees cannot be empty!");
                return;
            }
            else
            {
                errorProvider1.SetError(txtFees, null);
            };


            if (!clsValidation.IsNumber(txtFees.Text))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtFees, "Invalid Number.");
            }
            else
            {
                errorProvider1.SetError(txtFees, null);
            };
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!this.ValidateChildren())
            {
                MessageBox.Show("Some fileds are not valide!, put the mouse over the red icon(s) to see the erro", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            else
            {
                Test.Title = txtTitle.Text;
                Test.Description = txtDescription.Text;
                Test.Fees = Convert.ToSingle(txtFees.Text);

                if (Test.Save())
                {
                    MessageBox.Show("Data Saved Successfully.", "Saved", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                    MessageBox.Show("Error: Data Is not Saved Successfully.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }
    }
}
