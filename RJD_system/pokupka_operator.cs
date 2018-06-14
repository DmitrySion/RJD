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
    public partial class pokupka_operator : Form
    {
        public pokupka_operator()
        {
            InitializeComponent();
        }
        public static string id_clienta, id_sotrudnica, data_vidachi, usluga;
        public static string id_reisa, nazvanie, otprv, pribit, num_otprv, pereod;

        private void button1_Click(object sender, EventArgs e)
        {
            string dat1time = Convert.ToString(dateTimePicker1.Value).Remove(10);

            string[] dat2time = dat1time.Split(new char[] { '.' });
            data_rojdenia = dat2time[2] + "-" + dat2time[1] + "-" + dat2time[0];
            //ДОБАВЛЯЕМ ПОЛЬЗОВАТЕЛЯ В БАЗУ
            try
            {
                MySqlConnection conn = new MySqlConnection(Form1.connStr);
                // устанавливаем соединение с БД
                conn.Open();
                string add = "INSERT INTO Client SET " +
                    "ID_Clienta = '" + id_clienta + "', " +
                    "Familia = '" + textBox2.Text + "', " +
                    "Imya = '" + textBox1.Text + "', " +
                    "Ochestvo = '" + textBox6.Text + "', " +
                    "Pasport = '" + textBox5.Text + "', " +
                    "Data_Rojdenia = '" + data_rojdenia + "'";
                MySqlCommand adda = new MySqlCommand(add, conn);

                MySqlDataReader MyDataReader;
                MyDataReader = adda.ExecuteReader();

                while (MyDataReader.Read())
                {
                }
                MyDataReader.Close();
                conn.Close();

                // MessageBox.Show("Новый рейс успешно добавлен!", "ЖД Вокзал", MessageBoxButtons.OK, MessageBoxIcon.Information);
                // Close();
            }
            catch
            {
                MessageBox.Show("Ошибка добавления пользователя в базу данных!", "ЖД Вокзал", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            //////////////////////////////////////////////////////////////////////////////////////////////////
            ////ЗАПОЛНЯЕМ TALON_BILET///////////////////////////////////////////////////////////////////////
            try
            {
                string[] dates_new = Convert.ToString(System.DateTime.Now.Date).Split(new char[] { '.' });
                string seconds = DateTime.Now.Second.ToString();
                string seconds_upper;
                if (seconds.Length == 1)
                {
                    seconds_upper = "0" + seconds;
                }
                else
                {
                    seconds_upper = seconds;
                }
                string dates_new_single = dates_new[2].Remove(4) + "-" + dates_new[1] + "-" + dates_new[0];
                string data = dates_new_single + " " + label3.Text + ":" + seconds_upper;





                string dat1time1 = Convert.ToString(vremya_otprv).Remove(10);

                string[] otp = dat1time1.Split(new char[] { '.' });
                vremya_otprv = otp[2] + "-" + otp[1] + "-" + otp[0];


                string dat1time2 = Convert.ToString(vremya_pribit).Remove(10);

                string[] pr = dat1time2.Split(new char[] { '.' });
                vremya_pribit = pr[2] + "-" + pr[1] + "-" + pr[0];
                MySqlConnection conn = new MySqlConnection(Form1.connStr);
                // устанавливаем соединение с БД
                conn.Open();
                string add = "INSERT INTO Talon_Bilet SET " +
                    "ID_Talona = '" + uslugi_operator.id_pokupka + "', " +
                    "ID_Bileta = '" + id_bileta + "', " +
                    "ID_Clienta = '" + id_clienta + "', " +
                    "ID_Sotrudnica = '" + id_sotrudnica + "', " +
                    "ID_Reisa = '" + id_reisa + "', " +
                    "Vremya_Data_Vidachi = '" + data + "', " +
                    "Usluga = '" + usluga + "', " +
                    "Vremya_Data_Otpravlenia = '" + vremya_otprv + "', " +
                    "Vremya_Data_Pribitia = '" + vremya_pribit + "', " +
                    "Stoimost_Bileta = '" + stoimost + "', " +
                    "Nomer_Mesta = '" + nomer_mesta + "', " +
                    "Tip_Mesta = '" + tip_mesta + "'";
                MySqlCommand adda = new MySqlCommand(add, conn);

                MySqlDataReader MyDataReader;
                MyDataReader = adda.ExecuteReader();

                while (MyDataReader.Read())
                {
                }
                MyDataReader.Close();
                conn.Close();


                // Close();
            }
            catch
            {
                MessageBox.Show("Ошибка добавления билета в базу данных!", "ЖД Вокзал", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            //////////////////////////////////////////////////////////////////
            ////УДАЛЯЕМ ТАЛОН/////////////////////////////////
            ////////////////////////////////////////////////////////////////////
            try
            {
                MySqlConnection conn = new MySqlConnection(Form1.connStr);
                // устанавливаем соединение с БД
                conn.Open();
                string add = "DELETE FROM Talon WHERE ID_Talona = '" + uslugi_operator.id_pokupka + "'";
                MySqlCommand adda = new MySqlCommand(add, conn);
                //   MySqlCommand insrt = new MySqlCommand(insert, conn);
                MySqlDataReader MyDataReader;
                MyDataReader = adda.ExecuteReader();

                while (MyDataReader.Read())
                {
                }
                MyDataReader.Close();
                conn.Close();
                MessageBox.Show("Оплата успешно принята!", "ЖД Вокзал", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Close();
            }
            catch
            {
                MessageBox.Show("Ошибка удаления использованного талона!", "ЖД Вокзал", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }

        public static string id_clienta1, familia, imya, ochestvo, data_rojdenia;
        public static string id_bileta, id_reisa2, vremya_otprv, vremya_pribit, stoimost, nomer_mesta, tip_mesta;
        private void pokupka_operator_Load(object sender, EventArgs e)
        {
            label22.Text = "№ заявки: " + uslugi_operator.id_pokupka;
            //ДЕЛАЕМ ЗАПРОС К БД


            try
            {
                MySqlConnection conn = new MySqlConnection(Form1.connStr);
                // устанавливаем соединение с БД
                conn.Open();
                // запрос
                string auth = "SELECT * FROM Talon WHERE ID_Talona = '" + uslugi_operator.id_pokupka + "'";
                MySqlCommand commandauth = new MySqlCommand(auth, conn);

                // string name = commandauth.ExecuteScalar().ToString();
                MySqlDataReader MyDataReader;
                MyDataReader = commandauth.ExecuteReader();

                while (MyDataReader.Read())
                {


                    id_clienta = MyDataReader.GetString(1);
                    id_sotrudnica = MyDataReader.GetString(2);
                    data_vidachi = MyDataReader.GetString(3);
                    usluga = MyDataReader.GetString(4);
                    id_reisa = usluga.Remove(0, 34);
                    id_bileta = usluga.Remove(0, 19).Remove(6);
                }
                MyDataReader.Close();
                // закрываем соединение с БД
                conn.Close();

                //  MessageBox.Show(id_bileta, "");

            }
            catch
            {
                MessageBox.Show("Ошибка получения данных о талоне!", "ЖД Вокзал", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            try
            {
                MySqlConnection conn = new MySqlConnection(Form1.connStr);
                // устанавливаем соединение с БД
                conn.Open();
                // запрос
                string auth = "SELECT * FROM Reis WHERE ID_Reisa = '" + id_reisa + "'";
                MySqlCommand commandauth = new MySqlCommand(auth, conn);

                // string name = commandauth.ExecuteScalar().ToString();
                MySqlDataReader MyDataReader;
                MyDataReader = commandauth.ExecuteReader();

                while (MyDataReader.Read())
                {


                    nazvanie = MyDataReader.GetString(1);
                    otprv = MyDataReader.GetString(2);
                    pribit = MyDataReader.GetString(3);
                    num_otprv = MyDataReader.GetString(4);
                    pereod = MyDataReader.GetString(5);

                }
                MyDataReader.Close();
                // закрываем соединение с БД
                conn.Close();


            }
            catch
            {
                MessageBox.Show("Ошибка получения данных о рейсе!", "ЖД Вокзал", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            try
            {
                MySqlConnection conn = new MySqlConnection(Form1.connStr);
                // устанавливаем соединение с БД
                conn.Open();
                // запрос
                string auth = "SELECT * FROM Bilet WHERE ID_Bileta = '" + id_bileta + "'";
                MySqlCommand commandauth = new MySqlCommand(auth, conn);

                // string name = commandauth.ExecuteScalar().ToString();
                MySqlDataReader MyDataReader;
                MyDataReader = commandauth.ExecuteReader();

                while (MyDataReader.Read())
                {


                    vremya_otprv = MyDataReader.GetString(2);
                    vremya_pribit = MyDataReader.GetString(3);
                    stoimost = MyDataReader.GetString(4);
                    nomer_mesta = MyDataReader.GetString(5);
                    tip_mesta = MyDataReader.GetString(6);

                }

                MyDataReader.Close();
                // закрываем соединение с БД
                conn.Close();


            }
            catch
            {
                MessageBox.Show("Ошибка получения данных о билете!", "ЖД Вокзал", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            /////////////////////////////////
            //ВЫВОДИМ ВСЮ ИНФОРМАЦИЮ
            /////////////////////////////////
            label1.Text = "№ рейса: " + id_reisa;
            label11.Text = "№ клиента: " + id_clienta;
            label12.Text = "№ сотрудника: " + Form1.id;
            label22.Text = "№ заявки: " + uslugi_operator.id_pokupka;
            label9.Text = "Время выдачи талона: " + data_vidachi;
            label10.Text = usluga;
            label14.Text = "Название рейса: " + nazvanie;
            label17.Text = "Переодичность: " + pereod;
            label13.Text = "Станция отправления: " + otprv;
            label16.Text = "Станция прибытия: " + pribit;
            label15.Text = "№ станции отправления: " + num_otprv;
            label18.Text = "Время отправления: " + vremya_otprv;
            label19.Text = "Время прибытия: " + vremya_pribit;
            label20.Text = "Место №: " + nomer_mesta;
            label21.Text = "Тип места: " + tip_mesta;
            label7.Text = stoimost + " р.";
        }
    }
}
