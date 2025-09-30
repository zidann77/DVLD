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

namespace DVLDProject.UserFolder
{
    public partial class frmListUsers : Form
    {

        DataTable UserTable;
        public frmListUsers()
        {
            InitializeComponent();
        }

        void UpdateRecordLabel()
        {
            lbRecords.Text = UserTable.Rows.Count.ToString();   
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

        void RefreshData(bool resetFilters = false)
        {
            _DisableFormWhileRefresh();

            cbFilter.SelectedItem = 0;


            UserTable = clsUser.GetAllUsers();

            dgvUsers.DataSource = UserTable;

            UpdateRecordLabel();


            if (resetFilters)
            {
                UserTable.DefaultView.RowFilter = null;
                cbFilter.SelectedIndex = 0;

                if (cbActiveChoices.Visible)
                    cbActiveChoices.SelectedIndex = 0;
                else
                    txtFilter.Text = string.Empty;
            }

            _EnableFormAfterRefresh();
        }

        void SetUpDataGridView()
        {


            if (dgvUsers.DataSource == null || dgvUsers.Rows.Count == 0)
            {
                MessageBox.Show("No data available to display.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            dgvUsers.Columns[0].HeaderText = "User ID";
            dgvUsers.Columns[0].Width = 110;

            dgvUsers.Columns[1].HeaderText = "Person ID";
            dgvUsers.Columns[1].Width = 120;

            dgvUsers.Columns[2].HeaderText = "Full Name";
            dgvUsers.Columns[2].Width = 370;

            dgvUsers.Columns[3].HeaderText = "UserName";
            dgvUsers.Columns[3].Width = 120;

            dgvUsers.Columns[4].HeaderText = "Is Active";
            dgvUsers.Columns[4].Width = 130;

        }



        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            string Selected = cbFilter.SelectedItem?.ToString();

            if (Selected == "IsActive")
            {
                txtFilter.Visible = false;
                cbActiveChoices.Visible = true;
            }
            else if (Selected == "None")
            {
                txtFilter.Visible = false;
                cbActiveChoices.Visible = false;
            }

            else
            {
                txtFilter.Visible = true;
                cbActiveChoices.Visible = false;
            }

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            string selected = cbFilter.SelectedItem?.ToString();

            if (txtFilter.Text.Trim() == string.Empty || selected == "None")
            {
                UserTable.DefaultView.RowFilter = null;
                UpdateRecordLabel();
                return;
            }
            // Operator Precedence
            if ((selected == "UserID" || selected == "PersonID"))
            {
                if (int.TryParse(txtFilter.Text.Trim(), out int id))
                    UserTable.DefaultView.RowFilter = $"[{selected}] = {id}";
                else
                    return;
            }
           
            else
            {
                UserTable.DefaultView.RowFilter = $"[{selected}] like '%{txtFilter.Text.Trim()}%'";

            }
            UpdateRecordLabel();
        }

        private void cbActiveChoices_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (cbActiveChoices.Text == "All")
            {
                UserTable.DefaultView.RowFilter = null;
            }
            else
            {
                string value = (cbActiveChoices.Text == "Yes" || cbActiveChoices.Text == "ALL") ? "1" : "0";
                UserTable.DefaultView.RowFilter = $"[IsActive] = {value}";
            }

            UpdateRecordLabel();
        }


        private void frmListUsers_Load(object sender, EventArgs e)
        {
            cbFilter.SelectedIndex = 0;
           RefreshData();
            SetUpDataGridView();
        }

        private void btnAddUser_Click(object sender, EventArgs e)
        {
            frmAddUpdateUser frm = new frmAddUpdateUser();
            frm.ShowDialog();
            RefreshData();
        }

        //  

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
        }

        private void showDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            UserInfo frm = new UserInfo((int)dgvUsers.CurrentRow.Cells[0].Value);
            frm.ShowDialog();  
        }

        private void addNewUserToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmAddUpdateUser frm = new frmAddUpdateUser();
            frm.ShowDialog();
            RefreshData();
        }

        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmAddUpdateUser frm = new frmAddUpdateUser((int)dgvUsers.CurrentRow.Cells[0].Value);
            frm.ShowDialog();
            RefreshData();
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int ID = Convert.ToInt32(dgvUsers.CurrentRow.Cells[0].Value);
            DialogResult result = MessageBox.Show("Are You Sure from Deleting User With ID = " + ID, "Message ", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);

            if(result == DialogResult.OK)
            {
                if(clsUser.DeleteUser(ID))
                {
                    MessageBox.Show("User has been deleted successfully", "Deleted", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    RefreshData();  
                }
                else
                {
                    MessageBox.Show("User is not deleted due to existing data dependencies.", "Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void changePasswordToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int ID = Convert.ToInt32(dgvUsers.CurrentRow.Cells[0].Value);
            ChangePassword frm = new ChangePassword(ID);

            frm.ShowDialog();

        //    RefreshData();
            
        }

        private void txtFilter_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((cbFilter.SelectedItem?.ToString() == "User ID" || cbFilter.SelectedItem?.ToString() == "Person ID"))
            {
                e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
            }
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            RefreshData();
        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void lbRecords_Click(object sender, EventArgs e)
        {

        }
    }
}
