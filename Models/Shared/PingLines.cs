using System.ComponentModel;

namespace Models.Shared
{
    public static class PingLines
    {
        private const string _query = "Is there anybody out there?";
        private const string _statement = "Welcome to the machine.";
        private const string _request = "Wish you were here.";
        private const string _detail = "I'm alright Jack!";
        public static string Query { get { return _query; } }
        public static string Statement { get { return _statement; } }
        public static string Request { get { return _request; } }
        public static string Detail { get { return _detail; } }
    }
}
