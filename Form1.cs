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

namespace Data_Base
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            MySqlConnection connection;
            string server = "localhost";
            string database = "project";
            string uid = "root";
            string password = "";
            string connectionString;
            connectionString = "SERVER=" + server + ";" + "DATABASE=" +
            database + ";" + "UID=" + uid + ";" + "PASSWORD=" + password + ";";
            connection = new MySqlConnection(connectionString);

            connection.ConnectionString = connectionString;
            connection.Open();
            MySqlCommand cmd = new MySqlCommand("insert into problems (mail, problem, problem_info) values(@mail, @problem, @problem_info)", connection);
            cmd.Parameters.AddWithValue("@mail", TextBox1.Text);
            cmd.Parameters.AddWithValue("@problem", comboBox2.Text);
            cmd.Parameters.AddWithValue("@problem_info", richTextBox1.Text);

            // проверка на заполнение полей
            string err = "";
            foreach (Control c in Controls)
            {
                if (c is ComboBox)
                {
                    if (string.IsNullOrEmpty((c as ComboBox).Text)) err = "Укажите проблему с которой Вы столкнулись!";
                }
                if (c is TextBox)
                {
                    if (string.IsNullOrEmpty((c as TextBox).Text)) err = "Введите адрес электронной почты!";
                }

                if (c is RichTextBox)
                {
                    if (string.IsNullOrEmpty((c as RichTextBox).Text)) err = "Опишите подробно проблему с которой Вы столкнулись!";
                }
            }
            if (err != "")
                MessageBox.Show($"{err}");
            // если поля заполнены и работает бд, даные отправляются
            else if (cmd.ExecuteNonQuery() == 1)
            {
                MessageBox.Show("Ваше сообщение было отправленно!");
                connection.Close();
                // выход в меню после успешного отправления данных
                Form mfrm = Application.OpenForms[0];
                mfrm.StartPosition = FormStartPosition.Manual;
                mfrm.Show();
                this.Hide();
            }
            else
                MessageBox.Show("Не удалось отправить сообщение!");
            connection.Close();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
