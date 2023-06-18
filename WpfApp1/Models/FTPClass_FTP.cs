
using MessBox;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media;

namespace TrueMainForm.Models
{
    public class FTPClass_FTP
    {
        public static  ObservableCollection<FSFile> collection = new ObservableCollection<FSFile>();
       

        public static void InsertFromFtp(MainWindow mw)
        {
            string[] type = FTPConnection.GetFileType();
            string[] filename = FTPConnection.GetFileName();

            ICollectionView view = CollectionViewSource.GetDefaultView(collection);
            // mw.lv_file.Items.Clear();
            view.Refresh();
            collection.Clear();
            for (int i = 0; i < filename.Length; i++)
            {
                FSFile file = new FSFile();

                if (type[i] == ".")
                {
                    continue;
                }
                else
                {
                    file.Face = FSFile.ImageSourceFromBitmap(WpfApp1.Properties.Resources.document);
                }
                file.file = filename[i];
                collection.Add(file);

            }

            mw.lv_FTP.ItemsSource = collection;
        }
        public static void pamagite_Ftp(MainWindow mw, int num, string _path)
        {
            if(num==-1)
            {
                WpfMessageBox.Show("No item is selected", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            string[] filename = FTPConnection.GetFileName();
       
            using (var web = new WebClient())
            {
                web.Credentials = new NetworkCredential("f0476939", "Agodods166");
                string filePath = Path.Combine(_path, filename[num]+ ".crypt");
                
                using (var stream = web.OpenRead("ftp://141.8.193.236/" + MainWindow.us_log + "/" + filename[num]))
                using (var fstream = File.Create(filePath))
                {
                    stream.CopyTo(fstream);
                }
                MyEcoding.Decrypt(filePath, filePath.Split('.')[0]+"."+ filePath.Split('.')[1]);
                File.Delete(filePath);
                WpfMessageBox.Show("File successfull downloaded to:" + _path +"\\"+ filename[num], "Succes", MessageBoxImage.Information);
            }
           

        }

        public static  void DeleteFTP(MainWindow mw, int num)
        {
            string[] filename = FTPConnection.GetFileName();
            var ftpreq = (FtpWebRequest)FtpWebRequest.Create(new Uri("ftp://141.8.193.236/" + MainWindow.us_log + "/" + filename[num]));
            ftpreq.UseBinary = true;
            ftpreq.KeepAlive = false;
            ftpreq.Credentials = new NetworkCredential("f0476939", "Agodods166");
            ftpreq.Method = WebRequestMethods.Ftp.DeleteFile;
            FtpWebResponse response = (FtpWebResponse)ftpreq.GetResponse();
            Stream stream = response.GetResponseStream();
            response.Close();
            WpfMessageBox.Show("File " + filename[num] + " successful deleted", "Succes", System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Information);
        }

        public static void AppendFTP(MainWindow mw, FileInfo info)
        {

            using (WebClient client = new WebClient())
            {
                client.Credentials = new NetworkCredential("f0476939", "Agodods166");
                MyEcoding.Encrypt(info.FullName, info.Name + ".crypt");
                client.UploadFile("ftp://141.8.193.236/" + MainWindow.us_log + "/" + info.Name, WebRequestMethods.Ftp.UploadFile, info.Name+".crypt");
                File.Delete(info.FullName);
            }
          
      

        }

        public static bool FTPCheckFolder(string nameFolder)
        {
            FtpWebRequest reqFTP;
            //Создаем объект FtpWebRequest
            reqFTP = (FtpWebRequest)FtpWebRequest.Create(new Uri("ftp://141.8.193.236/"));
            //Указываеи учетную запись
            reqFTP.Credentials = new NetworkCredential("f0476939", "Agodods166");
            reqFTP.KeepAlive = false;
            //Выбираем метод, который возвращает подробный список файлов на фтп-сервере
            reqFTP.Method = WebRequestMethods.Ftp.ListDirectory;
            reqFTP.UseBinary = true;
            //Получаем ответ от фтп-сервера
            FtpWebResponse resp = (FtpWebResponse)reqFTP.GetResponse();
            //Получаем поток данных
            Stream responseStream = resp.GetResponseStream();
            StreamReader reader = new StreamReader(responseStream);
            //Считываем данные из потока
            var contents = reader.ReadToEnd();
            //Закрываем потоки
            reader.Close();
            resp.Close();
            //Разбиваем полученную строку на массив строк, проверяем есть ли там папка с именем nameFolder
            if (contents.Replace("\r\n", " ").Split(' ').Any(c => c == nameFolder))
                return true;
            return false;
        }

        public  static void FTPCreateFolder()
        {
            FtpWebRequest reqFTP;
            //Создаем объект FtpWebRequest
            reqFTP = (FtpWebRequest)FtpWebRequest.Create(new Uri("ftp://141.8.193.236/" + MainWindow.us_log + "/"));
            //Указываем учетную запись
            reqFTP.Credentials = new NetworkCredential("f0476939", "Agodods166");
            reqFTP.KeepAlive = false;
            //Выбираем метод создания папки
            reqFTP.Method = WebRequestMethods.Ftp.MakeDirectory;
            reqFTP.UseBinary = true;
            //Получаем ответ от фтп-сервера
            FtpWebResponse resp = (FtpWebResponse)reqFTP.GetResponse();
            //Закрываем поток
            resp.Close();
        }
    }
}
