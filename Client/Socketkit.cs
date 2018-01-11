using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.Net;
using System.IO;
using System.Threading;

namespace HiaChat_Client
{

    class Socketkit
    {
        TcpClient tcpc;
        NetworkStream tcpStream;
        StreamWriter reqStreamW;
        IPEndPoint localIP;

        TcpListener tcpl;
        Socket s;

        EndPoint tempRemoteEP;
        IPEndPoint tempRemoteIP;


        public Socketkit()
        {
            
            
        }

        public string TcpLogin(string number,string password)
        {
            Connect(Properties.Resources.ServerAddress, int.Parse(Properties.Resources.ServerLoginPort));
            
            Send(string.Format("{0}:{1}",number,password));
            ListenStart();
            string msg = ReceiveOnce();
            ListenStop();
            
            return msg;
        }

        public string TcpSignup(string name,string password)
        {
            Connect(Properties.Resources.ServerAddress, int.Parse(Properties.Resources.ServerSignupPort));
            Send(string.Format("{0}:{1}", name, password));
            ListenStart();
            string msg = ReceiveOnce();
            ListenStop();

            return msg;
        }

        public void Connect(string address,int port)
        {
            tcpc = new TcpClient(address, port);
            //向远程计算机提出连接申请 
            tcpStream = tcpc.GetStream();
            localIP = (IPEndPoint)tcpc.Client.LocalEndPoint;
            Thread.Sleep(50);
        }


        public void ListenStart()
        {
            tcpl = new TcpListener(localIP.Address, localIP.Port);
            //监听
            tcpl.Start();
            s = tcpl.AcceptSocket();
            tempRemoteEP = s.RemoteEndPoint;
            tempRemoteIP = (IPEndPoint)tempRemoteEP;

            Console.WriteLine(string.Format("=={0}:{1} {2}:{3}",localIP.Address,localIP.Port,tempRemoteIP.Address,tempRemoteIP.Port));
        }

        public void ListenStop()
        {
            tcpl.Stop();
        }

        public string ReceiveOnce()
        {
            string msg;
            Byte[] stream = new Byte[80];
            //定义从远程计算机接收到数据存放的数据缓冲区 
            string time = DateTime.Now.ToString();
            //获得当前的时间 
            int i = s.ReceiveFrom(stream, ref tempRemoteEP);
            //接收数据，并存放到定义的缓冲区中 
            msg = Encoding.UTF8.GetString(stream);
            //以指定的编码，从缓冲区中解析出内容 

            return msg.TrimEnd('\0');
        }

        public void Send(string msg)
        {
            try
            {
                string sMsg = msg;
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
}
