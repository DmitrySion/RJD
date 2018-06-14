using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Windows.Forms;

namespace RJD_system
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        public static string login;
        public static string password;
        public static int roleid;   //роль 0 - администратор 1 - оператор
        public static string name;
        public static string surname;
        public static string otchestvo;
        public static int id;
        public static string phone;


        public static string connStr = "server=localhost;user=root;database=JDVokzal;password=;SslMode=none";  // строка подключения к БД
        private void Form1_Load(object sender, EventArgs e)
        {
            //roleid = 0;
            comboBox1.SelectedIndex = 0;
            login = "";
            password = "";
            roleid = 0;   //роль 0 - администратор 1 - оператор
            name = "";
            surname = "";
            otchestvo = "";
            id = 0;
            phone = "";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                // создаём объект для подключения к БД
                MySqlConnection conn = new MySqlConnection(connStr);
                // устанавливаем соединение с БД
                conn.Open();
                // запрос
                string auth = "SELECT * FROM Sotrudnic WHERE Login = ''";
                if (comboBox1.SelectedIndex == 0)
                {
                    auth = "SELECT * FROM Sotrudnic WHERE Login = '" + textBox1.Text + "' AND RoleID = '0'";
                }
                if (comboBox1.SelectedIndex == 1)
                {
                    auth = "SELECT * FROM Sotrudnic WHERE Login = '" + textBox1.Text + "' AND RoleID = '1'";
                }
                try
                {
                    MySqlCommand commandauth = new MySqlCommand(auth, conn);


                    MySqlDataReader MyDataReader;
                    MyDataReader = commandauth.ExecuteReader();

                    while (MyDataReader.Read())
                    {

                        id = MyDataReader.GetInt32(0);
                        name = MyDataReader.GetString(1);
                        surname = MyDataReader.GetString(2);
                        otchestvo = MyDataReader.GetString(3);
                        login = MyDataReader.GetString(5);
                        password = MyDataReader.GetString(6);
                        phone = MyDataReader.GetString(4);
                        roleid = MyDataReader.GetInt32(7);
                    }
                    MyDataReader.Close();
                    // закрываем соединение с БД
                    conn.Close();
                    if (id == 0)
                    {
                        MessageBox.Show("Пользователь не найден", "ЖД Вокзал", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else
                    {
                        string notshapass = textBox2.Text;
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
                        shapass = result;

                        if (shapass == password)
                        {


                            if (roleid == 0)
                            {
                                //Админ
                                Form adminka = new adminka();
                                this.Visible = false;
                                adminka.ShowDialog();
                                this.Visible = true;
                                Application.Exit();
                            }
                            if (roleid == 1)
                            {
                                //Оператор
                                Form operator_form = new operator_form();
                                this.Visible = false;
                                operator_form.ShowDialog();
                                this.Visible = true;
                                Application.Exit();
                            }


                        }
                        else
                        {
                            MessageBox.Show("Пароль введен неверно", "ЖД Вокзал", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
                catch (Exception ex)
                {

                    MessageBox.Show("Ошибка работы с базой данных" + ex, "ЖД Вокзал", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch
            {
                MessageBox.Show("Ошибка подключения к базе данных!", "ЖД Вокзал", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
