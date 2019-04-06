using System;
using System.Threading;
using System.Net.Sockets;
using System.Text;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;

namespace Client
{
    public partial class ClientForm : Form
    {
        private bool IsConnected = false;
        static string userName;
        static TcpClient client;
        static NetworkStream stream;

        private void SendMessage(string message)
        {
            byte[] data = Encoding.Unicode.GetBytes(message);
            stream.Write(data, 0, data.Length);
        }

        // отправка сообщений
        private void SendMessageClick(object sender, EventArgs e)
        {
            SendMessage(Message.Text);
            Message.Text = string.Empty;
        }

        // получение сообщений
        void ReceiveMessage()
        {
            while (true)
            {
                try
                {
                    byte[] data = new byte[64]; // буфер для получаемых данных
                    StringBuilder builder = new StringBuilder();
                    int bytes = 0;
                    do
                    {
                        bytes = stream.Read(data, 0, data.Length);
                        builder.Append(Encoding.Unicode.GetString(data, 0, bytes));
                    }
                    while (stream.DataAvailable);

                    var command = builder.ToString();
                    InvokeCommand(command);            
                }    
                catch
                {
                    Console.WriteLine("Подключение прервано!"); //соединение было прервано
                    Disconnect();
                }
            }
        }

        static void Disconnect()
        {
            if (stream != null)
                stream.Close();//отключение потока
            if (client != null)
                client.Close();//отключение клиента
            Environment.Exit(0); //завершение процесса
        }

        public ClientForm()
        {
            InitializeComponent();
            ChatBox.Enabled = false;
            UserBox.Enabled = false;
            SendButton.Enabled = false;
            Message.Enabled = false;
        }

        private void ConnectClick(object sender, EventArgs e)
        {     
            client = new TcpClient();
            try
            {
                client.Connect(IpAddress.Text, int.Parse(Port.Text)); //подключение клиента
                stream = client.GetStream(); // получаем поток
       
                // запускаем новый поток для получения данных
                Thread receiveThread = new Thread(new ThreadStart(ReceiveMessage));
                receiveThread.Start(); //старт потока
                SendMessage(UserName.Text);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private void Client_Load(object sender, EventArgs e)
        {
        }

        private void InvokeCommand(string command)
        {
            var commands = command.Split('|');
            switch (commands[0])
            {
                case "msg":
                    PrintMessage(commands[1]);
                    break;
                case "successeslogin":
                    SuccessfulLogin();
                    break;
                case "invalidlogin":
                    InvalidLogin();
                    break;
                case "userlist":
                    UserList(commands[1]);
                    break;
                
            }
        }

        private void PrintMessage(string message)
        {
            ChatBox.AppendText(message+"\n");
        }

        private void SuccessfulLogin()
        {
            IsConnected = !IsConnected;
            ChatBox.Enabled = !ChatBox.Enabled;
            UserBox.Enabled = !UserBox.Enabled;
            SendButton.Enabled = !SendButton.Enabled;
            Message.Enabled = !Message.Enabled;
            IpAddress.Enabled = !IpAddress.Enabled;
            Port.Enabled = !Port.Enabled;
            UserName.Enabled = !UserName.Enabled;
            ConnectButton.Enabled = !ConnectButton.Enabled;
            ConnectButton.Text = "Подключено";
        }

        private void InvalidLogin()
        {
            MessageBox.Show($"Неверный логин.","Файл",MessageBoxButtons.OK);
        }

        private void UserList(string userlist)
        {
            var users = userlist.Split('/');
            UserBox.Items.Clear();
            foreach (var user in users)
            {
                UserBox.Items.Add(user);
            }
        }
    }
}
