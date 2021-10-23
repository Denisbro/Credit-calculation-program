using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using MaterialDesignColors;
using MaterialDesignThemes;

namespace WpfApp6
{
    /// <summary>
    /// Логика взаимодействия для Window3.xaml
    /// </summary>
    public partial class Window3 : Window
    {
       public string user;
        public static string connectString = ConfigurationManager.ConnectionStrings["DB"].ConnectionString;
        public Window3(string username)
        {
            InitializeComponent();
            user = username;
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

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            MainWindow mw = new MainWindow();
            mw.Show();
            this.Close();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

            OleDbConnection Con = new OleDbConnection(connectString); //Новое подключение
            OleDbCommand command = new OleDbCommand("SELECT фио FROM tbluser WHERE(UserName = '"+user+"')", Con); //Команда выбора данных
            Con.Open(); //Открываем соединение
            OleDbDataReader read = command.ExecuteReader(); //Считываем и извлекаем данные
            while (read.Read()) //Читаем пока есть данные
            {
                user=read.GetValue(0).ToString(); //Добавляем данные
            }
            Con.Close(); //Закрываем соединение

            ne.Content = user;
            Load("Код кредита",w1,user);
            Load("Ежемесячный платёж", w3, user);
            Load("Дата возврата", w2, user);
            Load("Сумма возврата", w4, user);
            Load2("Одобрение",bt1,user);
            if (bt1.Content.ToString() == "True")
            {
                bt1.Background = new SolidColorBrush(Colors.LawnGreen);
                bt1.Content = "Одобрено";
            }
            else
            {
                bt1.Background = new SolidColorBrush(Colors.Red);
                bt1.Content = "Отказано";
            }
        }

        private static void Load(string pole,Label lb,string user)
        {
            OleDbConnection Con = new OleDbConnection(connectString); //Новое подключение
            OleDbCommand command = new OleDbCommand("SELECT ["+pole+"] FROM Возврат WHERE(ФИО = '"+user+"')", Con); //Команда выбора данных
            Con.Open(); //Открываем соединение
            OleDbDataReader read = command.ExecuteReader(); //Считываем и извлекаем данные
            while (read.Read()) //Читаем пока есть данные
            {
               lb.Content=read.GetValue(0).ToString(); //Добавляем данные в лист итем
            }
            Con.Close(); //Закрываем соединение
        }

        private static void Load2(string pole, Button bt, string user)
        {
            OleDbConnection Con = new OleDbConnection(connectString); //Новое подключение
            OleDbCommand command = new OleDbCommand("SELECT [" + pole + "] FROM Клиент WHERE(ФИО = '" + user + "')", Con); //Команда выбора данных
            Con.Open(); //Открываем соединение
            OleDbDataReader read = command.ExecuteReader(); //Считываем и извлекаем данные
            while (read.Read()) //Читаем пока есть данные
            {
              bt.Content = read.GetValue(0).ToString(); //Добавляем данные в лист итем
            }
            Con.Close(); //Закрываем соединение

        }
    }
}
