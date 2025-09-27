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
        int TestAppointment = -1;

        clsTestType.enTestType TestType;

   /*     enum enMode { addResult , ShowResult}

        enMode Mode = enMode.addResult; */

        clsTest _Test;
        public frmTakeTest(int AppID , clsTestType.enTestType Type)
        {
            InitializeComponent();
            TestAppointment = AppID;
            TestType = Type;
        }

        private void frmTakeTest_Load(object sender, EventArgs e)
        {
           
            ctrlScheduledTest1.TestTypeID = TestType;
            ctrlScheduledTest1.LoadInfo(TestAppointment);

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

            }

            else
            {
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
