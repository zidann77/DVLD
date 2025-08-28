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

namespace DVLDProject.Applications.Application_Types
{
    public partial class ListApplicationType : Form
    {
        DataTable dt;
        public ListApplicationType()
        {
            InitializeComponent();
        }
        private void _RefreshData()
        {
            dt = clsApplicationType.GetAllApplicationTypes();
            dgvApplicationTypes.DataSource = dt;

            lbRecordes.Text = dgvApplicationTypes.RowCount.ToString();

            if (dgvApplicationTypes.RowCount > 0)
            {

                dgvApplicationTypes.Columns[0].HeaderText = "ID";
                dgvApplicationTypes.Columns[0].Width = 110;

                dgvApplicationTypes.Columns[1].HeaderText = "Title";
                dgvApplicationTypes.Columns[1].Width = 440;

                dgvApplicationTypes.Columns[2].HeaderText = "Fees";
                dgvApplicationTypes.Columns[2].Width = 110;

            }
        }

        private void ListApplicationType_Load(object sender, EventArgs e)
        {
            _RefreshData();
        }

        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {
            EditApplicationTypes frm = new EditApplicationTypes((int)dgvApplicationTypes.CurrentRow.Cells[0].Value);
            frm.ShowDialog();
            _RefreshData();
        }
    }
}
