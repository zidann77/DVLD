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
    public partial class ChangePassword : Form
    {
        clsUser User;
        int _UserID = -1;

        public ChangePassword(int ID)
        {
            InitializeComponent();
            _UserID = ID;
        }

        void ResetDefaultValues()
        {
            userCard1.ResetDefaultValues();

            txtConfirmNewPassword.Text = string.Empty;
            txtCureentPassword.Text = string.Empty;
            txtNewPassword.Text = string.Empty;

            txtCureentPassword.Focus();
        }



        private void ChangePassword_Load(object sender, EventArgs e)
        {
            //  ResetDefaultValues();

        
            User = clsUser.FindUser(_UserID);

            if (User == null)
            {
                // close form and show message

                MessageBox.Show("Could not Find User with id = " + _UserID,
                   "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
                return;
            }
            else
            {
                userCard1.LoadInfo(_UserID);
            }
        }

        private void txtCureentPassword_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(txtCureentPassword.Text.Trim()))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtCureentPassword, "This Field Is Required");
            }
            else
                errorProvider1.SetError(txtCureentPassword, null);

            if (txtCureentPassword.Text.Trim() != User.Password)
            {
                e.Cancel = true;
                errorProvider1.SetError(txtCureentPassword, "Current Password Is Wrong");
            }
            else
                errorProvider1.SetError(txtCureentPassword, null);
        }

        private void txtNewPassword_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(txtNewPassword.Text.Trim()))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtNewPassword, "This Field Is Required");
            }
            else
                errorProvider1.SetError(txtNewPassword, null);
        }

        private void txtConfirmNewPassword_Validating(object sender, CancelEventArgs e)
        {
            if (txtConfirmNewPassword.Text.Trim() != txtNewPassword.Text.Trim())
            {
                e.Cancel = true;
                errorProvider1.SetError(txtConfirmNewPassword, "Password Confirmation Does not Match New Password ");
            }
            else
                errorProvider1.SetError(txtConfirmNewPassword, null);

        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!this.ValidateChildren())
            {
                MessageBox.Show("Some fileds are not valide!, put the mouse over the red icon(s) to see the erro",
                    "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            else
            {
                User.Password = txtNewPassword.Text;
                if (User.Save())
                {
                    MessageBox.Show("Password Changed Successfully.",
                  "Saved.", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    ResetDefaultValues();
                }
                else
                {
                    MessageBox.Show("An Erro Occured, Password did not change.",
                  "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}
