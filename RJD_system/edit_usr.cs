using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RJD_system
{
    public partial class edit_usr : Form
    {
        public edit_usr()
        {
            InitializeComponent();
        }
        public static string id_addusr;
        public static string name_addusr;
        public static string surname_addusr;
        public static string otchestvo_addusr;
        public static string phones_addusr;
        public static string login_addusr;
        public static string password_addusr;
        public static string role_addusr;
        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(MessageBox.Show("Сохранить изменения?", "ЖД Вокзал", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {

                string notshapass = textBox4.Text;
                string shapass = "";
                //ПОЛУЧАЕМ ХЭШ ПАРОЛЯ
                byte[] hash = Encoding.ASCII.GetBytes(notshapass);
                MD5 md5 = new MD5CryptoServiceProvider();
                byte[] hashenc = md5.ComputeHash(hash);
                string result = "";
                foreach (var b in hashenc)
                {
                    result += b.ToString("x2");
                }

                shapass = result; //полученный хэш пароля
                                  //инициализация переменных
                //id_addusr = rnd_value.ToString();
                name_addusr = textBox1.Text;
                surname_addusr = textBox2.Text;
                otchestvo_addusr = textBox6.Text;
                phones_addusr = textBox5.Text;
                login_addusr = textBox3.Text;
                password_addusr = shapass;
                role_addusr = comboBox1.SelectedIndex.ToString();

                //начинаем запрос
                try
                {
                    MySqlConnection conn = new MySqlConnection(Form1.connStr);
                    // устанавливаем соединение с БД
                    conn.Open();
                    string add = "UPDATE Sotrudnic SET " +
                        "Familia = '" + surname_addusr + "', " +
                        "Imya = '" + name_addusr + "', " +
                        "Ochestvo = '" + otchestvo_addusr + "', " +
                        "Telefon = '" + phones_addusr + "', " +
                        "Login = '" + login_addusr + "', " +
                        "Password = '" + password_addusr + "', " +
                        "RoleID = '" + role_addusr + "' WHERE " +
                        "ID_Sotrudnica = '" + sotrudniki.idedit + "'";
                    MySqlCommand adda = new MySqlCommand(add, conn);
                    //   MySqlCommand insrt = new MySqlCommand(insert, conn);
                    MySqlDataReader MyDataReader;
                    MyDataReader = adda.ExecuteReader();

                    while (MyDataReader.Read())
                    {
                    }
                    MyDataReader.Close();
                    conn.Close();
                    MessageBox.Show("Сотрудник отредактирован!", "ЖД Вокзал", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Close();
                }
                catch
                {
                    MessageBox.Show("Ошибка редактирования!", "ЖД Вокзал", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void Clickass(object sender, EventArgs e)
        {
            if (textBox4.UseSystemPasswordChar == true)
            {
                textBox4.UseSystemPasswordChar = false;
            }
            else
            {
                textBox4.UseSystemPasswordChar = true;
            }
        }

        private void edit_usr_Load(object sender, EventArgs e)
        {
            label1.Text = "ID: " + sotrudniki.idedit;
            //ДЕЛАЕМ ЗАПРОС К БД
            MySqlConnection conn = new MySqlConnection(Form1.connStr);
            // устанавливаем соединение с БД
            conn.Open();
            // запрос
            string auth = "SELECT * FROM Sotrudnic WHERE ID_Sotrudnica = '" + sotrudniki.idedit + "'";

            try
            {
                MySqlCommand commandauth = new MySqlCommand(auth, conn);

                // string name = commandauth.ExecuteScalar().ToString();
                MySqlDataReader MyDataReader;
                MyDataReader = commandauth.ExecuteReader();

                while (MyDataReader.Read())
                {

                    //  id = MyDataReader.GetInt32(0);
                    surname_addusr = MyDataReader.GetString(1);
                    name_addusr = MyDataReader.GetString(2);
                    otchestvo_addusr = MyDataReader.GetString(3);
                    phones_addusr = MyDataReader.GetString(4);
                    login_addusr = MyDataReader.GetString(5);
                  //  podrzdid = MyDataReader.GetInt32(7);
                    role_addusr = MyDataReader.GetString(7);
                }
                MyDataReader.Close();
                // закрываем соединение с БД
                conn.Close();
                textBox2.Text = surname_addusr;
                textBox1.Text = name_addusr;
                textBox6.Text = otchestvo_addusr;
                textBox5.Text = phones_addusr;
                textBox3.Text = login_addusr;
                comboBox1.SelectedIndex = Convert.ToInt32(role_addusr);

            }
            catch
            {

            }
        }
    }
}
