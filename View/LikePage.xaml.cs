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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace life.View
{
    /// <summary>
    /// Логика взаимодействия для Like.xaml
    /// </summary>
    public partial class LikePage : Page
    {
        private bool isStarClicked = false;

        public LikePage()
        {
            InitializeComponent();
        }






        private void Star1_Click(object sender, RoutedEventArgs e)
        {
            // Переключаем состояние флага
            isStarClicked = !isStarClicked;

            // Меняем изображение в зависимости от состояния
            var imageControl = (Image)Star1.Template.FindName("ButtonImage1", Star1);
            if (imageControl != null)
            {
                if (isStarClicked)
                {
                    imageControl.Source = new BitmapImage(new Uri("Rec/Icons/star3.png", UriKind.Relative));
                }
                else
                {
                    imageControl.Source = new BitmapImage(new Uri("Rec/Icons/star1.png", UriKind.Relative));
                }
            }
        }





        private void Star2_Click(object sender, RoutedEventArgs e)
        {
            // Переключаем состояние флага
            isStarClicked = !isStarClicked;

            // Меняем изображение для Star2
            var imageControl2 = (Image)Star2.Template.FindName("ButtonImage2", Star2);
            if (imageControl2 != null)
            {
                if (isStarClicked)
                {
                    imageControl2.Source = new BitmapImage(new Uri("Rec/Icons/star3.png", UriKind.Relative));
                }
                else
                {
                    imageControl2.Source = new BitmapImage(new Uri("Rec/Icons/star1.png", UriKind.Relative));
                }
            }

            // Меняем изображение для Star1
            var imageControl1 = (Image)Star1.Template.FindName("ButtonImage1", Star1);
            if (imageControl1 != null)
            {
                if (isStarClicked)
                {
                    imageControl1.Source = new BitmapImage(new Uri("Rec/Icons/star3.png", UriKind.Relative));
                }
                else
                {
                    imageControl1.Source = new BitmapImage(new Uri("Rec/Icons/star1.png", UriKind.Relative));
                }
            }
        }






        private void Star3_Click(object sender, RoutedEventArgs e)
        {
            // Переключаем состояние флага
            isStarClicked = !isStarClicked;


            // Меняем изображение для Star3
            var imageControl3 = (Image)Star3.Template.FindName("ButtonImage3", Star3);
            if (imageControl3 != null)
            {
                if (isStarClicked)
                {
                    imageControl3.Source = new BitmapImage(new Uri("Rec/Icons/star3.png", UriKind.Relative));
                }
                else
                {
                    imageControl3.Source = new BitmapImage(new Uri("Rec/Icons/star1.png", UriKind.Relative));
                }
            }

            // Меняем изображение для Star2
            var imageControl2 = (Image)Star2.Template.FindName("ButtonImage2", Star2);
            if (imageControl2 != null)
            {
                if (isStarClicked)
                {
                    imageControl2.Source = new BitmapImage(new Uri("Rec/Icons/star3.png", UriKind.Relative));
                }
                else
                {
                    imageControl2.Source = new BitmapImage(new Uri("Rec/Icons/star1.png", UriKind.Relative));
                }
            }

            // Меняем изображение для Star1
            var imageControl1 = (Image)Star1.Template.FindName("ButtonImage1", Star1);
            if (imageControl1 != null)
            {
                if (isStarClicked)
                {
                    imageControl1.Source = new BitmapImage(new Uri("Rec/Icons/star3.png", UriKind.Relative));
                }
                else
                {
                    imageControl1.Source = new BitmapImage(new Uri("Rec/Icons/star1.png", UriKind.Relative));
                }
            }
        }








        private void Star4_Click(object sender, RoutedEventArgs e)
        {
            // Переключаем состояние флага
            isStarClicked = !isStarClicked;




            // Меняем изображение для Star4
            var imageControl4 = (Image)Star4.Template.FindName("ButtonImage4", Star4);
            if (imageControl4 != null)
            {
                if (isStarClicked)
                {
                    imageControl4.Source = new BitmapImage(new Uri("Rec/Icons/star3.png", UriKind.Relative));
                }
                else
                {
                    imageControl4.Source = new BitmapImage(new Uri("Rec/Icons/star1.png", UriKind.Relative));
                }
            }


            // Меняем изображение для Star3
            var imageControl3 = (Image)Star3.Template.FindName("ButtonImage3", Star3);
            if (imageControl3 != null)
            {
                if (isStarClicked)
                {
                    imageControl3.Source = new BitmapImage(new Uri("Rec/Icons/star3.png", UriKind.Relative));
                }
                else
                {
                    imageControl3.Source = new BitmapImage(new Uri("Rec/Icons/star1.png", UriKind.Relative));
                }
            }

            // Меняем изображение для Star2
            var imageControl2 = (Image)Star2.Template.FindName("ButtonImage2", Star2);
            if (imageControl2 != null)
            {
                if (isStarClicked)
                {
                    imageControl2.Source = new BitmapImage(new Uri("Rec/Icons/star3.png", UriKind.Relative));
                }
                else
                {
                    imageControl2.Source = new BitmapImage(new Uri("Rec/Icons/star1.png", UriKind.Relative));
                }
            }

            // Меняем изображение для Star1
            var imageControl1 = (Image)Star1.Template.FindName("ButtonImage1", Star1);
            if (imageControl1 != null)
            {
                if (isStarClicked)
                {
                    imageControl1.Source = new BitmapImage(new Uri("Rec/Icons/star3.png", UriKind.Relative));
                }
                else
                {
                    imageControl1.Source = new BitmapImage(new Uri("Rec/Icons/star1.png", UriKind.Relative));
                }
            }
        }






        private void Star5_Click(object sender, RoutedEventArgs e)
        {
            // Переключаем состояние флага
            isStarClicked = !isStarClicked;


            // Меняем изображение для Star4
            var imageControl5 = (Image)Star5.Template.FindName("ButtonImage5", Star5);
            if (imageControl5 != null)
            {
                if (isStarClicked)
                {
                    imageControl5.Source = new BitmapImage(new Uri("Rec/Icons/star3.png", UriKind.Relative));
                }
                else
                {
                    imageControl5.Source = new BitmapImage(new Uri("Rec/Icons/star1.png", UriKind.Relative));
                }
            }




            // Меняем изображение для Star4
            var imageControl4 = (Image)Star4.Template.FindName("ButtonImage4", Star4);
            if (imageControl4 != null)
            {
                if (isStarClicked)
                {
                    imageControl4.Source = new BitmapImage(new Uri("Rec/Icons/star3.png", UriKind.Relative));
                }
                else
                {
                    imageControl4.Source = new BitmapImage(new Uri("Rec/Icons/star1.png", UriKind.Relative));
                }
            }


            // Меняем изображение для Star3
            var imageControl3 = (Image)Star3.Template.FindName("ButtonImage3", Star3);
            if (imageControl3 != null)
            {
                if (isStarClicked)
                {
                    imageControl3.Source = new BitmapImage(new Uri("Rec/Icons/star3.png", UriKind.Relative));
                }
                else
                {
                    imageControl3.Source = new BitmapImage(new Uri("Rec/Icons/star1.png", UriKind.Relative));
                }
            }

            // Меняем изображение для Star2
            var imageControl2 = (Image)Star2.Template.FindName("ButtonImage2", Star2);
            if (imageControl2 != null)
            {
                if (isStarClicked)
                {
                    imageControl2.Source = new BitmapImage(new Uri("Rec/Icons/star3.png", UriKind.Relative));
                }
                else
                {
                    imageControl2.Source = new BitmapImage(new Uri("Rec/Icons/star1.png", UriKind.Relative));
                }
            }

            // Меняем изображение для Star1
            var imageControl1 = (Image)Star1.Template.FindName("ButtonImage1", Star1);
            if (imageControl1 != null)
            {
                if (isStarClicked)
                {
                    imageControl1.Source = new BitmapImage(new Uri("Rec/Icons/star3.png", UriKind.Relative));
                }
                else
                {
                    imageControl1.Source = new BitmapImage(new Uri("Rec/Icons/star1.png", UriKind.Relative));
                }
            }
        }















    }



}
