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
    public partial class transport : Form
    {
        public transport()
        {
            InitializeComponent();
        }
        private void refresh_poezda()
        {
            // создаём объект для подключения к БД
            MySqlConnection conn = new MySqlConnection(Form1.connStr);
            // устанавливаем соединение с БД
            conn.Open();

            //ЗАГРУЖАЕМ ТАБЛИЦУ ПОЛЬЗОВАТЕЛЕЙ
            DataSet ds1 = new DataSet();
            MySqlDataAdapter ad = new MySqlDataAdapter("Select * from Poezd", conn);// параметры- команда для выполнения + connection;
            ad.Fill(ds1, "Poezd");
            dataGridView2.AllowUserToAddRows = false;
            dataGridView2.DataSource = ds1.Tables[0];
            conn.Close();
            dataGridView2.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            //переименовываем столбцы
            dataGridView2.Columns[0].HeaderText = "ID";
            dataGridView2.Columns[1].HeaderText = "Кол-во вагонов";
            dataGridView2.Columns[2].HeaderText = "Кол-во мест";
            int index = dataGridView2.CurrentCell.RowIndex;
            dataGridView2.Rows[index].Selected = true;
        }
        private void refresh_reis()
        {
            // создаём объект для подключения к БД
            MySqlConnection conn = new MySqlConnection(Form1.connStr);
            // устанавливаем соединение с БД
            conn.Open();

            //ЗАГРУЖАЕМ ТАБЛИЦУ ПОЛЬЗОВАТЕЛЕЙ
            DataSet ds = new DataSet();
            MySqlDataAdapter ad = new MySqlDataAdapter("Select * from Reis", conn);// параметры- команда для выполнения + connection;
            ad.Fill(ds, "Reis");
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.DataSource = ds.Tables[0];
            conn.Close();
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            //переименовываем столбцы
            dataGridView1.Columns[0].HeaderText = "ID рейса";
            dataGridView1.Columns[1].HeaderText = "Название";
            dataGridView1.Columns[2].HeaderText = "Станция отправления";
            dataGridView1.Columns[3].HeaderText = "Станция прибытия";
            dataGridView1.Columns[4].HeaderText = "Номер станции отправления";
            dataGridView1.Columns[5].HeaderText = "Переодичность";
           

         
           
        }
        void refresh_all()
        {
            refresh_reis(); refresh_poezda();
        }
        private void transport_Load(object sender, EventArgs e)
        {
            refresh_reis();refresh_poezda();
        }

        private void TabSelect(object sender, TabControlCancelEventArgs e)
        {
           
        }

        private void datagrid1paint(object sender, PaintEventArgs e)
        {
            int index = dataGridView1.CurrentCell.RowIndex;
            dataGridView1.Rows[index].Selected = true;
        }

        private void PoezdaCellClick(object sender, DataGridViewCellEventArgs e)
        {
            int index = dataGridView2.CurrentCell.RowIndex;
            dataGridView2.Rows[index].Selected = true;
        }

        private void ReisCellClick(object sender, DataGridViewCellEventArgs e)
        {
            int index = dataGridView1.CurrentCell.RowIndex;
            dataGridView1.Rows[index].Selected = true;
        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            Form add_poezd = new add_poezd();
           // idadd = Convert.ToString(dataGridView1.Rows.Count + 1);
            add_poezd.ShowDialog();
            refresh_all();
        }
        public static string idedit_poezd;
        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            int index = dataGridView2.CurrentCell.RowIndex;
            idedit_poezd = Convert.ToString(dataGridView2.Rows[index].Cells[0].Value);
            Form edit_poezd = new edit_poezd();
            // idadd = Convert.ToString(dataGridView1.Rows.Count + 1);
            edit_poezd.ShowDialog();
            refresh_all();
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            int index = dataGridView2.CurrentCell.RowIndex;
            string ind = Convert.ToString(dataGridView2.Rows[index].Cells[0].Value);
            try
            {
                
                    if (MessageBox.Show("Удалить поезд?\nОтменить данное действие будет невозможно.", "ЖД Вокзал", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        MySqlConnection conn = new MySqlConnection(Form1.connStr);
                        // устанавливаем соединение с БД
                        conn.Open();
                        string add = "DELETE FROM Poezd WHERE ID_Poezda = '" + ind + "'";
                        MySqlCommand adda = new MySqlCommand(add, conn);

                        MySqlDataReader MyDataReader;
                        MyDataReader = adda.ExecuteReader();

                        while (MyDataReader.Read())
                        {
                        }
                        MyDataReader.Close();
                        conn.Close();
                        MessageBox.Show("Поезд удален!", "ЖД Вокзал", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        refresh_all();
                        // Close();
                    }
                    else
                    {
                        MessageBox.Show("Удаление отменено!", "ЖД Вокзал", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }

               
            }
            catch
            {
                MessageBox.Show("Ошибка удаления!", "ЖД Вокзал", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void toolStripButton6_Click(object sender, EventArgs e)
        {

        }

        private void toolStripButton4_Click(object sender, EventArgs e)
        {
            Form add_reis = new add_reis();
            // idadd = Convert.ToString(dataGridView1.Rows.Count + 1);
            add_reis.ShowDialog();
            refresh_all();
        }

        private void toolStripButton5_Click(object sender, EventArgs e)
        {

        }
    }
}
