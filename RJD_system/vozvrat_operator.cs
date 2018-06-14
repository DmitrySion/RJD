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
    public partial class vozvrat_operator : Form
    {
        public vozvrat_operator()
        {
            InitializeComponent();
        }
        public static string id_clienta, id_sotrudnica, data_vidachi, usluga;
        public static string id_reisa, nazvanie, otprv, pribit, num_otprv, pereod;

        private void button1_Click(object sender, EventArgs e)
        {
            /////////////////////////////////////////////
            ////ПРОИЗВОДИМ ВОЗВРАТ БИЛЕТА////
            /////////////////////////////////////////////





            //////////////////////////////////////////////////////////////////
            ////УДАЛЯЕМ БИЛЕТ ИЗ TALONBILET/////////////////////////////////
            ////////////////////////////////////////////////////////////////////
            try
            {
                MySqlConnection conn = new MySqlConnection(Form1.connStr);
                // устанавливаем соединение с БД
                conn.Open();
                string add = "DELETE FROM Talon_Bilet WHERE ID_Bileta = '" + id_bileta + "'";
                MySqlCommand adda = new MySqlCommand(add, conn);
                //   MySqlCommand insrt = new MySqlCommand(insert, conn);
                MySqlDataReader MyDataReader;
                MyDataReader = adda.ExecuteReader();

                while (MyDataReader.Read())
                {
                }
                MyDataReader.Close();
                conn.Close();

                Close();
            }
            catch
            {
                MessageBox.Show("Ошибка возврата билета в продажу!", "ЖД Вокзал", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                MessageBox.Show("Возврат успешно произведен!", "ЖД Вокзал", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
        private void vozvrat_operator_Load(object sender, EventArgs e)
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
            //ВЫВОД ИНФО О КЛИЕНТЕ
            try
            {
                MySqlConnection conn = new MySqlConnection(Form1.connStr);
                // устанавливаем соединение с БД
                conn.Open();
                // запрос
                string auth = "SELECT * FROM Client WHERE ID_Clienta = '" + id_clienta + "'";
                MySqlCommand commandauth = new MySqlCommand(auth, conn);

                // string name = commandauth.ExecuteScalar().ToString();
                MySqlDataReader MyDataReader;
                MyDataReader = commandauth.ExecuteReader();

                while (MyDataReader.Read())
                {


                    textBox2.Text = MyDataReader.GetString(1);
                    textBox1.Text = MyDataReader.GetString(2);
                    textBox6.Text = MyDataReader.GetString(3);
                    textBox5.Text = MyDataReader.GetString(4);
                    // textBox5.Text = MyDataReader.GetString(4); ///ДАТА РОЖДЕНИЯ НЕ ЗАБЫТЬ

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
