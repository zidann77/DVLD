using BusinessLogic_DVLD;
using DVLDProject.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using DVLDProject.Global_Classes;


namespace DVLDProject.PersonFolder.Controls
{
    public partial class PersonCard : UserControl
    {


        clsPerson _Person;
        int _ID = -1;

        public int PersonID
        {
            get { return _ID; }
        }

        public clsPerson selectedPersonInfo
        {
            get { return _Person; }
        }

        public PersonCard()
        {
            InitializeComponent();
        }


        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void PersonCaed_Load(object sender, EventArgs e)
        {

        }



        public void ResetInfo()
        {
            label10.Text =   "[????]";
             label12 .Text = "[????]";
             label13 .Text = "[????]";
             label15 .Text = "[????]";
             label14 .Text = "[????]";
             label11 .Text = "[????]";
             label16.Text =  "[????]";
            label17.Text =   "[????]";

            pictureBox1.Image = Resources.Male_512;

            _ID = -1;
            linkLabel2.Enabled = false;
            _Person = null;

        }

        void FillData()
        {
           linkLabel2.Enabled = true;
            _ID = _Person.PersonID;

            label10.Text = _Person.PersonID.ToString();
            label12.Text = _Person.FullName;
            label13.Text=_Person.NationalNo;
            label15.Text = _Person.Gendor == 0 ? "Male" : "FeMale";
            label14.Text = _Person.Email;
            label11.Text = clsFormat.DateToShort(_Person.DateOfBirth);
            label16.Text = _Person.Phone;
            label17.Text = _Person.Address;
            label18.Text = _Person.Country.CountryName;
            //  label17.Text =clsCountry.FindByID(_Person.NationalityCountryID).CountryName;

            if (_Person.ImagePath != null)
            {
                pictureBox1.ImageLocation = _Person.ImagePath;
            }
            else if (_Person.Gendor == 0)
                pictureBox1.Image = Resources.Male_512;
            else
                pictureBox1.Image = Resources.Female_512;





        }

        private void _LoadPersonImage()
        {
            if (_Person.Gendor == 0)
                pictureBox1.Image = Resources.Male_512;
            else
                pictureBox1.Image = Resources.Female_512;

            string ImagePath = _Person.ImagePath;
            if (ImagePath != "")
                if (File.Exists(ImagePath))
                    pictureBox1.ImageLocation = ImagePath;
                else
                    MessageBox.Show("Could not find this image: = " + ImagePath, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

        }


        public void LoadData(int id)
        {
            _Person = clsPerson.Find(id);
            if(_Person == null )
            {
                ResetInfo();
                return;
            }
            FillData();
        }

        public void LoadData(string NationalNo)
        {
            _Person = clsPerson.Find(NationalNo);
            if (_Person == null)
            {
                ResetInfo();
                return;
            }
            FillData();
        }


        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmAddUpdatePerson frm = new frmAddUpdatePerson(PersonID);
            frm.ShowDialog();
            
            // refresh

            LoadData(PersonID);
        }

















        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void label12_Click(object sender, EventArgs e)
        {

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

       
    }

}
