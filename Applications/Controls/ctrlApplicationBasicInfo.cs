using BusinessLogic_DVLD;
using DVLDProject.Global_Classes;
using DVLDProject.PersonFolder;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Net.Mime.MediaTypeNames;

namespace DVLDProject.Applications.Controls
{
    public partial class ctrlApplicationBasicInfo : UserControl
    {

        int _ApplicationID = -1;
        public int ApplicationId
        {
            get { return _ApplicationID; }
        }

        clsApplication _Application;

        public void ResetDefaultValues()
        {
            lblApplicationID.Text = "[????]";
            lblStatus.Text = "[????]";
            lblType.Text = "[????]";
            lblFees.Text = "[????]";
            lblApplicant.Text = "[????]";
            lblDate.Text = "[????]";
            lblStatusDate.Text = "[????]";
            lblCreatedByUser.Text = "[????]";

            llViewPersonInfo.Enabled = false;
        }

        public void LoadData(int AppID)
        {
            _Application = clsApplication.FindBaseApplication(AppID);
            if (_Application == null)
            {
                ResetDefaultValues();
                MessageBox.Show("No Application with ApplicationID = " + AppID.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            FillInfo();
        }

        void FillInfo()
        {
            _ApplicationID = _Application.ApplicationID;
            lblApplicationID.Text = _Application.ApplicationID.ToString();
            lblStatus.Text = _Application.StatusText;
            lblType.Text = _Application.ApplicationTypeInfo.Title;
            lblFees.Text = _Application.PaidFees.ToString();
            UpdateApplicantName();
            lblDate.Text = clsFormat.DateToShort(_Application.ApplicationDate);
            lblStatusDate.Text = clsFormat.DateToShort(_Application.LastStatusDate);
            lblCreatedByUser.Text = _Application.CreatedByUserInfo.UserName;

            llViewPersonInfo.Enabled = true;
        }

        public ctrlApplicationBasicInfo()
        {
            InitializeComponent();
        }

        void UpdateApplicantName()
        {
            lblApplicant.Text = _Application.ApplicantFullName;
        }


        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }


        private void llViewPersonInfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmShowPersonDetails frm = new frmShowPersonDetails(_Application.ApplicantPersonID);
            frm.ShowDialog();
            UpdateApplicantName();
        }

    }
}
