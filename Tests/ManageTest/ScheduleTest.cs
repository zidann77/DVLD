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
    public partial class ScheduleTest : Form
    {
        public ScheduleTest(int LocalAppID ,clsTestType.enTestType Type , int TestAppointemnet = -1)
        {
            InitializeComponent();
            ctrlScheduleTest1.TestTypeID = Type;

            ctrlScheduleTest1.LoadInfo(LocalAppID, TestAppointemnet);
        }



        private void ctrlScheduleTest1_Load(object sender, EventArgs e)
        {

        }

        private void ScheduleTest_Load(object sender, EventArgs e)
        {

        }

        private void ctrlScheduleTest1_Load_1(object sender, EventArgs e)
        {

        }
    }
}
