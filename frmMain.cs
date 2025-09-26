using BusinessLogic_DVLD;
using DVLDProject.Applications.Application_Types;
using DVLDProject.Applications.InternationalLicense;
using DVLDProject.Applications.Local_Driving_License;
using DVLDProject.Applications.Release_Detained_License;
using DVLDProject.Applications.Renew_Local_License;
using DVLDProject.Applications.ReplaceLostOrDamagedLicense;
using DVLDProject.Drivers;
using DVLDProject.Licenses;
using DVLDProject.Licenses.Detain_license;
using DVLDProject.PersonFolder;
using DVLDProject.UserFolder;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLDProject
{
    public partial class frmMain : Form
    {
        frmloginForm frmloginForm = new frmloginForm();
        public frmMain(frmloginForm frmLogin)
        {
            InitializeComponent();
            frmloginForm = frmLogin;
        }

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void signOutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            clsGlobal.CurrentUser = null;
            this.Close();
            frmloginForm.Show();
        }

        private void currentUserInfoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            UserInfo frm = new UserInfo(clsGlobal.CurrentUser.id);
            frm.ShowDialog();
        }

        private void changePasswordToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ChangePassword frm = new ChangePassword(clsGlobal.CurrentUser.id);
            frm.ShowDialog();
        }

        private void usersToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmListUsers frm = new frmListUsers();
            frm.ShowDialog();
        }

        private void peopleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmListPeople frm = new frmListPeople();
            frm.ShowDialog();
            
        }

        private void manageTestTypesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmListTypes frm = new frmListTypes();
            frm.ShowDialog();
        }

        private void manageApplicationTypesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ListApplicationType frm = new ListApplicationType();
            frm.ShowDialog();
        }

        private void detainLicensesToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void localDrivingLicenseApplicationsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmListLocalDrivingLicesnseApplications frm = new frmListLocalDrivingLicesnseApplications();
            frm.ShowDialog();
        }

        private void localToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmAddUpdateLocalLicense frm = new frmAddUpdateLocalLicense();
            frm.ShowDialog();
        }

        private void driversToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ListDrivers listDrivers = new ListDrivers();
            listDrivers.ShowDialog();
        }

        private void internationalDrivingLicenseApplicatiosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ListInternationalLicenceApplications frm = new ListInternationalLicenceApplications();
            frm.ShowDialog();
        }

        private void internationalLiceenseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmNewInternationalLicense frm  = new frmNewInternationalLicense();
            frm.ShowDialog();
        }

        private void manageDetainedLicensesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmListDetainedLicenses frm = new frmListDetainedLicenses();
            frm.ShowDialog();
        }

        private void renewDrivingLicenseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmRenewLocalDrivingLicenseApplication frm = new frmRenewLocalDrivingLicenseApplication();
            frm.ShowDialog();
        }

        private void detainLicenseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmDetainLicenseApplication frm = new frmDetainLicenseApplication();    
            frm.ShowDialog();
        }

        private void releaseDetainedDrivingLicenseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmReleaseDetainedLicenseApplication frm = new frmReleaseDetainedLicenseApplication();
            frm.ShowDialog();
        }

        private void releaseDetainedLicenseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmReleaseDetainedLicenseApplication frmReleaseDetainedLicenseApplication = new frmReleaseDetainedLicenseApplication();
            frmReleaseDetainedLicenseApplication.ShowDialog();
        }

        private void replacementToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmReplaceLostOrDamagedLicenseApplication frm = new frmReplaceLostOrDamagedLicenseApplication();
            frm.ShowDialog();
        }

        private void retakeTestToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmListLocalDrivingLicesnseApplications frm = new frmListLocalDrivingLicesnseApplications();
            frm.ShowDialog();
        }
    }
}
