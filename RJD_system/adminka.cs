using MySql.Data.MySqlClient;
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
    public partial class adminka : Form
    {
        public adminka()
        {
            InitializeComponent();
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Выйти из программы?\n.", "ЖД Вокзал", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                Application.Exit();
            }
            }

        private void refresh_window ()
        {
          
          
           
        }

        private void adminka_Load(object sender, EventArgs e)
        {
            refresh_window();
            label1.Text = Form1.name + " " + Form1.surname + " " + Form1.otchestvo;
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            Form sotrudniki = new sotrudniki();
            this.Visible = false;
            sotrudniki.ShowDialog();
            this.Visible = true;
            this.Focus();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            Form transport = new transport();
            this.Visible = false;
            transport.ShowDialog();
            this.Visible = true;
            this.Focus();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Выйти из программы?\n.", "ЖД Вокзал", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                Application.Exit();
            }
        }
    }
}
