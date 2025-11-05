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
    /// Логика взаимодействия для Account.xaml
    /// </summary>
    public partial class Account : Window
    {
        int LG { get; set; }
        string box { get; set; }
        string name_track { get; set; }
        string artist_name { get; set; }

        int traks_id { get; set; }
       


        bool b { get; set; }
        public Account(int Login_user)
        {
            InitializeComponent();
            _timer.Interval = TimeSpan.FromMilliseconds(1000);
            _timer.Tick += new EventHandler(ticktock);
            _timer.Start();
            Console.WriteLine(LG);
            LG = Login_user;
            b = false;
        }

        DispatcherTimer _timer = new DispatcherTimer();
        MediaPlayer player = new MediaPlayer();
        private void opredel()
        {

            /*

            DB db = new DB();

            db.openConnection();

            DataTable table = new DataTable();

            NpgsqlDataAdapter adapter = new NpgsqlDataAdapter();

            NpgsqlCommand command = new NpgsqlCommand("SELECT location FROM tracks WHERE name LIKE @num", db.GetConnection());

            string result = box;

            command.Parameters.Add("@num", NpgsqlTypes.NpgsqlDbType.Varchar).Value = result;





            var client = new SftpClient("192.168.197.204", "pavel", "00000000");

            client.Connect();
            Console.WriteLine(client.ConnectionInfo.ServerVersion);

            string such = @"C:\Users\user\Desktop\WTFpa — копия\WTFpa\music\" + result + ".mp3";
            string suchlin = "/home/pavel/Downloads/KIS/" + result + ".mp3";
            Console.WriteLine(such);



            using (Stream fileStream = System.IO.File.Create(such))
            {
                client.DownloadFile(suchlin, fileStream);
            }





            adapter.SelectCommand = command;
            adapter.Fill(table);




            string res = string.Join(Environment.NewLine, table.Rows.OfType<DataRow>().Select(x => string.Join(" ; ", x.ItemArray)));







            string Filename = res;

            player.Open(new Uri(such, UriKind.Relative));


            b = true;
            client.Disconnect();*/
            //SoundPlayer = new SoundPlayer();
            /*//////////////////////////////////////////////////////////////////////////////////////////////////////
            DB db = new DB();

            db.openConnection();

            DataTable table = new DataTable();

            NpgsqlDataAdapter adapter = new NpgsqlDataAdapter();

            NpgsqlCommand command = new NpgsqlCommand("SELECT track_id FROM tracks WHERE name = @num AND artist_id = (SELECT artist_id FROM artists WHERE artist_name = @art)", db.GetConnection());

            
            */////////////////////////////////////////////////////////////////////////////////////////////////
           /* if (table.Rows.Count > 0)
            {*/
           /*******************************************************************************
                traks_id = (int)command.ExecuteScalar();

                string[] words = result.Split('-');

                name_track = words[0];
                artist_name = words[1];
            *///////////////////////////////////////////////////////////////////////////////////

                //int ind = name_track.Length - 1;
                //////////////////////////////////////////////////////////////////////////////////////////////
                /*name_track = name_track.Trim();
                artist_name = artist_name.Trim();



                command.Parameters.Add("@num", NpgsqlTypes.NpgsqlDbType.Varchar).Value = name_track;
                command.Parameters.Add("@art", NpgsqlTypes.NpgsqlDbType.Varchar).Value = artist_name;*/
                ////////////////////////////////////////////////////////////////////////////////////////////////



                string result = box;
                var client = new SftpClient("217.25.228.218", "pavel", "00000000");
                //

                client.Connect();
                Console.WriteLine(client.ConnectionInfo.ServerVersion);

                string such = @"c:\music\music\" + result + ".mp3";
                string suchlin = "/media/pavel/Music/music/KIS/" + result + ".mp3";
                Console.WriteLine(such);



                using (Stream fileStream = System.IO.File.Create(such))
                {
                    client.DownloadFile(suchlin, fileStream);
                }




                /*
                adapter.SelectCommand = command;
                adapter.Fill(table);




                string res = string.Join(Environment.NewLine, table.Rows.OfType<DataRow>().Select(x => string.Join(" ; ", x.ItemArray)));







                string Filename = res;*/

                player.Open(new Uri(such, UriKind.Relative));
                b = true;
                client.Disconnect();
            /*}
            else MessageBox.Show("СИПИ(Система интуитивно понятного интерфейса): Та фигня на которую вы нажали - пустая, она ничего не запустит. Заново не повторишь");
            */
        }

        private void Search_Like_Click(object sender, RoutedEventArgs e)
        {


            DB db = new DB();


            db.openConnection();

            //NpgsqlCommand command = new NpgsqlCommand("SELECT name FROM tracks WHERE name LIKE @num", db.GetConnection());
            NpgsqlCommand command = new NpgsqlCommand("SELECT a.artist_name, t.name FROM tracks t JOIN artists a ON t.artist_ID = a.artist_ID WHERE track_id in (SELECT track_id FROM us_track WHERE users_id = @log ORDER BY ust_id LIMIT 10);", db.GetConnection());
            //string loga = LG.ToString();
            //Console.WriteLine();
            command.Parameters.Add("@log", NpgsqlTypes.NpgsqlDbType.Integer).Value = LG;

            //command.Parameters.Add("@num", NpgsqlTypes.NpgsqlDbType.Varchar).Value = ID чела который юзает прогу;

            NpgsqlDataReader dataReader = command.ExecuteReader();

            string[] sur = new string[10];
            int a = 0;
            if (dataReader.HasRows)
            {

                while (dataReader.Read())
                {



                    int i = 0;
                    while (i < 2)
                    {
                        if (i == 0)
                        {
                            sur[a] = dataReader.GetValue(i).ToString();
                            i++;
                        }
                        else
                        {
                            sur[a] = sur[a] + " - " + dataReader.GetValue(i).ToString();
                            i++;
                        }

                        //sur[a] = dataReader.GetFieldValue<object[]>(0);
                    }
                    i = 0;
                    a = a + 1;

                }

            }
            
            textBox2.Content = sur[0];
            textBox3.Content = sur[1];
            textBox4.Content = sur[2];
            textBox5.Content = sur[3];
            textBox6.Content = sur[4];
            textBox7.Content = sur[5];
            textBox8.Content = sur[6];
            textBox9.Content = sur[7];
            textBox10.Content = sur[8];
            textBox11.Content = sur[9];

            
            /*string[] sur = new string[10];
            int a = 0;
            if (dataReader.HasRows)
            {

                while (dataReader.Read())
                {



                    sur[a] = dataReader.GetValue(0).ToString();
                    a = a + 1;

                }


                command.Dispose();
                db.closeConnection();
                db.openConnection();

                a = 0;
                NpgsqlCommand commandL = new NpgsqlCommand("SELECT artist_name FROM artists WHERE artist_id in (SELECT artist_id FROM tracks WHERE name in (SELECT name FROM tracks WHERE track_id in (SELECT track_id FROM us_track WHERE users_id = @log ORDER BY ust_id LIMIT 10)))", db.GetConnection());
                commandL.Parameters.Add("@log", NpgsqlTypes.NpgsqlDbType.Integer).Value = LG;
                NpgsqlDataReader dataReade = commandL.ExecuteReader();

                if (dataReade.HasRows)
                {

                    while (dataReade.Read())
                    {


                        sur[a] = sur[a] + " - " + dataReade.GetValue(0).ToString();
                        a = a + 1;

                    }
                }



                textBox1.Content = sur[0];
                textBox2.Content = sur[1];
                textBox3.Content = sur[2];
                textBox4.Content = sur[3];
                textBox5.Content = sur[4];
                textBox6.Content = sur[5];
                textBox7.Content = sur[6];
                textBox8.Content = sur[7];
                textBox9.Content = sur[8];
                textBox10.Content = sur[9];







                */
            command.Dispose();
        }
    

        public void check_file()
        {
            //box = Output1.Content.ToString();

            string path = @"c:\music\music";

            if (Directory.GetFiles(path).Length > 0)
            {
                DirectoryInfo dirInfo = new DirectoryInfo(@"C:\music\music");

                foreach (FileInfo file in dirInfo.GetFiles())
                {
                    file.Delete();
                }
            }
        }

        private void textBox2_Click(object sender, MouseButtonEventArgs e)
        {
            /*string No = textBox1.Content.ToString();
            if (box != No)
            {
                box = textBox1.Content.ToString();

                string path = @"C:\Users\user\Desktop\music\";

                if (Directory.GetFiles(path).Length > 0)
                {
                    DirectoryInfo dirInfo = new DirectoryInfo(@"C:\Users\user\Desktop\music\");

                    foreach (FileInfo file in dirInfo.GetFiles())
                    {
                        file.Delete();
                    }
                }



                opredel();


            }*/

            string No = textBox2.Content.ToString();
            if (box != No)
            {
                box = textBox2.Content.ToString();

                string path = @"c:\music\music";

                if (Directory.GetFiles(path).Length > 0)
                {
                    DirectoryInfo dirInfo = new DirectoryInfo(@"c:\music\music");

                    foreach (FileInfo file in dirInfo.GetFiles())
                    {
                        file.Delete();
                    }
                }
                check_file();
                opredel();
            }
        }
        private void textBox3_Click(object sender, MouseButtonEventArgs e)
        {
            string No = textBox3.Content.ToString();
            if (box != No)
            {
                box = textBox3.Content.ToString();

                string path = @"c:\music\music";

                if (Directory.GetFiles(path).Length > 0)
                {
                    DirectoryInfo dirInfo = new DirectoryInfo(@"c:\music\music");

                    foreach (FileInfo file in dirInfo.GetFiles())
                    {
                        file.Delete();
                    }
                }



                opredel();


            }



        }

        private void textBox4_Click(object sender, MouseButtonEventArgs e)
        {
            string No = textBox4.Content.ToString();
            if (box != No)
            {
                box = textBox4.Content.ToString();
                check_file();
                opredel();
                
            }

        }
        private void textBox5_Click(object sender, MouseButtonEventArgs e)
        {
            string No = textBox5.Content.ToString();
            if (box != No)
            {
                box = textBox5.Content.ToString();
                check_file();
                opredel();

            }

        }
        private void textBox6_Click(object sender, MouseButtonEventArgs e)
        {
            string No = textBox6.Content.ToString();
            if (box != No)
            {
                box = textBox6.Content.ToString();
                check_file();
                opredel();

            }

        }
        private void textBox7_Click(object sender, MouseButtonEventArgs e)
        {
            string No = textBox7.Content.ToString();
            if (box != No)
            {
                box = textBox7.Content.ToString();
                check_file();
                opredel();

            }

        }

        private void textBox8_Click(object sender, MouseButtonEventArgs e)
        {
            string No = textBox8.Content.ToString();
            if (box != No)
            {
                box = textBox8.Content.ToString();
                check_file();
                opredel();

            }

        }
        private void textBox9_Click(object sender, MouseButtonEventArgs e)
        {
            string No = textBox9.Content.ToString();
            if (box != No)
            {
                box = textBox9.Content.ToString();
                check_file();
                opredel();

            }

        }

        private void textBox10_Click(object sender, MouseButtonEventArgs e)
        {
            string No = textBox9.Content.ToString();
            if (box != No)
            {
                box = textBox9.Content.ToString();
                check_file();
                opredel();

            }
        }
        private void textBox11_Click(object sender, MouseButtonEventArgs e)
        {
            string No = textBox10.Content.ToString();
            if (box != No)
            {
                box = textBox10.Content.ToString();
                check_file();
                opredel();

            }

        }

        private void PlaySoundButton_Click(object sender, RoutedEventArgs e)
        {
            if (b == true)
            {
                player.Play();

                Position_music.Maximum = player.NaturalDuration.TimeSpan.TotalSeconds;
                Progress.Maximum = player.NaturalDuration.TimeSpan.TotalSeconds;
            }
        }

        private void StopSoundButton_Click(object sender, RoutedEventArgs e)
        {
            player.Pause();
        }

        private void Position_music_MouseLeave(object sender, MouseEventArgs e)
        {
            player.Play();

        }

        private void sliderPosition_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            player.Pause();
            player.Position = TimeSpan.FromSeconds(Position_music.Value);


        }

        private void Progress_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            /*Progress.Value = player.NaturalDuration.TimeSpan.Ticks;*/


        }

        void ticktock(object sender, EventArgs e)
        {
            if (!Progress.IsMouseCaptureWithin)

                Progress.Value = player.Position.TotalSeconds;


        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
            MainForm MainForm = new MainForm(LG);
            player.Close();
            MainForm.Show();
        }

        private void Like_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Style_Life_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
            Life life = new Life(LG);
            player.Stop();
            player.Close();
            life.Show();
        }

        private void Account_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
            Account Account = new Account(LG);
            player.Stop();
            player.Close();
            Account.Show();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            if (b == true)
            {
                this.Hide();
                comment comment = new comment(traks_id, LG);
                player.Stop();
                player.Close();
                comment.Show();
            }
            else
            {
                MessageBox.Show("Сначала нужно выбрать марку тренбалона");
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {

            this.Close();

            player.Stop();
            player.Close();

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

       
    }
}
