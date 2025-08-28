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
    public partial class UserInfo : Form
    {

        int _UserId = -1;

        public UserInfo(int UserId)
        {
            InitializeComponent();
            _UserId = UserId;
        }

        private void userCard1_Load(object sender, EventArgs e)
        {
            // load  at form level for more readable code 
        }

        private void UserInfo_Load(object sender, EventArgs e)
        {
            if (clsUser.FindUser(_UserId) == null)
            {
                this.Close();
                return;
            }

            userCard1.LoadInfo(_UserId);
        }
    }
}
