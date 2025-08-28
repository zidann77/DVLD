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

namespace DVLDProject.PersonFolder.Controls
{
    public partial class PersonCardWithFilter : UserControl
    {


        // Define a custom event handler delegate with parameters
        public event Action<int> OnPersonSelected;
        // Create a protected method to raise the event with a parameter
        protected virtual void PersonSelected(int PersonID)
        {
            Action<int> handler = OnPersonSelected;
            if (handler != null)
            {
                handler(PersonID); // Raise the event with the parameter
            }
        }

        private bool _ShowAddNewPerson = true;
        private bool _EnableFilter = true;
    //    int _PersonID = -1;


     

        public bool ShowAddPerson
        {
            get { return _ShowAddNewPerson; }
            set
            {
                _ShowAddNewPerson = value;
                btnAddNew.Visible = ShowAddPerson;
            }
        }

        public bool EnableFilter
        {
            get => _EnableFilter;
            set 
            {
                _EnableFilter = value;
                btnAddNew.Visible = EnableFilter;
            }
        }

        public int PersonID
        {
          //  get => _PersonID;
          get
            {
                return personCard1.PersonID;
            }
        }

        public clsPerson SelectedPerson
        {
            get
            {
                return personCard1.selectedPersonInfo;
            }
        }

      public  void LoadPersonInfo(int ID)
        {
            textBox1.Text = ID.ToString();
            comboBox1.SelectedIndex = 0;
            personCard1.LoadData(ID);
        }


        void FindNow()
        {
            switch (comboBox1.SelectedIndex)
            {
                case 0:
                    personCard1.LoadData(Convert.ToInt32(textBox1.Text));
                    break;

                case 1:
                    personCard1.LoadData(textBox1.Text.ToString());
                    break;

            }

            // firing the event

            if(OnPersonSelected != null && EnableFilter == true)
            {
                OnPersonSelected(personCard1.PersonID);
            }
        }

        private void textBox1_Validating(object sender, CancelEventArgs e)
        {
            //if (string.IsNullOrEmpty(textBox1.Text.Trim()) && comboBox1.Text != string.Empty) 
            //{
            //    e.Cancel = true;
            //    errorProvider1.SetError(textBox1, "This Feild is Required");
            //}
            //else 
            //{
            // //   e.Cancel = false;
            //    errorProvider1.SetError(textBox1, null);
            //}
        }



        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            textBox1.Text = string.Empty;
            textBox1.Focus();
        }


        private void btnAddNew_Click(object sender, EventArgs e)
        {
            frmAddUpdatePerson frm = new frmAddUpdatePerson();
            frm.DataBack += databackevent;
            frm.ShowDialog();
           
        }

        void databackevent(object sender , int id)
        {
            comboBox1.SelectedIndex = 0;
            textBox1.Text=id.ToString();
            personCard1.LoadData(id);

        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        { 
            if (e.KeyChar == (char)13)
            {
               btnSearch.PerformClick();
            }


            if (comboBox1.SelectedIndex == 0)
                e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);


        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            if(!this.ValidateChildren())
            {
                MessageBox.Show("Some fileds are not valide!, put the mouse over the red icon(s) to see the erro", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            FindNow();
        }

        public void FilterFocus()
        {
            textBox1.Focus();
        }












        public PersonCardWithFilter()
        {
            InitializeComponent();
        }




        // ============================================================

        private void personCard1_Load(object sender, EventArgs e)
        {

        }

        private void PersonCardWithFilter_Load(object sender, EventArgs e)
        {
            comboBox1 .SelectedIndex = 0;
        }

        private void FilterBox_Enter(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void personCard1_Load_1(object sender, EventArgs e)
        {

        }
    }
}
