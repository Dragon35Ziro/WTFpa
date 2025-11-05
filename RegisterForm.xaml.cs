using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Npgsql;
using System.Data;

namespace WTFpa
{
    /// <summary>
    /// Логика взаимодействия для RegisterForm.xaml
    /// </summary>
    public partial class RegisterForm : Window
    {
       
        public RegisterForm()
        {
            InitializeComponent();

            UserNameField.Text = "";
            /*UserNameField.ForeColor = Color.Gray;*/

            UserSurnameField.Text = "";
            /*UserSurnameField.ForeColor = Color.Gray;*/

            LoginField.Text = "";
            /*LoginField.ForeColor = Color.Gray;*/

            //passwordField.Text = "Введите пароль";
            /*passwordField.ForeColor = Color.Gray;*/
        }


        private void UserNameField_Enter(object sender, EventArgs e)
        {
            if (UserNameField.Text == "Введите имя")
            {
                UserNameField.Text = "";
                /*UserNameField.ForeColor = Color.Black;*/
            }



        }

        private void UserNameField_Leave(object sender, EventArgs e)
        {
            if (UserNameField.Text == "")
            {
                UserNameField.Text = "Введите имя";
                /*UserNameField.ForeColor = Color.Gray;*/
            }

        }

        private void UserSurnameField_Enter_1(object sender, EventArgs e)
        {
            if (UserSurnameField.Text == "Введите фамилию")
            {
                UserSurnameField.Text = "";
                /*UserSurnameField.ForeColor = Color.Black;*/
            }



        }

        private void UserSurnameField_Leave_1(object sender, EventArgs e)
        {
            if (UserSurnameField.Text == "")
            {
                UserSurnameField.Text = "Введите фамилию";
               /* UserSurnameField.ForeColor = Color.Gray;*/
            }

        }


        private void loginField_Enter(object sender, EventArgs e)
        {
            if (LoginField.Text == "Введите логин")
            {
                LoginField.Text = "";
                /*LoginField.ForeColor = Color.Black;*/
            }



        }

        private void loginField_Leave(object sender, EventArgs e)
        {
            if (LoginField.Text == "")
            {
                LoginField.Text = "Введите логин";
               /* LoginField.ForeColor = Color.Gray;*/
            }

        }

       /* private void passwordField_Enter(object sender, EventArgs e)
        {
            if (passwordField.Text == "Введите пароль")
            {
                passwordField.Text = "";
                /*passwordField.ForeColor = Color.Black;
            }



        }*/

        /*private void passwordField_Leave(object sender, EventArgs e)
        {
            if (passwordField.Text == "")
            {
                passwordField.Text = "Введите пароль";
                passwordField.ForeColor = Color.Gray;
            }

        }*/

        public Boolean checkUser()
        {
            DB db = new DB();

            DataTable table = new DataTable();

            NpgsqlDataAdapter adapter = new NpgsqlDataAdapter();

            db.openConnection();
            NpgsqlCommand command = new NpgsqlCommand("SELECT * FROM users WHERE login = @uL", db.GetConnection());
            command.Parameters.Add("@uL", NpgsqlTypes.NpgsqlDbType.Varchar).Value = LoginField.Text;


            adapter.SelectCommand = command;
            adapter.Fill(table);

            if (table.Rows.Count > 0)
            {
                MessageBox.Show("Пользователь с таким логином уже сущесвует, введите другой логин");
                return true;
            }

            else
            {
                db.closeConnection();
                return false;

            }
                
        }




        private void buttonRegister_Click(object sender, RoutedEventArgs e)
        {
            if (UserNameField.Text == "Введите имя")
            {
                MessageBox.Show("Укажите имя");
                return;
            }

            if (UserSurnameField.Text == "Введите фамилию")
            {
                MessageBox.Show("Укажите фамилию");
                return;
            }

            if (LoginField.Text == "Введите логин")
            {
                MessageBox.Show("Укажите логин");
                return;
            }

            if (passwordFieldf.Password == "")
            {
                MessageBox.Show("Укажите пароль");
                return;
            }


            if (checkUser())
                return;


            DB db = new DB();
            db.openConnection();
            //NpgsqlDataAdapter adapter = new NpgsqlDataAdapter();
            NpgsqlCommand command = new NpgsqlCommand("INSERT INTO users(login, password_, surname, forename) VALUES(@login, @pass, @name, @surname)", db.GetConnection());
            
            command.Parameters.Add("@login", NpgsqlTypes.NpgsqlDbType.Varchar).Value = LoginField.Text;
            //command.Parameters.Add("@pass", NpgsqlTypes.NpgsqlDbType.Varchar).Value = passwordField.Text;
            command.Parameters.Add("@name", NpgsqlTypes.NpgsqlDbType.Varchar).Value = UserNameField.Text;
            command.Parameters.Add("@surname", NpgsqlTypes.NpgsqlDbType.Varchar).Value = UserSurnameField.Text;
            command.Parameters.Add("@pass", NpgsqlTypes.NpgsqlDbType.Varchar).Value = passwordFieldf.Password;


            int rowsAffected = command.ExecuteNonQuery();

            Console.WriteLine($"{rowsAffected} запись(и) добавлено(ы).");


            if (command.ExecuteNonQuery() == 1)
            {
                MessageBox.Show("Аккаунт был создан");
                this.Hide();
                LoginWorm loginForm = new LoginWorm();
                loginForm.Show();
            }

            else
                MessageBox.Show("Аккаунт не был создан");


            db.closeConnection();
        }

        private void AutLabel_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
            LoginWorm loginForm = new LoginWorm();
            loginForm.Show();
        }

        private void passwordField_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}
