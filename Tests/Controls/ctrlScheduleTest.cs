using BusinessLogic_DVLD;
using DVLDProject.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLDProject.Tests.Controls
{
    public partial class ctrlScheduleTest : UserControl
    {
        public enum enMode { FirstAppiontment = 0, Update = 1, RetakeTest = 2 };

        private enMode CurrentMode = enMode.FirstAppiontment;

        private clsTestType.enTestType _TestTypeID = clsTestType.enTestType.VisionTest;
        private clsLocalDrivingLicenseApplication _LocalDrivingLicenseApplication;
        private clsTestAppointment _TestAppointment;
        private int _TestAppointmentID = -1;

        public ctrlScheduleTest()
        {
            InitializeComponent(); // 🛠️ Must be first
            TestTypeID = clsTestType.enTestType.VisionTest; // ✅ Use the property to trigger UI update


        }


        public clsTestType.enTestType TestTypeID
        {
            get => _TestTypeID;
            set
            {
                _TestTypeID = value;

                // Optional debug output
                Debug.WriteLine($"TestTypeID set to: {_TestTypeID}");

                // Update UI based on test type
                switch (_TestTypeID)
                {
                    case clsTestType.enTestType.VisionTest:
                        gbTestType.Text = "Vision Test";
                        pbTestTypeImage.Image = Resources.Vision_512;
                        break;

                    case clsTestType.enTestType.WrittenTest:
                        gbTestType.Text = "Written Test";
                        pbTestTypeImage.Image = Resources.Written_Test_512;
                        break;

                    case clsTestType.enTestType.StreetTest:
                        gbTestType.Text = "Street Test";
                        pbTestTypeImage.Image = Resources.driving_test_512;
                        break;
                }
            }
        }

        public void LoadInfo(int localDrivingLicenseApplicationID, int appointmentID = -1)
        {
            dtpTestDate.MinDate = DateTime.Now;
            gbRetakeTestInfo.Enabled = false;
            lblFees.Text = clsTestType.Find(_TestTypeID).Fees.ToString();

            lblFees.Text = clsTestType.Find(_TestTypeID).Fees.ToString();
            btnSave.Enabled = true;

            _LocalDrivingLicenseApplication = clsLocalDrivingLicenseApplication.FindByLocalDrivingAppLicenseID(localDrivingLicenseApplicationID);

            if (_LocalDrivingLicenseApplication == null)
            {
                MessageBox.Show("No application found. Cannot proceed.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                btnSave.Enabled = false;
            }

            if (_LocalDrivingLicenseApplication.DoesPassTestType(_TestTypeID))
            {
                MessageBox.Show("Applicant has already passed this test type.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                btnSave.Enabled = false;
            }


            if (appointmentID != -1)
            {
                _TestAppointmentID = appointmentID;
                _TestAppointment = clsTestAppointment.Find(_TestAppointmentID);


                if (_TestAppointment.AppointmentDate < DateTime.Now)
                {
                    _TestAppointment.IsLocked = true;
                    _TestAppointment.Save();
                }


                if (_TestAppointment != null && (!_TestAppointment.IsLocked))
                {
                    CurrentMode = enMode.Update;

                    if(_TestAppointment.RetakeTestAppInfo != null)
                    {
                        SetRetakeInfo();
                    }
                }

                else if (_TestAppointment != null && _TestAppointment.IsLocked)
                {
                    MessageBox.Show("The applicant has a previous appointment that is already due or has passed.",
                                    "Past Appointment",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Information);
                    MessageBox.Show("The Application Now is Retake Test.",
                                    "Past Appointment",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Information);

                    _TestAppointment.IsLocked = true;
                    _TestAppointment.Save();

                    CurrentMode = enMode.RetakeTest;
                    SetRetakeInfo();
                }

                else
                {
                    if (_LocalDrivingLicenseApplication.IsThereAnActiveScheduledTest(TestTypeID))
                    {
                        MessageBox.Show("There is already an active scheduled test for this test type.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        btnSave.Enabled = false;
                    }
                }
            }
            else
            {
                CurrentMode = enMode.FirstAppiontment;
                _TestAppointment = new clsTestAppointment();
            }

            // Check if applicant already attended this test





            lblLocalDrivingLicenseAppID.Text = _LocalDrivingLicenseApplication.LocalDrivingLicenseApplicationID.ToString();
            lblDrivingClass.Text = _LocalDrivingLicenseApplication.LicenseClassInfo.ClassName;
            lblFullName.Text = _LocalDrivingLicenseApplication.PersonFullName;
            lblTrial.Text = _LocalDrivingLicenseApplication.TotalTrialsPerTest(_TestTypeID).ToString();

            dtpTestDate.MinDate = DateTime.Now;
        }

        void SetRetakeInfo()
        {

                gbRetakeTestInfo.Enabled = true;
                lblRetakeAppFees.Text = clsApplicationType.Find((int)clsApplication.enApplicationType.RetakeTest).Fees.ToString();
                lblRetakeTestAppID.Text = _TestAppointment.RetakeTestApplicationID.ToString();
                lblTotalFees.Text = (Convert.ToSingle(lblFees.Text) + Convert.ToSingle(lblRetakeAppFees.Text)).ToString();
            
        }

        bool CreateRetakeApplication()
        {
            if (CurrentMode != enMode.RetakeTest)
                return false;


            clsApplication retakeApp = new clsApplication();

            retakeApp.Applicant = _LocalDrivingLicenseApplication.Applicant;
            retakeApp.ApplicantPersonID = _LocalDrivingLicenseApplication.ApplicantPersonID;
            retakeApp.ApplicationStatus = clsApplication.enApplicationStatus.New;
            retakeApp.ApplicationDate = dtpTestDate.Value;
            retakeApp.ApplicationTypeID = (int)clsApplication.enApplicationType.RetakeTest;
            retakeApp.LastStatusDate = DateTime.Now;
            retakeApp.CreatedByUserID = clsGlobal.CurrentUser.id;
            retakeApp.PaidFees = (float)clsApplicationType.Find((int)clsApplication.enApplicationType.RetakeTest).Fees;



            if(retakeApp.Save())
            {
              return  CreateAppointment(retakeApp.ApplicationID);
            }

            return false;   
        }

        bool CreateAppointment(int RetakeID = -1)
        {
            _TestAppointment = new clsTestAppointment();

            _TestAppointment.TestTypeID = _TestTypeID;
            _TestAppointment.LocalDrivingLicenseApplicationID = _LocalDrivingLicenseApplication.LocalDrivingLicenseApplicationID;
          //  _TestAppointment.AppointmentDate = dtpTestDate.Value.AddHours(6);
            _TestAppointment.AppointmentDate = dtpTestDate.Value;
            _TestAppointment.PaidFees = Convert.ToSingle(lblFees.Text.ToString());
            _TestAppointment.CreatedByUserID = clsGlobal.CurrentUser.id;

            if(RetakeID != -1)
            {
                _TestAppointment.RetakeTestApplicationID = RetakeID;
            }
            return _TestAppointment.Save();
        }

        bool UpdateInfo()
        {
            //if (CurrentMode != enMode.Update)
            //    return false;

            _TestAppointment.AppointmentDate = dtpTestDate.Value;

            return _TestAppointment.Save();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (CurrentMode == enMode.FirstAppiontment && CreateAppointment())
            {
                CurrentMode = enMode.Update;
                MessageBox.Show("Data Saved Successfully.", "Saved", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else if (CurrentMode == enMode.RetakeTest && CreateRetakeApplication())
            {
                CurrentMode = enMode.Update;
                lblRetakeTestAppID.Text = _TestAppointment.RetakeTestApplicationID.ToString();

                MessageBox.Show("Data Saved Successfully.", "Saved", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else if (CurrentMode == enMode.Update && UpdateInfo())
            {
                MessageBox.Show("Data Saved Successfully.", "Saved", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Error: Data was not saved successfully.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void gbTestType_Enter(object sender, EventArgs e) { }
        private void gbTestType_Enter_1(object sender, EventArgs e) { }
    }

}

