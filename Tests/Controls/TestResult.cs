using BusinessLogic_DVLD;
using DVLDProject.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLDProject.Tests.Controls
{
    public partial class TestResult : UserControl
    {
        public TestResult()
        {
            InitializeComponent();
        }

        clsTest Test;
        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        public enum enmode { pass = 1, failed = 2, absent = 3 }

        enmode _Result;

        public enmode Result
        {
            get { return _Result; }
            set
            {
                _Result = value;

                switch (_Result)
                {
                    case enmode.pass:
                        pbResult.Image = Resources.test__1_;
                        lbresult.Text = "Passed";
                        lbresult.BackColor = Color.Green;
                        break;

                    case enmode.failed:
                        pbResult.Image = Resources.test;
                        lbresult.Text = "Failed";
                        lbresult.BackColor = Color.Red;
                        break;

                    case enmode.absent:
                        pbResult.Image = Resources.absent;
                        lbresult.Text = "Absent";
                        lbresult.BackColor = Color.Blue;
                        break;
                }
            }
        }


        private void TestResult_Load(object sender, EventArgs e)
        {

        }

        void LoadInfo(clsTestAppointment appointment)
        {
            clsPerson Person = clsLocalDrivingLicenseApplication.FindByLocalDrivingAppLicenseID(appointment.LocalDrivingLicenseApplicationID).Applicant;
            if (Person == null)
                return;
            lblFullName.Text = Person.FullName;
            lbPhone.Text = Person.Phone;
            lbCountry.Text = Person.Country.CountryName;

            lbtestID.Text = Test.TestID.ToString();
            lblLocalDrivingLicenseAppID.Text = appointment.LocalDrivingLicenseApplicationID.ToString();
            lblDrivingClass.Text = clsLocalDrivingLicenseApplication.FindByLocalDrivingAppLicenseID(appointment.LocalDrivingLicenseApplicationID).LicenseClassInfo.ClassName;
            lblTrial.Text = clsLocalDrivingLicenseApplication.TotalTrialsPerTest(appointment.LocalDrivingLicenseApplicationID, appointment.TestTypeID).ToString();

            if (Person.Gendor == 1)
                pbPersonImg.Image = Resources.Female_512;
            else
                pbPersonImg.Image = Resources.Male_512;

            if (Person.ImagePath != "")
            {
                if (File.Exists(Person.ImagePath))
                    pbPersonImg.ImageLocation = Person.ImagePath;
                else
                    MessageBox.Show("Could not find this image: = " + Person.ImagePath, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        public void LoadResult(int TestAppID)
        {
            clsTestAppointment Appointment = clsTestAppointment.Find(TestAppID);

            if (Appointment != null)
            {
                Test = clsTest.Find(Appointment.TestID);

                if (Test != null)
                {
                    switch (Test.TestResult)
                    {
                        case true:
                            Result = enmode.pass;
                            break;
                        case false:
                            Result = enmode.failed;
                            break;

                        default:
                            Result = enmode.absent;
                            break;
                    }

                    LoadInfo(Appointment);

                }

                else
                {
                    MessageBox.Show("No test found.", "Missing Test",
        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }

            else
            {
                MessageBox.Show("No appointment found.", "Missing Appointment",
        MessageBoxButtons.OK, MessageBoxIcon.Warning);

            }

        }
    }
}
