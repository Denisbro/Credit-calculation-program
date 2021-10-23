using System;
using System.Configuration;
using System.Data.OleDb;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Security.Cryptography;
using System.IO;

namespace WpfApp6
{
    /// <summary>
    /// Логика взаимодействия для Window2.xaml
    /// </summary>
    public partial class Window2 : Window
    {
        string w1;
        string w2;
        string w3;
        string w4;
        string w5;
        string w6;
        string w7;
        public static string connectString = ConfigurationManager.ConnectionStrings["DB"].ConnectionString;
        private static OleDbConnection myConnection;
        private static OleDbCommand DA;
        private MainWindow w;
        MainWindow second;
        private readonly Random random = new Random();

        public Window2()
        {
            InitializeComponent();
        }

        private void textBoxLastName_TextInput(object sender, TextCompositionEventArgs e)
        {
            if (!Char.IsDigit(e.Text, 0))
            {
                e.Handled = true;
            }
        }

        private void textBoxFirstName_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (Char.IsDigit(e.Text, 0))
            {
                e.Handled = true;
            }
        }

        private void button2_Click(object sender, RoutedEventArgs e)
        {
            reset();
        }

        public void reset()
        {
            textBoxFirstName.Text = "";
            textBoxLastName.Text = "";
            textBoxEmail.Text = "";
            textBoxAddress.Text = "";
            datepi.Text="";
            pa.Text = "";
            y.Text = "";
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            w = new MainWindow();
            w.Show();
            this.Close();
        }

        private void Submit_Click(object sender, RoutedEventArgs e)
        {
            w1 = textBoxFirstName.Text;
            w2 = textBoxLastName.Text;
            w3 = textBoxEmail.Text;
            w4 = textBoxAddress.Text;
            if (datepi.Text!="")
            {
                w5 = datepi.SelectedDate.Value.ToString("dd.MM.yyyy");
            }
            w6 = pa.Text;
            w7 = y.Text;
            if (true)              //Ужасный код   !!!WARNING!!!         !!!WARNING!!!         !!!WARNING!!!
            {
                if (w1 == "")
                {
                    MessageOK("ЗАПОЛНИ ФИО");
                    textBoxFirstName.Focus();
                }
                else if (w2 == "")
                {
                    MessageOK("ЗАПОЛНИ ПАСПОРТНЫЕ ДАННЫЕ");
                    textBoxLastName.Focus();
                }
                else if (w3 == "")
                {
                    MessageOK("ЗАПОЛНИ Среднюю заработную плату");
                    textBoxEmail.Focus();
                }
                else if (w5 == null)
                {
                    MessageOK("ЗАПОЛНИ дату рождения");
                    datepi.Focus();
                }
                else if (w6 == "")
                {
                    MessageOK("ЗАПОЛНИ телефон ");
                    pa.Focus();
                }
                else if (w4 == "")
                {
                    MessageOK("ЗАПОЛНИ адрес проживания ");
                    textBoxAddress.Focus();
                }
                else if (w7 == "")
                {
                    MessageOK("ЗАПОЛНИ место работы ");
                    y.Focus();
                }
            }
            if (w1 != "" && w2 != "" && w3 != "" && w4 != "" && (w5 != null || w5!="")&& w6 != "" && w7 != "")
            {
                tab.SelectedIndex = 1;
            }
        }

        private static void MessageOK(string stroka)
        {
            MessageBoxButton messBoxBut = MessageBoxButton.OK;
            MessageBoxResult result;
            result = MessageBox.Show(stroka,"Warning",messBoxBut);
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int w = comb.SelectedIndex;
                switch (w)
                {
                    case 0:
                        et1.Text = "111";
                        et2.Text = "15000";
                        et3.Text = "12";
                        et4.Text = "6";                       
                            break;
                    case 1:
                        et1.Text = "221";
                        et2.Text = "16000";
                        et3.Text = "12";
                        et4.Text = "96";
                        break;
                    case 2:
                        et1.Text = "222";
                        et2.Text = "10000";
                        et3.Text = "18";
                        et4.Text = "36";
                        break;
                    case 3:
                        et1.Text = "223";
                        et2.Text = "19000";
                        et3.Text = "9";
                        et4.Text = "12";
                        break;
                    case 4:
                        et1.Text = "331";
                        et2.Text = "10000";
                        et3.Text = "14";
                        et4.Text = "36";
                        break;
                    case 5:
                        et1.Text = "332";
                        et2.Text = "8000";
                        et3.Text = "18";
                        et4.Text = "24";
                        break;
                }
            
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            tab.SelectedIndex = 0;
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            int r =Convert.ToInt32(textBoxEmail.Text);

            if (r < Convert.ToInt32(et2.Text))
            {
                string title = "Ошибка";
                MessageBoxButton messBoxBut = MessageBoxButton.OK;
                MessageBoxResult result;
                result = MessageBox.Show("Ваша заработная плата не соответствует минимальной\nМы вынуждены вам отказать в кредите", title, messBoxBut);
                second = new MainWindow();
                second.Show();
                this.Close();
            }
            else
            {
                if (inp.Text == "" || inp.Text == "0")
                {
                    MessageOK("Введите сумму кредитования"); inp.Focus();
                }
                else if (comb.SelectedIndex == -1)
                {
                    MessageOK("Вибирите вид кредитования");
                    comb.Focus();
                }
                if ((!(comb.SelectedIndex == -1) && !(inp.Text == "" || inp.Text == "0")))
                {
                    string title = "Подтвердите";
                    MessageBoxButton messBoxBut = MessageBoxButton.YesNo;
                    MessageBoxResult result;
                    result = MessageBox.Show("Сумма ежемесячного платежа составит:" + calc(inp.Text, et4.Text, et3.Text) + "\nВы согласны?", title, messBoxBut);

                    if (result == MessageBoxResult.Yes)
                    {
                        insertdb();
                      //  insert();
                    }
                    else if (result == MessageBoxResult.No)
                    {

                    }
                }
            }

        }
        private static double sumpereplat(int mec,int stavka,int cym)
        {
            double q = (double)1 + ((double)stavka / 1200);
            double t = Math.Pow(q, mec) * (q - 1) / (Math.Pow(q, mec) - 1);
            t *= cym;
            t *= mec;
            int y = (int)t;
            return y;
        }

        private void insert()
        {

            string querry = "INSERT INTO Возврат (ФИО, [Код кредита], [Ежемесячный платёж], [Дата возврата], [Сумма возврата]) VALUES('"+w1+"','"+ et1 + "',"+ calc(inp.Text, et4.Text, et3.Text) + ",'"+ datepi.SelectedDate.Value.AddMonths(Convert.ToInt32(et4)).ToString("dd.MM.yyyy")+"',"+ sumpereplat(Convert.ToInt32(et4), Convert.ToInt32(et3), Convert.ToInt32(inp)) + ")";
            try
            {
                // создаем экземпляр класса OleDbConnection
                myConnection = new OleDbConnection(connectString);
                // открываем соединение с БД
                myConnection.Open();

                DA = new OleDbCommand(querry, myConnection);
                DA.ExecuteNonQuery();

                myConnection.Close();

                MessageOK("Операция выполнена!");
                string passworduser = CreatePassword(12);
                string username = "user" + random.Next(0, 3000);
                MessageOK($"Ваш логин:{username}\nВаш пароль:{passworduser}\nНе сообщайте логин и пароль сторонним лицам!");


                passworduser = CreateMD5(passworduser);

                string querry2 = "INSERT INTO tbluser(UserName, [Password], [Right]) VALUES('" + username + "','" + passworduser + "','user')";
                // создаем экземпляр класса OleDbConnection
                myConnection = new OleDbConnection(connectString);
                // открываем соединение с БД
                myConnection.Open();

                DA = new OleDbCommand(querry2, myConnection);
                DA.ExecuteNonQuery();

                myConnection.Close();


                second = new MainWindow();
                second.Show();
                this.Close();
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void insertdb()
        {
            string querry = "INSERT INTO Клиент (Фио, [Паспортные данные], [Средняя заработная плата], Телефон, [Адрес проживания], [Место работы],[Дата рождения]) VALUES('" + w1 + "'," + w2 + "," + w3 + "," + w6 + ",'" + w4 + "','" + w7 + "','" + w5 + "')";
            try
            {
                // создаем экземпляр класса OleDbConnection
                myConnection = new OleDbConnection(connectString);
                // открываем соединение с БД
                myConnection.Open();

                DA = new OleDbCommand(querry, myConnection);
                DA.ExecuteNonQuery();

                myConnection.Close();

                MessageOK("Операция выполнена!");
                string passworduser = CreatePassword(12);
                string username = "user" + random.Next(0, 3000);
                MessageOK($"Ваш логин:{username}\nВаш пароль:{passworduser}\nНе сообщайте логин и пароль сторонним лицам!\nДля вашего удобства пароль скопирован в буфер.");
                Clipboard.SetText($"{passworduser}");

                using (FileStream fstream = new FileStream(@"Ваша учетная запись.txt", FileMode.OpenOrCreate))
                {
                    // преобразуем строку в байты
                    byte[] array = Encoding.Default.GetBytes($"Ваш логин:{username}\nВаш пароль:{passworduser}.\nНе сообщайте логин и пароль сторонним лицам!");
                    // запись массива байтов в файл
                    fstream.Write(array, 0, array.Length);
                }
                passworduser = CreateMD5(passworduser);

                string querry2 = "INSERT INTO tbluser(UserName, [Password], [Right],фио) VALUES('" + username + "','" + passworduser + "','user','" + w1 + "')";
                // создаем экземпляр класса OleDbConnection
                myConnection = new OleDbConnection(connectString);
                // открываем соединение с БД
                myConnection.Open();

                DA = new OleDbCommand(querry2, myConnection);
                DA.ExecuteNonQuery();

                myConnection.Close();


                string querry3 = "INSERT INTO Возврат (ФИО, [Код кредита], [Ежемесячный платёж], [Дата возврата], [Сумма возврата]) VALUES('"+w1+"','"+et1.Text+"','"+ calc(inp.Text, et4.Text, et3.Text) + "','"+ datepi.SelectedDate.Value.AddMonths(Convert.ToInt32(et4.Text)).ToString("dd.MM.yyyy") + "','"+ sumpereplat (Convert.ToInt32(et4.Text), Convert.ToInt32(et3.Text), Convert.ToInt32(inp.Text)) + "')";
                // создаем экземпляр класса OleDbConnection
                myConnection = new OleDbConnection(connectString);
                // открываем соединение с БД
                myConnection.Open();

                DA = new OleDbCommand(querry3, myConnection);
                DA.ExecuteNonQuery();


                second = new MainWindow();
                second.Show();
                this.Close();

                myConnection.Close();
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public string CreatePassword(int length)
        {
            const string valid = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890";
            StringBuilder res = new StringBuilder();
            Random rnd = new Random();
            while (0 < length--)
            {
                res.Append(valid[rnd.Next(valid.Length)]);
            }
            return res.ToString();
        }

        private static string calc(string q,string w,string i) {
            int sum = Int32.Parse(q);
            int mec = Int32.Parse(w);
            int proc=1;
            switch (i)
            {
                case "12%":
                    proc = 12;
                    break;
                case "18%":
                    proc = 18;
                    break;
                case "9%":
                    proc = 9;
                    break;
                case "14%":
                    proc = 14;
                    break;
            }

            int x = sum/12+1;
            int s = (int)proch(sum,proc)*mec/365+1;
            s+=x;
            return s.ToString();
        } 

        private static double proch(int x, int y) => Math.Round((double)(x * y / 100));
       
        public static string CreateMD5(string input)
        {// Функция преобразования строки к MD5 

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

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            Window4 w4 = new Window4();
            w4.ShowDialog();
        }
    }
}
