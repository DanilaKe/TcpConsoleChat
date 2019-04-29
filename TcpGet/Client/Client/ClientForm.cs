using System;
using System.Threading;
using System.Net.Sockets;
using System.Text;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;
using System.IO;
using System.Linq;

namespace Client
{
    public partial class ClientForm : Form
    {
        private bool IsConnected = false;
        static string userName;
        static TcpClient client;
            private delegate void ChatEvent(string content);
            private ChatEvent _addMessage;
        static NetworkStream stream;

        private void SendCommand(string command)
        {
            byte[] data = Encoding.Unicode.GetBytes(command);
            stream.Write(data, 0, data.Length);
        }

        // отправка сообщений
        private void SendMessageClick(object sender, EventArgs e)
        {
            SendCommand(Command.Text);
            Command.Text = string.Empty;    
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
                    } while (stream.DataAvailable);

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
            SendButton.Enabled = false;
            Command.Enabled = false;
            _addMessage = new ChatEvent(PrintFile);
            client = new TcpClient();
            FileText.Enabled = true;
            Directory.Text = ".";
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
                    IpAddress.Enabled = !IpAddress.Enabled;
                    Port.Enabled = !Port.Enabled;
                    Command.Enabled = !Command.Enabled;
                    SendButton.Enabled = !SendButton.Enabled;
                    ConnectButton.Enabled = !ConnectButton.Enabled;
                    Command.Text = "GET .\\1.txt";

                    // запускаем новый поток для получения данных
                    Thread receiveThread = new Thread(new ThreadStart(ReceiveMessage));
                    receiveThread.Start(); //старт потока
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
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
                case "#gfile":
                    GetFile(command);
                    break;
            }
        }

        private void PrintFile(string path)
        {
            if (InvokeRequired)
            {
                Invoke(_addMessage, path);
                return;
            }
            string file = string.Empty;
            using (StreamReader sr = new StreamReader(path))
            {
                file = sr.ReadToEnd();
            }
            FileText.Clear();
            FileText.SelectionStart = FileText.TextLength;
            FileText.SelectionLength = file.Length;
            FileText.AppendText(file + Environment.NewLine);
        }

        private void GetFile(string currentCommand)
        {
            string[] Arguments = currentCommand.Split('|');
            string fileName = Arguments[1];
            string FromName = Arguments[2];
            string FileSize = Arguments[3];
            string idFile = Arguments[4];
            DialogResult Result = MessageBox.Show($"Вы хотите принять файл {fileName} размером {FileSize} от {FromName}", "Файл", MessageBoxButtons.YesNo);
            if (Result == DialogResult.Yes)
            {
                Thread.Sleep(1000);
                SendCommand("#yy|" + idFile);
                byte[] fileBuffer = new byte[int.Parse(FileSize)];
                StringBuilder builder = new StringBuilder();
                int bytes = 0;
                do
                {
                    bytes = stream.Read(fileBuffer, 0, fileBuffer.Length);
                    builder.Append(Encoding.Unicode.GetString(fileBuffer, 0, bytes));
                }
                while (stream.DataAvailable);

                var path = Directory.Text + "\\" + fileName.Split('\\').Last();
                File.WriteAllBytes(path, fileBuffer);
                PrintFile(path);
                MessageBox.Show($"Файл {fileName} принят.");
            }
            else
                SendCommand("nn");
        }

        private void ClientForm_Load(object sender, EventArgs e)
        {

        }

        private void ClientForm_Close(object sender, FormClosedEventArgs e)
        {
            Disconnect();
        }
    }
}
