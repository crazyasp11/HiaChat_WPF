using System;
using System.Collections.Generic;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using Npgsql;

namespace HiaChat_WPF
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //NpgsqlConnection connection = new NpgsqlConnection("Server = 192.168.44.10; Port = 5432; UserId = HiaChat; Password = hiahia;Database = HiaChatDB");
            //NpgsqlConnection connection = new NpgsqlConnection("Server = localhost; Port = 2222; UserId = HiaChat; Password = hiahia;Database = HiaChatDB");
            NpgsqlConnection connection = new NpgsqlConnection("Server = 192.168.1.109; Port = 2222; UserId = HiaChat; Password = hiahia;Database = HiaChatDB");

            connection.Open();
            string sqlCommand = "SELECT nick_name FROM public.basic_user_inf where id = 1;";
            NpgsqlCommand command = new NpgsqlCommand(sqlCommand, connection);
            NpgsqlDataReader reader = command.ExecuteReader();
            reader.Read();
            string data;
            data = reader.GetString(0);

            label.Content = data;
            connection.Close();


            //仅执行SQL语句
            //command.ExecuteNonQuery();

        }
    }
}
