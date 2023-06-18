using Ionic.Zip;
using MessBox;
using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.Data.Common;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Windows.Automation;

namespace TrueMainForm
{
    public class BDClient :IDisposable
    {
        public static MySqlConnection ClientConnection = new MySqlConnection("server=f0476939.xsph.ru;username=f0476939_gurkrivmam;password=4Uctbletpycuku;database=f0476939_pksdisck;CharacterSet = utf8; AllowPublicKeyRetrieval=True");

        public static string local_path;
        public string unstatic_path=local_path;
        private static string zip_pass = MainWindow.us_log+ MainWindow.us_pass;
        public  static string user_login;
        public  static string user_pass;
        //user_login = login;
        //cs.login = login;

        
        ~BDClient()
        {
            this.Dispose();
        }
   
        //if (checkIsNew(login))
        //{
        //    createDefaultFolder();
        //    updateUserInfo();
        //}
        //else
        //{
        //    getPath();
        //    insertFromZip();
        //}


        public static string getPath()
        {
            CloseConnection(ClientConnection);
            openConnection(ClientConnection);
         
            if (ClientConnection.State == ConnectionState.Open)
            {
                string sql = "select LocalPath from f0476939_pksdisck.`Users` where login = @login";
                MySqlCommand cmd_insert = new MySqlCommand(sql, ClientConnection);
               
                cmd_insert.Parameters.AddWithValue("@login", MainWindow.us_log);
               
                DbDataReader reader = cmd_insert.ExecuteReader();

                if (reader.HasRows)
                    
                    reader.Read();
                    local_path = reader.GetString(0);
                    }
            CloseConnection(ClientConnection);
           
            return local_path;
            
        }

        public static void updateUserInfo_Aut(string _path)
        {
           
            WpfApp1.Properties.Settings.Default.Path = _path;
           
        }
        public static void updateUserInfo(string _path)
        {
            CloseConnection(ClientConnection);
            openConnection(ClientConnection);

            if (ClientConnection.State == ConnectionState.Open)
            {
                string sql = "update f0476939_pksdisck.`Users` set  LocalPath=@path where login = @login";
                MySqlCommand cmd_insert = new MySqlCommand(sql, ClientConnection);
                cmd_insert.Parameters.AddWithValue("@path", _path);
                cmd_insert.Parameters.AddWithValue("@login", MainWindow.us_log);
                cmd_insert.ExecuteNonQuery();
            }
            CloseConnection(ClientConnection);
        }


        public static bool checkIsNew(string login)
        {
            if (ClientConnection.State == ConnectionState.Open)
            {
                string sql = "select is_new from f0476939_pksdisck.`Users` where login=@login";
                MySqlCommand cmd_insert = new MySqlCommand(sql, ClientConnection);
                cmd_insert.Parameters.AddWithValue("@login", login);
                DbDataReader reader = cmd_insert.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                        return reader.GetBoolean(0);
                }
            }
            CloseConnection(ClientConnection);
            return false;
        }

        public static void createDefaultFolder()
        {
          
            string path = Directory.GetLogicalDrives()[0] + "PksDisck";
            updateUserInfo(path);
            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);
            
            string fullpath = path;
            Directory.CreateDirectory(fullpath);
            local_path = fullpath;
            using (ZipFile zip = new ZipFile())
            {
                zip.Password = MainWindow.us_log+MainWindow.us_pass;
                zip.Encryption = EncryptionAlgorithm.WinZipAes256;
               
                zip.Save(fullpath + "\\" + MainWindow.us_log + "Files.zip");
              
            }
        }

        public static void openConnection(MySqlConnection conn)
        {
            if (conn.State == ConnectionState.Closed)
            {
                try
                {
                    conn.Open();

                }
                catch (MySqlException) { };
            }

        }
        public static void CloseConnection(MySqlConnection conn)
        {
            if (conn.State == ConnectionState.Open)
                conn.Close();
        }


        public static MySqlConnection getConnection(MySqlConnection conn)
        {
            return conn;
        }

        

        public void Dispose()
        {
            GC.Collect();
            GC.WaitForPendingFinalizers();
        }

        //private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)           
        //{
        //    this.DragMove();
        //    //mb.Close();
        //}

        //private void btn_ok_Click(object sender, RoutedEventArgs e)
        //{
        //    cs.Show();
        //    local_path = cs.path;
        //}
    }
}


