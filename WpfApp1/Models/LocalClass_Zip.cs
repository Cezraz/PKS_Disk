using Ionic.Zip;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using MessBox;
using System.Runtime.InteropServices;
using System.Drawing;
using System.Windows;
using System.Windows.Media.Imaging;
using System.Windows.Interop;
using System.ComponentModel;
using System.Windows.Data;
using System.Windows.Controls;
using System.IO;
using TrueMainForm.Models;

namespace TrueMainForm
{
    public class LocalClass_Zip
    {


        public static ObservableCollection<FSFile> collection = new ObservableCollection<FSFile>();
    
        public static void insertFromZip(MainWindow mw)
        {
            ReadOptions a = new ReadOptions();
            a.Encoding = Encoding.UTF8;
            var archive = ZipFile.Read(BDClient.getPath() + "\\" + MainWindow.us_log + "Files.zip", a);
            ICollectionView view = CollectionViewSource.GetDefaultView(collection);
            view.Refresh();
            collection.Clear();
            foreach (var e in archive.Entries)
            {
                FSFile file = new FSFile();
                if (e.IsDirectory)
                    file.Face = FSFile.ImageSourceFromBitmap(WpfApp1.Properties.Resources.folder);
                else
                    file.Face = FSFile.ImageSourceFromBitmap(WpfApp1.Properties.Resources.document);
                file.file = e.FileName.Split('/')[e.FileName.Split('/').Length - 1];
                collection.Add(file);
            }


            mw.lv_file.ItemsSource = collection;
            archive.Dispose();

        }
        public static string getPath_Aut()
        {
            //WpfMessageBox.Show(File.ReadAllText("save_user.txt").Split(' ')[2]);
            return WpfApp1.Properties.Settings.Default.Path;

        }

        public static void Extract(MainWindow mw, int num, string _path)
        {
            if(num==-1)
            { WpfMessageBox.Show("No item is selected", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            int i = 0;
            ReadOptions a = new ReadOptions();
            a.Encoding = Encoding.UTF8;
            ZipFile archive;
            if (!System.Net.NetworkInformation.NetworkInterface.GetIsNetworkAvailable())
            {
                archive = ZipFile.Read(LocalClass_Zip.getPath_Aut() + "\\" + MainWindow.us_log + "Files.zip", a);
            }
            else
            {
                archive = ZipFile.Read(BDClient.getPath() + "\\" + MainWindow.us_log + "Files.zip", a);
            }
            string darova = "",cryp="";
            foreach (var e in archive.Entries)
            {

                if (i == num)
                {
                    e.OpenReader(MainWindow.us_log + MainWindow.us_pass);
                    if(e.FileName.Split('.')[2]=="crypt")
                    e.ExtractWithPassword(_path, ExtractExistingFileAction.OverwriteSilently, MainWindow.us_log + MainWindow.us_pass);
                    darova = e.FileName;
                    // WpfMessageBox.Show(_path +"\\"+ e.FileName.Split('/')[e.FileName.Split('/').Length - 1]);
                    cryp = _path + "\\" + e.FileName.Split('/')[e.FileName.Split('/').Length - 1];
                    MyEcoding.Decrypt( e.FileName, _path+"\\"+e.FileName.Split('/')[e.FileName.Split('/').Length-1]);
                    File.Delete("\\"+e.FileName);
                }
                i++;


            }

            if (darova != "")
            {
                archive.RemoveEntry(darova);
                archive.Save();
                // WpfMessageBox.Show(cryp.Split('.')[0] +"."+ cryp.Split('.')[1]);
                try
                {
                    File.Move(cryp, cryp.Split('.')[0] + "." + cryp.Split('.')[1]);
                }
                catch(Exception )
                {
                    File.Move(cryp, cryp.Split('.')[0] + "(copy)." + cryp.Split('.')[1]);
                }
            }





        }

        public static string OpenFile(MainWindow mw, int num, string _path)
        {
            int i = 0;
            ReadOptions a = new ReadOptions();
            a.Encoding = Encoding.UTF8;
            ZipFile archive;
            string darova = "",cryp="",per="";
            if (!System.Net.NetworkInformation.NetworkInterface.GetIsNetworkAvailable())
            {
                archive = ZipFile.Read(LocalClass_Zip.getPath_Aut() + "\\" + MainWindow.us_log + "Files.zip", a);
              
            }
            else
            {
                archive = ZipFile.Read(BDClient.getPath() + "\\" + MainWindow.us_log + "Files.zip", a);
            }
            
            foreach (var e in archive.Entries)
            {

                if (i == num)
                {
                    e.OpenReader(MainWindow.us_log + MainWindow.us_pass);
                    e.ExtractWithPassword(_path, ExtractExistingFileAction.OverwriteSilently, MainWindow.us_log + MainWindow.us_pass);
                    darova = MainWindow.timout_files+"\\"+ e.FileName;
                    cryp = _path + "\\" + e.FileName.Split('/')[e.FileName.Split('/').Length - 1];
                    MyEcoding.Decrypt(e.FileName, _path + "\\" + e.FileName.Split('/')[e.FileName.Split('/').Length - 1]);
                    File.Delete("\\" + e.FileName);
                    cryp = darova.Split('.')[0] + "." + darova.Split('.')[1];

                     try
                    {
                        File.Move(darova, cryp);
                        per= cryp;
                    }
                    catch (Exception)
                    {
                        File.Move(cryp, cryp.Split('.')[0] + "(copy)." + cryp.Split('.')[1]);       
                        per= cryp.Split('.')[0] + "(copy)." + cryp.Split('.')[1];
                        cryp.Split('.')[0] += "(copy)";
                    }
                }
                i++;
             

            }

            return per;





        }

        public static void insertFromZip_Aut(MainWindow mw)
        {
            ReadOptions a = new ReadOptions();
            a.Encoding = Encoding.UTF8;
            string path = WpfApp1.Properties.Settings.Default.Path;
            if (path == "")
            {
                path = Directory.GetLogicalDrives()[0] + "PksDisck";
                WpfApp1.Properties.Settings.Default.Path = path;
            }
            var archive = ZipFile.Read(path + "\\" + MainWindow.us_log + "Files.zip", a);
            ICollectionView view = CollectionViewSource.GetDefaultView(collection);
            view.Refresh();
            collection.Clear();
            foreach (var e in archive.Entries)
            {
                FSFile file = new FSFile();
                if (e.IsDirectory)
                    file.Face = FSFile.ImageSourceFromBitmap(WpfApp1.Properties.Resources.folder);
                else
                    file.Face = FSFile.ImageSourceFromBitmap(WpfApp1.Properties.Resources.document);
                file.file = e.FileName.Split('/')[e.FileName.Split('/').Length - 1];
                collection.Add(file);
            }


            mw.lv_file.ItemsSource = collection;
            archive.Dispose();

        }
    }

    public class FSFile
    {
        public ImageSource Face { get; set; }
        public string file { get; set; }

        [DllImport("gdi32.dll", EntryPoint = "DeleteObject")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool DeleteObject([In] IntPtr hObject);

        public static ImageSource ImageSourceFromBitmap(Bitmap bmp)
        {
            var handle = bmp.GetHbitmap();
            try
            {
                return Imaging.CreateBitmapSourceFromHBitmap(handle, IntPtr.Zero, Int32Rect.Empty, BitmapSizeOptions.FromEmptyOptions());
            }
            finally { DeleteObject(handle); }
        }
    }
}
