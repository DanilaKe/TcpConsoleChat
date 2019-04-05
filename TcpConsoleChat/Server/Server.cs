using System;
using System.Collections.Generic;
using System.Net.Sockets;

namespace Server
{
    public static class Server
    {
        public static List<FileData> Files = new List<FileData>();
        public delegate void UserEvent(string Name);
        public static List<User> UserList = new List<User>();
        public static Socket ServerSocket;
        public const string Host = "127.0.0.1";
        public const int Port = 2222;
        public static bool Work = true;
        
        public static event UserEvent UserConnected = (Username) =>
        {
            Console.WriteLine($"User {Username} connected.");
            SendGlobalMessage($"Пользователь {Username} подключился к чату.", "Black");
            SendUserList();
        };
        
        public static event UserEvent UserDisconnected = (Username) =>
        {
            Console.WriteLine($"User {Username} disconnected.");
            SendGlobalMessage($"Пользователь {Username} отключился от чата.","Black");
            SendUserList();
        };
        
        public static FileData GetFileByID(int ID)
        {
            foreach (var file in Files)
            {
                if (file.ID == ID)
                    return file;
            }
            
            return new FileData() { ID = 0};
        }
        
        public static void NewUser(User user)
        {
            if (UserList.Contains(user))
                return;
            UserList.Add(user);
            UserConnected(user.Username);
        }
        
        public static void EndUser(User user)
        {
            if (!UserList.Contains(user))
                return;
            UserList.Remove(user);
            user.End();
            UserDisconnected(user.Username);

        }

        public static User GetUser(string Name)
        {
            foreach (var user in UserList)
            {
                if (user.Username == Name)
                    return user;
            }
            
            return null;
        }
        
        public static void SendUserList()
        {
            var userList = "#userlist|";
            foreach (var user in UserList)
            {
                userList += user.Username + ",";
            }

            SendAllUsers(userList);
        }
        
        public static void SendGlobalMessage(string content,string clr)
        {
            foreach (var user in UserList)
            {
                user.SendMessage(content, clr);
            }
        }
        
        public static void SendAllUsers(byte[] data)
        {
            foreach (var user in UserList)
            {
                user.Send(data);
            }
        }
        
        public static void SendAllUsers(string data)
        {
            foreach (var user in UserList)
            {
                user.Send(data);
            }
        }
    }
}
