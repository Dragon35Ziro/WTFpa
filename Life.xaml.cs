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
    /// Логика взаимодействия для Life.xaml
    /// </summary>
    public partial class Life : Window
    {
        SoundPlayer SoundPlayer = null;
        MediaPlayer player = new MediaPlayer();
        bool b { get; set; }
        int LG { get; set; }
        string name_track { get; set; }
        string artist_name { get; set; }
        public Life(int Login_user)
        {

            InitializeComponent();

            LG = Login_user;

            _timer.Interval = TimeSpan.FromMilliseconds(1000);
            _timer.Tick += new EventHandler(ticktock);
            _timer.Start();
            Console.WriteLine(LG);
            b = false;
        }
        DispatcherTimer _timer = new DispatcherTimer();

        private void opredel()
        {
            if (box != "")
            {
                SoundPlayer = new SoundPlayer();

            DB db = new DB();

            db.openConnection();

            DataTable table = new DataTable();

            NpgsqlDataAdapter adapter = new NpgsqlDataAdapter();

            NpgsqlCommand command = new NpgsqlCommand("SELECT location FROM tracks WHERE name LIKE @num AND artist_id = (SELECT artist_id FROM artists WHERE artist_name LIKE @art) ", db.GetConnection());

            string result = box;

            adapter.SelectCommand = command;
           // adapter.Fill(table);

            string[] words = result.Split('-');

            
                name_track = words[0];
                artist_name = words[1];


                //int ind = name_track.Length - 1;

                name_track = name_track.Trim();
                artist_name = artist_name.Trim();



                command.Parameters.Add("@num", NpgsqlTypes.NpgsqlDbType.Varchar).Value = name_track;
                command.Parameters.Add("@art", NpgsqlTypes.NpgsqlDbType.Varchar).Value = artist_name;





                var client = new SftpClient("217.25.228.218", "pavel", "00000000");
                //217.25.228.218

                client.Connect();
                Console.WriteLine(client.ConnectionInfo.ServerVersion);

                string such = @"c:\music\music\" + result + ".mp3";
                string suchlin = "/media/pavel/Music/music/KIS/" + result + ".mp3";
                Console.WriteLine(such);



                using (Stream fileStream = System.IO.File.Create(such))
                {
                    client.DownloadFile(suchlin, fileStream);
                }





                adapter.SelectCommand = command;
                //adapter.Fill(table);




                string res = string.Join(Environment.NewLine, table.Rows.OfType<DataRow>().Select(x => string.Join(" ; ", x.ItemArray)));







                string Filename = res;

                player.Open(new Uri(such, UriKind.Relative));


                b = true;
                client.Disconnect();
            }
            else
            {
                MessageBox.Show("СИПИ (Система интуитивно понятного интерфейса): Ничего нету. Заново не повторю");
            }
        }

        private void Search_Life_Click(object sender, RoutedEventArgs e)
        {


            DB db = new DB();


            db.openConnection();

            //NpgsqlCommand command = new NpgsqlCommand("SELECT name FROM tracks WHERE name LIKE @num", db.GetConnection());
            NpgsqlCommand command = new NpgsqlCommand("SELECT * FROM (SELECT a.artist_name, t.name FROM tracks t JOIN artists a ON t.artist_ID = a.artist_ID WHERE genre_id in (SELECT genre_id FROM tracks WHERE track_id in (SELECT track_ID FROM us_lhistory WHERE users_id = 7 ORDER BY us_lhistory_id DESC LIMIT 10))) AS t ORDER BY RANDOM() LIMIT 10;", db.GetConnection());
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
                    while (i < 2) {
                        if ( i == 0) {
                            sur[a] = dataReader.GetValue(i).ToString();
                            i++;
                            if (a == 10) break;
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
                    if (a == 10) break;
                }

            }
            Output1.Content = sur[0];
            Output2.Content = sur[1];
            Output3.Content = sur[2];
            Output4.Content = sur[3];
            Output5.Content = sur[4];
            Output6.Content = sur[5];
            Output7.Content = sur[6];
            Output8.Content = sur[7];
            Output9.Content = sur[8];
            Output10.Content = sur[9];

            command.Dispose();
            db.closeConnection();
            db.openConnection();

            /*NpgsqlCommand commandP = new NpgsqlCommand("SELECT artist_name FROM artists WHERE artist_id in (SELECT artist_id FROM tracks WHERE name in (SELECT name FROM tracks WHERE genre_id in (SELECT genre_id FROM tracks WHERE track_id in (SELECT track_ID FROM us_lhistory WHERE users_id = @log ORDER BY us_lhistory_id DESC LIMIT 10))) )", db.GetConnection());
            commandP.Parameters.Add("@log", NpgsqlTypes.NpgsqlDbType.Integer).Value = LG;
            NpgsqlDataReader dataRead = commandP.ExecuteReader();


            string[] bur = new string[10];

            a = 0;
            if (dataRead.HasRows)
            {

                while (dataRead.Read())
                {


                    bur[a] = dataReader.GetValue(0).ToString();
                    a = a + 1;

                }

            }


            


            commandP.Dispose();
            db.closeConnection();
            db.openConnection();
            */
            
            
            
            /*
            NpgsqlCommand commandL = new NpgsqlCommand("SELECT artist_name FROM artists WHERE artist_id in (SELECT artist_id FROM tracks WHERE name in (SELECT name FROM tracks WHERE genre_id in (SELECT genre_id FROM tracks WHERE track_id in (SELECT track_ID FROM us_lhistory WHERE users_id = @log ORDER BY us_lhistory_id DESC LIMIT 10))) )", db.GetConnection());
            commandL.Parameters.Add("@log", NpgsqlTypes.NpgsqlDbType.Integer).Value = LG;
            NpgsqlDataReader dataReade = commandL.ExecuteReader();


            a = 0;

            if (dataReade.HasRows)
            {

                while (dataReade.Read())
                {


                    sur[a] = sur[a] + " - " + dataReade.GetValue(0).ToString();
                    a = a + 1;

                }

            }

            Output1.Content = sur[0];
            Output2.Content = sur[1];
            Output3.Content = sur[2];
            Output4.Content = sur[3];
            Output5.Content = sur[4];
            Output6.Content = sur[5];
            Output7.Content = sur[6];
            Output8.Content = sur[7];
            Output9.Content = sur[8];
            Output10.Content = sur[9];



            */




            command.Dispose();
        }

        string box { get; set; }

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

       


        private void Output1_Click(object sender, MouseButtonEventArgs e)
        {
            string No = Output1.Content.ToString();
            if (box != No)
            {
                box = Output1.Content.ToString();

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

        private void Output2_Click(object sender, MouseButtonEventArgs e)
        {
            string No = Output2.Content.ToString();
            if (box != No)
            {
                string path = @"c:\music\music";

                if (Directory.GetFiles(path).Length > 0)
                {
                    DirectoryInfo dirInfo = new DirectoryInfo(@"c:\music\music");

                    foreach (FileInfo file in dirInfo.GetFiles())
                    {
                        file.Delete();
                    }
                }

                box = Output2.Content.ToString();
                //check_file();
                opredel();
            }
        }

        private void Output3_Click(object sender, MouseButtonEventArgs e)
        {
            string No = Output3.Content.ToString();
            if (box != No)
            {
                box = Output3.Content.ToString();
                check_file();
                opredel();
            }
        }
        private void Output4_Click(object sender, MouseButtonEventArgs e)
        {
            string No = Output4.Content.ToString();
            if (box != No)
            {
                box = Output4.Content.ToString();
                check_file();
                opredel();
            }
        }
        private void Output5_Click(object sender, MouseButtonEventArgs e)
        {
            string No = Output5.Content.ToString();
            if (box != No)
            {
                box = Output5.Content.ToString();
                check_file();
                opredel();
            }
        }
        private void Output6_Click(object sender, MouseButtonEventArgs e)
        {
            string No = Output6.Content.ToString();
            if (box != No)
            {
                box = Output6.Content.ToString();
                check_file();
                opredel();
            }
        }

        private void Output7_Click(object sender, MouseButtonEventArgs e)
        {
            string No = Output7.Content.ToString();
            if (box != No)
            {
                box = Output7.Content.ToString();
                check_file();
                opredel();
            }
        }
        private void Output8_Click(object sender, MouseButtonEventArgs e)
        {
            string No = Output8.Content.ToString();
            if (box != No) { 
            box = Output8.Content.ToString();
            check_file();
            opredel();
            }
        }

        private void Output9_Click(object sender, MouseButtonEventArgs e)
        {
            string No = Output9.Content.ToString();
            if (box != No)
            {
                box = Output9.Content.ToString();
                check_file();
                opredel();
            }
        }
        private void Output10_Click(object sender, MouseButtonEventArgs e)
        {
            string No = Output10.Content.ToString();
            if (box != No)
            {
                box = Output10.Content.ToString();
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




        /**/

       

        private void Like_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Style_Life_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
            Life life = new Life(LG);

            life.Show();
        }

        private void Account_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
            Account Account = new Account(LG);

            Account.Show();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            
            if (b == true)
            {
                /*
                this.Hide();
                comment comment = new comment(traks_id, LG);
                comment.Show();*/
                MessageBox.Show("Комментов с этой страницы не будет, коменты приняли буддизм и ушли в Шаулинь");
            }
            else
            {
                MessageBox.Show("Сначала нужно выбрать марку тренбалона");
            }
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



    }
}
