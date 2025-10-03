using BusinessLogic_DVLD;
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

namespace DVLDProject.Tests.ManageTest
{
    public partial class frmListTestAppointments : Form
    {
        DataTable _dtLicenseTestAppointments;
        int _LocalDrivingLicenseApplicationID;
        clsTestType.enTestType _TestType = clsTestType.enTestType.VisionTest;

        public frmListTestAppointments(int LocalDrivingLicenseID, clsTestType.enTestType testType)
        {
            InitializeComponent();
            _LocalDrivingLicenseApplicationID = LocalDrivingLicenseID;
            _TestType = testType;
        }

        private void btnAddNewAppointment_Click(object sender, EventArgs e)
        {
            clsLocalDrivingLicenseApplication localDrivingLicenseApplication = clsLocalDrivingLicenseApplication.FindByLocalDrivingAppLicenseID(_LocalDrivingLicenseApplicationID);


            if (localDrivingLicenseApplication.IsThereAnActiveScheduledTest(_TestType))
            {
                MessageBox.Show("Person Already have an active appointment for this test, You cannot add new appointment", "Not allowed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            else if(localDrivingLicenseApplication.DoesPassTestType(_TestType))
            {
                MessageBox.Show("This person already passed this test before, you can only retake faild test", "Not Allowed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }


            //clsTest LastTest = localDrivingLicenseApplication.GetLastTestPerTestType(_TestType);

            //if (LastTest == null)
            //{
            //    ScheduleTest frm1 = new ScheduleTest(_LocalDrivingLicenseApplicationID, _TestType);
            //    frm1.ShowDialog();
            //}
            //else if (LastTest.TestResult)
            //{
            //    MessageBox.Show("This person already passed this test before, you can only retake faild test", "Not Allowed", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //    return;
            //}

            //else
            //{
            //    ScheduleTest frm2 = new ScheduleTest
            //    (LastTest.TestAppointmentInfo.LocalDrivingLicenseApplicationID, _TestType, LastTest.TestAppointmentID);
            //    frm2.ShowDialog();
            //}

            clsTestAppointment lastAppointment = clsTestAppointment.GetLastTestAppointment(localDrivingLicenseApplication.LocalDrivingLicenseApplicationID, _TestType);

            if (lastAppointment == null)
            {
                ScheduleTest frm1 = new ScheduleTest(_LocalDrivingLicenseApplicationID, _TestType);
                frm1.ShowDialog();
            }

            else
            {
                ScheduleTest frm1 = new ScheduleTest(localDrivingLicenseApplication.LocalDrivingLicenseApplicationID, _TestType, lastAppointment.TestAppointmentID);
                frm1.ShowDialog();
            }


            RefreshData();
        }


        private void _LoadTestTypeImageAndTitle()
        {
            switch (_TestType)
            {

                case clsTestType.enTestType.VisionTest:
                    {
                        lblTitle.Text = "Vision Test Appointments";
                        this.Text = lblTitle.Text;
                        pbTestTypeImage.Image = Resources.Vision_512;
                        break;
                    }

                case clsTestType.enTestType.WrittenTest:
                    {
                        lblTitle.Text = "Written Test Appointments";
                        this.Text = lblTitle.Text;
                        pbTestTypeImage.Image = Resources.Written_Test_512;
                        break;
                    }
                case clsTestType.enTestType.StreetTest:
                    {
                        lblTitle.Text = "Street Test Appointments";
                        this.Text = lblTitle.Text;
                        pbTestTypeImage.Image = Resources.driving_test_512;
                        break;
                    }
            }
        }

        void UpdateRecords()
        {
            lblRecordsCount.Text = _dtLicenseTestAppointments.Rows.Count.ToString();
        }

        void RefreshData()
        {
            ctrlDrivingLicneseApplicationInfo1.LoadData(_LocalDrivingLicenseApplicationID);
            _dtLicenseTestAppointments = clsTestAppointment.GetApplicationTestAppointmentsPerTestType(_LocalDrivingLicenseApplicationID, _TestType);
            dgvLicenseTestAppointments.DataSource = _dtLicenseTestAppointments;
            UpdateRecords();

            if (dgvLicenseTestAppointments.RowCount > 0)
            {
                dgvLicenseTestAppointments.Columns[0].HeaderText = "Appointment ID";
                dgvLicenseTestAppointments.Columns[0].Width = 200;

                dgvLicenseTestAppointments.Columns[1].HeaderText = "Appointment Date";
                dgvLicenseTestAppointments.Columns[1].Width = 250;

                dgvLicenseTestAppointments.Columns[2].HeaderText = "Paid Fees";
                dgvLicenseTestAppointments.Columns[2].Width = 280;

                dgvLicenseTestAppointments.Columns[3].HeaderText = "Is Locked";
                dgvLicenseTestAppointments.Columns[3].Width = 120;
            }

            //refresh

            ctrlDrivingLicneseApplicationInfo1.LoadData( _LocalDrivingLicenseApplicationID);
        }

        private void frmListTestAppointments_Load(object sender, EventArgs e)
        {
            _LoadTestTypeImageAndTitle();
            RefreshData();
        }

        private void takeTestToolStripMenuItem_Click(object sender, EventArgs e)
        {

            int TestAppointmentID = (int)dgvLicenseTestAppointments.CurrentRow.Cells[0].Value;

            frmTakeTest frm = new frmTakeTest(TestAppointmentID, _TestType);
            frm.ShowDialog();

            RefreshData();
        }

        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int TestAppointmentID = (int)dgvLicenseTestAppointments.CurrentRow.Cells[0].Value;


            ScheduleTest frm = new ScheduleTest(_LocalDrivingLicenseApplicationID, _TestType, TestAppointmentID);
            frm.ShowDialog();

            RefreshData();
        }

        private void contextMenuStrip1_Opening(object sender, CancelEventArgs e)
        {
            if ((bool)dgvLicenseTestAppointments.CurrentRow.Cells[3].Value)
            {
                editToolStripMenuItem.Enabled = false;
                takeTestToolStripMenuItem.Enabled = false;
                showResultToolStripMenuItem.Enabled = true;
            }

            else
            {
                editToolStripMenuItem.Enabled = true;
                takeTestToolStripMenuItem.Enabled = true;
                showResultToolStripMenuItem.Enabled = false;
            }
        }

        private void showResultToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmShowResult frm = new frmShowResult((int)dgvLicenseTestAppointments.CurrentRow.Cells[0].Value);
            frm.ShowDialog();

        }

        private void ctrlDrivingLicneseApplicationInfo1_Load(object sender, EventArgs e)
        {

        }
    }
}