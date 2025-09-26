using BusinessLogic_DVLD;
using DVLDProject.Global_Classes;
using DVLDProject.Licenses;
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
using static BusinessLogic_DVLD.clsLicense;

namespace DVLDProject.Applications.ReplaceLostOrDamagedLicense
{
    public partial class frmReplaceLostOrDamagedLicenseApplication : Form
    {
        public frmReplaceLostOrDamagedLicenseApplication()
        {
            InitializeComponent();
        }

        private int NewLicenseID = -1;

        private void frmReplaceLostOrDamagedLicenseApplication_Load(object sender, EventArgs e)
        {
            lblApplicationDate.Text = clsFormat.DateToShort(DateTime.Now);
            lblCreatedByUser.Text = clsUser.FindUser(clsGlobal.CurrentUser.id).UserName;
            rbLostLicense.Checked = true;
        }

        private void frmReplaceLostOrDamagedLicenseApplication_Activated(object sender, EventArgs e)
        {
            ctrlDriverLicenseInfoWithFilter1.txtLicenseIDFocus();
        }

        private void ctrlDriverLicenseInfoWithFilter1_OnLicenseSelected(int obj)
        {
            NewLicenseID = obj;
            lblOldLicenseID.Text = NewLicenseID.ToString();
            llShowLicenseHistory.Visible = true;

            if(NewLicenseID == -1)
            {
                return;
            }
            else if (!ctrlDriverLicenseInfoWithFilter1.SelectedLicenseInfo.IsActive)
            {
                MessageBox.Show("Selected License is not Not Active, choose an active license."
                   , "Not allowed", MessageBoxButtons.OK, MessageBoxIcon.Error);
           
                return;
            }

            btnIssueReplacement.Enabled = true;

        }

        private enIssueReason _GetIssueReason()
        {
            //this will decide which reason to issue a replacement for

            if (rbDamagedLicense.Checked)

                return enIssueReason.DamagedReplacement;
            else
                return enIssueReason.LostReplacement;
        }


        private void btnIssueReplacement_Click(object sender, EventArgs e)
        {

            if (MessageBox.Show("Are you sure you want to Issue a Replacement for the license?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
            {
                return;
            }


            clsLicense NewLicense =
               ctrlDriverLicenseInfoWithFilter1.SelectedLicenseInfo.Replace(_GetIssueReason(),
               clsGlobal.CurrentUser.id);

            if (NewLicense == null)
            {
                MessageBox.Show("Faild to Issue a replacemnet for this  License", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                return;
            }

            lblApplicationID.Text = NewLicense.ApplicationID.ToString();
            NewLicenseID = NewLicense.LicenseID;

            lblRreplacedLicenseID.Text = NewLicenseID.ToString();
            MessageBox.Show("Licensed Replaced Successfully with ID=" + NewLicenseID.ToString(), "License Issued", MessageBoxButtons.OK, MessageBoxIcon.Information);

            btnIssueReplacement.Enabled = false;
            gbReplacementFor.Enabled = false;
            ctrlDriverLicenseInfoWithFilter1.FilterEnabled = false;
            llShowLicenseInfo.Enabled = true;
        }

        private void llShowLicenseHistory_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmShowPersonLicenseHistory frm = new frmShowPersonLicenseHistory(ctrlDriverLicenseInfoWithFilter1.SelectedLicenseInfo.DriverInfo.PersonID);
            frm.ShowDialog();
        }

        private void llShowLicenseInfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmShowLicenseInfo frm = new frmShowLicenseInfo(ctrlDriverLicenseInfoWithFilter1.LicenseID);
            frm.ShowDialog();
        }

        private int _GetApplicationTypeID()
        {
            //this will decide which application type to use accirding 
            // to user selection.

            if (rbDamagedLicense.Checked)

                return (int)clsApplication.enApplicationType.ReplaceDamagedDrivingLicense;
            else
                return (int)clsApplication.enApplicationType.ReplaceLostDrivingLicense;
        }

        private void rbDamagedLicense_CheckedChanged(object sender, EventArgs e)
        {
            lblTitle.Text = "Replacement for Damaged License";
            this.Text = lblTitle.Text;
            lblApplicationFees.Text = clsApplicationType.Find(_GetApplicationTypeID()).Fees.ToString();
        }

        private void rbLostLicense_CheckedChanged(object sender, EventArgs e)
        {
            lblTitle.Text = "Replacement for Lost License";
            this.Text = lblTitle.Text;
            lblApplicationFees.Text = clsApplicationType.Find(_GetApplicationTypeID()).Fees.ToString();
        }
    }
}
