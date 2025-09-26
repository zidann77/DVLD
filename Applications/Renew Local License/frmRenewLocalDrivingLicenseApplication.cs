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

namespace DVLDProject.Applications.Renew_Local_License
{
    public partial class frmRenewLocalDrivingLicenseApplication : Form
    {
        int NewLicenseID = -1;
        public frmRenewLocalDrivingLicenseApplication()
        {
            InitializeComponent();
        }

        private void frmRenewLocalDrivingLicenseApplication_Load(object sender, EventArgs e)
        {
            lblApplicationDate.Text = clsFormat.DateToShort(DateTime.Now);
            lblIssueDate.Text = lblApplicationDate.Text;
            lblExpirationDate.Text = "???";
            lblCreatedByUser.Text = clsUser.FindUser(clsGlobal.CurrentUser.id).UserName;
            lblApplicationFees.Text = clsApplicationType.Find((int)clsApplication.enApplicationType.RenewDrivingLicense).Fees.ToString();
        }


        private void frmRenewLocalDrivingLicenseApplication_Activated(object sender, EventArgs e)
        {
            ctrlDriverLicenseInfoWithFilter1.txtLicenseIDFocus();
        }


        private void ctrlDriverLicenseInfoWithFilter1_OnLicenseSelected(int obj)
        {
            int OldLicenceID = obj;

             if (OldLicenceID == -1)
                return;

            lblOldLicenseID.Text = OldLicenceID.ToString();
            lbShowHistory.Enabled = true;

            int DefaultValidityLength = ctrlDriverLicenseInfoWithFilter1.SelectedLicenseInfo.LicenseClassIfo.DefaultValidityLength;
            lblExpirationDate.Text = clsFormat.DateToShort(DateTime.Now.AddYears(DefaultValidityLength));
            lblLicenseFees.Text = ctrlDriverLicenseInfoWithFilter1.SelectedLicenseInfo.LicenseClassIfo.ClassFees.ToString();
            lblTotalFees.Text = (Convert.ToSingle(lblApplicationFees.Text) + Convert.ToSingle(lblLicenseFees.Text)).ToString();
            txtNotes.Text = ctrlDriverLicenseInfoWithFilter1.SelectedLicenseInfo.Notes;

            if (!ctrlDriverLicenseInfoWithFilter1.SelectedLicenseInfo.IsLicenseExpired())
            {
                MessageBox.Show("Selected License is not yet expiared, it will expire on: " + clsFormat.DateToShort(ctrlDriverLicenseInfoWithFilter1.SelectedLicenseInfo.ExpirationDate)
                    , "Not allowed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                btnRenewLicense.Enabled = false;
                return;
            }

            if (!ctrlDriverLicenseInfoWithFilter1.SelectedLicenseInfo.IsActive)
            {
                MessageBox.Show("Selected License is not Not Active, choose an active license."
                    , "Not allowed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                btnRenewLicense.Enabled = false;
                return;
            }

            btnRenewLicense.Enabled = true;
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmShowPersonLicenseHistory frm =
              new frmShowPersonLicenseHistory(ctrlDriverLicenseInfoWithFilter1.SelectedLicenseInfo.DriverInfo.PersonID);
            frm.ShowDialog();
        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmShowLicenseInfo frm =
          new frmShowLicenseInfo(NewLicenseID);
            frm.ShowDialog();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            clsLicense NewLicense =
                ctrlDriverLicenseInfoWithFilter1.SelectedLicenseInfo.RenewLicense(txtNotes.Text.Trim(),
                clsGlobal.CurrentUser.id);

            if (NewLicense == null)
            {
                MessageBox.Show("Faild to Renew the License", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                return;
            }


            lblApplicationID.Text = NewLicense.ApplicationID.ToString();
            NewLicenseID = NewLicense.LicenseID;
            lblRenewedLicenseID.Text = NewLicenseID.ToString();
            MessageBox.Show("Licensed Renewed Successfully with ID=" + NewLicenseID.ToString(), "License Issued", MessageBoxButtons.OK, MessageBoxIcon.Information);

            btnRenewLicense.Enabled = false;
            ctrlDriverLicenseInfoWithFilter1.FilterEnabled = false;
            lbShowInfo.Enabled = true; 
        }


        //*********************************************************************


        private void gpApplicationInfo_Enter(object sender, EventArgs e)
        {

        }
                private void lblApplicationID_Click(object sender, EventArgs e)
        {

        }
                private void label4_Click(object sender, EventArgs e)
        {

        }
                private void pictureBox2_Click(object sender, EventArgs e)
        {

        }
                private void pictureBox11_Click(object sender, EventArgs e)
        {

        }
                private void label3_Click(object sender, EventArgs e)
        {

        }
                private void txtNotes_TextChanged(object sender, EventArgs e)
        {

        }
                private void lblTotalFees_Click(object sender, EventArgs e)
        {

        }
                private void label9_Click(object sender, EventArgs e)
        {

        }
                private void pictureBox10_Click(object sender, EventArgs e)
        {

        }
                private void lblLicenseFees_Click(object sender, EventArgs e)
        {

        }
        private void label7_Click(object sender, EventArgs e)
        {

        }
                private void pictureBox9_Click(object sender, EventArgs e)
        {

        }
                private void pictureBox8_Click(object sender, EventArgs e)
        {

        }
        private void lblOldLicenseID_Click(object sender, EventArgs e)
        {

        }
                private void label12_Click(object sender, EventArgs e)
        {

        }
                private void pictureBox7_Click(object sender, EventArgs e)
        {

        }

        private void lblRenewedLicenseID_Click(object sender, EventArgs e)
        {

        }

        private void label10_Click(object sender, EventArgs e)
        {

        }

        private void lblExpirationDate_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox6_Click(object sender, EventArgs e)
        {

        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void lblIssueDate_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox5_Click(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void lblCreatedByUser_Click(object sender, EventArgs e)
        {

        }

        private void lblApplicationFees_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {

        }

        private void lblApplicationDate_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

       
        private void lblTitle_Click(object sender, EventArgs e)
        {

        }
    }
}
