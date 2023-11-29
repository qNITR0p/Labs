using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lab9
{
    public partial class SortingForm : Form
    {
        public string Period { get; private set; }

        public SortingForm()
        {
            InitializeComponent();

            // Запретить изменение размера формы
            this.FormBorderStyle = FormBorderStyle.FixedSingle;

            // Запретить разворачивание формы на весь экран
            this.MaximizeBox = false;

            // Отключить кнопку "ОК" по умолчанию
            this.okButton.Enabled = false;

            // Добавить периоды в ComboBox
            this.periodComboBox.Items.Add("День");
            this.periodComboBox.Items.Add("Неделя");
            this.periodComboBox.Items.Add("Месяц");

            // Подключить обработчик событий к событию SelectedIndexChanged
            this.periodComboBox.SelectedIndexChanged += new EventHandler(periodComboBox_SelectedIndexChanged);
        }



        private void okButton_Click(object sender, EventArgs e)
        {
            Period = periodComboBox.Text;
            DialogResult = DialogResult.OK;
            Close();
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void periodComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Включить кнопку "ОК", когда пользователь выберет период
            this.okButton.Enabled = true;
        }
    }


}
