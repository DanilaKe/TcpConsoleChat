using System;
using System.Threading;
using System.Net.Sockets;
using System.Text;
using System.Windows.Forms;

namespace Client
{
    public partial class ClientForm : Form
    {
        private bool IsConnected = false;
        static string userName;
        private delegate void ChatEvent(string content);
        private ChatEvent _addMessage;
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
            _addMessage = new ChatEvent(PrintMessage);
            ChatBox.Enabled = true;
            UserBox.Enabled = false;
            SendButton.Enabled = false;
            Message.Enabled = false;
            client = new TcpClient();
            UserMenu = new ContextMenuStrip();
            ToolStripMenuItem PrivateMessageItem = new ToolStripMenuItem();
            PrivateMessageItem.Text = "Личное сообщение";
            PrivateMessageItem.Click += delegate 
            {
                if (UserBox.SelectedItems.Count > 0)
                {
                    Message.Text = $"@{UserBox.SelectedItem} ";
                }
            };
            UserMenu.Items.Add(PrivateMessageItem);
            UserBox.ContextMenuStrip = UserMenu;
        }

        private void ConnectClick(object sender, EventArgs e)
        {
            try
            {
                if (!IsConnected)
                {
                    client.Connect(IpAddress.Text, int.Parse(Port.Text)); //подключение клиента
                    stream = client.GetStream(); // получаем поток
                    IsConnected = !IsConnected;
                    Invoke((MethodInvoker)delegate
                    {
                        PrintMessage($"Подключенно к серверу {IpAddress.Text}:{Port.Text}");
                        IpAddress.Enabled = !IpAddress.Enabled;
                        Port.Enabled = !Port.Enabled;
                    });
       
                    // запускаем новый поток для получения данных
                    Thread receiveThread = new Thread(new ThreadStart(ReceiveMessage));
                    receiveThread.Start(); //старт потока
                }
                SendMessage(UserName.Text);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
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
            if (InvokeRequired)
            {
                Invoke(_addMessage, message);
                return;
            }
            ChatBox.SelectionStart = ChatBox.TextLength;
            ChatBox.SelectionLength = message.Length;
            ChatBox.AppendText(message + Environment.NewLine);

        }

        private void SuccessfulLogin()
        {
            Invoke((MethodInvoker)delegate
            {
                PrintMessage($"Добро пожаловать");
                UserBox.Enabled = true;
                SendButton.Enabled = true;
                Message.Enabled = true;
                UserName.Enabled = false;
                ConnectButton.Enabled = false;
                ConnectButton.Text = "Подключено";
            });
        }

        private void InvalidLogin()
        {
            MessageBox.Show($"Неверный логин.","Файл",MessageBoxButtons.OK);
        }

        private void UserList(string userlist)
        {
            var users = userlist.Split('/');
            int countUsers = users.Length;
            UserBox.Invoke((MethodInvoker)delegate { UserBox.Items.Clear(); });
            for (int j = 0; j < countUsers; j++)
            {
                UserBox.Invoke((MethodInvoker)delegate { UserBox.Items.Add(users[j]); });
            }
        }

        private void ClientFom_Close(object sender, FormClosedEventArgs e)
        {
            SendMessage("#userdisconnect");
            Disconnect();
        }
    }
}
