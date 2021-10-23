using System;
using System.Windows;
using System.Collections.ObjectModel;
using System.Windows.Controls;
using System.Data.OleDb;
using System.Configuration;
using System.Data;
using System.Windows.Media;
using System.Windows.Input;
using System.Security.Cryptography;
using System.Text;

namespace WpfApp6
{
    /// <summary>
    /// Логика взаимодействия для Window1.xaml
    /// </summary>
    public partial class Window1 : Window
    {
        // строка подключения к MS Access
        public static string connectString = ConfigurationManager.ConnectionStrings["DB"].ConnectionString;
        private static OleDbConnection myConnection;
        private static OleDbDataAdapter DA;
        private static DataTable ds;
        string sw;

        OleDbCommand sqlcmd = new OleDbCommand();
        OleDbDataAdapter sqladp = new OleDbDataAdapter();
        DataSet dss = new DataSet();
        bool prov = true;

        string querry = "SELECT * FROM Клиент";
        string querry2 = "SELECT Фио FROM Клиент ORDER BY фио ASC";

        public Window1()
        {
            InitializeComponent();
        }

        private void Oledatabase()
        {
            try
            {
                // создаем экземпляр класса OleDbConnection
                myConnection = new OleDbConnection(connectString);
                // открываем соединение с БД
                myConnection.Open();

                DA = new OleDbDataAdapter(querry, myConnection);
                OleDbCommandBuilder CB = new OleDbCommandBuilder(DA);
                ds = new DataTable("Клиент");
                DA.Fill(ds);
                Maingrid.ItemsSource = ds.DefaultView;
                myConnection.Close();

                listupdate(list);

                listupdate(list2);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
  
        }

        private void listupdate(ComboBox bb)
        {
            OleDbConnection Con = new OleDbConnection(connectString); //Новое подключение
            OleDbCommand command = new OleDbCommand(querry2, Con); //Команда выбора данных
            Con.Open(); //Открываем соединение
            bb.Items.Clear();
            OleDbDataReader read = command.ExecuteReader(); //Считываем и извлекаем данные
            while (read.Read()) //Читаем пока есть данные
            {
                bb.Items.Add(read.GetValue(0).ToString()); //Добавляем данные в лист итем
            }
            Con.Close(); //Закрываем соединение
        }

         private void Button_Click(object sender, RoutedEventArgs e)
        {
            string title = "Close Application";
            MessageBoxButton messBoxBut = MessageBoxButton.YesNo;
            MessageBoxResult result;
            result = MessageBox.Show("Вы хотите выйти?", title, messBoxBut);

            if (result == MessageBoxResult.Yes)
            {
                Application.Current.Shutdown();
            }
            else if (result == MessageBoxResult.No)
            {

            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Oledatabase();
        }

        private void Window_Unloaded(object sender, RoutedEventArgs e)
        {
            
        }

        private void Maingrid_RowEditEnding(object sender, DataGridRowEditEndingEventArgs e)
        {
        }

        private void Window_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            DragMove();
        }

        private void Likecommand(string nn,string lk)
        {
            OleDbConnection connectionQuery1 = new OleDbConnection(connectString);
            connectionQuery1.Open();

            string sql1 = "SELECT * FROM     Клиент WHERE(["+nn+"] LIKE '"+lk+"%')";
            OleDbCommand command3 = new OleDbCommand(sql1, connectionQuery1);
            DataTable ds = new DataTable();

            ds.Load(command3.ExecuteReader());
            connectionQuery1.Close();
            Maingrid.ItemsSource = ds.DefaultView;
        }

        private void Lokekomandplus(string nn, string lk)
        {
            OleDbConnection connectionQuery1 = new OleDbConnection(connectString);
            connectionQuery1.Open();

            string sql1 = "SELECT * FROM     Клиент WHERE([" + nn + "] LIKE '%" + lk + "%')";
            OleDbCommand command3 = new OleDbCommand(sql1, connectionQuery1);
            DataTable ds = new DataTable();

            ds.Load(command3.ExecuteReader());
            connectionQuery1.Close();
            Maingrid.ItemsSource = ds.DefaultView;
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (textlike.Text=="")Oledatabase();
            else
            Likecommand("Фио", textlike.Text);
        }

        private void textlike2_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (textlike2.Text == "") Oledatabase();
            else
                Likecommand("Паспортные данные", textlike2.Text);
        }

        private void TextBox_TextChanged_1(object sender, TextChangedEventArgs e)
        {
            if (textlike3.Text == "") Oledatabase();
            else
                Lokekomandplus("Дата рождения", textlike3.Text);
        }

        private void textlike4_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (textlike4.Text == "") Oledatabase();
            else
                Lokekomandplus("Адрес проживания", textlike4.Text);
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {   
            MainWindow mw = new MainWindow();
            mw.Show();
            this.Close();
        }

        private void Windows_PreviewKeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.KeyboardDevice.Modifiers == ModifierKeys.Shift && e.Key == Key.F1 && prov == true) {
                windows.Background = new SolidColorBrush(Colors.DimGray);
                prov = false;

            }
            else if(e.KeyboardDevice.Modifiers == ModifierKeys.Shift && e.Key == Key.F1 && prov == false)
            {
                windows.Background = new SolidColorBrush(Colors.White);
                prov = true;
            }
            if (e.KeyboardDevice.Modifiers == ModifierKeys.Shift && e.Key == Key.F2 && prov == true)
            {
                snake sn = new snake();
                sn.ShowDialog();
                sn.Close();

            }
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            if (true)              //Ужасный код   !!!WARNING!!!         !!!WARNING!!!         !!!WARNING!!!
            {
                if (r1.Text == "")
                {
                    MessageOK("ЗАПОЛНИ ФИО");
                    textBoxFirstName.Focus();
                }
                else if (r2.Text == "")
                {
                    MessageOK("ЗАПОЛНИ ПАСПОРТНЫЕ ДАННЫЕ");
                    textBoxLastName.Focus();
                }
                else if (r3.Text == "")
                {
                    MessageOK("ЗАПОЛНИ Среднюю заработную плату");
                    textBoxEmail.Focus();
                }
                else if (r5.Text == null)
                {
                    MessageOK("ЗАПОЛНИ дату рождения");
                    datepi.Focus();
                }
                else if (r6.Text == "")
                {
                    MessageOK("ЗАПОЛНИ телефон ");
                    pa.Focus();
                }
                else if (r4.Text == "")
                {
                    MessageOK("ЗАПОЛНИ адрес проживания ");
                    textBoxAddress.Focus();
                }
                else if (r7.Text == "")
                {
                    MessageOK("ЗАПОЛНИ место работы ");
                    y.Focus();
                }

            }
            if (r1.Text != "" && r2.Text != "" && r3.Text != "" && r4.Text != "" && (r5.Text != null || r5.Text != "") && r6.Text != "" && r7.Text != "")
            {
                update("фио", r1.Text, textBoxFirstName.Text);
                update("Паспортные данные", r2.Text, textBoxLastName.Text);
                update2("Средняя заработная плата", Convert.ToInt32(r3.Text), textBoxEmail.Text);
                update("телефон", r5.Text, pa.Text);
                update("Адрес проживания", r6.Text, textBoxAddress.Text);
                update("Место работы", r7.Text, y.Text);
                update3("Дата рождения", datepi, r4.Text);

                r1.Text = "";
                r2.Text = "";
                r3.Text = "";
                r4.Text = "";
                r5.Clear();
                r6.Text = "";
                r7.Text = "";
                MessageOK("Данные успешно обновлены");
                listupdate(list);
            }
        }

        private static void update(string table,string value,string old)
        {
            OleDbConnection connectionQuery1 = new OleDbConnection(connectString);
            connectionQuery1.Open();

            string sql1 = "UPDATE Клиент SET ["+table+"] = '"+value+ "' WHERE  (["+table+"] = '"+old+"')";
            OleDbCommand command3 = new OleDbCommand(sql1, connectionQuery1);

            command3.ExecuteReader();
            connectionQuery1.Close();
        }

        private static void update2(string table, int value, string old)
        {
            OleDbConnection connectionQuery1 = new OleDbConnection(connectString);
            connectionQuery1.Open();

            string sql1 = "UPDATE Клиент SET [" + table + "] = " + value + " WHERE  ([" + table + "] = " + old + ")";
            OleDbCommand command3 = new OleDbCommand(sql1, connectionQuery1);

            command3.ExecuteReader();
            connectionQuery1.Close();
        }

        private static void update3(string table, DatePicker da, string old)
        {
            OleDbConnection connectionQuery1 = new OleDbConnection(connectString);
            connectionQuery1.Open();

            string sql1 = "UPDATE Клиент SET [" + table + "] = '" + old + "' WHERE  ([" + table + "] = '" + da.SelectedDate.Value.ToString("dd.MM.yyyy") + "')";
            OleDbCommand command3 = new OleDbCommand(sql1, connectionQuery1);

            command3.ExecuteReader();
            connectionQuery1.Close();
        }

        private void list_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (list.SelectedItem != null)
            {
                edit("фио", textBoxFirstName);
                edit("Паспортные данные", textBoxLastName);
                edit("Средняя заработная плата", textBoxEmail);
                edit("телефон", pa);
                edit("Адрес проживания", textBoxAddress);
                edit("Место работы", y);
                editdate("Дата рождения", datepi);
            }
        }

        private void edit(string tablename, TextBox bx)
        {
            string fio = list.SelectedItem.ToString();
            string str = null;

            OleDbConnection Con = new OleDbConnection(connectString); //Новое подключение
            OleDbCommand command = new OleDbCommand("SELECT ["+tablename+"] FROM  Клиент WHERE(фио=@fio)", Con)
            {
                CommandType = CommandType.Text
            }; //Команда выбора данных
            Con.Open(); //Открываем соединение
            command.Parameters.AddWithValue("@fio", fio);
            OleDbDataReader read = command.ExecuteReader(); //Считываем и извлекаем данные

            while (read.Read())
            {
                str = read[0].ToString(); //get Value of ppassword Column
            }
            bx.Text = str;
            myConnection.Close();
        }

        private void editdate(string tablename, DatePicker bx)
        {
            string fio = list.SelectedItem.ToString();
            string str = null;

            OleDbConnection Con = new OleDbConnection(connectString); //Новое подключение
            OleDbCommand command = new OleDbCommand("SELECT [" + tablename + "] FROM  Клиент WHERE(фио=@fio)", Con)
            {
                CommandType = CommandType.Text
            }; //Команда выбора данных
            Con.Open(); //Открываем соединение
            command.Parameters.AddWithValue("@fio", fio);
            OleDbDataReader read = command.ExecuteReader(); //Считываем и извлекаем данные

            while (read.Read())
            {
                str = read[0].ToString(); //get Value of ppassword Column
            }
            bx.Text = str;
            myConnection.Close();
        }

        private static void MessageOK(string stroka)
            {
                MessageBoxButton messBoxBut = MessageBoxButton.OK;
                MessageBoxResult result;
                result = MessageBox.Show(stroka, "Warning", messBoxBut);
            }

        private void list2_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            q2.IsEnabled = true;
            q3.IsEnabled = true;
            q4.IsEnabled = true;

            firstpg("UserID", q1);
            firstpg("UserName", q2);
            firstpg("Password", q3);
            test(q4);
        }

        private void test(ComboBox cb)
        {
            string fio = list2.SelectedItem.ToString();
            string str = null;
            cb.Items.Clear();

            OleDbConnection Con = new OleDbConnection(connectString); //Новое подключение
            OleDbCommand command = new OleDbCommand("SELECT [right] FROM tbluser WHERE(фио=@fio)", Con)
            {
                CommandType = CommandType.Text
            }; //Команда выбора данных
            Con.Open(); //Открываем соединение
            command.Parameters.AddWithValue("@fio", fio);
            OleDbDataReader read = command.ExecuteReader(); //Считываем и извлекаем данные

            while (read.Read())
            {
                str = read[0].ToString(); //get Value of ppassword Column
            }
            if (str == "admin")
            {
                cb.Items.Add($"{str}");
                cb.Items.Add("user");
            }
            else if (str == "user")
            {
                cb.Items.Add($"{str}");
                cb.Items.Add("admin");
            }         
            cb.SelectedIndex = 0;
            myConnection.Close();
        }

        private void firstpg(string par,TextBox bx)
        {
            string fio = list2.SelectedItem.ToString();
            string str = null;

            OleDbConnection Con = new OleDbConnection(connectString); //Новое подключение
            OleDbCommand command = new OleDbCommand("SELECT [" + par + "] FROM  tbluser WHERE(фио=@fio)", Con)
            {
                CommandType = CommandType.Text
            }; //Команда выбора данных
            Con.Open(); //Открываем соединение
            command.Parameters.AddWithValue("@fio", fio);
            OleDbDataReader read = command.ExecuteReader(); //Считываем и извлекаем данные

            while (read.Read())
            {
                str = read[0].ToString(); //get Value of ppassword Column
            }
            bx.Text = str;
            myConnection.Close();
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            if(q2.Text != "" && q3.Text != "" && q4.Text != "")
            {
                try
                {
                    OleDbConnection connectionQuery1 = new OleDbConnection(connectString);
                    connectionQuery1.Open();

                    string sql1 = "UPDATE tbluser SET [UserName]='" + q2.Text + "',[Password]='" + sw + "',[right]='" + q4.Text + "' WHERE  (фио = '" + list2.SelectedItem + "')   ";
                    OleDbCommand command3 = new OleDbCommand(sql1, connectionQuery1);

                    command3.ExecuteReader();
                    connectionQuery1.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                finally
                {
                    MessageOK("Успешно");
                }
            }
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

        private void q3_TextChanged(object sender, TextChangedEventArgs e)
        {
            sw = CreateMD5(q3.Text);
        }
    }

}
