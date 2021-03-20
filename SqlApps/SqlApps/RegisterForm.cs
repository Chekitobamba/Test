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

namespace SqlApps
{
    public partial class RegisterForm : Form
    {
        public RegisterForm()
        {
            InitializeComponent();


            userNamefield.Text = "Введите имя";//Изначально какой текст в поле
            userSurenamefield.ForeColor = Color.Black;

            userSurenamefield.Text = "Фамилия";
            userSurenamefield.ForeColor = Color.Black;

            loginField.Text = "Логин";
            loginField.ForeColor = Color.Black;
        }

        private void closebutton_Click(object sender, EventArgs e)
        {
            this.Close();
        }


        Point lastPoint; //Point это класс для координат
        

        
        private void panel1_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left) //Создаем событие для того чтобы можно было перемещать окно при нажатий и удержаний мыши
            {
                this.Left += e.X - lastPoint.X;
                this.Top += e.Y - lastPoint.Y;
            }
        }

        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            lastPoint = new Point(e.X, e.Y);
        }

        private void userNamefield_Enter(object sender, EventArgs e)
        {
            if (userNamefield.Text == "Введите имя")
            {
                userNamefield.Text = "";
                userSurenamefield.ForeColor = Color.Black;
            }
        }

        private void userNamefield_Leave(object sender, EventArgs e)
        {
            if (userNamefield.Text == "")
            {
                userNamefield.Text = "Введите имя";
                userNamefield.ForeColor = Color.Gray;
            }
        }

        private void userSurenamefield_Enter(object sender, EventArgs e)
        {
            if (userSurenamefield.Text == "Фамилия")
            {
                userSurenamefield.Text = "";
                userSurenamefield.ForeColor = Color.Black;
            }
        }

        private void userSurenamefield_Leave(object sender, EventArgs e)
        {
            if (userSurenamefield.Text == "")
            {
                userSurenamefield.Text = "Фамилия";
                userSurenamefield.ForeColor = Color.Gray;
            }
        }

        private void buttonregister_Click(object sender, EventArgs e)
        {

            if (userNamefield.Text == "Введите имя") //Проверка ввел ли пользователь данные или нет
            {
                MessageBox.Show("Введите имя");
                return; //выходим из функций
            }

            if (userSurenamefield.Text == "Фамилия") //Проверка ввел ли пользователь данные или нет
            {
                MessageBox.Show("Введите Фамилию");
                return; //выходим из функций
            }

            if (IsUserExists())//выход из функций
                return;

            DB db = new DB();
            MySqlCommand command = new MySqlCommand(" INSERT INTO `users` ( `login`, `pass`, `name`, `surname`) VALUES ( @login, @pass, @name, @surname);", db.getConnection());
            command.Parameters.Add("@login", MySqlDbType.VarChar).Value = loginField.Text;
            command.Parameters.Add("@pass", MySqlDbType.VarChar).Value = passField.Text;
            command.Parameters.Add("@name", MySqlDbType.VarChar).Value = userNamefield.Text;
            command.Parameters.Add("@surname", MySqlDbType.VarChar).Value = userSurenamefield.Text;

            db.openConnection();

            if (command.ExecuteNonQuery() == 1) //Если sql команда = 1 то аккаунт бы лсоздан

                MessageBox.Show("Аккаунт был создан");
            else
                MessageBox.Show("Аккаунт не был создан");


            db.closeConnection();
        }
        public Boolean IsUserExists() //функция для того что-бы небыло зарегистрировано два одинаковых пользователя 
        {
            DB db = new DB(); //Подключение к базе

            DataTable table = new DataTable();

            MySqlDataAdapter adapter = new MySqlDataAdapter();

            MySqlCommand command = new MySqlCommand("SELECT * FROM `users` WHERE 'login' = @ul", db.getConnection()); //db.getConnection() это к какой базе мы подключаемся

            command.Parameters.Add("@ul", MySqlDbType.VarChar).Value = loginField.Text;


            adapter.SelectCommand = command;
            adapter.Fill(table);

            if (table.Rows.Count > 0)
            {
                MessageBox.Show("Такой логин уже есть");
                return true;
            }
            else
                return false;



        }

        private void registerLabel_Click(object sender, EventArgs e)
        {
            this.Hide();
            LoginForm loginsform = new LoginForm();
            loginsform.Show();
        }
    }
}
