using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RJD_system
{
    public partial class operator_form : Form
    {
        public operator_form()
        {
            InitializeComponent();
        }

        private void operator_form_Load(object sender, EventArgs e)
        {

            label1.Text = Form1.name + " " + Form1.surname + " " + Form1.otchestvo;
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Выйти из системы?", "ЖД Вокзал", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                Form Form1 = new Form1();
                this.Visible = false;
                Form1.ShowDialog();

            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Выйти из системы?", "ЖД Вокзал", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                Form Form1 = new Form1();
                this.Visible = false;
                Form1.ShowDialog();

            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            Form uslugi_operator = new uslugi_operator();
            this.Visible = false;
            uslugi_operator.ShowDialog();
            this.Visible = true;
            this.Focus();
        }
    }
}
