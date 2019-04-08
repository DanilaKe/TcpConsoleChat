using System;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;

namespace TcpGet
{
    public class ClientObject
    {
        protected internal string Id { get; private set; }
        protected internal NetworkStream Stream { get; private set; }
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
                server.PrintMessage(Id + "подключен.");
                // в бесконечном цикле получаем сообщения от клиента
                while (true)
                {
                    var message = GetMessage();
                    if(new string(message.Take(3).ToArray()) == "GET")
                    {
                        var fileName = new string(message.Skip(4).ToArray());
                        if (!File.Exists(fileName))
                        {
                            server.SendMessageToId($"Файл : {fileName} не найден.", Id);
                            continue;
                        }
                        FileInfo fi = new FileInfo(fileName);
                        byte[] buffer = File.ReadAllBytes(fileName);
                        FileData newFile = new FileData()
                        {
                            ID = ServerObject.Files.Count + 1,
                            FileName = fileName,
                            From = "Server",
                            fileBuffer = buffer,
                            FileSize = buffer.Length
                        };
                        ServerObject.Files.Add(newFile);
                        SendFile(newFile, Id);
                    }

                    if (message.Contains("#yy|"))
                    {
                        string id = message.Split('|')[1];
                        FileData file = ServerObject.GetFileByID(int.Parse(id));
                        if (file.ID == 0)
                        {
                            server.SendMessageToId("Ошибка при передаче файла...", Id);
                            continue;
                        }
                        Stream.Write(file.fileBuffer, 0, file.fileBuffer.Length);
                    }
                }
            }
            catch (Exception e)
            {
                server.PrintMessage(e.Message);
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

        public void SendFile(FileData fd, string Id)
        {
            byte[] answerBuffer = new byte[48];
            server.PrintMessage($"Sending {fd.FileName} from {fd.From} to {Id}");
            server.SendMessageToId($"#gfile|{fd.FileName}|{fd.From}|{fd.fileBuffer.Length}|{fd.ID}", Id);
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