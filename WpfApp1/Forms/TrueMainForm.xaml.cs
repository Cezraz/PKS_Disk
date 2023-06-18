using Ionic.Zip;
using MessBox;
using MySql.Data.MySqlClient;
using System;
using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Forms;
using TrueMainForm.Models;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Threading;

namespace TrueMainForm
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>

    public partial class MainWindow : IDisposable
    {
        
        public static string timout_files = Directory.GetLogicalDrives()[0] + "PksDisck\\timeout_files";

        public static string us_log;
        public static string us_pass;
        public static bool flag;

        protected static MySqlConnection ClientConnect { get; set; }

        public MainWindow(string log, string pas, bool f)
        {
            InitializeComponent();

            us_log = log;
            us_pass = pas;
            flag = f;
            var check = new Thread(CheckConnection);
            check.Start();
            check.IsBackground = true;

        }
        ~MainWindow()
        {
            this.Dispose();
        }

        void Window_Loaded_Wait()
        {
            
            Dispatcher.BeginInvoke(System.Windows.Threading.DispatcherPriority.Normal, (ThreadStart)delegate ()
            {
                if(Directory.Exists(timout_files))
                    try
                    {
                        Directory.Delete(timout_files, true);
                    }
                    catch (Exception ex) { }
                


          
            if (flag)
            {
                WpfMessageBox.Show("Current Location of Local Storage: " + BDClient.getPath() + "\\" + us_log + "Files.zip", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
                if (!File.Exists(BDClient.getPath() + "\\" + us_log + "Files.zip") || BDClient.getPath() == "")
                {
                    BDClient.createDefaultFolder();

                }
               
                    LocalClass_Zip.insertFromZip(this);
                    if (!FTPClass_FTP.FTPCheckFolder(us_log))
                        FTPClass_FTP.FTPCreateFolder();
                    FTPClass_FTP.InsertFromFtp(this);
              
               
            }
            else
            {
                if (!File.Exists(LocalClass_Zip.getPath_Aut() + "\\" + us_log + "Files.zip") || LocalClass_Zip.getPath_Aut() == "")
                    BDClient.createDefaultFolder();

                LocalClass_Zip.insertFromZip_Aut(this);
                AddFTP_button.Visibility = Visibility.Hidden;
                lv_FTP.Visibility = Visibility.Collapsed;
                danya.ColumnDefinitions[1].Width = new GridLength(0);
                win.Width -= 400;
                    Thickness positionLeftpic = new Thickness(-200, 1, 0, 0);
                    ldPic.Margin = positionLeftpic;
                Thickness positionLeft = new Thickness(-30, 1, 0, 0);
                exit.Margin = positionLeft;
                    WpfMessageBox.Show("Current Location of Local Storage: " + LocalClass_Zip.getPath_Aut() + "\\" + us_log + "Files.zip", "Information", MessageBoxButton.OK, MessageBoxImage.Information);

                }
            });

            Dispatcher.Invoke(() => { ld.Visibility = Visibility.Hidden; });
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {


            Thread t = new Thread(Window_Loaded_Wait);
            t.SetApartmentState(ApartmentState.STA);
            t.Start();
            Process proc = new Process();
            ld.Visibility = Visibility.Visible;
            lv_file.MouseDoubleClick += (s, a) =>
            {

                //try
                //{
                    
                    proc.StartInfo.UseShellExecute = true;
                    proc.StartInfo.RedirectStandardOutput = false;
                proc.StartInfo.ErrorDialog = false;
                    proc.StartInfo.FileName = LocalClass_Zip.OpenFile(this, lv_file.SelectedIndex, timout_files);
                  //  WpfMessageBox.Show(LocalClass_Zip.OpenFile(this, lv_file.SelectedIndex, timout_files));
                    proc.StartInfo.WindowStyle = ProcessWindowStyle.Normal;
                    proc.Start();

                try
                {
                    proc.WaitForExit();
  
                }
                catch { };
               
                
            };
            lv_file.LostFocus += (s, a) =>
              {
                  try
                  {
                      Directory.Delete(timout_files, true);
                  }
                  catch { };
              };

        }

        private void Window_Closed(object sender, EventArgs e)
        {
            this.Close();


        }
        void Add_button_Click_Wait()
        {
            System.Windows.Forms.OpenFileDialog ofd = new System.Windows.Forms.OpenFileDialog();

            ofd.Reset();
            ofd.Multiselect = true;
            if (ofd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {

                if (!Directory.Exists(timout_files))
                {
                    Directory.CreateDirectory(timout_files);
                }

                try
                {
                    foreach (string fileName in ofd.FileNames)
                    {
                        File.Copy(fileName, timout_files + "\\" + Path.GetFileName(fileName));
                    }
                }
                catch (Exception ex)
                {

                    string[] temp = ex.Message.Split(' ');

                    if (temp[2] == "уже" && temp[3] == "существует.") WpfMessageBox.Show("This file has already been added to your local storage.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }

            }
            ofd.Dispose();
            ofd.Reset();
            save_zip();
            try
            {
                Directory.Delete(timout_files, true);
            }
            catch (Exception ex) { };
            Dispatcher.BeginInvoke(System.Windows.Threading.DispatcherPriority.Normal, (ThreadStart)delegate ()
            {
                if (flag)
                LocalClass_Zip.insertFromZip(this);
            else LocalClass_Zip.insertFromZip_Aut(this);
            });
            Dispatcher.Invoke(() => ld.Visibility = Visibility.Hidden);
        }


        private void Add_button_Click(object sender, RoutedEventArgs e)
        {
            Thread t = new Thread(Add_button_Click_Wait);
            t.SetApartmentState(ApartmentState.STA);
            t.Start();

            ld.Visibility = Visibility.Visible;
        }

        private void AddFolder_button_Click(object sender, RoutedEventArgs e)
        {

            var fbd = new FolderBrowserDialog();



            if (fbd.ShowDialog() == System.Windows.Forms.DialogResult.OK && !string.IsNullOrWhiteSpace(fbd.SelectedPath))
            {
                if (!Directory.Exists(timout_files))
                {
                    Directory.CreateDirectory(timout_files);
                }


                try
                {
                    string[] temp = fbd.SelectedPath.Split('\\');
                    Directory.CreateDirectory(timout_files + "\\" + temp[temp.Length - 1]);
                    DirectoryInfo sourceDir = new DirectoryInfo(fbd.SelectedPath);
                    DirectoryInfo destinationDir = new DirectoryInfo(timout_files + "\\" + temp[temp.Length - 1]);
                    CopyDirectory(sourceDir, destinationDir);

                }
                catch (Exception ex)
                {

                }

            }


            save_zip();
            try
            {
                Directory.Delete(timout_files, true);
            }
            catch (Exception ex) { };
            LocalClass_Zip.insertFromZip(this);


        }



        void save_zip()
        {
            //try
            //{

            ReadOptions a = new ReadOptions();
            ZipFile zip;
            a.Encoding = Encoding.UTF8;
            if (flag)
            {
                zip = ZipFile.Read(BDClient.getPath() + "\\" + us_log + "Files.zip", a);

            }
            else
            {
                zip = ZipFile.Read(LocalClass_Zip.getPath_Aut() + "\\" + us_log + "Files.zip", a);

            }
            zip.Dispose();
            zip.CompressionLevel = Ionic.Zlib.CompressionLevel.BestCompression; // Задаем максимальную степень сжатия
            zip.Encryption = EncryptionAlgorithm.WinZipAes256;
            zip.Password = us_log + us_pass;

            if (Directory.Exists(timout_files))
                try
                {
                    var dir = new DirectoryInfo(timout_files);
         
                    foreach (FileInfo file in dir.GetFiles())
                    {
                      //WpfMessageBox.Show( file.Split('\\')[file.Split('\\').Length-1] + ".crypt");
                        MyEcoding.Encrypt(file.FullName,  file.Name + ".crypt");
                        zip.AddFile(file.Name+ ".crypt");
                        File.Delete(file.FullName);
                    }
                   // zip.AddDirectory(timout_files); // Кладем в архив папку вместе с содежимым
                }
                catch (Exception ex)
                {
                    WpfMessageBox.Show(ex.Message);

                }
            try
            {
                if (flag)
                {
                    zip.Save(BDClient.getPath() + "\\" + us_log + "Files.zip");
                }
                else zip.Save(LocalClass_Zip.getPath_Aut() + "\\" + us_log + "Files.zip");
            }
            catch (Exception ex)
            {

                WpfMessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                if (Directory.Exists(timout_files))
                    Directory.Delete(timout_files, true);
            }




        }

        private void CopyDirectory(DirectoryInfo source, DirectoryInfo destination)
        {
            if (!destination.Exists)
            {
                destination.Create();
            }


            FileInfo[] files = source.GetFiles();
            foreach (FileInfo file in files)
            {
                file.CopyTo(Path.Combine(destination.FullName,
                file.Name));
            }


            DirectoryInfo[] dirs = source.GetDirectories();
            foreach (DirectoryInfo dir in dirs)
            {

                string destinationDir = Path.Combine(destination.FullName, dir.Name);


                CopyDirectory(dir, new DirectoryInfo(destinationDir));
            }

        }


        private void ChangeLocation_button_Click_Wait()
        {
            var fbd = new FolderBrowserDialog();
            fbd.Dispose();
            string oldPath;
            if (flag)
            {
                oldPath = BDClient.getPath();
            }
            else oldPath = LocalClass_Zip.getPath_Aut();

            try
            {
                if (fbd.ShowDialog() == System.Windows.Forms.DialogResult.OK && !string.IsNullOrWhiteSpace(fbd.SelectedPath))
                {


                    File.Move(oldPath + "\\" + us_log + "Files.zip", fbd.SelectedPath + "\\" + us_log + "Files.zip");
                    fbd.Dispose();
                    Dispatcher.BeginInvoke(System.Windows.Threading.DispatcherPriority.Normal, (ThreadStart)delegate ()
                    {
                        if (flag)
                    {

                        BDClient.updateUserInfo(fbd.SelectedPath);
                        save_zip();
                        LocalClass_Zip.insertFromZip(this);
                        WpfMessageBox.Show("New location: " + BDClient.getPath(), "Succes", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                    else
                    {
                        BDClient.updateUserInfo_Aut(fbd.SelectedPath);
                        save_zip();

                        LocalClass_Zip.insertFromZip_Aut(this);
                        WpfMessageBox.Show("New location: " + LocalClass_Zip.getPath_Aut(), "Succes", MessageBoxButton.OK, MessageBoxImage.Information);
                    }

                    });
                   
                }
                Dispatcher.Invoke(() => ld.Visibility = Visibility.Hidden);
            }
            catch (System.IO.IOException) { };

        }
        private void ChangeLocation_button_Click(object sender, RoutedEventArgs e)
        {

            Thread t = new Thread(ChangeLocation_button_Click_Wait);
            t.SetApartmentState(ApartmentState.STA);
            t.Start();

            ld.Visibility = Visibility.Visible;

        }

        void AppendFtp_Wait()
        {
            if (System.Net.NetworkInformation.NetworkInterface.GetIsNetworkAvailable())
            {
                System.Windows.Forms.OpenFileDialog ofd = new System.Windows.Forms.OpenFileDialog();
                if (ofd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    FileInfo info = new FileInfo(ofd.FileName);
                    FTPClass_FTP.AppendFTP(this, info);
                }
                Dispatcher.BeginInvoke(System.Windows.Threading.DispatcherPriority.Normal, (ThreadStart)delegate ()
                {
                    FTPClass_FTP.InsertFromFtp(this);
                });
            }
            else
            {
                
                WpfMessageBox.Show("Connection Error", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            Dispatcher.Invoke(() => ld.Visibility = Visibility.Hidden);
        }
        private void AddFTP_button_Click(object sender, RoutedEventArgs e)
        {
            Thread t = new Thread(AppendFtp_Wait);
            t.SetApartmentState(ApartmentState.STA);
            t.Start();

            ld.Visibility = Visibility.Visible;

        }

        private void Window_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            this.DragMove();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();


        }
        void DownLoad_Wait()
        {
    
                var fbd = new FolderBrowserDialog();
                try
                {
                    if (fbd.ShowDialog() == System.Windows.Forms.DialogResult.OK && !string.IsNullOrWhiteSpace(fbd.SelectedPath))
                    {
                        Dispatcher.BeginInvoke(System.Windows.Threading.DispatcherPriority.Normal, (ThreadStart)delegate ()
                        {

                            if (lv_file.SelectedIndex != -1)
                        {

                            LocalClass_Zip.Extract(this, lv_file.SelectedIndex, fbd.SelectedPath);
                            if (flag)
                                LocalClass_Zip.insertFromZip(this);
                            else
                                LocalClass_Zip.insertFromZip_Aut(this);
                            WpfMessageBox.Show("Extracted to: " + fbd.SelectedPath, "Succes", MessageBoxButton.OK, MessageBoxImage.Information);
                                
                        }
                        else if (lv_FTP.SelectedIndex != -1)
                        {
                            if (System.Net.NetworkInformation.NetworkInterface.GetIsNetworkAvailable())
                            {
                                FTPClass_FTP.pamagite_Ftp(this, lv_FTP.SelectedIndex + 2, fbd.SelectedPath);
                                
                                    FTPClass_FTP.InsertFromFtp(this);
                            }
                            else
                            {
                                WpfMessageBox.Show("Connection Error", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                            }

                            }
                        });


                }
                   

                }
                catch (System.IO.IOException) { };
            Dispatcher.Invoke(() => ld.Visibility = Visibility.Hidden);
        }
           
           
        
        private void DownLoad_Click(object sender, RoutedEventArgs e)
        {
            Thread t = new Thread(DownLoad_Wait);
            t.SetApartmentState(ApartmentState.STA);
            t.Start();

            ld.Visibility = Visibility.Visible;


        }

        void Delete_Wait()
        {
            Dispatcher.BeginInvoke(System.Windows.Threading.DispatcherPriority.Normal, (ThreadStart)delegate ()
            {
            if (lv_FTP.SelectedIndex != -1)
            {
                if (System.Net.NetworkInformation.NetworkInterface.GetIsNetworkAvailable())
                {
                    FTPClass_FTP.DeleteFTP(this, lv_FTP.SelectedIndex+2);
                    
                        FTPClass_FTP.InsertFromFtp(this);
                    
                }
                else
                {
                    
                    WpfMessageBox.Show("Connection Error", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            });
            Dispatcher.Invoke(() => ld.Visibility = Visibility.Hidden);
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Thread t = new Thread(Delete_Wait);
            t.SetApartmentState(ApartmentState.STA);
            t.Start();

            ld.Visibility = Visibility.Visible;

        }


        private void CheckConnection()
        {
            while (true)
            {
                if (!System.Net.NetworkInformation.NetworkInterface.GetIsNetworkAvailable())
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

        public void Dispose()
        {
            GC.Collect();
            GC.WaitForPendingFinalizers();
        }

    }


}
