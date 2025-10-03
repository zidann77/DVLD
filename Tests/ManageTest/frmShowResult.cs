using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLDProject.Tests.ManageTest
{
    public partial class frmShowResult : Form
    {
        public frmShowResult(int AppiontmentID)
        {
            InitializeComponent();

            testResult1.LoadResult(AppiontmentID);

        }
    }
}
