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
    public partial class sotrudniki : Form
    {
        public sotrudniki()
        {
            InitializeComponent();
        }
        private void refreshing()
        {
            // создаём объект для подключения к БД
            MySqlConnection conn = new MySqlConnection(Form1.connStr);
            // устанавливаем соединение с БД
            conn.Open();

            //ЗАГРУЖАЕМ ТАБЛИЦУ ПОЛЬЗОВАТЕЛЕЙ
            DataSet ds = new DataSet();
            MySqlDataAdapter ad = new MySqlDataAdapter("Select * from Sotrudnic", conn);// параметры- команда для выполнения + connection;
            ad.Fill(ds, "Sotrudnic");
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.DataSource = ds.Tables[0];
            conn.Close();
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            //переименовываем столбцы
            dataGridView1.Columns[0].HeaderText = "ID";
            dataGridView1.Columns[1].HeaderText = "Имя";
            dataGridView1.Columns[2].HeaderText = "Фамилия";
            dataGridView1.Columns[3].HeaderText = "Отчество";
            dataGridView1.Columns[4].HeaderText = "Телефон";
            dataGridView1.Columns[5].HeaderText = "Логин";
            dataGridView1.Columns[7].HeaderText = "Роль в системе";

            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                //РОЛИ В СИСТЕМЕ
                if (Convert.ToString(dataGridView1.Rows[i].Cells[7].Value) == "0")
                {

                    dataGridView1.Rows[i].Cells[7].Value = "Администратор";
                }
                if (Convert.ToString(dataGridView1.Rows[i].Cells[7].Value) == "1")
                {

                    dataGridView1.Rows[i].Cells[7].Value = "Оператор";
                }
                if (Convert.ToString(dataGridView1.Rows[i].Cells[7].Value) == "2")
                {

                    dataGridView1.Rows[i].Cells[7].Value = "Персонал поезда";
                }

            }
            dataGridView1.Columns.RemoveAt(6); //удаляем хэш паролей
            int index = dataGridView1.CurrentCell.RowIndex;
            dataGridView1.Rows[index].Selected = true;
        }
        private void sotrudniki_Load(object sender, EventArgs e)
        {
            refreshing();
        }
        public static string idadd;
        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            Form add_user = new add_user();
            idadd = Convert.ToString(dataGridView1.Rows.Count + 1);
            add_user.ShowDialog();
            refreshing();
        }
        public static string idedit;
        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            int index = dataGridView1.CurrentCell.RowIndex;
            idedit = Convert.ToString(dataGridView1.Rows[index].Cells[0].Value);
            Form edit_usr = new edit_usr();
            edit_usr.ShowDialog();
            refreshing();
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            int index = dataGridView1.CurrentCell.RowIndex;
            string ind = Convert.ToString(dataGridView1.Rows[index].Cells[0].Value);
            try
            {
                if (ind != Form1.id.ToString())
                {
                    if (MessageBox.Show("Удалить сотрудника?\nОтменить данное действие будет невозможно.", "ЖД Вокзал", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        MySqlConnection conn = new MySqlConnection(Form1.connStr);
                        // устанавливаем соединение с БД
                        conn.Open();
                        string add = "DELETE FROM Sotrudnic WHERE ID_Sotrudnica = '" + ind + "'";
                        MySqlCommand adda = new MySqlCommand(add, conn);

                        MySqlDataReader MyDataReader;
                        MyDataReader = adda.ExecuteReader();

                        while (MyDataReader.Read())
                        {
                        }
                        MyDataReader.Close();
                        conn.Close();
                        MessageBox.Show("Сотрудник удален!", "ЖД Вокзал", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        refreshing();
                        // Close();
                    }
                    else
                    {
                        MessageBox.Show("Удаление отменено!", "ЖД Вокзал", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }

                }
                else
                {
                    MessageBox.Show("Нельзя удалить самого себя!", "ЖД Вокзал", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch
            {
                MessageBox.Show("Ошибка удаления!", "ЖД Вокзал", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void SotrudnikiCellClick(object sender, DataGridViewCellEventArgs e)
        {
            int index = dataGridView1.CurrentCell.RowIndex;
            dataGridView1.Rows[index].Selected = true;
        }
    }
}
