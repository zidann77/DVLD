using BusinessLogic_DVLD;
using DVLDProject.PersonFolder;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLDProject.Applications.InternationalLicense
{
    public partial class ListInternationalLicenceApplications : Form
    {
        DataTable Table;

        public ListInternationalLicenceApplications()
        {
            InitializeComponent();
        }

        private void lblInternationalLicensesRecords_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }


        private void _DisableFormWhileRefresh()
        {
            this.Enabled = false;

            Application.DoEvents(); // Allow UI to update

            Cursor.Current = Cursors.WaitCursor;

        }

        private void _EnableFormAfterRefresh()
        {
            this.Enabled = true;

            Application.DoEvents(); // Allow UI to update

            Cursor.Current = Cursors.Default;
        }


        void RefreshData()
        {
            _DisableFormWhileRefresh();

            Table = clsInternationalLicense.GetAllInternationalLicenses();

            dgvInternationalLicenses.DataSource = Table;
            cbFilterBy.SelectedIndex = 0;

            lblInternationalLicensesRecords.Text = dgvInternationalLicenses.RowCount.ToString();

            if (dgvInternationalLicenses.RowCount > 0)
            {
                dgvInternationalLicenses.Columns[0].HeaderText = "Int.License ID";
                dgvInternationalLicenses.Columns[0].Width = 160;

                dgvInternationalLicenses.Columns[1].HeaderText = "Application ID";
                dgvInternationalLicenses.Columns[1].Width = 150;

                dgvInternationalLicenses.Columns[2].HeaderText = "Driver ID";
                dgvInternationalLicenses.Columns[2].Width = 130;

                dgvInternationalLicenses.Columns[3].HeaderText = "L.License ID";
                dgvInternationalLicenses.Columns[3].Width = 130;

                dgvInternationalLicenses.Columns[4].HeaderText = "Issue Date";
                dgvInternationalLicenses.Columns[4].Width = 180;

                dgvInternationalLicenses.Columns[5].HeaderText = "Expiration Date";
                dgvInternationalLicenses.Columns[5].Width = 180;

                dgvInternationalLicenses.Columns[6].HeaderText = "Is Active";
                dgvInternationalLicenses.Columns[6].Width = 120;
            }

            else
                MessageBox.Show("There is No Data To Show", "No Data", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

            _EnableFormAfterRefresh();
             
        }

        private void ListInternationalLicenceApplications_Load(object sender, EventArgs e)
        {
            RefreshData();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            _DisableFormWhileRefresh();
            RefreshData();
            _EnableFormAfterRefresh();
        }

        private void PesonDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int DriverID = Convert.ToInt32(dgvInternationalLicenses.CurrentRow.Cells[2].Value);

            int personID = clsDriver.FindByDriverID(DriverID).PersonID;

            frmShowPersonDetails frm = new frmShowPersonDetails(personID);
            frm.ShowDialog();

        }

        private void cbFilterBy_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (cbFilterBy.Text == "Is Active")
            {
                cbIsReleased.Visible = true;
                txtFilterValue.Visible = false;

                cbIsReleased.Focus();
                cbIsReleased.SelectedIndex = 0;

            }
            else
            {
                if (((txtFilterValue.Visible = (cbFilterBy.Text != "None")) == false))
                    txtFilterValue.Enabled = false;
                else
                    txtFilterValue.Enabled = true;

                cbIsReleased.Visible = false;

                txtFilterValue.Focus();
                txtFilterValue.Text = "";

            }
        }

        void UpdateCountRecord()
        {
            lblInternationalLicensesRecords.Text = dgvInternationalLicenses.RowCount.ToString();
        }

        private void cbIsReleased_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbIsReleased.SelectedIndex == 0)
                Table.DefaultView.RowFilter = "";

            else if (cbIsReleased.SelectedIndex == 1)
                Table.DefaultView.RowFilter = string.Format("[{0}] = {1}", "IsActive", "1");
            else
                Table.DefaultView.RowFilter = string.Format("[{0}] = {1}", "IsActive", "0");


            UpdateCountRecord();
        }

        private void txtFilterValue_TextChanged(object sender, EventArgs e)
        {

            string FilterColumn = "";
            //Map Selected Filter to real Column name 
            switch (cbFilterBy.Text)
            {
                case "International License ID":
                    FilterColumn = "InternationalLicenseID";
                    break;
                case "Application ID":
                    {
                        FilterColumn = "ApplicationID";
                        break;
                    };

                case "Driver ID":
                    FilterColumn = "DriverID";
                    break;

                case "Local License ID":
                    FilterColumn = "IssuedUsingLocalLicenseID";
                    break;

                    // handled in cbIsReleased
                case "Is Active":
                    FilterColumn = "IsActive";
                    break;


                default:
                    FilterColumn = "None";
                    break;
            }


            //Reset the filters in case nothing selected or filter value conains nothing.
            if (txtFilterValue.Text.Trim() == "" || FilterColumn == "None")
            {
                Table.DefaultView.RowFilter = "";
                lblInternationalLicensesRecords.Text = dgvInternationalLicenses.Rows.Count.ToString();
                return;
            }



            Table.DefaultView.RowFilter = string.Format("[{0}] = {1}", FilterColumn, txtFilterValue.Text.Trim());

            UpdateCountRecord();
        }

        private void txtFilterValue_KeyPress(object sender, KeyPressEventArgs e)
        {
            //we allow numbers only becasue all fiters are numbers.
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void btnNewApplication_Click(object sender, EventArgs e)
        {
            frmNewInternationalLicense frmNewInternationalLicense = new frmNewInternationalLicense();
            frmNewInternationalLicense.ShowDialog();
            RefreshData();  
        }

        private void showDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
        }
    }
}
