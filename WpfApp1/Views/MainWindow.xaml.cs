using System;
using System.Windows;
using System.Windows.Input;
using Innovative;
using MessBox;
using WpfApp1.Models;
using TrueMainForm;
using System.Threading;
using System.Security.Principal;
using System.Diagnostics;
using System.IO;

namespace WpfApp1
{

    public partial class MainWindow : IDisposable
    {
        public MainWindow()
        {
            InitializeComponent();

            var check = new Thread(CheckConnection);
            check.Start();
            check.IsBackground = true;

        }
        #region Public Objects     

        public string[] zip_path;
        public static bool flag=true;
        public bool ld_f = false;

        

        ~MainWindow()
        {
            Dispose();
        }


        #endregion

        #region Change Visible
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Rec.Visibility = (System.Windows.Visibility)
Enum.Parse(typeof(System.Windows.Visibility), "Hidden");
            Reg.Visibility = (System.Windows.Visibility)
 Enum.Parse(typeof(System.Windows.Visibility), "Visible");
        }



        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            Reg.Visibility = (System.Windows.Visibility)
Enum.Parse(typeof(System.Windows.Visibility), "Hidden");
            Rec.Visibility = (System.Windows.Visibility)
 Enum.Parse(typeof(System.Windows.Visibility), "Visible");
        }

        private void Button_MouseEnter(object sender, MouseEventArgs e)
        {
            show_pass.Text = pass.Password;
            show_pass.Visibility = (Visibility)
Enum.Parse(typeof(Visibility), "Visible");
            pass.Visibility = (Visibility)
Enum.Parse(typeof(Visibility), "Hidden");

        }

        private void Button_MouseLeave(object sender, MouseEventArgs e)
        {
            pass.Visibility = (Visibility)
Enum.Parse(typeof(Visibility), "Visible");
            show_pass.Visibility = (Visibility)
 Enum.Parse(typeof(Visibility), "Hidden");
        }


        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Reg.Visibility = (System.Windows.Visibility)
           Enum.Parse(typeof(System.Windows.Visibility), "Hidden");
            Rec.Visibility = (System.Windows.Visibility)
Enum.Parse(typeof(System.Windows.Visibility), "Hidden");
        }
        #endregion

        #region Red TextBox
        private void login_LostFocus(object sender, RoutedEventArgs e)
        {
            if (!DBclass.check_rex(login.Text))
            {
                WpfMessageBox.Show("Incorrect login!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                login.Foreground = System.Windows.Media.Brushes.Red;
            }
            else login.Foreground = System.Windows.Media.Brushes.Black;
        }

        private void regpass_LostFocus(object sender, RoutedEventArgs e)
        {
            if (!DBclass.check_rex(regpass.Text))
            {
                WpfMessageBox.Show("Incorrect password!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                regpass.Foreground = System.Windows.Media.Brushes.Red;
            }
            else regpass.Foreground = System.Windows.Media.Brushes.Black;
        }

        private void mail_LostFocus(object sender, RoutedEventArgs e)
        {
            if (DBclass.check_mail(mail.Text))
            {
                WpfMessageBox.Show("Incorrect email!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                mail.Foreground = System.Windows.Media.Brushes.Red;
            }
            else mail.Foreground = System.Windows.Media.Brushes.Black;

        }
        #endregion

        void Wait_Authorizatuon()
        {
            Dispatcher.BeginInvoke(System.Windows.Threading.DispatcherPriority.Normal, (ThreadStart)delegate ()
            {
                if (Aut.IsChecked == true)
                {
                    if (Properties.Settings.Default.Login != "" && Properties.Settings.Default.Path != "")
                    {
                        LocalClass.save_me_aut(ga.Text, DBclass.getEmail(this), pass.Password, LocalClass_Zip.getPath_Aut());
                        TrueMainForm.MainWindow tmf = new TrueMainForm.MainWindow(ga.Text, pass.Password, false);
                        tmf.Loaded += Tmf_Loaded;
                        tmf.Show();
                        tmf.Closing += Tmf_Closing;
                    }
                    else
                        WpfMessageBox.Show("Local saves not found", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
             else    if (System.Net.NetworkInformation.NetworkInterface.GetIsNetworkAvailable())
            {
                DBclass.openConnection();
                if (DBclass.getAuthentication(ga.Text, pass.Password))
                {
                    if (Aut.IsChecked == true) LocalClass.save_me_aut(ga.Text, DBclass.getEmail(this), pass.Password, DBclass.getPath(this));

                    if (DBclass.GetID_Role(ga.Text, pass.Password) == "2")
                    {

                        using (Admin.MainWindow tmf2 = new Admin.MainWindow())
                        {
                            tmf2.Loaded += Tmf_Loaded;
                            tmf2.Show();
                            tmf2.Closing += Tmf_Closing;
                            tmf2.Dispose();
                            Dispose();
                        }

                    }
                    else
                    {
                        using (TrueMainForm.MainWindow tmf = new TrueMainForm.MainWindow(ga.Text, pass.Password, flag))
                        {
                            tmf.Loaded += Tmf_Loaded;
                            tmf.Show();
                            tmf.Closing += Tmf_Closing;
                            tmf.Dispose();
                            Dispose();
                        }


                    }
                }
            }
            else if (!System.Net.NetworkInformation.NetworkInterface.GetIsNetworkAvailable() && Aut.IsChecked == false)
                WpfMessageBox.Show("Connection Error", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
          
               
            });
            Dispatcher.Invoke(() =>
            { ld.Visibility = Visibility.Hidden;  });
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
           
            Thread t = new Thread(Wait_Authorizatuon);
            t.SetApartmentState(ApartmentState.STA);
            t.Start();
            ld.Visibility = Visibility.Visible;
           

        }
        

        private void Tmf_Closing(object sender, EventArgs e)
        {
           // ld.Visibility = Visibility.Collapsed;
            Show();
            
        }

        private void Tmf_Loaded(object sender, RoutedEventArgs e)
        {
            Hide();

        }

        void Resitration_Wait()
        {
            Dispatcher.BeginInvoke(System.Windows.Threading.DispatcherPriority.Normal, (ThreadStart)delegate ()
            {
                if (System.Net.NetworkInformation.NetworkInterface.GetIsNetworkAvailable())
            {
                DBclass.CloseConnection();
                if (regpass.Text == conf_pass.Text)
                {

                    DBclass.openConnection();
  
                         if (!(login.Text == "" && regpass.Text == "" && phone.Text == "" && name.Text == "")) 
                        { 
                            DBclass.registration(login.Text, regpass.Text, mail.Text, phone.Text, name.Text, this); 
                        }

 
                }

                else WpfMessageBox.Show("Regestration form is filled incorrectly", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                DBclass.CloseConnection();
            }
            else WpfMessageBox.Show("Connection Error", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            });
            Dispatcher.Invoke(() => ld.Visibility = Visibility.Hidden);
        }

       
        private void Button_Click_4(object sender, RoutedEventArgs e)
        {

            Thread t = new Thread(Resitration_Wait);
            t.SetApartmentState(ApartmentState.STA);
            t.Start();

            ld.Visibility = Visibility.Visible;
        }

        private void Button_Click_5(object sender, RoutedEventArgs e)
        {
            if (System.Net.NetworkInformation.NetworkInterface.GetIsNetworkAvailable())
            {
                DBclass.openConnection();
                DBclass.recovery(recLog.Text);
            }
            else WpfMessageBox.Show("Connection Error", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
           
            this.ResizeMode = ResizeMode.NoResize;
           // Save.IsEnabled = false;
            ga.Text =Properties.Settings.Default.Login;
            pass.Password = Properties.Settings.Default.Password;
            if (ga.Text != ""&& pass.Password!="") Save.IsChecked = true;
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (Save.IsChecked == true) LocalClass.save_me_aut(ga.Text,DBclass.getEmail(this) , pass.Password, DBclass.getPath(this));
            else LocalClass.fogot_me();
            Application.Current.Shutdown();
          
        }

     

        private void Button_Click_6(object sender, RoutedEventArgs e)
        {

            Application.Current.Shutdown();
        }

       

        private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }

      

        private void Save_Checked(object sender, RoutedEventArgs e)
        {
          
        }

        private void CheckBox_Click(object sender, RoutedEventArgs e)
        {
            flag = !flag;
        }

        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {

        }

        public void Dispose()
        {
            GC.Collect();
            GC.WaitForPendingFinalizers();
        }
       

        private  void CheckConnection()
        { 
            while(true)
            {
                if(!System.Net.NetworkInformation.NetworkInterface.GetIsNetworkAvailable())
                {
                   Dispatcher.BeginInvoke(System.Windows.Threading.DispatcherPriority.Normal, (ThreadStart)delegate ()
                     {
                         //WpfMessageBox.Show("Connection Error", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                         Togle.IsChecked = false;

                     });
                        
                }
                else
                {
                    Dispatcher.BeginInvoke(System.Windows.Threading.DispatcherPriority.Normal, (ThreadStart)delegate ()
                    {
                        if (System.Net.NetworkInformation.NetworkInterface.GetIsNetworkAvailable())
                        {
                            Togle.IsChecked = true;
                        }
                    });
                }
                Thread.Sleep(3000);
            }
        }

        private void Aut_Checked(object sender, RoutedEventArgs e)
        {

        }

        

        private void Button_Click_7(object sender, RoutedEventArgs e)
        {

            
            Process proc = new Process();
            proc.StartInfo.UseShellExecute = true;
            proc.StartInfo.RedirectStandardOutput = false;
            proc.StartInfo.ErrorDialog = false;
            proc.StartInfo.LoadUserProfile = true;
            proc.StartInfo.FileName = Directory.GetCurrentDirectory() + "\\UserGuid.docx";
            //  WpfMessageBox.Show(LocalClass_Zip.OpenFile(this, lv_file.SelectedIndex, timout_files));
            proc.StartInfo.WindowStyle = ProcessWindowStyle.Normal;
            proc.Start();
           
            //proc.

            try
            {
                proc.WaitForExit();

            }
            catch { };
        }

        private void About_Click_1(object sender, RoutedEventArgs e)
        {
            WpfMessageBox.Show("PKS Disck - is a program for using a secure information file server\n" +
               "Author of the program - Guriew Daniil Vadimovich\n" +
               "GurKrivMam production, 2020\n" +
               "Version 1.0.5", "About programm", MessageBoxImage.Question);
        }
    }

    
}
