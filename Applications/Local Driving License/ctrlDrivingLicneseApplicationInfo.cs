using BusinessLogic_DVLD;
using DVLDProject.Licenses.LocalLicense;
using DVLDProject.PersonFolder;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Permissions;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLDProject.Applications.Local_Driving_License
{
    public partial class ctrlDrivingLicneseApplicationInfo : UserControl
    {
        clsLocalDrivingLicenseApplication _Application;
        int _AppID = -1;

        public int ApplicationID
        {
            get { return _AppID; }
        }



        int LicenseID = -1;
        public ctrlDrivingLicneseApplicationInfo()
        {
            InitializeComponent();
        }

      

        void FillInfo()
        {
            ctrlApplicationBasicInfo1.LoadData(_Application.ApplicationID);

            LicenseID = _Application.GetActiveLicenseID();

            llShowLicenceInfo.Enabled =( LicenseID != -1);

            lblLocalDrivingLicenseApplicationID.Text = _Application.ApplicationID.ToString();
            lblAppliedFor.Text = clsLicenseClass.Find(_Application.LicenseClassID).ClassName;

            lblPassedTests.Text = clsLocalDrivingLicenseApplication.GetPassedTestCount(_Application.LocalDrivingLicenseApplicationID).ToString();
     
        }

        public void LoadData(int ApplicationId)
        {
            _Application = clsLocalDrivingLicenseApplication.FindByLocalDrivingAppLicenseID(ApplicationId);

            if (_Application == null)
            {
                MessageBox.Show("No Application with ApplicationID = " + ApplicationID.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            FillInfo();

        }

        public void LoadDataByLicenseID(int LicenseId)
        {
            _Application = clsLocalDrivingLicenseApplication.FindByLocalDrivingAppLicenseID(LicenseId);

            if (_Application == null)
            {
                MessageBox.Show("No Application with ApplicationID = " + ApplicationID.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            FillInfo();

        }

        private void _ResetLocalDrivingLicenseApplicationInfo()
        {
            _AppID = -1;
            ctrlApplicationBasicInfo1.ResetDefaultValues();
            lblLocalDrivingLicenseApplicationID.Text = "[????]";
            lblAppliedFor.Text = "[????]";
            lblPassedTests.Text = "0";

        }


        private void ctrlDrivingLicneseApplicationInfo_Load(object sender, EventArgs e)
        {

        }

        private void ctrlApplicationBasicInfo1_Load(object sender, EventArgs e)
        {
           
        }

        private void llShowLicenceInfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmShowLicenseInfo frm  = new frmShowLicenseInfo(LicenseID);
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }
    }
}
