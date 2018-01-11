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
using System.Net.WebSockets;
using System.Threading;
using System.Net.Sockets;
using System.Net;
using System.IO;

namespace HiaChat_Server
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
            ////NpgsqlConnection connection = new NpgsqlConnection("Server = 192.168.44.10; Port = 5432; UserId = HiaChat; Password = hiahia;Database = HiaChatDB");
            ////NpgsqlConnection connection = new NpgsqlConnection("Server = localhost; Port = 2222; UserId = HiaChat; Password = hiahia;Database = HiaChatDB");
            //NpgsqlConnection connection = new NpgsqlConnection("Server = 192.168.1.109; Port = 2222; UserId = HiaChat; Password = hiahia;Database = HiaChatDB");

            //connection.Open();
            //string sqlCommand = "SELECT nick_name FROM public.basic_user_inf where id = 5;";
            //NpgsqlCommand command = new NpgsqlCommand(sqlCommand, connection);
            //NpgsqlDataReader reader = command.ExecuteReader();
            //reader.Read();
            //string data;
            //data = reader.GetString(0);

            //label.Content = data;
            //connection.Close();


            ////仅执行SQL语句
            ////command.ExecuteNonQuery();
            //ClientWebSocket clientWebSocket = new ClientWebSocket();

            IPAddress[] ip = Dns.GetHostEntry(Dns.GetHostName()).AddressList;
            IPAddress localAddress = ip[6];//本机IP
            Console.WriteLine(localAddress.ToString());
            foreach (IPAddress ipa in ip)
            {
                //if (ipa.AddressFamily == AddressFamily.InterNetwork)
                {
                   // localAddress = ipa;
                    Console.WriteLine("#{0}",ipa.ToString());
                }

            }
            TcpListener server = new TcpListener(IPAddress.Parse(Properties.Resources.ServerAddress), int.Parse(Properties.Resources.ServerLoginPort));
            
            
            Console.WriteLine("Server has started on {0}:{1}.{2}Waiting for a connection...", IPAddress.Parse(Properties.Resources.ServerAddress), int.Parse(Properties.Resources.ServerLoginPort),Environment.NewLine);
            server.Start();
            Socket s;
            while (true)
            {
                s = server.AcceptSocket();
                Thread t1=new Thread(new ThreadStart(mai));
                t1.Start();
            }

            void mai()
            {

                EndPoint tempRemoteEP = s.RemoteEndPoint;
                IPEndPoint tempRemoteIP = (IPEndPoint)tempRemoteEP;
                Console.WriteLine("temp Remote IP {0} {1}", tempRemoteIP.Address, tempRemoteIP.Port);
                IPHostEntry host = Dns.GetHostEntry(tempRemoteIP.Address);

                string HostName = host.HostName;

                Console.WriteLine(HostName);

                int j = 0;
                while (true)
                {
                    Console.WriteLine(j++);
                    Byte[] stream = new Byte[80];
                    //定义从远程计算机接收到数据存放的数据缓冲区 
                    string time = DateTime.Now.ToString();
                    //获得当前的时间 
                    int i = s.ReceiveFrom(stream, ref tempRemoteEP);
                    //接收数据，并存放到定义的缓冲区中 
                    string sMessage = Encoding.UTF8.GetString(stream);
                    //以指定的编码，从缓冲区中解析出内容 

                    Console.WriteLine(sMessage.TrimEnd('\0'));


                    //
                    TcpClient tcpc;
                    NetworkStream tcpStream;
                    StreamWriter reqStreamW;
                    IPEndPoint localIP;

                    tcpc = new TcpClient(tempRemoteIP.Address.ToString(), tempRemoteIP.Port);
                    tcpStream = tcpc.GetStream();
                    try
                    {
                        string sMsg = "====================";
                        string MyName = Dns.GetHostName();
                        //以特定的编码往向数据流中写入数据, 
                        //默认为UTF8Encoding 的实例 
                        reqStreamW = new StreamWriter(tcpStream);
                        //将字符串写入数据流中 
                        reqStreamW.Write(sMsg);
                        //清理当前编写器的所有缓冲区，并使所有缓冲数据写入基础流 
                        reqStreamW.Flush();

                        //Console.WriteLine(tcpc.Client.LocalEndPoint);
                        Thread.Sleep(50);
                    }
                    catch (Exception)
                    {
                        Console.WriteLine("Socketkit Send Error");
                    }

                }
            }



            server.Stop();
        }
        //delegate void AppendStrDg(string str);
        //private void AppendStr(string str)
        //{
        //    if (label.InvokeRequired)   //其他线程操作该控件
        //    {
        //        AppendStrDg dg = new AppendStrDg(AppendStr); //创建一个委托实例
        //        listBox2.Invoke(dg, str);
        //    }
        //    else
        //    {
        //        listBox2.Items.Add(str);
        //    }
        //}

    }
}
