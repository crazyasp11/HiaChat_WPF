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
using System.Net.WebSockets;
using System.Threading;
using System.Collections.ObjectModel;

namespace HiaChat_Client
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        ObservableCollection<Friend> list_of_friend;
        public MainWindow()
        {
            InitializeComponent();
            list_of_friend = new ObservableCollection<Friend>();
            listboxfriend.ItemsSource = list_of_friend;
            //list_of_friend.Add(new Friend("wo", "1"));

            InitCommunication();
            
        }
        
        
        private void InitCommunication()
        {
            //=====================
            //启动发送消息线程
            //启动监听消息线程
            //添加好友一个PORT AND 聊天一个PORT

        }

        private void Listboxfriend_MouseDown(object sender, MouseButtonEventArgs e)
        {
            int index = listboxfriend.SelectedIndex;
            //显示选中的人的聊天记录=======================

        }

        private void SearchFriend_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key.ToString() == "Retuen")
            {
                if (tbsearch.Text.Equals(null))
                {
                    return;
                }
                //查找朋友==========================
                string msg = " ";

                if (msg == "错误")
                {
                    MessageBox.Show(string.Format("寻找{0}号码失败",tbsearch.Text),"失败");
                    tbsearch.Clear();
                }
                else if (msg == "已添加")
                {
                    MessageBox.Show(string.Format("{0}已经是好友了", msg), "已添加");
                    tbsearch.Clear();
                }
                else
                {
                    MessageBox.Show(string.Format("添加朋友{0}成功",msg),"成功");
                    list_of_friend.Add(new Friend(msg, tbsearch.Text));
                    tbsearch.Clear();
                    
                }

            }
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            //关闭前的操作================

            base.OnClosed(e);
        }

        private void Send_MouseDown(object sender, MouseButtonEventArgs e)
        {
            //发送信息===================
        }
    }
}
