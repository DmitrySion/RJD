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
    public partial class add_reis : Form
    {
        public add_reis()
        {
            InitializeComponent();
        }
        public static int rnd_value;
        public static string id_reisa;
        public static string name_reis;
        public static string otprv;
        public static string pribit;
        public static string num_otprv;
        public static string pereodichnost;
        public static string id_poezda;

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text != "" && textBox2.Text != "" && textBox3.Text != "" && textBox6.Text != "" && comboBox1.Text != "")
            {


                id_reisa = rnd_value.ToString();

                string[] idpomas = new string[5];


                if (comboBox1.SelectedItem.ToString() != "")
                {

                    idpomas = comboBox1.SelectedItem.ToString().Split('№');
                    id_poezda = idpomas[1];
                }
                name_reis = textBox2.Text;
                otprv = textBox1.Text;
                num_otprv = textBox3.Text;
                pribit = textBox6.Text;
                pereodichnost = textBox5.Text.Split(' ')[1];
                //начинаем запрос
                try
                {
                    MySqlConnection conn = new MySqlConnection(Form1.connStr);
                    // устанавливаем соединение с БД
                    conn.Open();
                    string add = "INSERT INTO Reis SET " +
                        "ID_Reisa = '" + id_reisa + "', " +
                        "Nazvanie = '" + name_reis + "', " +
                        "Stancia_Otpravlenia = '" + otprv + "', " +
                        "Stancia_Pribitia = '" + pribit + "', " +
                        "Nomer_Stancii_Otpravlenia = '" + num_otprv + "', " +
                        "Pereodichnost = '" + pereodichnost + "'";
                    MySqlCommand adda = new MySqlCommand(add, conn);
                    //   MySqlCommand insrt = new MySqlCommand(insert, conn);
                    MySqlDataReader MyDataReader;
                    MyDataReader = adda.ExecuteReader();

                    while (MyDataReader.Read())
                    {
                    }
                    MyDataReader.Close();
                    conn.Close();
                    //добавляю еще в одну таблицу
                    // MessageBox.Show("Новый рейс успешно добавлен!", "ЖД Вокзал", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    // Close();
                }
                catch
                {
                    //MessageBox.Show("Ошибка добавления!", "ЖД Вокзал", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                try
                {
                    MySqlConnection conn = new MySqlConnection(Form1.connStr);
                    // устанавливаем соединение с БД
                    conn.Open();
                    string add = "INSERT INTO Poezd_Reis SET " +
                        "ID_Poezda = '" + id_poezda + "', " +
                        "ID_Reisa = '" + id_reisa + "'";
                    // MessageBox.Show(id_poezda + " " + id_reisa, "ЖД Вокзал", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    MySqlCommand adda = new MySqlCommand(add, conn);
                    //   MySqlCommand insrt = new MySqlCommand(insert, conn);
                    MySqlDataReader MyDataReader;
                    MyDataReader = adda.ExecuteReader();

                    while (MyDataReader.Read())
                    {
                    }
                    MyDataReader.Close();
                    conn.Close();
                    //добавляю еще в одну таблицу
                    MessageBox.Show("Новый рейс успешно добавлен!", "ЖД Вокзал", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ошибка добавления!\n" + ex, "ЖД Вокзал", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Не все поля заполнены!", "ЖД Вокзал", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Random rnd = new Random();
            rnd_value = rnd.Next(100001, 999998);
            label1.Text = "ID: " + rnd_value.ToString();
            id_reisa = rnd_value.ToString();
        }

        private void add_reis_Load(object sender, EventArgs e)
        {
            pereod = 0;
            Random rnd = new Random();
            rnd_value = rnd.Next(100001, 999998);
            label1.Text = "ID: " + rnd_value.ToString();
            id_reisa = rnd_value.ToString();
            //////////////ЗДЕСЬ ПОДГРУЖАЕМ  ПОЕЗДА В КОМБОБОКСЫ

            try
            {
                // создаём объект для подключения к БД
                MySqlConnection conn = new MySqlConnection(Form1.connStr);
                // устанавливаем соединение с БД
                conn.Open();

                //ЗАГРУЖАЕМ ТАБЛИЦУ ПОЕЗДОВ
                DataSet ds = new DataSet();
                MySqlDataAdapter ad = new MySqlDataAdapter("SELECT " +
                    "ID_Poezda, " +
                    "Kolichestvo_vagonov, " +
                    "Kolichestvo_mest " +
                    "FROM " +
                    "Poezd WHERE " +
                    "ID_Poezda " +
                    "NOT IN(SELECT ID_Poezda FROM Poezd_Reis)", conn);// параметры- команда для выполнения + connection;
                ad.Fill(ds, "poezda");
                dataGridView1.AllowUserToAddRows = false;
                dataGridView1.DataSource = ds.Tables[0];
                conn.Close();
                dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
                for (int i = 0; i < dataGridView1.Rows.Count; i++)
                {
                    //добавляем в комбобокс список пользователей
                    comboBox1.Items.Add("Поезд №" + Convert.ToString(dataGridView1.Rows[i].Cells[0].Value));
                }
            }
            catch
            {
                MessageBox.Show("Ошибка получения списка поездов", "ЖД Вокзал", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }



        }
        public static string kolvovagonov, kolvomest;
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

            string id_po = "";
            string[] idpomas = new string[5];


            if (comboBox1.SelectedItem.ToString() != "")
            {

                idpomas = comboBox1.SelectedItem.ToString().Split('№');
                id_po = idpomas[1];
            }
            try
            {

                MySqlConnection conn = new MySqlConnection(Form1.connStr);
                // устанавливаем соединение с БД
                conn.Open();//
                string add = "SELECT * FROM Poezd WHERE ID_Poezda = '" + id_po + "'";

                MySqlCommand adda = new MySqlCommand(add, conn);
                //   MySqlCommand insrt = new MySqlCommand(insert, conn);
                MySqlDataReader MyDataReader;
                MyDataReader = adda.ExecuteReader();

                while (MyDataReader.Read())
                {

                    kolvovagonov = MyDataReader.GetString(1);
                    kolvomest = MyDataReader.GetString(2);



                }
                // MessageBox.Show(id_po, "ЖД Вокзал", MessageBoxButtons.OK, MessageBoxIcon.Information);
                MyDataReader.Close();
                conn.Close();
                label10.Text = "Вагонов: " + kolvovagonov + "         Мест: " + kolvomest + "";

                //  MessageBox.Show("Задание успешно добавлено!", "JobReciever", MessageBoxButtons.OK, MessageBoxIcon.Information);
                //  Close();
            }
            catch (Exception msg)
            {
                MessageBox.Show("Ошибка загрузки списка!\n" + msg, "ЖД Вокзал", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        public static int pereod = 1;
        private void button4_Click(object sender, EventArgs e)
        {
            pereod++;
            if (pereod.ToString().Length == 1)
            {
                if (pereod == 1)
                {
                    textBox5.Text = "Каждый " + pereod + " час";
                }
                if (pereod == 2 || pereod == 3 || pereod == 4)
                {
                    textBox5.Text = "Каждые " + pereod + " часа";
                }
                if (pereod == 5 || pereod == 6 || pereod == 7 ||
                    pereod == 8 || pereod == 9 || pereod == 10 ||
                    pereod == 11 || pereod == 12 || pereod == 13 ||
                    pereod == 14 || pereod == 15 || pereod == 16 ||
                    pereod == 17 || pereod == 18 || pereod == 19 || pereod == 20)
                {
                    textBox5.Text = "Каждые " + pereod + " часов";
                }
            }

            if (pereod.ToString().Length == 2)
            {
                if (pereod < 20)
                {
                    if (pereod == 5 || pereod == 6 || pereod == 7 ||
                      pereod == 8 || pereod == 9 || pereod == 10 ||
                      pereod == 11 || pereod == 12 || pereod == 13 ||
                      pereod == 14 || pereod == 15 || pereod == 16 ||
                      pereod == 17 || pereod == 18 || pereod == 19 || pereod == 20)
                    {
                        textBox5.Text = "Каждые " + pereod + " часов";
                    }
                }
                if (pereod > 20)
                {
                    if (pereod.ToString().Remove(0, 1) == "1")
                    {
                        textBox5.Text = "Каждый " + pereod + " час";
                    }
                    if (pereod.ToString().Remove(0, 1) == "2" || pereod.ToString().Remove(0, 1) == "3" || pereod.ToString().Remove(0, 1) == "4")
                    {
                        textBox5.Text = "Каждые " + pereod + " часа";
                    }
                    if (pereod.ToString().Remove(0, 1) == "5" || pereod.ToString().Remove(0, 1) == "6" || pereod.ToString().Remove(0, 1) == "7" ||
                        pereod.ToString().Remove(0, 1) == "8" || pereod.ToString().Remove(0, 1) == "9" || pereod.ToString().Remove(0, 1) == "0")
                    {
                        textBox5.Text = "Каждые " + pereod + " часов";
                    }
                }
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            pereod--;
            if (pereod.ToString().Length == 1)
            {
                if (pereod == 1)
                {
                    textBox5.Text = "Каждый " + pereod + " час";
                }
                if (pereod == 2 || pereod == 3 || pereod == 4)
                {
                    textBox5.Text = "Каждые " + pereod + " часа";
                }
                if (pereod == 5 || pereod == 6 || pereod == 7 ||
                    pereod == 8 || pereod == 9 || pereod == 10 ||
                    pereod == 11 || pereod == 12 || pereod == 13 ||
                    pereod == 14 || pereod == 15 || pereod == 16 ||
                    pereod == 17 || pereod == 18 || pereod == 19 || pereod == 20)
                {
                    textBox5.Text = "Каждые " + pereod + " часов";
                }
            }

            if (pereod.ToString().Length == 2)
            {
                if (pereod < 20)
                {
                    if (pereod == 5 || pereod == 6 || pereod == 7 ||
                      pereod == 8 || pereod == 9 || pereod == 10 ||
                      pereod == 11 || pereod == 12 || pereod == 13 ||
                      pereod == 14 || pereod == 15 || pereod == 16 ||
                      pereod == 17 || pereod == 18 || pereod == 19 || pereod == 20)
                    {
                        textBox5.Text = "Каждые " + pereod + " часов";
                    }
                }
                if (pereod > 20)
                {
                    if (pereod.ToString().Remove(0, 1) == "1")
                    {
                        textBox5.Text = "Каждый " + pereod + " час";
                    }
                    if (pereod.ToString().Remove(0, 1) == "2" || pereod.ToString().Remove(0, 1) == "3" || pereod.ToString().Remove(0, 1) == "4")
                    {
                        textBox5.Text = "Каждые " + pereod + " часа";
                    }
                    if (pereod.ToString().Remove(0, 1) == "5" || pereod.ToString().Remove(0, 1) == "6" || pereod.ToString().Remove(0, 1) == "7" ||
                        pereod.ToString().Remove(0, 1) == "8" || pereod.ToString().Remove(0, 1) == "9" || pereod.ToString().Remove(0, 1) == "0")
                    {
                        textBox5.Text = "Каждые " + pereod + " часов";
                    }
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }

        public static string id_usr, name_usr, surname_usr, otchestvo_usr, phone_usr, dolzhnost;

    }
}
