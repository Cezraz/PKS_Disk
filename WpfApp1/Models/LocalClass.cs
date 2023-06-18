using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Innovative;
using System.Net.Http.Headers;
using System.Windows;
using MessBox;

namespace WpfApp1.Models
{
    class LocalClass: IDisposable
    {
       
        ~LocalClass()
        {
            Dispose();
        }
        public static void save_me(string login)
        {
            File.WriteAllText("save_user.txt",login);
        }
        public static void save_me_aut(string login, string email,string pass, string path)
        {
            Properties.Settings.Default.Login = login;
            Properties.Settings.Default.Password = pass;
            Properties.Settings.Default.Path = path;
            Properties.Settings.Default.Email = email;
            Properties.Settings.Default.Save();



            
        }

       
            
        public static void fogot_me()
        {
            Properties.Settings.Default.Login = "";
            Properties.Settings.Default.Password = "";
            Properties.Settings.Default.Path = "";
            Properties.Settings.Default.Email = "";
            Properties.Settings.Default.Save();
        }

        public  void Dispose()
        {
            GC.Collect();
            GC.WaitForPendingFinalizers();
        }
    }
}
