using BusinessLogic_DVLD;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLDProject.PersonFolder
{
    public partial class frmListPeople : Form
    {
        DataTable Table1;
        DataTable SubTable;

        void UpdateLabelRecord()
        {
            lbCountRecordes.Text = dataGridView1.RowCount.ToString();
        }

        private void RefreshData()
        {

            Table1 = clsPerson.GetAllPeople();
            SubTable = Table1.DefaultView.ToTable(false, "PersonID", "NationalNo",
          "FirstName", "SecondName", "ThirdName", "LastName",
          "GendorCaption", "DateOfBirth", "CountryName",
          "Phone", "Email");

            dataGridView1.DataSource = SubTable;

            UpdateLabelRecord();

            //   lbCountRecordes .Text = dataGridView1.RowCount.ToString();
            //   lbCountRecordes .Text = SubTable.Rows.Count.ToString();

        }

        private void SetUpTheDataGridView()
        {
            // Refresh data first
            RefreshData();

            // Check if the data source is null or if it has no rows
            if (dataGridView1.DataSource == null || dataGridView1.Rows.Count == 0)
            {
                // Optionally, clear the grid or show a message
                MessageBox.Show("No data available to display.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;  // Exit the method if there is no data
            }

            // Set column headers and widths
            dataGridView1.Columns[0].HeaderText = "Person ID";
            dataGridView1.Columns[0].Width = 100;

            dataGridView1.Columns[1].HeaderText = "National No.";
            dataGridView1.Columns[1].Width = 100;

            dataGridView1.Columns[2].HeaderText = "First Name";
            dataGridView1.Columns[2].Width = 120;

            dataGridView1.Columns[3].HeaderText = "Second Name";
            dataGridView1.Columns[3].Width = 140;

            dataGridView1.Columns[4].HeaderText = "Third Name";
            dataGridView1.Columns[4].Width = 120;

            dataGridView1.Columns[5].HeaderText = "Last Name";
            dataGridView1.Columns[5].Width = 120;

            dataGridView1.Columns[6].HeaderText = "Gender";
            dataGridView1.Columns[6].Width = 80;

            dataGridView1.Columns[7].HeaderText = "Date Of Birth";
            dataGridView1.Columns[7].Width = 140;

            dataGridView1.Columns[8].HeaderText = "Nationality";
            dataGridView1.Columns[8].Width = 120;

            dataGridView1.Columns[9].HeaderText = "Phone";
            dataGridView1.Columns[9].Width = 120;

            dataGridView1.Columns[10].HeaderText = "Email";
            dataGridView1.Columns[10].Width = 170;

        }


      
        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            string selectedFilter = cbFilter.SelectedItem?.ToString();

            if(textBox1.Text == null || selectedFilter == "None")
            {
                SubTable.DefaultView.RowFilter = null;
                UpdateLabelRecord();
                return;
            }

            else if(selectedFilter == "PersonID")
            {
                if (int.TryParse(textBox1.Text, out int value))
                {
                    SubTable.DefaultView.RowFilter = $"[{selectedFilter}] = {value}";
                    UpdateLabelRecord ();
                }
                else
                    return;
            }
            else
                SubTable.DefaultView.RowFilter = $"[{selectedFilter}] like '%{textBox1.Text.Trim()}%'";

            UpdateLabelRecord();
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (cbFilter.SelectedItem.ToString() == "Person ID")
                e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
            else
                return;
        }

        private void cbFilter_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectedFilter = cbFilter.SelectedItem?.ToString();
            //UpdateTextBoxVisiblity(cbFilter.SelectedItem.ToString() == "None");

            if (selectedFilter == "None" || selectedFilter == null)
            {
                textBox1.Visible = false;
                textBox1.Text = string.Empty;
            }
            else
                textBox1.Visible = true;



        }


        public frmListPeople()
        {
            InitializeComponent();
        }



        private void showToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int index = dataGridView1.SelectedCells[0].RowIndex;
            frmShowPersonDetails frm = new frmShowPersonDetails((int)dataGridView1.Rows[index].Cells[0].Value);
            frm.ShowDialog();

            
        }

        private void addNewPersonToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmAddUpdatePerson frm = new frmAddUpdatePerson();
            frm.ShowDialog();
            RefreshData();
        }

        private void ediToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int index = dataGridView1.SelectedCells[0].RowIndex;

            frmAddUpdatePerson frm = new frmAddUpdatePerson((int)dataGridView1.Rows[index].Cells[0].Value);
            frm.ShowDialog();
            RefreshData();
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int index = dataGridView1.SelectedCells[0].RowIndex;
            int PersonID = (int)dataGridView1.Rows[index].Cells[0].Value;
            DialogResult result = MessageBox.Show("Are You Sure From Deleting Person With ID = " + PersonID, "Delete Person", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
            if (result == DialogResult.OK)
            {
                if (clsPerson.DeletePerson(PersonID))
                {
                    MessageBox.Show("Person Deleted Successfully.", "Successful", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    RefreshData();
                }
                else
                {
                    MessageBox.Show("Person was not deleted because it has data linked to it.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void sendEmailToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("This Feature Is Not Implemented Yet!", "Not Ready!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

        }

        private void sendPhoneToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("This Feature Is Not Implemented Yet!", "Not Ready!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

        }

        private void dataGridView1_DoubleClick(object sender, EventArgs e)
        {
            int index = dataGridView1.SelectedCells[0].RowIndex;
            frmAddUpdatePerson frm = new frmAddUpdatePerson((int)dataGridView1.Rows[index].Cells[0].Value);
            frm.ShowDialog();
            RefreshData();
        }



















        // ###########################################

        private void frmListPeople_Load(object sender, EventArgs e)
        {
            cbFilter.SelectedIndex = 0;
            RefreshData();
          SetUpTheDataGridView();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

      
    }
}
