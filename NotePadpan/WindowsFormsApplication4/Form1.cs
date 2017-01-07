using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WindowsFormsApplication4
{
    public partial class searchForm : Form
    {
        NotePadForm mainForm;
        public String searchContentString;
        public bool upBoolean = false;
        public bool distinguishBoolean = false;
        public searchForm(NotePadForm Form2)
        { 
            InitializeComponent();
            mainForm = Form2;
        }

        private void searchTextBox_TextChanged(object sender, EventArgs e)
        {
            nextButton.Enabled = true;
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        int cursorLocationInteger;
        int count = 0;
        private void nextButton_Click(object sender, EventArgs e)
        {
            searchContentString = searchTextBox.Text;

            cursorLocationInteger=mainForm.textBox1.SelectionStart;

            if (!distinguishCheckBox.Checked)
            {
                string tempString = mainForm.textBox1.Text.ToUpper();
                if (upRadioButton.Checked)
                {
                    cursorLocationInteger = mainForm.textBox1.Text.IndexOf(searchContentString.ToUpper(), cursorLocationInteger+1);
                         
                }
                else
                {
                    cursorLocationInteger = mainForm.textBox1.Text.IndexOf(searchContentString.ToUpper(), cursorLocationInteger+1);
                }
            }

            else
            {
                if (upRadioButton.Checked)
                {
                    cursorLocationInteger = mainForm.textBox1.Text.IndexOf(searchContentString.ToUpper(), cursorLocationInteger + 1);
                }
                else
                {
                    cursorLocationInteger = mainForm.textBox1.Text.IndexOf(searchContentString.ToUpper(), cursorLocationInteger + 1);
                }
            }
            
            mainForm.textBox1.Select(cursorLocationInteger, searchContentString.Length);
            mainForm.Activate();

        }

        private void downRadioButton_CheckedChanged(object sender, EventArgs e)
        {

        }
    }
}
