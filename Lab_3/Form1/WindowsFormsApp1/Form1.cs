using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            this.MaximizeBox = false;

            this.FormBorderStyle = FormBorderStyle.FixedSingle;
        }

        private void btnHello_Click(object sender, EventArgs e)
        {
            string userName = tbName.Text.Trim();
            if (userName.Length < 3)
            {
                MessageBox.Show("Введите имя пользователя!");
            }
            else
            {
                MessageBox.Show("Привет " + userName);

            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
       
    }
    
}
