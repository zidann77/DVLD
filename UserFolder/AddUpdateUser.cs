using BusinessLogic_DVLD;
using System;
using System.CodeDom.Compiler;
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
    public partial class frmAddUpdateUser : Form
    {
        enum enMode { add = 0, Update = 1 }
        enMode Mode;

        void ChangeModeToUpdate()
        {
            lbTitle.Text = "Update User";
            Mode = enMode.Update;
        }

        int UserId = 0;
        clsUser User;


        void setDefaultValues()
        {
            lbTitle.Text = "Add User";
            this.Text = "Add New User";

            //textBox1.Enabled = false;
            //textBox2.Enabled = false;
            //textBox3.Enabled = false;

            tbLoginInfo.Enabled = false;

            User = new clsUser();

        }

        void FillLoginInfo()
        {
            lbUserId.Text = User.id.ToString();
            tbUserName.Text = User.UserName.ToString();
            tbPassword.Text = User.Password.ToString();
            tbConfirmPassword.Text = User.Password.ToString();
            checkBox1.Checked = User.isActive;
        }

        void FillUserInfo()
        {
            if (User == null)
            {
                MessageBox.Show("No User With UserID = " + UserId, "Warnning", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
                return;
            }
            else
            {
                personCardWithFilter1.LoadPersonInfo(User.Personid);
                FillLoginInfo();
            }
        }

        void LoadData()
        {
            lbTitle.Text = "Update User";
            this.Text = "Update User";

            //textBox1.Enabled =true;
            //textBox2.Enabled =true;
            //textBox3.Enabled =true;

            personCardWithFilter1.EnableFilter = false;

            User = clsUser.FindUser(UserId);

           FillUserInfo();
        }

        void UpdateForm()
        {
            switch (Mode)
            {
                case enMode.add:
                    setDefaultValues();
                    break;

                case enMode.Update:
                    LoadData();
                    break;
            }
        }

        public frmAddUpdateUser()
        {
            InitializeComponent();
            personCardWithFilter1.Focus();
            Mode = enMode.add;
            UpdateForm();
        }

        public frmAddUpdateUser(int ID)
        {
            InitializeComponent();
            personCardWithFilter1.Focus();
            UserId = ID;
            Mode = enMode.Update;
            UpdateForm();
        }

        private void frmAddUpdateUser_Load(object sender, EventArgs e)
        {
            UpdateForm();
        }

        void CopyDataToUser()
        {
            User.UserName = tbUserName.Text.Trim();
            User.Password = tbPassword.Text.Trim();
            User.isActive = checkBox1.Checked;
            User.Personid = personCardWithFilter1.PersonID;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!this.ValidateChildren())
            {
                MessageBox.Show("Some fileds are not valide!, put the mouse over the red icon(s) to see the erro",
                    "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            CopyDataToUser();

            if (User.Save())
            {
                ChangeModeToUpdate();
                lbTitle.Text = "Update User";
                lbUserId.Text = User.id.ToString();

                MessageBox.Show("Data Saved Successfully.", "Saved", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Error: Data Is not Saved Successfully.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void tbUserName_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(tbUserName.Text.Trim()))
            {
                e.Cancel = true;
                errorProvider1.SetError(tbUserName, "This Field is Required");
                return;
            }
            else
                errorProvider1.SetError(tbUserName, null);

            if (Mode == enMode.add)
            {

                if (clsUser.isUserExist(tbUserName.Text.Trim()))
                {
                    e.Cancel = true;
                    errorProvider1.SetError(tbUserName, "User Name Is Not Available");
                    return;
                }
                else
                {
                    errorProvider1.SetError(tbUserName, null);
                }
            }

            else if (User.UserName != tbUserName.Text.Trim())
            {
                if (clsUser.isUserExist(tbUserName.Text.Trim()))
                {
                    e.Cancel = true;
                    errorProvider1.SetError(tbUserName, "User Name Is Not Available");
                    return;
                }
                else
                {
                    errorProvider1.SetError(tbUserName, null);
                }
            }
        }

        private void PasswordValidating(object sender, CancelEventArgs e)
        {

            if (string.IsNullOrEmpty(tbPassword.Text.Trim()))
            {
                e.Cancel = true;
                errorProvider1.SetError(tbPassword, "this Field is required");
            }
            else
                errorProvider1.SetError(tbPassword, null);

        }

        private void tbConfirmPassword_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(tbConfirmPassword.Text.Trim()))
            {
                e.Cancel = true;
                errorProvider1.SetError(tbConfirmPassword, "this Field is required");
            }
            else
                errorProvider1.SetError(tbConfirmPassword, null);


            if (tbConfirmPassword.Text.Trim() != tbPassword.Text.Trim())
            {
                e.Cancel = true;
                errorProvider1.SetError(tbConfirmPassword, "Password Confirmation Does not Match Password");
            }
            else
                errorProvider1.SetError(tbConfirmPassword, null);
        }


        private void btnNext_Click(object sender, EventArgs e)
        {
            if (Mode == enMode.Update)
            {

                btnSave.Enabled = true;
                tbLoginInfo.Enabled = true;
                tcUserInfo.SelectedIndex = 1;
                //  tcUserInfo.SelectedTab =   tcUserInfo.TabPages["tbLoginInfo"];
            }

            else
            {
                if (personCardWithFilter1.PersonID == -1)
                {
                    MessageBox.Show("Please Select a Person", "Select a Person", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    personCardWithFilter1.FilterFocus();
                    return;
                }
                else
                {
                    if (clsUser.isUserExistForPersonID(personCardWithFilter1.PersonID))
                    {
                        MessageBox.Show("Selected Person already has a user, choose another one.", "Select another Person", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        personCardWithFilter1.FilterFocus();
                    }
                    else
                    {
                        btnSave.Enabled = true;
                        tbLoginInfo.Enabled = true;
                        tcUserInfo.SelectedIndex = 1;
                    }
                }

            }
        }

        private void personCardWithFilter1_Load(object sender, EventArgs e)
        {

        }

        private void tbConfirmPassword_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmAddUpdateUser_Activated(object sender, EventArgs e)
        {
            personCardWithFilter1.FilterFocus();


            //            When does this event trigger?
            //When the form first loads and becomes active.

            //When the user switches back to this form from another window.

            //When the form is programmatically activated using this.Activate();.

            //Would you like to modify its behavior or debug an issue with it?
        }

        private void personCardWithFilter1_Load_1(object sender, EventArgs e)
        {

        }

     
    }





    //    Conclusion: Are They 100% Functionally Identical?
    //🔹 Yes, the two classes are functionally identical(99% similar), but not 100% identical due to minor differences in naming conventions and method calls.

    //🔹 The core logic(adding/updating users, validation, form switching) is the same, but different naming conventions and method implementations slightly alter how the code is structured.

    //🔹 If both codes interact with the same clsUser class and database, they will behave identically in execution.

    //Would you like to merge them or standardize naming conventions? 🚀
}

//using BusinessLogic_DVLD;
//using System;
//using System.ComponentModel;
//using System.Windows.Forms;

//namespace DVLDProject.UserFolder
//{
//    public partial class frmAddUpdateUser : Form
//    {
//        enum enMode { Add = 0, Update = 1 }
//        enMode Mode;

//        int UserId = 0;
//        clsUser User;

//        public frmAddUpdateUser()
//        {
//            InitializeComponent();
//            Mode = enMode.Add;
//            UpdateForm();
//        }

//        public frmAddUpdateUser(int ID)
//        {
//            InitializeComponent();
//            Mode = enMode.Update;
//            UserId = ID;
//            UpdateForm();
//        }

//        private void frmAddUpdateUser_Load(object sender, EventArgs e)
//        {
//            UpdateForm();
//        }

//        // Method to reset form to the default values for adding a new user
//        private void SetDefaultValues()
//        {
//            lbTitle.Text = "Add User";
//            this.Text = "Add New User";
//            tbLoginInfo.Enabled = false;
//            User = new clsUser();
//            personCardWithFilter1.FilterFocus();
//        }

//        // Method to load user data for updating an existing user
//        private void LoadData()
//        {
//            lbTitle.Text = "Update User";
//            this.Text = "Update User";
//            personCardWithFilter1.EnableFilter = false;

//            User = clsUser.FindUser(UserId);

//            if (User == null)
//            {
//                MessageBox.Show("No User With UserID = " + UserId, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Error);
//                this.Close();
//                return;
//            }

//            FillLoginInfo();
//            personCardWithFilter1.LoadPersonInfo(User.Personid);
//        }

//        // Method to fill login information fields (username, password, etc.)
//        private void FillLoginInfo()
//        {
//            lbUserId.Text = User.id.ToString();
//            tbUserName.Text = User.Name;
//            tbPassword.Text = User.Password;
//            tbConfirmPassword.Text = User.Password;
//            checkBox1.Checked = User.isActive;
//        }

//        // Method to copy data from form fields to User object
//        private void CopyDataToUser()
//        {
//            User.Name = tbUserName.Text.Trim();
//            User.Password = tbPassword.Text.Trim();
//            User.isActive = checkBox1.Checked;
//            User.Personid = personCardWithFilter1.PersonID;
//        }

//        // Main method to update the form based on the mode (Add or Update)
//        private void UpdateForm()
//        {
//            switch (Mode)
//            {
//                case enMode.Add:
//                    SetDefaultValues();
//                    break;

//                case enMode.Update:
//                    LoadData();
//                    break;
//            }
//        }

//        // Save button click handler
//        private void btnSave_Click(object sender, EventArgs e)
//        {
//            if (!this.ValidateChildren())
//            {
//                MessageBox.Show("Some fields are not valid! Please check the error icons.",
//                    "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
//                return;
//            }

//            CopyDataToUser();

//            if (User.Save())
//            {
//                Mode = enMode.Update;
//                lbTitle.Text = "Update User";
//                lbUserId.Text = User.id.ToString();
//                MessageBox.Show("Data Saved Successfully.", "Saved", MessageBoxButtons.OK, MessageBoxIcon.Information);
//            }
//            else
//            {
//                MessageBox.Show("Error: Data was not saved successfully.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
//            }
//        }

//        // Username validation method
//        private void tbUserName_Validating(object sender, CancelEventArgs e)
//        {
//            if (string.IsNullOrEmpty(tbUserName.Text.Trim()))
//            {
//                e.Cancel = true;
//                errorProvider1.SetError(tbUserName, "This field is required.");
//                return;
//            }
//            else
//            {
//                errorProvider1.SetError(tbUserName, null);
//            }

//            if (Mode == enMode.Add && clsUser.isUserExist(tbUserName.Text.Trim()))
//            {
//                e.Cancel = true;
//                errorProvider1.SetError(tbUserName, "Username is not available.");
//                return;
//            }

//            if (Mode == enMode.Update && User.Name != tbUserName.Text.Trim() && clsUser.isUserExist(tbUserName.Text.Trim()))
//            {
//                e.Cancel = true;
//                errorProvider1.SetError(tbUserName, "Username is not available.");
//                return;
//            }

//            errorProvider1.SetError(tbUserName, null);
//        }

//        // Password validation method
//        private void PasswordValidating(object sender, CancelEventArgs e)
//        {
//            if (string.IsNullOrEmpty(tbPassword.Text.Trim()))
//            {
//                e.Cancel = true;
//                errorProvider1.SetError(tbPassword, "This field is required.");
//            }
//            else
//            {
//                errorProvider1.SetError(tbPassword, null);
//            }

//            if (tbConfirmPassword.Text.Trim() != tbPassword.Text.Trim())
//            {
//                e.Cancel = true;
//                errorProvider1.SetError(tbConfirmPassword, "Passwords do not match.");
//            }
//            else
//            {
//                errorProvider1.SetError(tbConfirmPassword, null);
//            }
//        }

//        // "Next" button click handler to move to the next tab
//        private void btnNext_Click(object sender, EventArgs e)
//        {
//            if (Mode == enMode.Update)
//            {
//                btnSave.Enabled = true;
//                tbLoginInfo.Enabled = true;
//                tcUserInfo.SelectedIndex = 1;
//            }
//            else
//            {
//                if (personCardWithFilter1.PersonID == -1)
//                {
//                    MessageBox.Show("Please select a person.", "Select a Person", MessageBoxButtons.OK, MessageBoxIcon.Error);
//                    personCardWithFilter1.FilterFocus();
//                    return;
//                }

//                if (clsUser.isUserExistForPersonID(personCardWithFilter1.PersonID))
//                {
//                    MessageBox.Show("The selected person already has a user. Please choose another one.",
//                        "Select Another Person", MessageBoxButtons.OK, MessageBoxIcon.Error);
//                    personCardWithFilter1.FilterFocus();
//                    return;
//                }

//                btnSave.Enabled = true;
//                tbLoginInfo.Enabled = true;
//                tcUserInfo.SelectedIndex = 1;
//            }
//        }

//        // Close button click handler
//        private void btnClose_Click(object sender, EventArgs e)
//        {
//            this.Close();
//        }

//        // Handle the form activation event
//        private void frmAddUpdateUser_Activated(object sender, EventArgs e)
//        {
//            personCardWithFilter1.FilterFocus();
//        }
//    }
//}
