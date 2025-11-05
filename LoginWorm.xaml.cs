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
using System.IO;



namespace WTFpa
{
    /// <summary>
    /// Логика взаимодействия для LoginWorm.xaml
    /// </summary>
    public partial class LoginWorm : Window
    {
        public LoginWorm()
        {
            InitializeComponent();
            string dir = @"c:\music\music";  // folder location
            Directory.CreateDirectory(dir);

            System.IO.FileInfo a = new FileInfo(@"c:\music\music\lap.txt");
            if (a.Exists)
            {
                string Pal = File.ReadAllText(@"c:\music\music\lap.txt");
                string[] words = Pal.Split(' ');
                string log = " ";
                string pas = " ";

                log = words[0];
                pas = words[1];

                DB db = new DB();

                DataTable table = new DataTable();

                NpgsqlDataAdapter adapter = new NpgsqlDataAdapter();


                db.openConnection();
                NpgsqlCommand command = new NpgsqlCommand("SELECT * FROM users WHERE password_ = @uP AND login = @uL;", db.GetConnection());
                //NpgsqlCommand command = new NpgsqlCommand("SELECT * FROM pg_catalog.pg_tables;", db.GetConnection());
                command.Parameters.Add("@uP", NpgsqlTypes.NpgsqlDbType.Varchar).Value = pas;
                command.Parameters.Add("@uL", NpgsqlTypes.NpgsqlDbType.Varchar).Value = log;



                adapter.SelectCommand = command;
                adapter.Fill(table);
                //Console.WriteLine(table);

                

                    NpgsqlCommand command_l = new NpgsqlCommand("SELECT users_id FROM users WHERE login = @uL;", db.GetConnection());


                    command_l.Parameters.Add("@uL", NpgsqlTypes.NpgsqlDbType.Varchar).Value = log;



                    adapter.SelectCommand = command_l;
                    adapter.Fill(table);

                    User_id = (int)command_l.ExecuteScalar();



                    this.Hide();
                    MainForm mainForm = new MainForm(User_id);
                    mainForm.Show();
                    Console.WriteLine("Пуск");



               
            }
        }

        string sql = "Server=localhost;port=5432;Database=Testing;User Id=postgres; Password=3572";
       /* string sql = "Server=localhost;port=5432;Database=postgres;User Id=postgres; Password=12345678";*/

        private void LoginForm_Load(object sender, EventArgs e)
        {

        }

        public int User_id { get; set; }
        private void buttonLogin_Click(object sender, RoutedEventArgs e)
        {
            String loginUser = loginField.Text;
            //String passUser = passwordField.Text;
            String passUser = passwordFieldf.Password;

            DB db = new DB();

            DataTable table = new DataTable();

            NpgsqlDataAdapter adapter = new NpgsqlDataAdapter();
            

            db.openConnection();
            NpgsqlCommand command = new NpgsqlCommand("SELECT * FROM users WHERE password_ = @uP AND login = @uL;", db.GetConnection());
            //NpgsqlCommand command = new NpgsqlCommand("SELECT * FROM pg_catalog.pg_tables;", db.GetConnection());
            command.Parameters.Add("@uP", NpgsqlTypes.NpgsqlDbType.Varchar).Value = passUser;
            command.Parameters.Add("@uL", NpgsqlTypes.NpgsqlDbType.Varchar).Value = loginUser;
            
            

            adapter.SelectCommand = command;
            adapter.Fill(table);
            //Console.WriteLine(table);

            if (table.Rows.Count > 0)
            {

                NpgsqlCommand command_l = new NpgsqlCommand("SELECT users_id FROM users WHERE login = @uL;", db.GetConnection());


                command_l.Parameters.Add("@uL", NpgsqlTypes.NpgsqlDbType.Varchar).Value = loginUser;



                adapter.SelectCommand = command_l;
                adapter.Fill(table);

                User_id = (int)command_l.ExecuteScalar();



                this.Hide();
                MainForm mainForm = new MainForm(User_id);
                mainForm.Show();
                Console.WriteLine("Пуск");

                string LaP = loginUser + " " + passUser;

                string path = @"c:\music\music\lap.txt";
                File.WriteAllText(path, LaP);

                MessageBox.Show("ПУУУУСК");


            }
            else MessageBox.Show("Не верный логин или пароль");

            
        }

        private void RegisterLabel_Click_1(object sender, RoutedEventArgs e)
        {
            this.Hide();
            RegisterForm registerForm = new RegisterForm();
            registerForm.Show();
            


        }

        private void passwordField_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void loginField_TextChanged(object sender, TextChangedEventArgs e)
        {
            loginField.Text = " ";
        }

        private void loginField_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (loginField.Text == "Логин")
            {
                loginField.Text = string.Empty; // Сбрасываем текст в пустую строку
                loginField.Foreground = new SolidColorBrush(Colors.White); // Изменяем цвет текста, если нужно
            }
        }

        private void loginField_TextInput(object sender, TextCompositionEventArgs e)
        {
            loginField.Text = " ";
        }

        private void Window_GotFocus(object sender, RoutedEventArgs e)
        {
           
        }

        private void loginField_GotFocus(object sender, RoutedEventArgs e)
        {
           
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}
