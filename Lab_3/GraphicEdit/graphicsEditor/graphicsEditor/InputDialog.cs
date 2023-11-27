using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace graphicsEditor
{
    public partial class InputDialog : Form
    {
        public InputDialog()
        {
            InitializeComponent();

            // Установите стиль границы формы на FixedDialog
            this.FormBorderStyle = FormBorderStyle.FixedDialog;

            // Отключите кнопку "Развернуть на весь экран"
            this.MaximizeBox = false;
        }

        public int W
        {
            get
            {
                int value = (int)txtbWidtht.Value;
                return value;
            }
        }
        public int H
        {
            get
            {
                int value = (int)txtbHeight.Value;
                return value;
            }
        }
        public string fileName
        {
            get
            {
                string text = txtbName.Text;
                string value = Convert.ToString(text);
                return value;
            }
        }

        bool _canceled = false;
        public bool Canceled
        {
            get { return _canceled; }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            _canceled = true;
            this.Close();
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(txtbName.Text) &&
                !string.IsNullOrWhiteSpace(txtbHeight.Text) &&
                !string.IsNullOrWhiteSpace(txtbWidtht.Text))
            {
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            else
            {
                MessageBox.Show("Заполните все поля");
            }
        }


    }
}
