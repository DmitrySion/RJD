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
    public partial class add_poezd : Form
    {
        public add_poezd()
        {
            InitializeComponent();
        }
        public static int idpoezd;
        private void add_poezd_Load(object sender, EventArgs e)
        {
            Random rnd = new Random();

            idpoezd = rnd.Next(1002, 9997);
            label1.Text = "ID: " + idpoezd.ToString();
        }
        

        private void button1_Click(object sender, EventArgs e)
        {
            Random rnd = new Random();

            idpoezd = rnd.Next(1002, 9997);
            label1.Text = "ID: " + idpoezd.ToString();
            //начинаем запрос
            try
            {
                MySqlConnection conn = new MySqlConnection(Form1.connStr);
                // устанавливаем соединение с БД
                conn.Open();
                string add = "INSERT INTO Poezd SET " +
                    "ID_Poezda = '" + idpoezd.ToString() + "', " +
                    "Kolichestvo_vagonov = '" + textBox2.Text + "', " +
                    "Kolichestvo_mest = '" + textBox3.Text + "'";


                MySqlCommand adda = new MySqlCommand(add, conn);
                //   MySqlCommand insrt = new MySqlCommand(insert, conn);
                MySqlDataReader MyDataReader;
                MyDataReader = adda.ExecuteReader();

                while (MyDataReader.Read())
                {
                }
                MyDataReader.Close();
                conn.Close();
                MessageBox.Show("Новый поезд успешно добавлен!", "ЖД Вокзал", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Close();
            }
            catch
            {
                MessageBox.Show("Ошибка добавления!", "ЖД Вокзал", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Random rnd = new Random();

            idpoezd = rnd.Next(1002, 9997);
            label1.Text = "ID: " + idpoezd.ToString();
        }
    }
}
