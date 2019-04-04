using System;
using System.Collections.Generic;
using System.Net.Sockets;

namespace Server
{
    public static class Server
    {
        public static List<FileD> Files = new List<FileD>();
        public struct FileD
        {
            public int ID;
            public string FileName;
            public string From;
            public int FileSize;
            public byte[] fileBuffer;
        }
        public static int CountUsers = 0;
        public delegate void UserEvent(string Name);
        public static event UserEvent UserConnected = (Username) =>
        {
            Console.WriteLine($"User {Username} connected.");
            CountUsers++;
            SendGlobalMessage($"Пользователь {Username} подключился к чату.", "Black");
            SendUserList();
        };
        public static event UserEvent UserDisconnected = (Username) =>
        {
            Console.WriteLine($"User {Username} disconnected.");
            CountUsers--;
            SendGlobalMessage($"Пользователь {Username} отключился от чата.","Black");
            SendUserList();
        };
        public static List<User> UserList = new List<User>();
        public static Socket ServerSocket;
        public const string Host = "127.0.0.1";
        public const int Port = 2222;
        public static bool Work = true;

    }
}
