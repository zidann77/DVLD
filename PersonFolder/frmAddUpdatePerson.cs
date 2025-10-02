using BusinessLogic_DVLD;
using DVLDProject.Global_Classes;
using DVLDProject.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Serialization;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Tab;

namespace DVLDProject.PersonFolder
{
    public partial class frmAddUpdatePerson : Form
    {
        // Declare a delegate
        public delegate void DataBackEventHandler(object sender, int PersonID);

        // Declare an event using the delegate
        public event DataBackEventHandler DataBack;


        enum enMode { Update = 0, AddNew = 1 }

        enum enGendor { Male = 0, Female = 1 }

        int PersonID = -1;
        clsPerson _Person;
        enMode Mode = enMode.AddNew;


        public frmAddUpdatePerson(int id)
        {

            InitializeComponent();
            PersonID = id;
            Mode = enMode.Update;

        }


        public frmAddUpdatePerson()
        {
            InitializeComponent();
            Mode = enMode.AddNew;
        }


        void setAddNewMode()
        {
            // default values

            LoadCountryinComboBox();

            _Person = new clsPerson();

            lbTitle.Text = "Add Person";

            //foreach (Control control in this.Controls)
            //{
            //    // Check if the control is a TextBox
            //    if (control is TextBox)
            //    {
            //        // Clear the text in the TextBox
            //        control.Text = string.Empty;
            //    }
            //}

            textBox1.Text = string.Empty;
            textBox2.Text = string.Empty;
            textBox3.Text = string.Empty;
            textBox4.Text = string.Empty;
            textBox5.Text = string.Empty;
            textBox6.Text = string.Empty;
            textBox7.Text = string.Empty;

            label14.Text = string.Empty;
            label1.Text =string.Empty;

            rbFemale.Checked = true;

             SetImage();

            PersonPicture.ImageLocation = null;



            UpdateDateTimePicker();

            UpdateReomveLink();


        }

        void UpdateDateTimePicker()
        {
            dateTimePicker1.MaxDate = DateTime.Now.AddYears(-18);
            dateTimePicker1.MinDate = DateTime.Now.AddYears(-100);
            // dateTimePicker1.Value = DateTime.Now;
            dateTimePicker1.Value = dateTimePicker1.MaxDate;
            // it restricts the user to pick a date that is not earlier than 18 years ago from today.

        }

        void LoadCountryinComboBox()
        {
            DataTable dt = clsCountry.getAllCountries();

            comboBox1.Items.Clear();

            foreach (DataRow Row in dt.Rows)
            {
                comboBox1.Items.Add(Row["CountryName"].ToString());
            }

            if (Mode == enMode.AddNew)
                comboBox1.SelectedIndex = comboBox1.FindString("Jordan");
        }

        void SetImage()
        {
            if (_Person.ImagePath != "")
            {
                PersonPicture.ImageLocation = _Person.ImagePath;
                return;
            }

            if (rbMale.Checked)
                PersonPicture.Image = Resources.Male_512;
            else
                PersonPicture.Image = Resources.Female_512;


        }

        void UpdateReomveLink()
        {
            linkRemove.Visible = PersonPicture.ImageLocation != null;
        }

        void LoadPersonData()
        {
            LoadCountryinComboBox();
            _Person = clsPerson.Find(PersonID);
            if (_Person == null)
            {
                MessageBox.Show("No Person In this System With Person ID = " + PersonID, "Person Not Found", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
                return;
            }

            label14.Text = _Person.PersonID.ToString();
            textBox1.Text = _Person.FirstName;
            textBox4.Text = _Person.SecondName;
            textBox3.Text = _Person.ThirdName;
            textBox2.Text = _Person.LastName;
            textBox5.Text = _Person.NationalNo;
            dateTimePicker1.Value = _Person.DateOfBirth;

            comboBox1.SelectedIndex = comboBox1.FindString(_Person.Country.CountryName);

            if (_Person.Gendor == 0)
                rbMale.Checked = true;
            else
                rbFemale.Checked = true;


            richTextBox1.Text = _Person.Address;
            textBox7.Text = _Person.Phone;
            textBox6.Text = _Person.Email;


            SetImage();

            UpdateReomveLink();

        }

        void SetUpdateMode()
        {
            lbTitle.Text = "Update Person Info";
            LoadPersonData();
        }
        //void ConfigurationForm()
        //{
        //    setAddNewMode();
        //    if (Mode == enMode.Update)
        //    {
        //        SetUpdateMode();
        //    }
        //        Which One is Better?
        //ConfigurationForm1() is generally the better approach:

        //Less redundant: You avoid the unnecessary call to setAddNewMode() when it's not needed.
        //Cleaner logic: The flow is more intuitive because it first checks the mode and then
        //            applies the appropriate settings without setting one mode unconditionally.
        //}

        void ConfigurationForm()
        {
            if (Mode == enMode.Update)
            {
                SetUpdateMode();
            }
            else
                setAddNewMode();

        }

        private void frmAddUpdatePerson_Load(object sender, EventArgs e)
        {
            ConfigurationForm();
        }


        bool ValidateChilldren()
        {
            if (!this.ValidateChildren())
            {
                MessageBox.Show("Some fileds are not valide!, put the mouse over the red icon(s) to see the erro", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            return true;
        }

        private bool _HandlePersonImage()
        {
            // Ensure the image was changed or added
            if (PersonPicture.ImageLocation != null &&
                (string.IsNullOrEmpty(_Person.ImagePath) || _Person.ImagePath != PersonPicture.ImageLocation))
            {
                // Delete old image if it exists
                if (!string.IsNullOrEmpty(_Person.ImagePath))
                {
                    try
                    {
                        File.Delete(_Person.ImagePath);
                    }
                    catch (IOException)
                    {
                        // Log if needed
                    }
                }

                // Copy new image
                string SourceImageFile = PersonPicture.ImageLocation.ToString();

                if (Util.CopyImageToProjectImagesFolder(ref SourceImageFile))
                {
                    PersonPicture.ImageLocation = SourceImageFile;
                    return true;
                }
                else
                {
                    MessageBox.Show("Error Copying Image File", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
            }

            return true;
        }

        void FillObject()
        {

            _Person.FirstName = textBox1.Text.Trim();
            _Person.SecondName = textBox4.Text.Trim();
            _Person.ThirdName = textBox3.Text.Trim();
            _Person.LastName = textBox2.Text.Trim();
            _Person.NationalNo = textBox5.Text.Trim();
            _Person.Email = textBox6.Text.Trim();
            _Person.Phone = textBox7.Text.Trim();
            _Person.Address = richTextBox1.Text.Trim();
            _Person.DateOfBirth = dateTimePicker1.Value;


            // Assuming FindByName expects the country name as a string, not the index.

            // _Person.Country = clsCountry.FindByName(comboBox1.SelectedIndex.ToString());

            _Person.NationalityCountryID = clsCountry.FindByName(comboBox1.Text).CountryID;



            if (rbMale.Checked)
                _Person.Gendor = (short)enGendor.Male;
            else
                _Person.Gendor = (short)enGendor.Female;

            if (PersonPicture.Image != null)
                _Person.ImagePath = PersonPicture.ImageLocation;
            else
                _Person.ImagePath = string.Empty;


            SavePerson();


        }

        void SavePerson()
        {
            if (_Person.save())
            {
                MessageBox.Show("Data Saved Successfully", "Save", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Mode = enMode.Update;
                lbTitle.Text = "Update Person";
                DataBack?.Invoke(this, _Person.PersonID);
                //  SetUpdateMode();
            }
            else
                MessageBox.Show("Data Didnt Saved ", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!ValidateChildren())
                return;

            if (!_HandlePersonImage())
                return;

            FillObject();

        }


        private void linkSet_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            openFileDialog1.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.gif;*.bmp";
            openFileDialog1.FilterIndex = 0;
            openFileDialog1.RestoreDirectory = true;

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                string SelectedImgPath = openFileDialog1.FileName;
                PersonPicture.Load(SelectedImgPath); // load path
                linkRemove.Visible = true;
            }
        }

        private void linkRemove_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            
            PersonPicture.ImageLocation = null;

            if (rbMale.Checked)
                PersonPicture.Image = Resources.Male_512;
            else
                PersonPicture.Image = Resources.Female_512;

            linkRemove.Visible = false;
        }





        private void textBox1_Validating(object sender, CancelEventArgs e)
        {
            TextBox textBox = sender as TextBox;

            if (String.IsNullOrEmpty(textBox.Text.Trim()))
            {
                e.Cancel = true;

                errorProvider1.SetError(textBox, "This field is required!");
            }
           else
            {
                errorProvider1.SetError(textBox, null);
            }
        }


        // Eamil Validation
      

        private void textBox6_Validating(object sender, CancelEventArgs e)
        {
            TextBox EamilBox = sender as TextBox;

            if (string.IsNullOrEmpty(EamilBox.Text.Trim()))
                return;

            if (!clsValidation.ValidateEmail(EamilBox.Text.Trim()))
            {
                e.Cancel = true;
                errorProvider1.SetError(EamilBox, "Invalid Email Address Format!");
            }
            else
                errorProvider1.SetError(EamilBox, null);
        }


        private void richTextBox1_Validating(object sender, CancelEventArgs e)
        {

        }


        // national no validation

        private void textBox5_Validating(object sender, CancelEventArgs e)
        {

            TextBox textBox = sender as TextBox;

            if (String.IsNullOrEmpty(textBox.Text.Trim()))
            {
              //  e.Cancel = true;

                errorProvider1.SetError(textBox, "This field is required!");

                return;
            }
            else
            {
                errorProvider1.SetError (textBox, null);    
            }

            //   if((textBox.Text.Trim()) != _Person.NationalNo && clsPerson.isPersonExist(textBox.Text.Trim()))
            if (clsPerson.isPersonExist(textBox.Text.Trim()) && Mode == enMode.AddNew)
            {
               // e.Cancel = true;

                errorProvider1.SetError(textBox, "National Number is Used For Another Person");

                return;
            }
            else
            {
                errorProvider1.SetError(textBox, null);
            }

        }





        //------------------------------------



        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void lbTitle_Click(object sender, EventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void label9_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

      

        private void rbFemale_Click(object sender, EventArgs e)
        {
            if (PersonPicture.ImageLocation == null)
                PersonPicture.Image = Resources.Female_512;
        }

        private void rbMale_Click(object sender, EventArgs e)
        {
            if (PersonPicture.ImageLocation == null)
                PersonPicture.Image = Resources.Male_512;
        }

       

        private void richTextBox1_Validating_1(object sender, CancelEventArgs e)
        {
            if(string.IsNullOrEmpty(richTextBox1.Text .Trim()))
            {
                e.Cancel = true;
                errorProvider1.SetError(richTextBox1, "This Feild Required");
            }
            else
                errorProvider1.SetError(richTextBox1, null);
        }

        private void textBox7_TextChanged(object sender, EventArgs e)
        {

        }

        private void comboBox1_Validating(object sender, CancelEventArgs e)
        {
            if(comboBox1.Text == null)
            {
                e.Cancel= true;
                errorProvider1.SetError(comboBox1, "This Field is Required");
            }
            else
                errorProvider1.SetError(comboBox1, null);
        }
    }



    }

