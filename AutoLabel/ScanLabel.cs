using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AutoLabel
{
    public partial class ScanLabel : Form
    {
        Form1 mainForm;

        public ScanLabel(Form1 frm)
        {
            InitializeComponent();
            mainForm = frm;
            this.ActiveControl = tbInputSN;

        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if(tbInputSN.Text != "")
            {
                if (tbInputSN.Text.Length == 13)
                {
                    mainForm.SScanLabel = tbInputSN.Text;
                    this.Hide();
                }
                else
                {
                    tbInputSN.Text = "";
                    tbInputSN.Focus();
                }
                
            }
            else
            {
                MessageBox.Show("S/N cannot be empty!!!");
            }
        }

        private void tbInputSN_KeyPress(object sender, KeyPressEventArgs e)
        {
         
        }

        private void tbInputSN_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Enter)
            {
                btnOK.Focus();
            }
        }

        private void ScanLabel_Load(object sender, EventArgs e)
        {
            
        }
    }
}
