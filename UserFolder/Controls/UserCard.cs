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
    public partial class UserCard : UserControl
    {
        int _UserID = -1;
        public int UserId { get { return _UserID; } }

        clsUser User;

        public UserCard()
        {
            InitializeComponent();
        }

        public void LoadInfo(int UserId)
        {
            User = clsUser.FindUser(UserId);

            if (User == null)
            {
                MessageBox.Show("No User with UserID = " + UserId.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            _FillUserInfo();
        }

        void _FillUserInfo()
        {
            personCard1.LoadData(User.Personid);

            lbUserName.Text = User.UserName;
            lbUserID.Text = User.id.ToString();

            if (User.isActive == true)
                lbIsActive.Text = "True";
            else
                lbIsActive.Text = "False";
        }

       public void ResetDefaultValues()
        {
            personCard1.ResetInfo();
            lbUserName.Text = string.Empty;
            lbUserID.Text=    string.Empty;
            lbIsActive.Text = string.Empty;
        }




        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void personCardWithFilter1_Load(object sender, EventArgs e)
        {

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void UserCard_Load(object sender, EventArgs e)
        {

        }
    }
}
