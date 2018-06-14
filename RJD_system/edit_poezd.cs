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
    public partial class edit_poezd : Form
    {
        public edit_poezd()
        {
            InitializeComponent();
        }

        private void edit_poezd_Load(object sender, EventArgs e)
        {
            label1.Text = "ID: " + transport.idedit_poezd;
            //ДЕЛАЕМ ЗАПРОС К БД
            MySqlConnection conn = new MySqlConnection(Form1.connStr);
            // устанавливаем соединение с БД
            conn.Open();
            // запрос
            string auth = "SELECT * FROM Poezd WHERE ID_Poezda = '" + transport.idedit_poezd + "'";

            try
            {
                MySqlCommand commandauth = new MySqlCommand(auth, conn);

                // string name = commandauth.ExecuteScalar().ToString();
                MySqlDataReader MyDataReader;
                MyDataReader = commandauth.ExecuteReader();

                while (MyDataReader.Read())
                {
                    textBox2.Text = MyDataReader.GetString(1);
                    textBox3.Text = MyDataReader.GetString(2);
                }
                MyDataReader.Close();
                // закрываем соединение с БД
                conn.Close();


            }
            catch
            {

            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Сохранить изменения?", "ЖД Вокзал", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                //начинаем запрос
                try
                {
                    MySqlConnection conn = new MySqlConnection(Form1.connStr);
                    // устанавливаем соединение с БД
                    conn.Open();
                    string add = "UPDATE Poezd SET " +
                        "Kolichestvo_vagonov = '" + textBox2.Text + "', " +
                        "Kolichestvo_mest = '" + textBox3.Text + "' WHERE " +
                        "ID_Poezda = '" + transport.idedit_poezd + "'";
                    MySqlCommand adda = new MySqlCommand(add, conn);
                    //   MySqlCommand insrt = new MySqlCommand(insert, conn);
                    MySqlDataReader MyDataReader;
                    MyDataReader = adda.ExecuteReader();

                    while (MyDataReader.Read())
                    {
                    }
                    MyDataReader.Close();
                    conn.Close();
                    MessageBox.Show("Поезд отредактирован!", "ЖД Вокзал", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Close();
                }
                catch
                {
                    MessageBox.Show("Ошибка редактирования!", "ЖД Вокзал", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
