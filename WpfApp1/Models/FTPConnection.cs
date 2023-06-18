//using Org.BouncyCastle.Crypto.Tls;
using MessBox;
using System;
using System.IO;
using System.Net;
using System.Text;
using System.Windows;

namespace TrueMainForm
{
    internal class FTPConnection
    {
        public static  string[] GetFileType()
        {
            StringBuilder result = new StringBuilder();
            FtpWebRequest ftpreq;
            try
            {

                ftpreq = (FtpWebRequest)FtpWebRequest.Create(new Uri("ftp://141.8.193.236/"+MainWindow.us_log+"/"));
                ftpreq.UseBinary = true;
                ftpreq.Credentials = new NetworkCredential("f0476939", "Agodods166");
                ftpreq.Method = WebRequestMethods.Ftp.ListDirectory;
                WebResponse response = ftpreq.GetResponse();
                StreamReader reader = new StreamReader(response.GetResponseStream());

                string str = reader.ReadLine();
                while (str != null)
                {
                    result.Append(str[0]);
                    result.Append("\n");
                    str = reader.ReadLine();
                }
                result.Remove(result.ToString().LastIndexOf('\n'), 1);
                reader.Close();
                response.Close();
                return result.ToString().Split('\n');
            }
            catch (Exception ex)
            {
                WpfMessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return null;
            }
        }

        public static string[] GetFileName()
        {
            StringBuilder result = new StringBuilder();
            FtpWebRequest ftpreq;
            try
            {

                ftpreq = (FtpWebRequest)FtpWebRequest.Create(new Uri("ftp://141.8.193.236/" + MainWindow.us_log + "/"));
                ftpreq.UseBinary = true;
                ftpreq.Credentials = new NetworkCredential("f0476939", "Agodods166");
                ftpreq.Method = WebRequestMethods.Ftp.ListDirectory;
                WebResponse response = ftpreq.GetResponse();
                StreamReader reader = new StreamReader(response.GetResponseStream());

                string str = reader.ReadLine();
                while (str != null)
                {
                    result.Append(str);
                    result.Append("\n");
                    str = reader.ReadLine();
                }
                result.Remove(result.ToString().LastIndexOf('\n'), 1);
                reader.Close();
                response.Close();
                return result.ToString().Split('\n');
            }
            catch (Exception ex)
            {
                WpfMessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return null;
            }
        }
    }
}