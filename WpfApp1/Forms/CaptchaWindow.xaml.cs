using MessBox;
using System.IO;
using System.Net;
using System.Windows;
using System.Windows.Media.Imaging;

namespace Captcha
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        
        
            string cap;
        public static bool flag=false;
        public static int _try = 0;
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
            _try++;
                if (cap == proverka.Text)
                {

                    WpfMessageBox.Show("Sucess", "Sucess", MessageBoxButton.OK, MessageBoxImage.Information);
                flag = true;
                Close();

                }
                else
                {
                    WpfMessageBox.Show("Incorrect CAPTCHA", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
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

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            _try++;
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
        public bool retrurn_flag()
        {
            
            return flag;
        }
    }
    
}
