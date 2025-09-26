using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLDProject.Licenses.International_Licenses
{
    public partial class frmShowInternationalLicenseInfo : Form
    {
        int _InterLicenseID;

        public frmShowInternationalLicenseInfo(int ID)
        {
            InitializeComponent();
            _InterLicenseID = ID;
        }


        private void frmShowInternationalLicenseInfo_Load(object sender, EventArgs e)
        {
            ctrlDriverInternationalLicenseInfo1.LoadInfo(_InterLicenseID);
        }
    }
}
