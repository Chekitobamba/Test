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
    public partial class LoginForm : Form
    {
        public LoginForm()
        {
            InitializeComponent();

            this.passField.AutoSize = false; // дял поля пароль отключили авторазмер
            this.passField.Size = new Size(this.passField.Size.Width, 213); //для панели пароля вводим размер ширина
            this.passField.Size = new Size(this.passField.Size.Height, 53); //высота
        }

        private void closebutton_Click(object sender, EventArgs e)
        {
            this.Close();
            // Application.Exit(); //Можно использовать эту команду тогда программа полностью закроется
        }

        private void closebutton_MouseEnter_1(object sender, EventArgs e)
        {
            closebutton.ForeColor = Color.Green; //создали событие при наведение на кнопку что бы она была зеленого цвета.
        }

        private void closebutton_MouseLeave_1(object sender, EventArgs e)
        {
            closebutton.ForeColor = Color.White; // создали событие при убираний курсора
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

        private void buttonlogin_Click(object sender, EventArgs e)
        {
            String loginUser = loginField.Text;
            String passUser = passField.Text;

            DB db = new DB(); //Подключение к базе

            DataTable table = new DataTable();

            MySqlDataAdapter adapter = new MySqlDataAdapter();

            MySqlCommand command = new MySqlCommand("SELECT * FROM `users` WHERE `login` = @uL AND `pass` = @uP ", db.getConnection()); //db.getConnection() это к какой базе мы подключаемся

            command.Parameters.Add("@uL", MySqlDbType.VarChar).Value = loginUser;
            command.Parameters.Add("@uP", MySqlDbType.VarChar).Value = passUser;

            adapter.SelectCommand = command;
            adapter.Fill(table);

            if (table.Rows.Count > 0)
                MessageBox.Show("Yes");
            else
                MessageBox.Show("No");

        }

        private void registerLabel_Click(object sender, EventArgs e) //Взаймодествие с кнопкой перехода
        {
            
            {

                this.Hide();
                RegisterForm registerform = new RegisterForm(); //обращаемся к классу и выделяем память
                registerform.Show(); //открывает окно регистраций при нажатий на кнопку Еще нет аккаунта?
            }
        }
    }

}
