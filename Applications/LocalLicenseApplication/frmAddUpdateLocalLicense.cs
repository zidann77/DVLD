using BusinessLogic_DVLD;
using DVLDProject.Global_Classes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLDProject
{
    public partial class frmAddUpdateLocalLicense : Form
    {
        public enum enMode { Addnew = 0, Update = 1 }
        private int LocalLicenseID;
        private int SelectedPersonID;
        public enMode Mode = enMode.Addnew;
        public clsLocalDrivingLicenseApplication LocalLicenseApp;
        
        
        public frmAddUpdateLocalLicense()
        {
            InitializeComponent();
        }

        public frmAddUpdateLocalLicense(int AppID)
        {
            InitializeComponent();
            LocalLicenseID = AppID;
            Mode = enMode.Update;
        }

        void FillLicenseClassComboBox()
        {
            DataTable DT = new DataTable(); 

            DT = clsLicenseClass.GetAllLicenseClasses();

            foreach(DataRow DR in DT.Rows)
            {
                cbLicenseClass.Items.Add(DR["ClassName"].ToString());
            }
        }
        void ResetDefualtValues()
        {
            FillLicenseClassComboBox();

            LocalLicenseApp = new clsLocalDrivingLicenseApplication();

            lblTitle.Text = "New Local Driving License Application";
            this.Text = "New Local Driving License Application";

            // to choose an person

            personCardWithFilter1.FilterFocus();

            lblApplicationDate.Text = DateTime.Now.ToShortDateString();
            lblCreatedByUser.Text = clsGlobal.CurrentUser.Name;

            tpApplicationInfo.Enabled = false;
        }

        void LoadData()
        {
            LocalLicenseApp = clsLocalDrivingLicenseApplication.FindByApplicationID(LocalLicenseID);

            if (LocalLicenseApp == null)
            {
                MessageBox.Show("No Application with ID = " + LocalLicenseID, "Application Not Found", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                this.Close();
                return;
            }

            personCardWithFilter1.EnableFilter = false;
            personCardWithFilter1.LoadPersonInfo(LocalLicenseApp.Applicant.PersonID);

            lblTitle.Text = "Update Local Driving License Application";
            this.Text = "Update Local Driving License Application";

            tpApplicationInfo.Enabled = true;

            lblLocalDrivingLicebseApplicationID.Text = LocalLicenseApp.ID.ToString();

            lblApplicationDate.Text = clsFormat.DateToShort(LocalLicenseApp.ApplicationDate);

            lblCreatedByUser.Text = clsUser.FindUser(LocalLicenseApp.CreatedByUserID).Name;

            lblFees.Text = LocalLicenseApp.PaidFees.ToString();

            cbLicenseClass.SelectedIndex = cbLicenseClass.FindString(clsLicenseClass.Find(LocalLicenseApp.LicenseClassID).ClassName);

        }

        private void frmAddUpdateLocalLicense_Load(object sender, EventArgs e)
        {
            switch(Mode)
            {
                case enMode.Addnew:
                    ResetDefualtValues();
                    break;

                case enMode.Update:
                    LoadData();
                    break;
            }
        }

        private void DataBackEvent(object sender, int PersonID)
        {
            SelectedPersonID = PersonID;
            personCardWithFilter1.LoadPersonInfo(PersonID);
        }


   

        private void personCardWithFilter1_OnPersonSelected(int obj)
        {
            SelectedPersonID = obj;
        }

        private void button1_Click(object sender, EventArgs e)
        {

            if (personCardWithFilter1.PersonID == -1)
            {
                MessageBox.Show("Please Select a Person", "Select a Person", MessageBoxButtons.OK, MessageBoxIcon.Error);
                personCardWithFilter1.FilterFocus();
                return;
            }

            tpApplicationInfo.Enabled = true;
            tabControl1.SelectedTab = tabControl1.TabPages["tpApplicationInfo"];

            if (Mode == enMode.Addnew)
            {
                lblLocalDrivingLicebseApplicationID.Text = LocalLicenseApp.ID.ToString();

                lblApplicationDate.Text = clsFormat.DateToShort(LocalLicenseApp.ApplicationDate);

                lblCreatedByUser.Text = clsUser.FindUser(clsGlobal.CurrentUser.id).Name;

                cbLicenseClass.SelectedIndex = 3;

                lblFees.Text = clsLicenseClass.Find(cbLicenseClass.Text).ClassFees.ToString();
            }

        }

        private void frmAddUpdateLocalLicense_Activated(object sender, EventArgs e)
        {
            personCardWithFilter1.FilterFocus();
        }

        private void btnsave_Click(object sender, EventArgs e)
        {
            if (!this.ValidateChildren())
            {
                MessageBox.Show("Some fileds are not valide!, put the mouse over the red icon(s) to see the erro", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            int LicenseClassID = clsLicenseClass.Find(cbLicenseClass.Text).LicenseClassID;

            int ApplicationID = clsApplication.GetActiveApplicationIDForLicenseClass(personCardWithFilter1.PersonID, clsApplication.enApplicationType.NewDrivingLicense, LicenseClassID);

            if (ApplicationID != -1)
            {
                MessageBox.Show("Choose another License Class, the selected Person Already have an active application for the selected class with id=" +ApplicationID, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                cbLicenseClass.Focus();
                return;
            }

            if (clsLicense.IsLicenseExistByPersonID(personCardWithFilter1.PersonID, LicenseClassID))
            {
                MessageBox.Show("Person already have a license with the same applied driving class, Choose diffrent driving class", "Not allowed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            LocalLicenseApp.ApplicantPersonID = personCardWithFilter1.PersonID; ;
            LocalLicenseApp.ApplicationDate = DateTime.Now;
            LocalLicenseApp.ApplicationTypeID = 1;
            LocalLicenseApp.ApplicationStatus = clsApplication.enApplicationStatus.New;
            LocalLicenseApp.LastStatusDate = DateTime.Now;
            LocalLicenseApp.PaidFees = Convert.ToSingle(lblFees.Text.ToString());
            LocalLicenseApp.CreatedByUserID = clsGlobal.CurrentUser.id;
            LocalLicenseApp.LicenseClassID = LicenseClassID;

            if (LocalLicenseApp.save())
            {
                lblLocalDrivingLicebseApplicationID.Text = LocalLicenseApp.ID.ToString();
                Mode = enMode.Update;
                lblTitle.Text = "Update Local Driving License Application";
                MessageBox.Show("Data Saved Successfully.", "Saved", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            else
            {
                MessageBox.Show("Error: Data Is not Saved Successfully.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void cbLicenseClass_SelectedIndexChanged(object sender, EventArgs e)
        {
            lblFees.Text = clsLicenseClass.Find(cbLicenseClass.Text).ClassFees.ToString();
        }
    }

}
