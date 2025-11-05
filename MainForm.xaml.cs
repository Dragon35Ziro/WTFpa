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
using System.Threading;

namespace WTFpa
{
    /// <summary>
    /// Логика взаимодействия для MainForm.xaml
    /// </summary>
    public partial class MainForm : Window
    {

        bool b { get; set; }
        SoundPlayer SoundPlayer = null;
        MediaPlayer player = new MediaPlayer();
        int LG { get; set; }
        string name_track { get; set; } = "";
        string artist_name { get; set; } = "";

        public MainForm(int Login_user)
        {
            InitializeComponent();
            LG = Login_user;

            

            _timer.Interval = TimeSpan.FromMilliseconds(1000);
            _timer.Tick += new EventHandler(ticktock);
            _timer.Start();
            b = false;

        }
        
        DispatcherTimer _timer = new DispatcherTimer();

        public void check_file()
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
        }

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



                string[] words = result.Split('-');

                name_track = words[0];
                artist_name = words[1];


                //int ind = name_track.Length - 1;

                name_track = name_track.Trim();
                artist_name = artist_name.Trim();



                command.Parameters.Add("@num", NpgsqlTypes.NpgsqlDbType.Varchar).Value = artist_name;
                command.Parameters.Add("@art", NpgsqlTypes.NpgsqlDbType.Varchar).Value =  name_track;




                var client = new SftpClient("217.25.228.218", "pavel", "00000000");


                client.Connect();
                Console.WriteLine(client.ConnectionInfo.ServerVersion);

                string such = @"c:\music\music\" + result + ".mp3";
                string suchlin = "/media/pavel/Music/music/KIS/" + result + ".mp3";
                //Console.WriteLine(such);

                //Console.WriteLine(words);



                using (Stream fileStream = System.IO.File.Create(such))
                {
                    client.DownloadFile(suchlin, fileStream);
                }






                adapter.SelectCommand = command;
                //adapter.Fill(table);



                //adapter.Fill(table);


                string res = string.Join(Environment.NewLine, table.Rows.OfType<DataRow>().Select(x => string.Join(" ; ", x.ItemArray)));







                string Filename = res;

                player.Open(new Uri(such, UriKind.Relative));
                b = true;
                client.Disconnect();
            }
            else MessageBox.Show("Система ИПИ (Интуитивно понятный интерфейс): Кнопка является пустой. Повторять снова не буду. Я НЕ ПОПУГАЙ");
           
            
        }

        string box { get; set; }
        int traks_id { get; set; }
        public void zaprosi()
        {
            if (box != "")
            {
                DB db = new DB();

            db.openConnection();

            DataTable table = new DataTable();

            NpgsqlDataAdapter adapter = new NpgsqlDataAdapter();

            NpgsqlCommand command = new NpgsqlCommand("SELECT track_id FROM tracks WHERE name = @num AND artist_id = (SELECT artist_id FROM artists WHERE artist_name = @art)", db.GetConnection());



            //command.Parameters.Add("@trackname", NpgsqlTypes.NpgsqlDbType.Varchar).Value = name_track;
            //ПЕРЕСТАВЛЯЛ
            command.Parameters.Add("@num", NpgsqlTypes.NpgsqlDbType.Varchar).Value = artist_name;
            command.Parameters.Add("@art", NpgsqlTypes.NpgsqlDbType.Varchar).Value = name_track;

                NpgsqlDataReader dataReaderSUKABL = command.ExecuteReader();
                while (dataReaderSUKABL.Read())
                {

                     //= (int)command.ExecuteScalar();
                    string traks_id_str = dataReaderSUKABL.GetValue(0).ToString();
                    traks_id = int.Parse(traks_id_str);


                }
               
                
                //adapter.SelectCommand = command;

                

                db.closeConnection();
                db.openConnection();
            
           
            //NpgsqlCommand comman_ins = new NpgsqlCommand("INSERT INTO us_lhistory(users_id, track_id) VALUES ( @LG , @traks_id )", db.GetConnection());
            NpgsqlCommand command_ins = new NpgsqlCommand("INSERT INTO us_lhistory(users_id, track_id) VALUES ( @LG , @traks_id )", db.GetConnection());
            
            command_ins.Parameters.Add("@LG", NpgsqlTypes.NpgsqlDbType.Integer).Value = LG;
            command_ins.Parameters.Add("@traks_id", NpgsqlTypes.NpgsqlDbType.Integer).Value = traks_id;

            int rowsAffected = command_ins.ExecuteNonQuery();

            Console.WriteLine($"{rowsAffected} запись(и) добавлено(ы).");
                db.closeConnection();
                //adapter.InsertCommand = command;
                //adapter.Fill(table);
            }
            //else MessageBox.Show("Система ИПИ (Интуитивно понятный интерфейс): Кнопка является пустой");

        }

        private void textBox2_Click(object sender, MouseButtonEventArgs e)
        {

            string No = textBox2.Content.ToString();
            if (box != No)
            {
                box = textBox2.Content.ToString();

                string path = @"c:\music\music";
                string fullPath = System.IO.Path.GetFullPath(path);
                //string path = @"..\Desktop\music\";

                if (Directory.GetFiles(fullPath).Length > 0)
                {
                    DirectoryInfo dirInfo = new DirectoryInfo(@"c:\music\music");

                    foreach (FileInfo file in dirInfo.GetFiles())
                    {
                        file.Delete();
                    }
                }



                opredel();
                zaprosi();
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
                zaprosi();
            }

        }

        private void textBox3_Click(object sender, MouseButtonEventArgs e)
        {
            string No = textBox3.Content.ToString();
            if (box != No) {
                box = textBox3.Content.ToString();
                check_file();
                opredel();
                zaprosi();
            }

        }
        private void textBox6_Click(object sender, MouseButtonEventArgs e)
        {
            string No = textBox6.Content.ToString();
            if (box != No) {
                box = textBox6.Content.ToString();
                check_file();
                opredel();
                zaprosi();
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
                zaprosi();
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
                zaprosi();
            }
                

        }

        private void textBox11_Click(object sender, MouseButtonEventArgs e)
        {
            string No = textBox11.Content.ToString();
            if (box != No) {
                box = textBox11.Content.ToString();
                check_file();
                opredel();
                zaprosi();
            }
                

        }
        private void textBox9_Click(object sender, MouseButtonEventArgs e)
        {
            string No = textBox9.Content.ToString();
            if (box != No) {
                box = textBox9.Content.ToString();
                check_file();
                opredel();
                zaprosi();
            }
                

        }

        private void textBox10_Click(object sender, MouseButtonEventArgs e)
        {
            string No = textBox10.Content.ToString();
            if (box != No) {
                box = textBox10.Content.ToString();
                check_file();
                opredel();
                zaprosi();
            }
                

        }
        private void textBox8_Click(object sender, MouseButtonEventArgs e)
        {
            string No = textBox8.Content.ToString();
            if (box != No) {
                box = textBox8.Content.ToString();
                check_file();
                opredel();
                zaprosi();
            }
                

        }

        /* int counter = 1;*/

        //string sql = "Server=localhost;port=5432;Database=Testing;User Id=postgres; Password=3572";

        

        private void StopSoundButton_Click(object sender, RoutedEventArgs e)
        {


            player.Pause();
            /*SoundPlayer.Stop();*/
            /*box = "";*/
            
        }


        private void Search_Click(object sender, RoutedEventArgs e)
        {
           

           

            DB db = new DB();


            db.openConnection();


            /*NpgsqlConnection sqlConnection = new NpgsqlConnection();*/

            /*sqlConnection.Open();*/
            NpgsqlCommand command = new NpgsqlCommand("SELECT track_id FROM tracks WHERE name LIKE @num OR artist_id = (SELECT artist_id FROM artists WHERE artist_name LIKE @pidoras) ", db.GetConnection());
            
            
            //command.Connection = sqlConnection;
            //command.CommandType = CommandType.Text;
            //command.CommandText = "SELECT name FROM audio WHERE name LIKE @num";

            string mask1 = "%";
            string mask2 = "%";
            string forms = SearchTextBox.Text;
            string result = mask1 + forms + mask2;

            //string pidoras 

            command.Parameters.Add("@num", NpgsqlTypes.NpgsqlDbType.Varchar).Value = result;
            command.Parameters.Add("@pidoras", NpgsqlTypes.NpgsqlDbType.Varchar).Value = result;

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
            int[] id = new int[10];
            
                if (dataReader.HasRows)
                {

                    while (dataReader.Read())
                    {
                    

                        sur[a] = dataReader.GetValue(0).ToString();
                        id[a] = int.Parse(sur[a]);
                        a = a + 1;
                        if (a == 10) break;
                    }

                }
            
            if (sur[0] == null)
            {
                MessageBox.Show("По вашему запросу мишки Фасбера не найдено");
            }
            command.Dispose();
            db.closeConnection();

            //db.openConnection();


            int c = 0;

            //БЛЯЯЯЯЯЯЯЯЯЯЯЯЯЯЯЯЯЯЯЯЯЯЯТЬ
            string[] SUKAsutulai = new string[10];
            

            while (c != 10)
            {

                    db.openConnection();
                    NpgsqlCommand commandSUKA = new NpgsqlCommand("SELECT name FROM tracks WHERE track_id = @ebani", db.GetConnection());
                    commandSUKA.Parameters.Add("@ebani", NpgsqlTypes.NpgsqlDbType.Integer).Value = id[c];
                    NpgsqlDataReader dataReaderSUKA = commandSUKA.ExecuteReader();
                    while (dataReaderSUKA.Read())
                    {


                        SUKAsutulai[c] = dataReaderSUKA.GetValue(0).ToString();


                    }
                     db.closeConnection();
                    commandSUKA.Dispose();

                    db.openConnection();
                    NpgsqlCommand commandsutulai = new NpgsqlCommand("SELECT artist_name FROM artists WHERE artist_id = (SELECT artist_id FROM tracks WHERE track_id = @pidor)", db.GetConnection());
                    commandsutulai.Parameters.Add("@pidor", NpgsqlTypes.NpgsqlDbType.Integer).Value = id[c];
                    NpgsqlDataReader dataReadersutulai = commandsutulai.ExecuteReader();
                while (dataReadersutulai.Read())
                {


                    SUKAsutulai[c] = dataReadersutulai.GetValue(0).ToString() + " - " + SUKAsutulai[c];







                }
                db.closeConnection();
                commandsutulai.Dispose();
                c++;
            }




            /*

            NpgsqlCommand commandblyat = new NpgsqlCommand("SELECT track_id FROM tracks WHERE name LIKE @num", db.GetConnection());

            string [] surblyat = new string[10];
            int[] blyatSUKA = new int[10];
            commandblyat.Parameters.Add("@num", NpgsqlTypes.NpgsqlDbType.Varchar).Value = result;

            int b = 0;
            a = 0;
            NpgsqlDataReader dataReaderL = commandblyat.ExecuteReader();
            if (dataReaderL.HasRows)
            {

                while (dataReaderL.Read())
                {
                   

                    surblyat[b] = dataReaderL.GetValue(0).ToString();
                    blyatSUKA[b] = int.Parse(surblyat[b]);
                    
                    NpgsqlCommand commandL = new NpgsqlCommand("SELECT artist_name FROM artists WHERE artist_id in (SELECT artist_id FROM tracks WHERE artist_id = num)", db.GetConnection());
                    commandL.Parameters.Add("@num", NpgsqlTypes.NpgsqlDbType.Integer).Value = blyatSUKA[b];
                    NpgsqlDataReader dataReade = commandL.ExecuteReader();

                    if (dataReade.HasRows)
                    {

                        while (dataReade.Read())
                        {


                            sur[a] = sur[a] + " - " + dataReade.GetValue(0).ToString();
                            a = a + 1;

                        }

                    }

                    commandL.Dispose();


                    b = b + 1;



                }

            }



            db.closeConnection();

            
            
            db.openConnection();

            a = 0;
            */















            //NpgsqlCommand commandL = new NpgsqlCommand("SELECT artist_name FROM artists WHERE artist_id in (SELECT artist_id FROM tracks WHERE name LIKE @num)", db.GetConnection());
            //***NpgsqlCommand commandL = new NpgsqlCommand("SELECT artist_name FROM artists WHERE artist_id in (SELECT artist_id FROM tracks WHERE artist_id = num)", db.GetConnection());
            //commandL.Parameters.Add("@num", NpgsqlTypes.NpgsqlDbType.Varchar).Value = result;
            //***commandL.Parameters.Add("@num", NpgsqlTypes.NpgsqlDbType.Varchar).Value = result;
            /*NpgsqlDataReader dataReade = commandL.ExecuteReader();
           
            if (dataReade.HasRows)
            {

                while (dataReade.Read())
                {


                    sur[a] = sur[a] + " - " + dataReade.GetValue(0).ToString();
                    a = a + 1;

                }

            }*/
            
            textBox2.Content = SUKAsutulai[0];
            textBox7.Content = SUKAsutulai[1];
            textBox3.Content = SUKAsutulai[2];
            textBox6.Content = SUKAsutulai[3];
            textBox4.Content = SUKAsutulai[4];
            textBox5.Content = SUKAsutulai[5];
            textBox11.Content = SUKAsutulai[6];
            textBox9.Content = SUKAsutulai[7];
            textBox10.Content = SUKAsutulai[8];
            textBox8.Content = SUKAsutulai[9];

            /********************************
            textBox2.Content = sur[0];
            textBox7.Content = sur[1];
            textBox3.Content = sur[2];
            textBox6.Content = sur[3];
            textBox4.Content = sur[4];
            textBox5.Content = sur[5];
            textBox11.Content = sur[6];
            textBox9.Content = sur[7];
            textBox10.Content = sur[8];
            textBox8.Content = sur[9];
            *********************************/


            /* if (dataReader.NextResult())
             {
                 while (dataReader.Read())
                 {

                     textBox3.Text = textBox3.Text + dataReader.GetValue(0).ToString();



                 }
             }
            */






            //sqlConnection.Close();

        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
            LoginWorm loginWorm = new LoginWorm();
            loginWorm.Show();
        }
        private void PlaySoundButton_Click(object sender, RoutedEventArgs e)
        {
            if (b == true)
            {
                Position_music.Maximum = player.NaturalDuration.TimeSpan.TotalSeconds;
                Progress.Maximum = player.NaturalDuration.TimeSpan.TotalSeconds;
                player.Play();
            }
            


        }
        /*private void PlaySoundButton_Click(object sender, RoutedEventArgs e)
        {
            if (player.NaturalDuration.HasTimeSpan)
            {
                Position_music.Maximum = player.NaturalDuration.TimeSpan.TotalSeconds;
                Progress.Maximum = player.NaturalDuration.TimeSpan.TotalSeconds;
            }
            else
            {
                // Здесь можно обработать случай, когда длительность не известна
                Position_music.Maximum = 100; // Или установите любое желаемое значение
                Progress.Maximum = 100; // Или установите любое желаемое значение
            }

            player.Play();
        }*/


        private void sliderPosition_ValueChanged1(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            player.Pause();
            player.Position = TimeSpan.FromSeconds(Position_music.Value);
            
            
        }

        private void Position_music_MouseLeave1(object sender, MouseEventArgs e)
        {
            player.Play();

        }

        
        private void Progress_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
           // Progress.Value = player.NaturalDuration.TimeSpan.Ticks;

            
        }


        void ticktock(object sender, EventArgs e)
        {
            if (!Progress.IsMouseCaptureWithin)
                
                Progress.Value = player.Position.TotalSeconds;
                

        }


        private void Style_Life_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
            Life life = new Life(LG);
            player.Stop();
            player.Close();
            life.Show();
        }

        private void Like_Click(object sender, RoutedEventArgs e)
        {
            if (b == true) { 
            DB db = new DB();

            db.openConnection();

            DataTable table = new DataTable();

            NpgsqlDataAdapter adapter = new NpgsqlDataAdapter();

            NpgsqlCommand command_ins = new NpgsqlCommand("INSERT INTO us_track(users_id, track_id) VALUES ( @LG , @traks_id )", db.GetConnection());

            command_ins.Parameters.Add("@LG", NpgsqlTypes.NpgsqlDbType.Integer).Value = LG;
            command_ins.Parameters.Add("@traks_id", NpgsqlTypes.NpgsqlDbType.Integer).Value = traks_id;

            int rowsAffected = command_ins.ExecuteNonQuery();
            Console.WriteLine($"{rowsAffected} запись(и) добавлено(ы).");
           

            }
            else
            {
                return;
            }
            
        }

        private void Account_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
            Account Account = new Account(LG);
            player.Stop();
            player.Close();
            Account.Show();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
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

     

















        /*private void textBox2_Click(object sender, EventArgs e)
        {
           string box = textBox2.Text;
           
        }*/
    }
}
