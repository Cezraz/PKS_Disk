//using Org.BouncyCastle.Crypto.Tls;
using System;
using System.Collections.Generic;
using System.Data;
using MySql.Data.MySqlClient;
using System.Net.NetworkInformation;
using System.Windows;
using System.Text.RegularExpressions;
using System.Net;
using System.Net.Mail;
using System.Threading;
//using WpfApp1;
using MessBox;
using System.IO;
using System.Data.Common;

namespace Innovative
{
    public class DBclass:Window
    {

        #region Public Objects
       
        
        public static bool f = false;
     
        
       public static string pattern_email = @"(\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*)";
       public static string pattern_login = @"(\s+)";
        public static string pattern_comment = @"([--]+)";
        public static string pattern_comment2 = @"([*\\]+)";
        public static MySqlConnection conn = new MySqlConnection("server=f0476939.xsph.ru;username=f0476939_gurkrivmam;password=4Uctbletpycuku;database=f0476939_pksdisck;CharacterSet = utf8; AllowPublicKeyRetrieval=True");
        
        #endregion

        #region Open/Close Connection
        public static void openConnection()
        {
            
            
            if (conn.State == ConnectionState.Closed)
            {
                try
                { 
                    conn.Open();
                    
                } catch (MySqlException ) { };
            }

        }
        public static void CloseConnection()
        {
            if (conn.State == ConnectionState.Open)
                conn.Close();
        }
        #endregion

        public static MySqlConnection getConnection()
        {
            return conn;
        }

        #region Autentification
        public static bool getAuthentication(String log, String pass)
        {
            string log_log;
            if (NetworkInterface.GetIsNetworkAvailable())
            {
                while (conn.State != ConnectionState.Open)
                {
                    try
                    {
                        conn.Open();
                    }
                    catch (MySqlException ex)
                    {
                        WpfMessageBox.Show(ex.Message, MessageBoxButton.OK, MessageBoxImage.Error);
                        Thread.Sleep(1000);
                    }
                }
              

                MySqlCommand com = new MySqlCommand("Select * from Users where login ='" + log + "' and pass='" + pass + "'", conn);
                if (Regex.IsMatch(log, pattern_email))
                {
                    com.CommandText = "Select * from Users where email = '" + log + "' and pass = '" + pass + "'";
                }

                if (com.ExecuteScalar() == null)
                {
              
                    WpfMessageBox.Show("Incorrect login or password", "Enter error",MessageBoxButton.OK,MessageBoxImage.Error);
                    return false;
                }


                MySqlCommand com2 = new MySqlCommand("SELECT crypt,delfrompc,delfromzip from users where login ='" + log + "'", conn);
                if (Regex.IsMatch(log, pattern_email))
                {
                    com2.CommandText = "SELECT crypt,delfrompc,delfromzip from users where email ='" + log + "'";
                    MySqlCommand sel_log = new MySqlCommand("Select login from Users where email ='" + log + "'", conn);
                    log_log = sel_log.ExecuteScalar().ToString();

                    WpfMessageBox.Show("Welcome " + log_log, "Succesful login",MessageBoxImage.Information);
                }
                else
                {
                    if (log == "maloi") WpfMessageBox.Show("Hello,clown");
                    else
                        WpfMessageBox.Show("Welcome " + log, "Succesful login", MessageBoxImage.Information);
                    
                }
                MySqlDataAdapter adapt = new MySqlDataAdapter(com2);
                DataSet ds = new DataSet();
                conn.Close();
                return true;

            }

            WpfMessageBox.Show("Connection error","Error",MessageBoxButton.OK,MessageBoxImage.Error);
            return false;
        }

        #endregion
        #region Get id role
        public static string GetID_Role(String log, String pass)
        {
            string id_role = "";
            if (NetworkInterface.GetIsNetworkAvailable())
            {
                while (conn.State != ConnectionState.Open)
                {
                    try
                    {
                        conn.Open();
                    }
                    catch (MySqlException ex)
                    {
                        WpfMessageBox.Show(ex.Message, MessageBoxButton.OK, MessageBoxImage.Error);
                        Thread.Sleep(1000);
                    }
                }
                MySqlCommand com = new MySqlCommand("Select id_role from Users where login ='" + log + "' and pass='" + pass + "'", conn);
                id_role = com.ExecuteScalar().ToString();
            }

            return id_role;
        }
        #endregion
        #region Registration
        public static void registration(string login, string password, string email, string phone, string name, WpfApp1.MainWindow mw)
        {
            f = false;
            
            MySqlCommand command = new MySqlCommand("INSERT INTO f0476939_pksdisck.`Users` (id_role, email, login, pass, name, number, LocalPath) VALUES (@idrole, @email, @login, @password, @name, @phone, @LocalPath);", getConnection());
            Captcha.MainWindow captcha = new Captcha.MainWindow();

            do
            {
                if (login == "") { WpfMessageBox.Show("Login not enter", "Error", MessageBoxButton.OK, MessageBoxImage.Error); f = true; break; };
                if (email == ""|| (!Regex.IsMatch(email, pattern_email))) { WpfMessageBox.Show("Email not enter", "Error", MessageBoxButton.OK, MessageBoxImage.Error); f = true; break; };
                if (password == "") { WpfMessageBox.Show("Password not enter", "Error", MessageBoxButton.OK, MessageBoxImage.Error); f = true; break; };
                if (Password.PasswordStrength(password)== Password.Strength.Low) { WpfMessageBox.Show("You used: "+Password.StengtToString(Password.PasswordStrength(password))+" it is unacceptable!","Error!", MessageBoxButton.OK, MessageBoxImage.Error); f = true; break; };
                
            } while (password == "" || email == "" || login == ""|| Password.PasswordStrength(password) == Password.Strength.Low);
            if (!f)
            {
                captcha.ShowDialog();

                if (Captcha.MainWindow.flag)
                {
                   
                    command.Parameters.Add("@login", MySqlDbType.VarChar).Value = login;
                    command.Parameters.Add("@password", MySqlDbType.VarChar).Value = password;
                    command.Parameters.Add("@email", MySqlDbType.VarChar).Value = email;
                    if (phone == "+7(___)___-__-__")
                    {
                        command.Parameters.Add("@phone", MySqlDbType.VarChar).Value = "Unknown";
                    }
                    else
                    {
                        command.Parameters.Add("@phone", MySqlDbType.VarChar).Value = phone;
                    }
                    if (name == "")
                    {
                        command.Parameters.Add("@name", MySqlDbType.VarChar).Value = "Unknown";
                    }
                    else
                    {
                        command.Parameters.Add("@name", MySqlDbType.VarChar).Value = name;
                    }

                    command.Parameters.Add("@idrole", MySqlDbType.Int32).Value = 3;
                    command.Parameters.Add("@LocalPath", MySqlDbType.VarChar).Value = Directory.GetLogicalDrives()[0] + "PksDisck";
                    

                    try
                    {

                        openConnection();

                        if (command.ExecuteNonQuery() == 1)
                        {
                            WpfMessageBox.Show("You are registered.\n Difficulty password: " + Password.StengtToString(Password.PasswordStrength(password)), "Successful!",MessageBoxButton.OK,MessageBoxImage.Information);
                            mw.login.Text = mw.regpass.Text = mw.mail.Text = mw.phone.Text = mw.name.Text = mw.conf_pass.Text = "";
                            MailAddress from = new MailAddress("pksov@bk.ru");
                            MailAddress to = new MailAddress(email);
                            MailMessage m = new MailMessage(from, to);
                            m.Subject = "Successful registration";
                            m.Body = "<h1>You have successfully registered on PKSDISK<h1><br>";
                            m.IsBodyHtml = true;
                            SmtpClient smpt = new SmtpClient("smpt.mail.ru", 2525);
                            smpt.Credentials = new NetworkCredential("pksov@bk.ru", "Agodods166");
                            smpt.EnableSsl = true;
                            smpt.Send(m);
                            
                        }

                        else
                        {
                            
                            WpfMessageBox.Show("Error of registration.", "Error!", MessageBoxButton.OK, MessageBoxImage.Error);
                        }
                        CloseConnection();
                    }
                    catch (Exception ex)
                    {
                       
                        if (ex.Message.Split()[0] == "Duplicate") WpfMessageBox.Show("Account with this mail or login already exist", "Error!", MessageBoxButton.OK, MessageBoxImage.Error);
                        else WpfMessageBox.Show(ex.Message, "Error!", MessageBoxButton.OK, MessageBoxImage.Error);
                    };

                }
                else
                {
                    WpfMessageBox.Show("Incorrect CAPTCHA","Error",MessageBoxButton.OK,MessageBoxImage.Error);
                    
                    
                }
            }


        }

        
        #endregion

        #region Checks
        public static bool check_mail(string mail)
        {
            if (Regex.IsMatch(mail, pattern_email)) return false;
            else return true;
        }
        public static bool check_rex(string str)
        {
            if (Regex.IsMatch(str, pattern_login)|| Regex.IsMatch(str, pattern_comment)|| Regex.IsMatch(str, pattern_comment2)) return false;
            else return true;
        }
        #endregion

        #region Recovery
        public static void recovery(string email)
        {
            try
            {
                MySqlCommand com = new MySqlCommand("Select email from Users where email='" + email + "';", conn);
                MySqlCommand com2 = new MySqlCommand("Select pass from Users where email='" + email + "';", conn);
                if (!Regex.IsMatch(email, pattern_email))
                {
                    com.CommandText = "Select email from Users where login='" + email + "';";
                    com2.CommandText = "Select pass from Users where login='" + email + "';";
                }
                if (com.ExecuteScalar() == null)
                {
                    WpfMessageBox.Show("Incorrect lofin or email", "Error!", MessageBoxButton.OK, MessageBoxImage.Error);

                }
                else
                {

                    MailAddress from = new MailAddress("pksov@bk.ru");
                    MailAddress to = new MailAddress(com.ExecuteScalar().ToString());
                    MailMessage m = new MailMessage(from, to);
                    m.Subject = "Recovery password";
                    if (email == "dimo4ka" || email == "Vladislav.reyh@gmail.com")
                    {
                        m.Body = "<h1>This is your password <h1><br>" + com2.ExecuteScalar().ToString() +
                        "<br><img src=\"https://i.postimg.cc/ryPpTQPk/nice-Dima.jpg\">";
                    }
                    else
                    {
                        m.Body = "<h1>This is your password <h1><br>" + com2.ExecuteScalar().ToString();
                    }
                    m.IsBodyHtml = true;
                    SmtpClient smpt = new SmtpClient("smpt.mail.ru", 2525);
                    smpt.Credentials = new NetworkCredential("pksov@bk.ru", "Agodods166");
                    smpt.EnableSsl = true;
                    smpt.Send(m);
                    WpfMessageBox.Show("Email with your password recovery has sent to your email " + com.ExecuteScalar().ToString(), "Recovery password");

                }
            } catch (Exception ex) { WpfMessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error); }
        }
        #endregion

        public static string getPath(WpfApp1.MainWindow mw)
        {
            string local_path="";
            CloseConnection();
            openConnection();

            if (conn.State == ConnectionState.Open)
            {
                string sql = "select LocalPath from f0476939_pksdisck.`Users` where login = @login";
                MySqlCommand cmd_insert = new MySqlCommand(sql, conn);

                cmd_insert.Parameters.AddWithValue("@login",mw.ga.Text);

                DbDataReader reader = cmd_insert.ExecuteReader();

                if (reader.HasRows)

                   while( reader.Read())
                local_path = reader.GetString(0);
            }
            CloseConnection();
           
            return local_path;

        }

        public static string getEmail(WpfApp1.MainWindow mw)
        {
            string email = "";
            CloseConnection();
            openConnection();

            if (conn.State == ConnectionState.Open)
            {
                string sql = "select email from f0476939_pksdisck.`Users` where login = @login";
                MySqlCommand cmd_insert = new MySqlCommand(sql, conn);

                cmd_insert.Parameters.AddWithValue("@login", mw.ga.Text);

                DbDataReader reader = cmd_insert.ExecuteReader();

                if (reader.HasRows)

                    while (reader.Read())
                        email = reader.GetString(0);
            }
            CloseConnection();

            return email;

        }

        //public static void  doCode()
        //{
        //     string codeString = "";
        //    string codeTimerString = "00:00";

        //    string[] liters = { "1", "2", "3", "4", "5", "6", "7", "8", "9", "0" };
        //    Random rnd = new Random();
        //    for (int i = 0; i < 4; i = i + 1) codeString += liters[rnd.Next(0, 10)];

        //    int num = 0;
        //    // устанавливаем метод обратного вызова
        //    TimerCallback tm = new TimerCallback(Count);
        //    // создаем таймер
        //    Timer timer = new Timer(tm, num, 0, 2000);
        //}
        //public static void Count(object obj)
        //{
        //    int x = (int)obj;
        //    for (int i = 1; i < 9; i++, x++)
        //    {
        //        Console.WriteLine($"{x * i}");
        //    }
        //}



        #region Password Strength
        public static class Password
        {
            public enum Strength { Low = 1, Medium, High, VeryHigh, Paranoid };

            public static Strength PasswordStrength(string password)
            {
                int score = 0;
                Dictionary<string, int> patterns = new Dictionary<string, int> { { @"\d", 5 }, //включает цифры
                                                                         { @"[a-zA-Z]", 10 }, //буквы
                                                                         { @"[!,@,#,\$,%,\^,&,\*,?,_,~]", 15 } }; //символы
                if (password.Length > 6)
                    foreach (var pattern in patterns)
                        score += Regex.Matches(password, pattern.Key).Count * pattern.Value;

                Strength result;
                switch (score / 50)
                {
                    case 0: result = Strength.Low; break;
                    case 1: result = Strength.Medium; break;
                    case 2: result = Strength.High; break;
                    case 3: result = Strength.VeryHigh; break;
                    default: result = Strength.Paranoid; break;
                }
                return result;
            }

            public static string StengtToString(Strength pass)
            {
                switch (pass)
                {
                    case Strength.Low: return "Weak password"; 
                    case Strength.Medium: return "Midle difficultly password"; 
                    case Strength.High: return "High difficulty password"; 
                    case Strength.VeryHigh: return " Very high difficulty password"; 
                    default: return "You are paranoik...."; 
                }
            }
        }

        #endregion


       

    };

       
        
}

