using BusinessLogic_DVLD;
using DVLDProject.Applications.Release_Detained_License;
using DVLDProject.Licenses.LocalLicense;
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

namespace DVLDProject.Licenses.Detain_license
{
    public partial class frmListDetainedLicenses : Form
    {
        DataTable _dtDetainedLicenses;

        public frmListDetainedLicenses()
        {
            InitializeComponent();
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

            cbFilterBy.SelectedIndex = 0;

            _dtDetainedLicenses = clsDetainedLicense.GetAllDetainedLicenses();

            dgvDetainedLicenses.DataSource = _dtDetainedLicenses;


            lblTotalRecords.Text = dgvDetainedLicenses.RowCount.ToString();

            if(dgvDetainedLicenses.RowCount > 0 )
            {
                dgvDetainedLicenses.Columns[0].HeaderText = "D.ID";
                dgvDetainedLicenses.Columns[0].Width = 90;

                dgvDetainedLicenses.Columns[1].HeaderText = "L.ID";
                dgvDetainedLicenses.Columns[1].Width = 90;

                dgvDetainedLicenses.Columns[2].HeaderText = "D.Date";
                dgvDetainedLicenses.Columns[2].Width = 160;

                dgvDetainedLicenses.Columns[3].HeaderText = "Is Released";
                dgvDetainedLicenses.Columns[3].Width = 57;

                dgvDetainedLicenses.Columns[4].HeaderText = "Fine Fees";
                dgvDetainedLicenses.Columns[4].Width = 89;

                dgvDetainedLicenses.Columns[5].HeaderText = "Release Date";
                dgvDetainedLicenses.Columns[5].Width = 163;

                dgvDetainedLicenses.Columns[6].HeaderText = "N.No.";
                dgvDetainedLicenses.Columns[6].Width = 70;

                dgvDetainedLicenses.Columns[7].HeaderText = "Full Name";
                dgvDetainedLicenses.Columns[7].Width = 333;

                dgvDetainedLicenses.Columns[8].HeaderText = "Rlease App.ID";
                dgvDetainedLicenses.Columns[8].Width = 75;
            }

        }
        private void dgvDetainedLicenses_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

     
        private void txtFilterValue_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (cbFilterBy.Text == "Detain ID" || cbFilterBy.Text == "Release Application ID")
                e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void frmListDetainedLicenses_Load(object sender, EventArgs e)
        {
            RefreshData();
        }

        private void cbFilterBy_SelectedIndexChanged(object sender, EventArgs e)
        {
           
            if (cbFilterBy.Text == "Is Released")
            {
                txtFilterValue.Visible = false;
                txtFilterValue.Clear();
                cbIsReleased.Visible = true;
                cbIsReleased.Focus();
                cbIsReleased.SelectedIndex = 0;
            }

            else
            {
                cbIsReleased.Visible = false;
                cbIsReleased.SelectedIndex = 0;

                if (cbFilterBy.Text != "None")
                {
                    txtFilterValue.Visible = true;
                   
                }
                else
                {
                    txtFilterValue.Visible = false;
                 
                }

                txtFilterValue.Clear();

            }
        }
      

        private void cbIsReleased_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbIsReleased.Visible == false)
                return;

            string FilterColumn = "IsReleased";
            string FilterValue = cbIsReleased.Text;

            switch (FilterValue)
            {
                case "All":
                    break;
                case "Yes":
                    FilterValue = "1";
                    break;
                case "No":
                    FilterValue = "0";
                    break;
            }

            // Check if the column exists in the DataTable
            if (!_dtDetainedLicenses.Columns.Contains(FilterColumn))
            {
                MessageBox.Show($"Column '{FilterColumn}' does not exist in the data.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return; // Exit the method early if the column is missing
            }

            if (FilterValue == "All")
                _dtDetainedLicenses.DefaultView.RowFilter = null;
            else
                //in this case we deal with numbers not string.
                _dtDetainedLicenses.DefaultView.RowFilter = string.Format("[{0}] = {1}", FilterColumn, FilterValue);

            lblTotalRecords.Text = _dtDetainedLicenses.Rows.Count.ToString();
        }

        private void txtFilterValue_TextChanged(object sender, EventArgs e)
        {
            string FilterColumn = "";
            //Map Selected Filter to real Column name 
            switch (cbFilterBy.Text)
            {
                case "Detain ID":
                    FilterColumn = "DetainID";
                    break;
                case "Is Released":
                    {
                        FilterColumn = "IsReleased";
                        break;
                    };

                case "National No.":
                    FilterColumn = "NationalNo";
                    break;


                case "Full Name":
                    FilterColumn = "FullName";
                    break;

                case "Release Application ID":
                    FilterColumn = "ReleaseApplicationID";
                    break;

                default:
                    FilterColumn = "None";
                    break;
            }


            //Reset the filters in case nothing selected or filter value conains nothing.
            if (txtFilterValue.Text.Trim() == "" || FilterColumn == "None")
            {
                _dtDetainedLicenses.DefaultView.RowFilter = "";
                lblTotalRecords.Text = dgvDetainedLicenses.Rows.Count.ToString();
                return;
            }

            if (!_dtDetainedLicenses.Columns.Contains(FilterColumn))
            {
                MessageBox.Show($"Column '{FilterColumn}' does not exist in the data.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return; // Exit the method early if the column is missing
            }



            if (FilterColumn == "DetainID" || FilterColumn == "ReleaseApplicationID")
                //in this case we deal with numbers not string.
                _dtDetainedLicenses.DefaultView.RowFilter = string.Format("[{0}] = {1}", FilterColumn, txtFilterValue.Text.Trim());
            else
                _dtDetainedLicenses.DefaultView.RowFilter = string.Format("[{0}] LIKE '{1}%'", FilterColumn, txtFilterValue.Text.Trim());

            lblTotalRecords.Text = _dtDetainedLicenses.Rows.Count.ToString();
        }

        private void cbIsReleased_VisibleChanged(object sender, EventArgs e)
        {
            //if (cbFilterBy.Visible == false)
            //{
                
            //    cbFilterBy.SelectedIndex = 0;
            //    cbFilterBy.SelectedIndexChanged -= cbFilterBy_SelectedIndexChanged;
            //}

            //cbFilterBy.SelectedIndexChanged += cbFilterBy_SelectedIndexChanged;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            _DisableFormWhileRefresh();
            RefreshData();
            _EnableFormAfterRefresh();
        }

        private void btnReleaseDetainedLicense_Click(object sender, EventArgs e)
        {
            frmReleaseDetainedLicenseApplication frm = new frmReleaseDetainedLicenseApplication();
            frm.ShowDialog();

            RefreshData();
        }

        private void btnDetainLicense_Click(object sender, EventArgs e)
        {
            frmDetainLicenseApplication frm = new frmDetainLicenseApplication();
            frm.ShowDialog();
            RefreshData();
        }

        private void releaseLicenseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int LicenseID = (int)dgvDetainedLicenses.CurrentRow.Cells[1].Value;
            frmReleaseDetainedLicenseApplication frm = new frmReleaseDetainedLicenseApplication(LicenseID);
            frm.ShowDialog();
            RefreshData();
        }

        private void showPersonLicenseHistoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int PersonID = clsPerson.Find(dgvDetainedLicenses.CurrentRow.Cells[6].Value.ToString()).PersonID;
            frmShowPersonLicenseHistory frm = new frmShowPersonLicenseHistory(PersonID);
            frm.ShowDialog();
        }

        private void showPersonDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int PersonID = clsPerson.Find(dgvDetainedLicenses.CurrentRow.Cells[6].Value.ToString()).PersonID;
            frmShowPersonDetails frm = new frmShowPersonDetails(PersonID);
            frm.ShowDialog();
        }

        private void showLicenseDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int LicenseID = (int)dgvDetainedLicenses.CurrentRow.Cells[1].Value;
            frmShowLicenseInfo frm = new frmShowLicenseInfo(LicenseID);
            frm.ShowDialog();
        }
    }
}
