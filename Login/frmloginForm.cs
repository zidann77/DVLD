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

namespace DVLDProject
{
    public partial class frmloginForm : Form
    {
        public frmloginForm()
        {
            InitializeComponent();
        }
        

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnHidePassword_Click(object sender, EventArgs e)
        {
            if (txtPassword.PasswordChar == '*')
            {
                btnHidePassword.BackColor = Color.Transparent;
                txtPassword.PasswordChar = '\0';  // default value
            }
            else
            {
                btnHidePassword.BackColor = Color.Teal;
                txtPassword.PasswordChar = '*';
            }
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            clsUser User = clsUser.FindUser(txtUserName.Text.Trim(), txtPassword.Text.Trim());

            if (User != null)
            {
                if (!User.isActive)
                {
                    txtUserName.Focus();
                    MessageBox.Show("Your accound is not Active, Contact Admin.", "In Active Account", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return; // Stops further processing, avoids weird bugs
                }

                if (chkRemeber.Checked)
                {
                    clsGlobal.RememberUsernameAndPassword(txtUserName.Text.Trim(), txtPassword.Text.Trim());
                }
                else
                {
                    clsGlobal.RememberUsernameAndPassword("", "");
                }

                clsGlobal.CurrentUser = User;

                this.Hide();
                frmMain Mainfrm = new frmMain(this);
                Mainfrm.ShowDialog();

            }

            else
            {
                txtUserName.Focus();
                MessageBox.Show("Invalid Username/Password.", "Wrong Credintials", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void frmloginForm_Load(object sender, EventArgs e)
        {
            string UserName = "", Password = "";

        //    splitContainer1.Panel2.Focus();

            if (clsGlobal.GetStoredCredential(ref UserName, ref Password))
            {
                txtPassword.Text = Password;
                txtUserName.Text = UserName;
                chkRemeber.Checked = true;
                btnLogin.Enabled = true;
                btnLogin.Focus();
            }
            else
            {
                txtUserName.Focus();
                btnLogin.Enabled = false;
                chkRemeber.Checked = false;
            }
        }

        private void splitContainer1_Panel2_Paint(object sender, PaintEventArgs e)
        {
           
        }

        private void ValidateTextBoxes(object sender, EventArgs e)
        {
            btnLogin.Enabled = !(string.IsNullOrEmpty(txtUserName.Text.Trim())) && !(string.IsNullOrEmpty(txtPassword.Text.Trim()));
        }
        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void splitContainer1_SplitterMoved(object sender, SplitterEventArgs e)
        {

        }

        private void splitContainer1_Panel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
