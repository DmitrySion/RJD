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
    public partial class uslugi_operator : Form
    {
        public uslugi_operator()
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
            MySqlDataAdapter ad = new MySqlDataAdapter("Select * from Talon WHERE ID_Sotrudnica = '" + Form1.id + "'", conn);// параметры- команда для выполнения + connection;
            ad.Fill(ds, "Talon");
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.DataSource = ds.Tables[0];
            conn.Close();
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            //переименовываем столбцы
            dataGridView1.Columns[0].HeaderText = "№ талона";
            dataGridView1.Columns[1].HeaderText = "№ клиента";
            dataGridView1.Columns[2].HeaderText = "№ сотрудника";
            dataGridView1.Columns[3].HeaderText = "Время выдачи талона";
            dataGridView1.Columns[4].HeaderText = "Услуга";
            dataGridView1.Columns.RemoveAt(2); //удаляем № сотрудника
        }
        private void uslugi_operator_Load(object sender, EventArgs e)
        {
            refreshing();
        }
        public static string id_pokupka;
        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            int off = 0;
            int index = dataGridView1.CurrentCell.RowIndex;
            if (Convert.ToString(dataGridView1.Rows[index].Cells[3].Value).Contains("Покупка") == true)
            {
                Form pokupka_operator = new pokupka_operator();
                int index2 = dataGridView1.CurrentCell.RowIndex;
                id_pokupka = Convert.ToString(dataGridView1.Rows[index2].Cells[0].Value);
                pokupka_operator.ShowDialog();
                off = 1;
                refreshing();

            }
            if (off == 0)
            {
                if (Convert.ToString(dataGridView1.Rows[index].Cells[3].Value).Contains("Возврат") == true)
                {
                    Form vozvrat_operator = new vozvrat_operator();
                    int index2 = dataGridView1.CurrentCell.RowIndex;
                    id_pokupka = Convert.ToString(dataGridView1.Rows[index2].Cells[0].Value);
                    vozvrat_operator.ShowDialog();
                    off = 1;
                    refreshing();
                }
            }

        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            int index = dataGridView1.CurrentCell.RowIndex;
            string ind = Convert.ToString(dataGridView1.Rows[index].Cells[0].Value);
            try
            {

                if (MessageBox.Show("Удалить заявку?\nОтменить данное действие будет невозможно.", "ЖД Вокзал", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    MySqlConnection conn = new MySqlConnection(Form1.connStr);
                    // устанавливаем соединение с БД
                    conn.Open();
                    string add = "DELETE FROM Talon WHERE ID_Talona = '" + ind + "'";
                    MySqlCommand adda = new MySqlCommand(add, conn);

                    MySqlDataReader MyDataReader;
                    MyDataReader = adda.ExecuteReader();

                    while (MyDataReader.Read())
                    {
                    }
                    MyDataReader.Close();
                    conn.Close();
                    MessageBox.Show("Заявка удалена!", "ЖД Вокзал", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    refreshing();
                    // Close();
                }
                else
                {
                    MessageBox.Show("Удаление отменено!", "ЖД Вокзал", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

            }
            catch
            {
                MessageBox.Show("Ошибка!", "ЖД Вокзал", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }
    }
}
