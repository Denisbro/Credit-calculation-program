using System;
using System.Windows;
using System.Windows.Controls;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;
using System.Security.Cryptography;
using System.Text;
using System.Windows.Input;
using System.Data.OleDb;
using Accessibility;
using System.IO;
using System.Threading.Tasks;
using System.Net.Mail;
using System.Net;

namespace WpfApp6
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
       public string connectionString;
        private Window3 secondww;
        private Window1 second;
        private Window2 W;
        public MainWindow()
        {
            InitializeComponent();
            connectionString = ConfigurationManager.ConnectionStrings["DB"].ConnectionString;
        }
        // Функция преобразования строки к MD5 
        public static string CreateMD5(string input)
        {
            // Use input string to calculate MD5 hash
            using (MD5 md5 = MD5.Create())
            {
                byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(input);
                byte[] hashBytes = md5.ComputeHash(inputBytes);

                // Convert the byte array to hexadecimal string
                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < hashBytes.Length; i++)
                {
                    sb.Append(hashBytes[i].ToString("X2"));
                }
                return sb.ToString();
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string pas = CreateMD5(Password.Password);
            string log = UserName.Text;

            OleDbConnection connection = null;
            if (true)
            {
                if (log == "admin" || log == "ADMIN")
                {
                    try
                    {
                        string Query = "SELECT COUNT(1) FROM(tbluser INNER JOIN adminuser ON tbluser.[right] = adminuser.[right]) WHERE(tbluser.[right] = 'admin' AND UserName=@Username AND Password=@Password)";
                        connection = new OleDbConnection(connectionString);
                        connection.Open();
                        OleDbCommand command = new OleDbCommand(Query, connection)
                        {
                            CommandType = CommandType.Text
                        };
                        command.Parameters.AddWithValue("@Username", log);
                        command.Parameters.AddWithValue("@Password", pas);
                        int count = Convert.ToInt32(command.ExecuteScalar());
                        if (count == 1)
                        {
                            second = new Window1();
                            second.Show();
                            this.Close();
                        }
                        else
                        {
                            MessageBox.Show("Ошибка логина/пароля");
                        }

                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                    finally
                    {
                        if (connection != null)
                            connection.Close();
                    }
                }
                else
                {
                    try
                    {
                        string Query = "SELECT COUNT(1) FROM(tbluser INNER JOIN adminuser ON tbluser.[right] = adminuser.[right]) WHERE(tbluser.[right] = 'user' AND UserName=@Username AND Password=@Password)";
                        connection = new OleDbConnection(connectionString);
                        connection.Open();
                        OleDbCommand command = new OleDbCommand(Query, connection)
                        {
                            CommandType = CommandType.Text
                        };
                        command.Parameters.AddWithValue("@Username", log);
                        command.Parameters.AddWithValue("@Password", pas);
                        int count = Convert.ToInt32(command.ExecuteScalar());
                        if (count == 1)
                        {
                            secondww = new Window3(log);
                            secondww.Show();
                            this.Close();
                        }
                        else
                        {
                            MessageBox.Show("Ошибка логина/пароля");
                        }

                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                    finally
                    {
                        if (connection != null)
                            connection.Close();
                    }
                }
            }
        }

        private void PressEnter(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
                Button_Click(this,e) ;
        }

        private void Next(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
                Password.Focus();
             
        }

        private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            W = new Window2();
            W.Show();
            this.Close();
        }
    }

}