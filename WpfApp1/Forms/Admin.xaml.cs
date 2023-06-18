using MessBox;
using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.Windows;



namespace Admin
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>

    public partial class MainWindow  :IDisposable
    {
        public static MySqlConnection conn = new MySqlConnection("server=UseYourServer;username=user;password=pass;database=db;CharacterSet = utf8; AllowPublicKeyRetrieval=True");

        public static string local_path;
        public static string iduser;
        public MainWindow()
        {
            InitializeComponent();
        }
        ~MainWindow()
        {
            this.Dispose();
        }

        public void Window_Loaded(object sender, RoutedEventArgs e)
        {
            conn.Open();
            string sql = @"SELECT * FROM Users";
            MySqlCommand command = new MySqlCommand(sql, conn);
            command.ExecuteNonQuery();
            MySqlDataAdapter dataAdp = new MySqlDataAdapter(command);
            DataTable dt = new DataTable("Students");
            dataAdp.Fill(dt);
            admin.ItemsSource = dt.DefaultView;
            conn.Close();
            accept.Visibility = Visibility.Hidden;

        }

        public void Button_Click(object sender, RoutedEventArgs e)
        {
            if (admin.SelectedItem != null)
            {
                accept.Visibility = Visibility.Visible;
                DataRowView drv = (DataRowView)admin.SelectedItem;
                Name.Text = (drv["Name"]).ToString();
                Pass.Text = (drv["pass"]).ToString();
                Phone.Text = (drv["number"]).ToString();
                Login.Text = (drv["Login"]).ToString();
                email.Text = (drv["Email"]).ToString();
                id_role.Text = (drv["id_role"]).ToString();
                local_path = (drv["LocalPath"]).ToString();
                iduser = (drv["id_user"]).ToString();
            }
            else WpfMessageBox.Show("You don`t selected user", "Error", MessageBoxButton.OK, MessageBoxImage.Error);

        }

        public void accept_Click(object sender, RoutedEventArgs e)
        {
            conn.Open();
            string co = "UPDATE `Users` SET `id_role`=" + id_role.Text + ", `email`='" + email.Text + "', `login`='" + Login.Text + "', `pass`='" + Pass.Text + "', `name`='" + Name.Text + "', `number`='" + Phone.Text + "', `LocalPath`='" + local_path + "' WHERE `Users`.`id_user`= " + iduser + ";";


            MySqlCommand command = new MySqlCommand(co, conn);
            command.ExecuteNonQuery();
            admin.ItemsSource = null;
            string sql = @"SELECT * FROM Users";
            command = new MySqlCommand(sql, conn);
            command.ExecuteNonQuery();
            DataTable dt = new DataTable("Students");
            MySqlDataAdapter dataAdp = new MySqlDataAdapter(command);
            dataAdp.Fill(dt);
            admin.ItemsSource = dt.DefaultView;
            conn.Close();
        }

        public void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if (admin.SelectedItem != null)
            {
                DataRowView drv = (DataRowView)admin.SelectedItem;
                iduser = (drv["id_user"]).ToString();
                conn.Open();
                string co = "DELETE FROM `Users` WHERE `Users`.`id_user` = " + iduser + " ";
                MySqlCommand command = new MySqlCommand(co, conn);
                command.ExecuteNonQuery();
                admin.ItemsSource = null;
                string sql = @"SELECT * FROM Users";
                command = new MySqlCommand(sql, conn);
                command.ExecuteNonQuery();
                DataTable dt = new DataTable("Students");
                MySqlDataAdapter dataAdp = new MySqlDataAdapter(command);
                dataAdp.Fill(dt);
                admin.ItemsSource = dt.DefaultView;
                conn.Close();
            }
            else WpfMessageBox.Show("You don`t selected user", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
        }

        public void Button_Click_2(object sender, RoutedEventArgs e)
        {
            conn.Open();
            string co = "INSERT INTO f0476939_pksdisck.`Users` (id_role, email, login, pass, name, number, LocalPath) VALUES (@idrole, @email, @login, @password, @name, @phone, @LocalPath);";
            MySqlCommand command = new MySqlCommand(co, conn);
            command.Parameters.Add("@login", MySqlDbType.VarChar).Value = Login.Text;
            command.Parameters.Add("@password", MySqlDbType.VarChar).Value = Pass.Text;
            command.Parameters.Add("@email", MySqlDbType.VarChar).Value = email.Text;
            if (Phone.Text == "+7(___)___-__-__")
            {
                command.Parameters.Add("@phone", MySqlDbType.VarChar).Value = "Unknown";
            }
            else
            {
                command.Parameters.Add("@phone", MySqlDbType.VarChar).Value = Phone.Text;
            }
            if (Name.Text == "")
            {
                command.Parameters.Add("@name", MySqlDbType.VarChar).Value = "Unknown";
            }
            else
            {
                command.Parameters.Add("@name", MySqlDbType.VarChar).Value = Name.Text;
            }

            command.Parameters.Add("@idrole", MySqlDbType.Int32).Value = id_role.Text;
            command.Parameters.Add("@LocalPath", MySqlDbType.VarChar).Value = local_path;
            try
            {
                command.ExecuteNonQuery();
                admin.ItemsSource = null;
                string sql = @"SELECT * FROM Users";
                command = new MySqlCommand(sql, conn);
                command.ExecuteNonQuery();
                DataTable dt = new DataTable("Students");
                MySqlDataAdapter dataAdp = new MySqlDataAdapter(command);
                dataAdp.Fill(dt);
                admin.ItemsSource = dt.DefaultView;
                conn.Close();
            }
            catch (Exception ex)
            {

                WpfMessageBox.Show("Account with this mail or login already exist", "Error!", MessageBoxButton.OK, MessageBoxImage.Error);
                conn.Close();
            }

        }

        public  void Dispose()
        {
            GC.Collect();
            GC.WaitForPendingFinalizers();
        }
    }
}
