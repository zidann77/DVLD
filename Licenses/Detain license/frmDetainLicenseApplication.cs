using BusinessLogic_DVLD;
using DVLDProject.Global_Classes;
using DVLDProject.Licenses.LocalLicense;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLDProject.Licenses.Detain_license
{
    public partial class frmDetainLicenseApplication : Form
    {
        int selectedLicenseID = -1;
        public frmDetainLicenseApplication()
        {
            InitializeComponent();
        }

        private void lblLicenseID_Click(object sender, EventArgs e)
        {

        }

        private void llShowLicenseHistory_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmShowPersonLicenseHistory frmShowPersonLicenseHistory = new frmShowPersonLicenseHistory(ctrlDriverLicenseInfoWithFilter1.SelectedLicenseInfo.DriverInfo.PersonID);
            frmShowPersonLicenseHistory.ShowDialog();
        }

        private void llShowLicenseInfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmShowLicenseInfo frm = new frmShowLicenseInfo(ctrlDriverLicenseInfoWithFilter1.LicenseID);
            frm.ShowDialog();
        }

        private void frmDetainLicenseApplication_Load(object sender, EventArgs e)
        {
            lblDetainDate.Text = clsFormat.DateToShort(DateTime.Now);
            lblCreatedByUser.Text = clsUser.FindUser(clsGlobal.CurrentUser.id).UserName;
        }

        private void ctrlDriverLicenseInfoWithFilter1_OnLicenseSelected(int obj)
        {
            selectedLicenseID = obj;
            lblLicenseID.Text = selectedLicenseID.ToString();
            llShowLicenseHistory.Enabled = true;

            if (selectedLicenseID == -1)
            {
                return;
            }
            else if(ctrlDriverLicenseInfoWithFilter1.SelectedLicenseInfo.IsDetained)
            {
                MessageBox.Show("Selected License i already detained, choose another one.", "Not allowed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            btnDetain.Enabled = true;
            txtFineFees.Focus();
        }

        private void frmDetainLicenseApplication_Activated(object sender, EventArgs e)
        {
            ctrlDriverLicenseInfoWithFilter1.txtLicenseIDFocus();
        }

        private void btnDetain_Click(object sender, EventArgs e)
        {
            if (this.ValidateChildren() && MessageBox.Show("Are you sure you want to detain this license?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
            {
                return;
            }

            int DetainedID = ctrlDriverLicenseInfoWithFilter1.SelectedLicenseInfo.Detain(Convert.ToSingle(txtFineFees.Text), clsGlobal.CurrentUser.id);

            if(DetainedID == -1) {
                MessageBox.Show("Faild to Detain License", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                return;
            }
            lblDetainID.Text = DetainedID.ToString();
            MessageBox.Show("License Detained Successfully with ID=" + DetainedID.ToString(), "License Issued", MessageBoxButtons.OK, MessageBoxIcon.Information);

            btnDetain.Enabled = false;
            ctrlDriverLicenseInfoWithFilter1.FilterEnabled = false;
            txtFineFees.Enabled = false;
            llShowLicenseInfo.Enabled = true;

        }

        private void txtFineFees_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(txtFineFees.Text.Trim()))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtFineFees, "Fees cannot be empty!");
                return;
            }
            else
            {
                errorProvider1.SetError(txtFineFees, null);
               
            };


            if (!clsValidation.IsNumber(txtFineFees.Text))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtFineFees, "Invalid Number.");
            }
            else
            {
                errorProvider1.SetError(txtFineFees, null);
              
            };
        }

        private void txtFineFees_Validated(object sender, EventArgs e)
        {
            btnDetain.Focus();
        }
    }
}
