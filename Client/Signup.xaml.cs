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

namespace HiaChat_Client
{
    /// <summary>
    /// Signup.xaml 的交互逻辑
    /// </summary>
    public partial class Signup : Window
    {
        Login login;
        public Signup(Login login)
        {
            InitializeComponent();
            this.login = login;
            this.login.Hide();
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

        private void Btsignup_Click(object sender, RoutedEventArgs e)
        {
            Socketkit s = new Socketkit();
            string number = s.TcpSignup(tb_number.Text,tb_password.Text);

            MessageBox.Show(string.Format("注册成功！您的号码为：{0}",number), "注册成功！");
            this.Hide();
            this.login.Show();
        }
    }
}
