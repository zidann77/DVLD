using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLDProject.PersonFolder
{
    public partial class frmShowPersonDetails : Form
    {
        public frmShowPersonDetails(int id)
        {
            InitializeComponent();
            personCard1.LoadData(id);
        }

        public frmShowPersonDetails(string NationalNo)
        {
            InitializeComponent();
           personCard1.LoadData(NationalNo);
        }


        private void ShowPersonDetails_Load(object sender, EventArgs e)
        {

        }
    }
}
