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
using System.Media;

using Npgsql;
using System.Data;
using System.Windows.Threading;
using System.IO;
using Renci.SshNet;

namespace WTFpa
{
    /// <summary>
    /// Логика взаимодействия для comment.xaml
    /// </summary>
    public partial class comment : Window
    {
        int TI { get; set; }
        int LOG { get; set; }

        int rat { get; set; }
        public comment(int Login_user, int LG)
        {
            InitializeComponent();
            TI = Login_user;
            LOG = LG;

        }

        private void Enter_Click(object sender, RoutedEventArgs e)
        {
            DB db = new DB();

            db.openConnection();

            DataTable table = new DataTable();

            NpgsqlDataAdapter adapter = new NpgsqlDataAdapter();

            NpgsqlCommand command_ins = new NpgsqlCommand("INSERT INTO track_rating(track_id, rating, comments) VALUES ( @traks_id , @Star , @Comment )", db.GetConnection());

            

            if (rat == 1 || rat ==2 || rat == 3 || rat == 4 || rat == 5)
            {
                

                string comm = Comment.Text;
                command_ins.Parameters.Add("@Star", NpgsqlTypes.NpgsqlDbType.Integer).Value = rat;
                command_ins.Parameters.Add("@Comment", NpgsqlTypes.NpgsqlDbType.Varchar).Value = comm;
                command_ins.Parameters.Add("@traks_id", NpgsqlTypes.NpgsqlDbType.Integer).Value = TI;

                int rowsAffected = command_ins.ExecuteNonQuery();
                Console.WriteLine($"{rowsAffected} запись(и) добавлено(ы).");
                MessageBox.Show("Комментарий отправлен");
            }
            else MessageBox.Show("Введите число");
        }

        private void Search_Click(object sender, RoutedEventArgs e)
        {
            //SELECT comments, rating FROM track_rating WHERE track_id = @num
            DB db = new DB();


            db.openConnection();


            /*NpgsqlConnection sqlConnection = new NpgsqlConnection();*/

            /*sqlConnection.Open();*/
            NpgsqlCommand command = new NpgsqlCommand("SELECT comments FROM track_rating WHERE track_id = @num", db.GetConnection());
            
            //command.Connection = sqlConnection;
            //command.CommandType = CommandType.Text;
            //command.CommandText = "SELECT name FROM audio WHERE name LIKE @num";

            string mask1 = "%";
            string mask2 = "%";
            //string forms = textBox1.Text;
            //string result = mask1 + forms + mask2;

            command.Parameters.Add("@num", NpgsqlTypes.NpgsqlDbType.Integer).Value = TI;
            
            NpgsqlDataReader dataReader = command.ExecuteReader();
            
            /*if (dataReader.HasRows)
            {
                DataTable data = new DataTable();
                data.Load(dataReader);

                dataGridView1.DataSource = data.Columns;

            }*/

            /*while (dataReader.Read())
            {
                
                textBox2.Text = dataReader.GetValue(0).ToString();

                
                textBox3.Text = dataReader.GetValue(0).ToString();
                
                    
            }*/

            string[] sur = new string[10];
            int a = 0;
            if (dataReader.HasRows)
            {

                while (dataReader.Read())
                {


                    /* textBox6.Text = textBox6.Text + dataReader.GetValue(0).ToString();*/
                    sur[a] = dataReader.GetValue(0).ToString();
                    a = a + 1;

                }

            }

            command.Dispose();
            db.closeConnection();
            db.openConnection();
            a = 0;


            NpgsqlCommand commandL = new NpgsqlCommand("SELECT rating FROM track_rating WHERE track_id = @num", db.GetConnection());
            commandL.Parameters.Add("@num", NpgsqlTypes.NpgsqlDbType.Integer).Value = TI;
            NpgsqlDataReader dataReade = commandL.ExecuteReader();

            if (dataReade.HasRows)
            {

                while (dataReade.Read())
                {


                    /* textBox6.Text = textBox6.Text + dataReader.GetValue(0).ToString();*/
                    sur[a] = sur[a] + " - " + dataReade.GetValue(0).ToString();
                    a = a + 1;

                }

            }
            /*for (int i = 0; i < 10; i++)
            {
               
            }*/

            Comm1.Text = sur[0];
            Comm2.Text = sur[1];
            Comm3.Text = sur[2];
            Comm4.Text = sur[3];
            Comm5.Text = sur[4];
            Comm6.Text = sur[5];
            Comm7.Text = sur[6];
            Comm8.Text = sur[7];
            Comm9.Text = sur[8];
            Comm10.Text = sur[9];



            /* if (dataReader.NextResult())
             {
                 while (dataReader.Read())
                 {

                     textBox3.Text = textBox3.Text + dataReader.GetValue(0).ToString();



                 }
             }
            */





            commandL.Dispose();
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
            MainForm MainForm = new MainForm(LOG);
            MainForm.Show();
        }

        private void Like_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Style_Life_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
            Life life = new Life(LOG);
           
            life.Show();
        }

        private void Account_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
            Account Account = new Account(LOG);
            
            Account.Show();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            /*
            if (b == true)
            {
                this.Hide();
                comment comment = new comment(traks_id, LG);
                comment.Show();
            }
            else
            {
                MessageBox.Show("Сначала нужно выбрать марку тренбалона");
            }*/
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {

            this.Close();



            File.Delete(@"C:\music\music\lap.txt");
            LoginWorm loginWorm = new LoginWorm();
            System.IO.FileInfo a = new FileInfo(@"c:\music\music\lap.txt");
            if (a.Exists)
            {

            }
            else
            {
                loginWorm.Show();
            }




        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
            MainForm MainForm = new MainForm(LOG);
            MainForm.Show();
        }

        private void Star1_Click(object sender, RoutedEventArgs e)
        {
            rat = 1;
        }

        private void Star2_Click(object sender, RoutedEventArgs e)
        {
            rat = 2;
        }

        private void Star3_Click(object sender, RoutedEventArgs e)
        {
            rat = 3;
        }

        private void Star4_Click(object sender, RoutedEventArgs e)
        {
            rat = 4;
        }

        private void Star5_Click(object sender, RoutedEventArgs e)
        {
            rat = 5;
            
        }
    }
}

