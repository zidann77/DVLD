using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLDProject.Licenses
{
    public partial class frmShowPersonLicenseHistory : Form
    {
        int _PersonID;

        public frmShowPersonLicenseHistory()
        {
            InitializeComponent();
            _PersonID = -1;
        }

        public frmShowPersonLicenseHistory(int ID)
        {
            InitializeComponent();
            _PersonID = ID;
        }


        private void pbPersonImage_Click(object sender, EventArgs e)
        {

        }

        private void frmShowPersonLicenseHistory_Load(object sender, EventArgs e)
        {
            if (_PersonID != -1)
            {
                personCardWithFilter1.EnableFilter=false;
                personCardWithFilter1.LoadPersonInfo(_PersonID);
                ctrlDriverLicenses1.LoadAllLicensesByPersonID(_PersonID);
            }
            else
            {
                personCardWithFilter1.Enabled = true;
                personCardWithFilter1.FilterFocus();
            }

        }

        private void personCardWithFilter1_OnPersonSelected(int obj)
        {
            _PersonID = obj;

            if (_PersonID == -1)
            {
                ctrlDriverLicenses1.Clear();
            }
            else
            ctrlDriverLicenses1.LoadAllLicensesByPersonID(obj);



        }
    }
}
