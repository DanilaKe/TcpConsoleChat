using System;
using System.Threading;
using System.Windows.Forms;

namespace TcpGet
{
    public partial class ServerForm : Form
    {
        static ServerObject server; // сервер
        static Thread listenThread; // потока для прослушивания
        private delegate void LogEvent(string content);
        private LogEvent _addMessage;

        public ServerForm()
        {
            InitializeComponent();
            _addMessage = new LogEvent(PrintLog);
        }

        private void Connect_Click(object sender, EventArgs e)
        {
            try
            {
                var _port = Convert.ToInt32(Port.Text);
                if (_port < 1 || _port > 999999)
                {
                    throw new Exception();
                }
                Port.Enabled = false;
                Connect.Enabled = false;
                ConnectServer(_port);
            }
            catch(Exception ex)
            {
                Port.Enabled = true;
                Connect.Enabled = true;
                MessageBox.Show(ex.Message);
            }
        }

        private void Server_FormClosed(object sender, FormClosedEventArgs e)
        {
            server.Disconnect();
            Environment.Exit(0);
        }

        private void ConnectServer(int port)
        {
            try
            {
                server = new ServerObject(port);
                server.ServerMessage += PrintLog;
                listenThread = new Thread(new ThreadStart(server.Listen));
                listenThread.Start(); //старт потока
            }
            catch (Exception ex)
            {
                server.Disconnect();
                throw;
            }
        }

        public void PrintLog(string message)
        {
            if (InvokeRequired)
            {
                Invoke(_addMessage, message);
                return;
            }
            LogText.SelectionStart = LogText.TextLength;
            LogText.SelectionLength = message.Length;
            LogText.AppendText(message + Environment.NewLine);
        }
    }
}
