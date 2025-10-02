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

namespace DVLDProject.Tests.ManageTest
{
    public partial class frmTakeTest : Form
    {
        int TestAppointmentID = -1;

        clsTestType.enTestType TestType;

   /*     enum enMode { addResult , ShowResult}

        enMode Mode = enMode.addResult; */

        clsTest _Test;
        public frmTakeTest(int AppID , clsTestType.enTestType Type)
        {
            InitializeComponent();
            TestAppointmentID = AppID;
            TestType = Type;

        }

        private void frmTakeTest_Load(object sender, EventArgs e)
        {
           
            ctrlScheduledTest1.TestTypeID = TestType;
            ctrlScheduledTest1.LoadInfo(TestAppointmentID);

            if (ctrlScheduledTest1.TestAppointmentID == -1)
                btnSave.Enabled = false;
            else
                btnSave.Enabled = true;

            int TestID = ctrlScheduledTest1.TestID;

            if (TestID != -1)
            {

                _Test = clsTest.Find(TestID);

                if (_Test.TestResult)
                    rbPass.Checked = true;
                else
                    rbFail.Checked = true;

                txtNotes.Text = _Test.Notes;
                txtNotes.Enabled = false;
                lblUserMessage.Visible = true;
                rbFail.Enabled = false;
                rbPass.Enabled = false;
                btnSave.Enabled = false;
            }

            else
            {
                DateTime appointmentDate = clsTestAppointment.Find(TestAppointmentID).AppointmentDate;
                if (appointmentDate < DateTime.Now)
                {

                    MessageBox.Show($"The test appointment scheduled for {appointmentDate:MMM dd, yyyy} at {appointmentDate:hh:mm tt} has been missed. Please reschedule.",
                                   "Missed Appointment",
                                   MessageBoxButtons.OK,
                                   MessageBoxIcon.Exclamation);
                    clsTestAppointment.Find(TestAppointmentID).IsLocked = true;
                    this.Close();
                }
                _Test = new clsTest();
                btnSave.Enabled = true;
            }
        } 
        
         
        private void btnSave_Click(object sender, EventArgs e)
        {

            if (MessageBox.Show("Are you sure you want to save? After that you cannot change the Pass/Fail results after you save?.",
                        "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.No
               )
            {
                return;
            }


            _Test.TestAppointmentID = ctrlScheduledTest1.TestAppointmentID;
            _Test.TestResult = rbPass.Checked;
            _Test.Notes = txtNotes.Text.Trim();
            _Test.CreatedByUserID = clsGlobal.CurrentUser.id;

            if (_Test.Save())
            {
                MessageBox.Show("Data Saved Successfully.", "Saved", MessageBoxButtons.OK, MessageBoxIcon.Information);
                btnSave.Enabled = false;

            }
            else
                MessageBox.Show("Error: Data Is not Saved Successfully.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);


        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();   
        }

        private void ctrlScheduledTest1_Load(object sender, EventArgs e)
        {

        }
    }
}
