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
    public partial class add_user : Form
    {
        public add_user()
        {
            InitializeComponent();
        }
        public static int rnd_value;
        public static string id_addusr;
        public static string name_addusr;
        public static string surname_addusr;
        public static string otchestvo_addusr;
        public static string phones_addusr;
        public static string login_addusr;
        public static string password_addusr;
        public static string role_addusr;

        private void button3_Click(object sender, EventArgs e)
        {
            Random rnd = new Random();
            rnd_value = rnd.Next(100001, 999998);
            label1.Text = "ID: " + rnd_value.ToString();
            id_addusr = rnd_value.ToString();
        }

        private void add_user_Load(object sender, EventArgs e)
        {
            button3_Click(null, null);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text != "" && textBox2.Text != "" && textBox3.Text != "" && textBox4.Text != "" && textBox5.Text != "" && textBox6.Text != "" && comboBox1.Text != "")
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
                id_addusr = rnd_value.ToString();
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
                    string add = "INSERT INTO Sotrudnic SET " +
                        "ID_Sotrudnica = '" + id_addusr + "', " +
                        "Familia = '" + surname_addusr + "', " +
                        "Imya = '" + name_addusr + "', " +
                        "Ochestvo = '" + otchestvo_addusr + "', " +
                        "Telefon = '" + phones_addusr + "', " +
                        "Login = '" + login_addusr + "', " +
                        "Password = '" + password_addusr + "', " +
                        "RoleID = '" + role_addusr + "' ";
                    MySqlCommand adda = new MySqlCommand(add, conn);
                    //   MySqlCommand insrt = new MySqlCommand(insert, conn);
                    MySqlDataReader MyDataReader;
                    MyDataReader = adda.ExecuteReader();

                    while (MyDataReader.Read())
                    {
                    }
                    MyDataReader.Close();
                    conn.Close();
                    MessageBox.Show("Новый сотрудник успешно добавлен!", "ЖД Вокзал", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Close();
                }
                catch
                {
                    MessageBox.Show("Ошибка добавления!", "ЖД Вокзал", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Не все поля заполнены!", "ЖД Вокзал", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void Clickaaas(object sender, EventArgs e)
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
        private readonly Random random = new Random();
        public string GetPass(int x)
        {
            string pass = "";
            var r = new Random();
            while (pass.Length < x)
            {
                Char c = (char)r.Next(33, 125);
                if (Char.IsLetterOrDigit(c))
                    pass += c;
            }
            return pass;
        }
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.SelectedIndex == 0)
            {

                textBox4.Enabled = true; textBox4.Text = "";
                textBox3.Enabled = true; textBox3.Text = "";
                label9.Text = "Чтобы показать пароль кликните в поле";
            }
            if (comboBox1.SelectedIndex == 1)
            {

                textBox4.Enabled = true; textBox4.Text = "";
                textBox3.Enabled = true; textBox3.Text = "";
                label9.Text = "Чтобы показать пароль кликните в поле";
            }
            if (comboBox1.SelectedIndex == 2)
            {

                textBox4.Enabled = false; textBox4.Text = "";
                textBox3.Enabled = false; textBox3.Text = GetPass(10);
                label9.Text = "Для персонала поезда авторизация недоступна";
            }
        }
    }
}
