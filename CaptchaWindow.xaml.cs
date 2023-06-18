using MessBox;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
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

namespace Captcha
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        string cap;
        public MainWindow()
        {
            InitializeComponent();
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            var request = (HttpWebRequest)WebRequest.Create("http://f0476939.xsph.ru");
            request.CookieContainer = new CookieContainer();
            var response = (HttpWebResponse)request.GetResponse();
            cap = response.Cookies["cap"].Value;
            Stream stream = response.GetResponseStream();
            BitmapImage temp_image = new BitmapImage();
            temp_image.BeginInit();
            temp_image.StreamSource = stream;
            temp_image.EndInit();
            image.Source = temp_image;
        }
      

       

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (cap == proverka.Text)
            {

                WpfMessageBox.Show("Regestration sucess", "Sucess", MessageBoxButton.OK, MessageBoxImage.Information);

            }
            else
            {
                WpfMessageBox.Show("Connection Error", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                var request = (HttpWebRequest)WebRequest.Create("http://f0476939.xsph.ru");
                request.CookieContainer = new CookieContainer();
                var response = (HttpWebResponse)request.GetResponse();
                cap = response.Cookies["cap"].Value;
                Stream stream = response.GetResponseStream();
                BitmapImage temp_image = new BitmapImage();
                temp_image.BeginInit();
                temp_image.StreamSource = stream;
                temp_image.EndInit();
                image.Source = temp_image;



            }
        }
    }
}
