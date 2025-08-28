using BusinessLogic_DVLD;
using DVLDProject.Tests;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLDProject
{
    public partial class frmListTypes : Form
    {
        DataTable dtList;

        void _RefreshData()
        {
            dtList = clsTestType.GetAllTestTypes();
            dgvTestTypes.DataSource = dtList;
            lbRecords.Text = dtList.Rows.Count.ToString();

            if(dtList.Rows.Count > 0 )
            {

                dgvTestTypes.Columns[0].HeaderText = "ID";
                dgvTestTypes.Columns[0].Width = 120;

                dgvTestTypes.Columns[1].HeaderText = "Title";
                dgvTestTypes.Columns[1].Width = 200;

                dgvTestTypes.Columns[2].HeaderText = "Description";
                dgvTestTypes.Columns[2].Width = 400;

                dgvTestTypes.Columns[3].HeaderText = "Fees";
                dgvTestTypes.Columns[3].Width = 100;
            }
        }
        public frmListTypes()
        {
            InitializeComponent();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void clsListTestTypes_Load(object sender, EventArgs e)
        {
           _RefreshData();
        }

        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmEditTestType frm = new frmEditTestType((int)dgvTestTypes.CurrentRow.Cells[0].Value);
            frm.ShowDialog();   

        }
    }
}
