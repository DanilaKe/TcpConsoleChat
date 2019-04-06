using System;
using System.Text;
using System.Net.Sockets;
using System.Threading;

namespace Server
{
    public class User
    {
        private Thread _userThread;
        private string _userName;
        private bool _authSuccess = false;
        private Socket _userHandle;
        public string Username => _userName;
        
        public User(Socket handle)
        {
            _userHandle = handle;
            _userThread = new Thread(Listen);
            _userThread.IsBackground = true;
            _userThread.Start();
        }
        
        private void Listen()
        {
            try
            {
                while (_userHandle.Connected)
                {
                    byte[] buffer = new byte[2048];
                    int bytesReceive = _userHandle.Receive(buffer);
                    HandleCommand(Encoding.Unicode.GetString(buffer, 0, bytesReceive));
                }
            }
            catch
            {
                Server.EndUser(this);
            }
        }
        
        private bool SetName(string Name)
        {
            _userName = Name;
            Server.NewUser(this);
            _authSuccess = true;
            return true;
        }
        
        private void HandleCommand(string cmd)
        {
            try
            {
                string[] commands = cmd.Split('#');
                int countCommands = commands.Length;
                for (int i = 0; i < countCommands; i++)
                {
                    string currentCommand = commands[i];
                    switch (IdentifyCommand(currentCommand))
                    {
                        case TypeOfCommand.Empty:
                            break;
                        case TypeOfCommand.SetName:
                            SetNameCommand(currentCommand);
                            break;
                        case TypeOfCommand.GetFile:
                            GetFileCommand(currentCommand);
                            break;    
                        case TypeOfCommand.SendGlobalMessage:
                            break;
                        case TypeOfCommand.EndSession:
                            break;
                        case TypeOfCommand.SendFileTo:
                            break;
                        case TypeOfCommand.PrivateMessage:
                            break;
                    }
                    if (string.IsNullOrEmpty(currentCommand))
                        continue;
                    if (!_authSuccess)
                    {
                        

                        continue;
                    }

                    if (currentCommand.Contains("yy"))
                    {
                        string id = currentCommand.Split('|')[1];
                        FileData file = Server.GetFileByID(int.Parse(id));
                        if (file.ID == 0)
                        {
                            SendMessage("Ошибка при передаче файла...", "1");
                            continue;
                        }

                        Send(file.fileBuffer);
                        Server.Files.Remove(file);
                    }

                    if (currentCommand.Contains("message"))
                    {
                        string[] Arguments = currentCommand.Split('|');
                        Server.SendGlobalMessage($"[{_userName}]: {Arguments[1]}", "Black");

                        continue;
                    }

                    if (currentCommand.Contains("endsession"))
                    {
                        Server.EndUser(this);
                        return;
                    }

                    if (currentCommand.Contains("sendfileto"))
                    {
                        string[] Arguments = currentCommand.Split('|');
                        string TargetName = Arguments[1];
                        int FileSize = int.Parse(Arguments[2]);
                        string FileName = Arguments[3];
                        byte[] fileBuffer = new byte[FileSize];
                        _userHandle.Receive(fileBuffer);
                        User targetUser = Server.GetUser(TargetName);
                        if (targetUser == null)
                        {
                            SendMessage($"Пользователь {FileName} не найден!", "Black");
                            continue;
                        }

                        FileData newFile = new FileData()
                        {
                            ID = Server.Files.Count + 1,
                            FileName = FileName,
                            From = Username,
                            fileBuffer = fileBuffer,
                            FileSize = fileBuffer.Length
                        };
                        Server.Files.Add(newFile);
                        targetUser.SendFile(newFile, targetUser);
                    }

                    if (currentCommand.Contains("private"))
                    {
                        string[] Arguments = currentCommand.Split('|');
                        string TargetName = Arguments[1];
                        string Content = Arguments[2];
                        User targetUser = Server.GetUser(TargetName);
                        if (targetUser == null)
                        {
                            SendMessage($"Пользователь {TargetName} не найден!", "Red");
                            continue;
                        }

                        SendMessage($"-[Отправлено][{TargetName}]: {Content}", "Black");
                        targetUser.SendMessage($"-[Получено][{Username}]: {Content}", "Black");
                        continue;
                    }

                }

            }
            catch (Exception exp)
            {
                Console.WriteLine("Error with HandleCommand: " + exp.Message);
            }
        }
        
        private TypeOfCommand IdentifyCommand(string currentCommand)
        {
            if (string.IsNullOrEmpty(currentCommand)) return TypeOfCommand.Empty;
            if (!_authSuccess) return TypeOfCommand.SetName;
            if (currentCommand.Contains("getfile")) return TypeOfCommand.GetFile;
            if (currentCommand.Contains("message")) return TypeOfCommand.SendGlobalMessage;
            if (currentCommand.Contains("endsession")) return TypeOfCommand.EndSession;
            if (currentCommand.Contains("sendfileto")) return TypeOfCommand.SendFileTo;
            if (currentCommand.Contains("private")) return TypeOfCommand.PrivateMessage;
            
            throw new Exception("Invalid command.");
        }

        private void SetNameCommand(string currentCommand)
        {
            if (currentCommand.Contains("setname"))
            {
                Send(SetName(currentCommand.Split('|')[1]) ? "#setnamesuccess" : "#setnamefailed");
            }
        }

        private void GetFileCommand(string currentCommand)
        {
            string id = currentCommand.Split('|')[1];
            FileData file = Server.GetFileByID(int.Parse(id));
            if (file.ID == 0)
            {
                SendMessage("Ошибка при передаче файла...", "1");
                return;
            }

            Send(file.fileBuffer);
            Server.Files.Remove(file);
        }
        
        public void SendFile(FileData fd, User To)
        {
            byte[] answerBuffer = new byte[48];
            Console.WriteLine($"Sending {fd.FileName} from {fd.From} to {To.Username}");
            To.Send($"#gfile|{fd.FileName}|{fd.From}|{fd.fileBuffer.Length}|{fd.ID}");
        }
        
        public void SendMessage(string content,string clr)
        {
            Send($"#msg|{content}|{clr}");
        }
        
        public void Send(byte[] buffer)
        {
            _userHandle.Send(buffer);
        }
        
        public void Send(string Buffer)
        {
            _userHandle.Send(Encoding.Unicode.GetBytes(Buffer));
        }
        
        public void End()
        {
            _userHandle.Close();
        }
    }
}
