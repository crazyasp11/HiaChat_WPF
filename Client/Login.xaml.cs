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
using System.Windows.Shapes;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.IO;

namespace HiaChat_Client
{
    /// <summary>
    /// Login.xaml 的交互逻辑
    /// </summary>
    public partial class Login : Window
    {
        public Login()
        {
            InitializeComponent();
        }

        private void Btlogin_Click(object sender, RoutedEventArgs e)
        {
            Socketkit sk = new Socketkit();
            string msg = sk.TcpLogin(tb_number.Text, tb_password.Password);
            MessageBox.Show(msg);
            if (msg.Equals("LOGIN?OK"))
            {
                //登录成功

            }
            else
            {
                return;
            }

            
            
        }

        private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            base.OnMouseLeftButtonDown(e);
            // 获取鼠标相对标题栏位置  
            Point position = e.GetPosition(gridTitle);

            // 如果鼠标位置在标题栏内，允许拖动  
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                if (position.X >= 0 && position.X < gridTitle.ActualWidth && position.Y >= 0 && position.Y < gridTitle.ActualHeight)
                {
                    this.DragMove();
                }
            }
        }

        private void Cancel_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Environment.Exit(0);
        }

        private void Signup_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Signup signup = new Signup(this);
            signup.Show();
            

        }
    }
}
