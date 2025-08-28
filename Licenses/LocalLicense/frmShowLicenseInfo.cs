﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLDProject.Licenses.LocalLicense
{
    public partial class frmShowLicenseInfo : Form
    {

      int licenseId;
        public frmShowLicenseInfo(int LicenseId)
        {
            InitializeComponent();
            licenseId = LicenseId;
        }

        private void frmShowLicenseInfo_Load(object sender, EventArgs e)
        {
            ctrlDriverLicenseInfo1.LoadInfo(licenseId);
        }
    }
}
