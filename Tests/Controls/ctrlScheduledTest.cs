using BusinessLogic_DVLD;
using DVLDProject.Global_Classes;
using DVLDProject.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLDProject.Tests.Controls
{
    public partial class ctrlScheduledTest : UserControl
    {
        Nullable<int> _TestID;

        int _TestAppointmentID = -1;

        clsTestType.enTestType _TestTypeID;

        public clsTestType.enTestType TestTypeID
        {
            set
            {
                _TestTypeID = value;

                switch (_TestTypeID)
                {

                    case clsTestType.enTestType.VisionTest:
                        {
                            gbTestType.Text = "Vision Test";
                            pbTestTypeImage.Image = Resources.Vision_512;
                            break;
                        }

                    case clsTestType.enTestType.WrittenTest:
                        {
                            gbTestType.Text = "Written Test";
                            pbTestTypeImage.Image = Resources.Written_Test_512;
                            break;
                        }
                    case clsTestType.enTestType.StreetTest:
                        {
                            gbTestType.Text = "Street Test";
                            pbTestTypeImage.Image = Resources.driving_test_512;
                            break;


                        }
                }
            }
            get { return _TestTypeID; }
        }

        public int TestID
        {
            get { return _TestID ?? -1; }
        }

        public int TestAppointmentID
        {
            get { return _TestAppointmentID; }
        }

        clsLocalDrivingLicenseApplication LApp;

        

        public void LoadInfo(int ID)
        {
            _TestAppointmentID = ID;

            clsTestAppointment Appointment = clsTestAppointment.Find(TestAppointmentID);    

            if(Appointment == null)
            {
                MessageBox.Show("Error: No  Appointment ID = " + _TestAppointmentID.ToString(),
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                _TestAppointmentID = -1;
                return;
            }


            int Lid = Appointment.LocalDrivingLicenseApplicationID;
             LApp = clsLocalDrivingLicenseApplication.FindByLocalDrivingAppLicenseID(Lid);

            if (LApp == null)
            {
                MessageBox.Show("Error: No Local Driving License Application with ID = " + Lid.ToString() ,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); 
                return;
            }
            _TestID = Appointment.TestID;

            lblLocalDrivingLicenseAppID.Text = LApp.LocalDrivingLicenseApplicationID.ToString();
            lblDrivingClass.Text = LApp.LicenseClassInfo.ClassName;

            lblFullName.Text = LApp.PersonFullName.ToString();

            lblTrial.Text = LApp.TotalTrialsPerTest(TestTypeID).ToString();


            lblDate.Text = clsFormat.DateToShort(Appointment.AppointmentDate);
            lblFees.Text = Appointment.PaidFees.ToString();

            lblTestID.Text = _TestID.HasValue ? _TestID.Value.ToString() : "Not Taken Yet";

        }

        public ctrlScheduledTest()
        {
            InitializeComponent();
        }

        private void gbTestType_Enter(object sender, EventArgs e)
        {

        }
    }
}
