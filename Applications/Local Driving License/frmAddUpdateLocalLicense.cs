using BusinessLogic_DVLD;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLDProject.Applications.Local_Driving_License
{
    public partial class frmAddUpdateLocalLicense : Form
    {
        int _LocalDrivingLicenseApplicationID = -1;

        clsLocalDrivingLicenseApplication _LocalDrivingLicenseApplication;


        enum enMode { AddNew = 1, Update = 2 }

        enMode Mode = enMode.AddNew;

        public frmAddUpdateLocalLicense()
        {
            InitializeComponent();
        }

        public frmAddUpdateLocalLicense(int AppID)
        {
            InitializeComponent();
            _LocalDrivingLicenseApplicationID = AppID;
            Mode = enMode.Update;
        }

        void FillLicenseClassesInBox()
        {
           DataTable DT = clsLicenseClass.GetAllLicenseClasses();

          foreach (DataRow dr in DT.Rows)
            {
                cbLicenseClass.Items.Add(dr["ClassName"].ToString());
            }
        }

        void ResetDefaultValues()
        {
            lblTitle.Text = "New Local Driving License Application";
            this.Text = "New Local Driving License Application";

            _LocalDrivingLicenseApplication = new clsLocalDrivingLicenseApplication();

            personCardWithFilter1.FilterFocus();

            tpApplicationInfo.Enabled = false;
           
        }

        void LoadData()
        {
            lblTitle.Text = "Update Local Driving License Application";
            this.Text = "Update Local Driving License Application";

            tpApplicationInfo.Enabled = true;
            personCardWithFilter1.EnableFilter = false;

            _LocalDrivingLicenseApplication = clsLocalDrivingLicenseApplication.FindByLocalDrivingAppLicenseID(_LocalDrivingLicenseApplicationID);

            if( _LocalDrivingLicenseApplication == null )
            {
                MessageBox.Show("No Application with ID = " + _LocalDrivingLicenseApplicationID, "Application Not Found", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                this.Close();

                return;
            }


            personCardWithFilter1.LoadPersonInfo(_LocalDrivingLicenseApplication.ApplicantPersonID);


        }

        void RefreshData()
        {
            FillLicenseClassesInBox();

            lblFees.Text = clsApplicationType.Find((int)clsApplication.enApplicationType.NewDrivingLicense).Fees.ToString();
            lblApplicationDate.Text = DateTime.Now.ToShortDateString();
            lbCreatedBy.Text = clsUser.FindUser(clsGlobal.CurrentUser.id).UserName;

            switch (Mode)
            {
                case enMode.AddNew:
                    ResetDefaultValues();
                    break;

                case enMode.Update:
                    LoadData(); 
                    break;
            }
        }

        private void frmAddUpdateLocalLicense_Load(object sender, EventArgs e)
        {
                          RefreshData();   
        }

        private void btnApplicationInfoNext_Click(object sender, EventArgs e)
        {
            if(personCardWithFilter1.PersonID != -1)
            {
                tpApplicationInfo.Enabled = true;

                tabControl1.SelectedTab = tabControl1.TabPages["tpApplicationInfo"];

            }

            else
            {
                MessageBox.Show("Please Select a Person", "Select a Person", MessageBoxButtons.OK, MessageBoxIcon.Error);
                personCardWithFilter1.FilterFocus();
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!this.ValidateChildren())
            {
                MessageBox.Show("Some fileds are not valide!, put the mouse over the red icon(s) to see the erro", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            int LicenseClassID = clsLicenseClass.Find(cbLicenseClass.Text).LicenseClassID;

            int ActiveAppID = clsApplication.GetActiveApplicationIDForLicenseClass(personCardWithFilter1.PersonID, clsApplication.enApplicationType.NewDrivingLicense, LicenseClassID);

            if (ActiveAppID != -1)
            {
                MessageBox.Show("Choose another License Class, the selected Person Already have an active application for the selected class with id=" + ActiveAppID, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                cbLicenseClass.Focus();
                return;
            }

            if (clsLicense.IsLicenseExistByPersonID(personCardWithFilter1.PersonID, LicenseClassID))
            {
                MessageBox.Show("Person already have a license with the same applied driving class, Choose diffrent driving class", "Not allowed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            _LocalDrivingLicenseApplication.ApplicantPersonID = personCardWithFilter1.PersonID; ;
            _LocalDrivingLicenseApplication.ApplicationDate = DateTime.Now;
            _LocalDrivingLicenseApplication.ApplicationTypeID = 1;
            _LocalDrivingLicenseApplication.ApplicationStatus = clsApplication.enApplicationStatus.New;
            _LocalDrivingLicenseApplication.LastStatusDate = DateTime.Now;
            _LocalDrivingLicenseApplication.PaidFees = Convert.ToSingle(lblFees.Text);
            _LocalDrivingLicenseApplication.CreatedByUserID = clsGlobal.CurrentUser.id;
            _LocalDrivingLicenseApplication.LicenseClassID = LicenseClassID;

            if (_LocalDrivingLicenseApplication.Save())
            {
                lblLocalDrivingLicebseApplicationID.Text = _LocalDrivingLicenseApplication.LocalDrivingLicenseApplicationID.ToString(); 
                //change form mode to update.
                Mode = enMode.Update;
                lblTitle.Text = "Update Local Driving License Application";

                MessageBox.Show("Data Saved Successfully.", "Saved", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
                MessageBox.Show("Error: Data Is not Saved Successfully.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

        }

        private void frmAddUpdateLocalLicense_Activated(object sender, EventArgs e)
        {
          personCardWithFilter1.FilterFocus();
        }

        private void lblTitle_Click(object sender, EventArgs e)
        {

        }

        private void cbLicenseClass_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(cbLicenseClass.Text != "" || cbLicenseClass.SelectedIndex >=0) {
                btnSave.Enabled=true;
            }
        }

        private void tpApplicationInfo_Click(object sender, EventArgs e)
        {

        }
    }
}
