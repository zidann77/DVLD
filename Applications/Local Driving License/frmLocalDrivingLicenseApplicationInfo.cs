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
    public partial class frmLocalDrivingLicenseApplicationInfo : Form
    {
        int ApplicationID = -1;
        public frmLocalDrivingLicenseApplicationInfo(int ID)
        {
            InitializeComponent();
            ApplicationID = ID;
        }

        private void frmLocalDrivingLicenseApplicationInfo_Load(object sender, EventArgs e)
        {
            ctrlDrivingLicneseApplicationInfo1.LoadDataByLicenseID(ApplicationID);
        }

        private void ctrlDrivingLicneseApplicationInfo1_Load(object sender, EventArgs e)
        {

        }
    }
}
