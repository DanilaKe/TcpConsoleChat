using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Threading;
 
namespace ChatServer
{
    public class ServerObject
    {
        static TcpListener tcpListener; // сервер для прослушивания
        List<ClientObject> clients = new List<ClientObject>(); // все подключения
 
        protected internal void AddConnection(ClientObject clientObject)
        {
            clients.Add(clientObject);
        }
        protected internal void RemoveConnection(string id)
        {
            // получаем по id закрытое подключение
            ClientObject client = clients.FirstOrDefault(c => c.Id == id);
            // и удаляем его из списка подключений
            if (client != null)
                clients.Remove(client);

            BroadcastUserlist();
        }
        // прослушивание входящих подключений
        protected internal void Listen()
        {
            try
            {
                tcpListener = new TcpListener(IPAddress.Any, 8888);
                tcpListener.Start();
                Console.WriteLine("Сервер запущен. Ожидание подключений...");
 
                while (true)
                {
                    TcpClient tcpClient = tcpListener.AcceptTcpClient();
 
                    ClientObject clientObject = new ClientObject(tcpClient, this);
                    Thread clientThread = new Thread(new ThreadStart(clientObject.Process));
                    clientThread.Start();
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                Disconnect();
            }
        }
 
        // трансляция сообщения подключенным клиентам
        protected internal void BroadcastMessage(string message)
        {
            byte[] data = Encoding.Unicode.GetBytes(message);
            for (int i = 0; i < clients.Count; i++)
            {
                clients[i].Stream.Write(data, 0, data.Length); //передача данных
            }
        }

        protected internal bool SendMessageTo(string message, string receiver)
        {
            byte[] data = Encoding.Unicode.GetBytes(message);
            foreach (var client in clients)
            {
                if (client.UserName == receiver)
                {
                    client.Stream.Write(data, 0, data.Length);
                    Thread.Sleep(1000);
                    return true;
                }
            }

            return false;
        }
        
        protected internal bool SendMessageToId(string message, string Id)
        {
            byte[] data = Encoding.Unicode.GetBytes(message);
            foreach (var client in clients)
            {
                if (client.Id == Id)
                {
                    client.Stream.Write(data, 0, data.Length);
                    Thread.Sleep(1000);
                    return true;
                }
            }

            return false;
        }
        
        // отключение всех клиентов
        protected internal void Disconnect()
        {
            tcpListener.Stop(); //остановка сервера
 
            for (int i = 0; i < clients.Count; i++)
            {
                clients[i].Close(); //отключение клиента
            }
            Environment.Exit(0); //завершение процесса
        }
        
        protected internal bool IsValidUsername(string username)
        {
            foreach (var client in clients)
            {
                if (client.UserName == username)
                {
                    return false;
                }
            }

            return true;
        }

        protected internal void BroadcastUserlist()
        {
            var message = new StringBuilder("userlist|");
            foreach (var client in clients)
            {
                message.Append(client.UserName + "/");
            }

            message.Append("|");
            BroadcastMessage(message.ToString());
        }
    }
}