using System;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace ChatServer
{
    public class ClientObject
    {
        public string UserName => userName;
        protected internal string Id { get; private set; }
        protected internal NetworkStream Stream {get; private set;}
        string userName;
        TcpClient client;
        ServerObject server; // объект сервера
 
        public ClientObject(TcpClient tcpClient, ServerObject serverObject)
        {
            Id = Guid.NewGuid().ToString();
            client = tcpClient;
            server = serverObject;
            serverObject.AddConnection(this);
        }
 
        public void Process()
        {
            try
            {
                Stream = client.GetStream();
                // получаем имя пользователя
                string message = GetMessage();
                while (!server.IsValidUsername(message))
                {
                    server.SendMessageToId("invalidlogin|", Id);
                    message = GetMessage();
                }
                userName = message;
                server.SendMessageTo("successeslogin|", UserName);
                server.BroadcastUserlist();
                
                message = $"msg|{userName} вошел в чат|";
                // посылаем сообщение о входе в чат всем подключенным пользователям
                server.BroadcastMessage(message);
                Console.WriteLine(message);
                // в бесконечном цикле получаем сообщения от клиента
                while (true)
                {
                    try
                    {
                        message = GetMessage();
                        if (message[0] == '@')
                        {
                            var receiver = new string(message.Skip(1).TakeWhile(x =>  x != ' ').ToArray());
                            message = new string(message.Skip(receiver.Length + 1).ToArray());
                            message = $"msg|[{userName}] -> [{receiver}] :{message}|";
                            Console.WriteLine(message);
                            if (!server.SendMessageTo(message, receiver))
                            {
                                server.SendMessageTo("msg| Пользователь не найден.|", userName);
                            }
                        }
                        else
                        {
                            message = $"msg|{userName}: {message}|";
                            Console.WriteLine(message);
                            server.BroadcastMessage(message);                           
                        }
                    }
                    catch
                    {
                        message = String.Format("msg|{0}: покинул чат|", userName);
                        Console.WriteLine(message);
                        server.BroadcastMessage(message);
                        break;
                    }
                }
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
            }
            finally
            {
                // в случае выхода из цикла закрываем ресурсы
                server.RemoveConnection(this.Id);
                Close();
            }
        }
 
        // чтение входящего сообщения и преобразование в строку
        private string GetMessage()
        {
            byte[] data = new byte[64]; // буфер для получаемых данных
            StringBuilder builder = new StringBuilder();
            int bytes = 0;
            do
            {
                bytes = Stream.Read(data, 0, data.Length);
                builder.Append(Encoding.Unicode.GetString(data, 0, bytes));
            }
            while (Stream.DataAvailable);
 
            return builder.ToString();
        }
 
        // закрытие подключения
        protected internal void Close()
        {
            if (Stream != null)
                Stream.Close();
            if (client != null)
                client.Close();
        }
    }
}