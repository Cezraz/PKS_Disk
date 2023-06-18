using MessBox;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace TrueMainForm.Models
{
    class Synhronize
    {
        public ObservableCollection<string> collection = new ObservableCollection<string>();
        public void From_FTP()
        {
            //string[] filename = FTPConnection.GetFileName();
            //var ftpreq = (FtpWebRequest)FtpWebRequest.Create(new Uri("ftp://141.8.193.236/" + MainWindow.us_log + "/" + filename[num]));
            //ftpreq.UseBinary = true;
            //ftpreq.KeepAlive = false;
            //ftpreq.Credentials = new NetworkCredential("f0476939", "Agodods166");
            //ftpreq.Method = WebRequestMethods.Ftp.DownloadFile;
            //FtpWebResponse response = (FtpWebResponse)ftpreq.GetResponse();
            //Stream stream = response.GetResponseStream();
            //List<byte> list = new List<byte>();
            //int b;
            //while ((b = stream.ReadByte()) != -1)
            //    list.Add((byte)b);
            //if (File.Exists(_path + "\\" + filename[num]))
            //    File.WriteAllBytes(_path + "\\" + "(Copy)" + filename[num], list.ToArray());

            //else File.WriteAllBytes(_path + "\\" + filename[num], list.ToArray());

            //response.Close();
            //WpfMessageBox.Show(filename[num]);
        }
    }
}
