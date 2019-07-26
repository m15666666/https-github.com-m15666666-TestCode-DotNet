using System;

namespace EasyNetQMessages
{
    public class TextMessage
    {
        public static string ConnectionString = "host=localhost;virtualHost=/;username=admin;password=admin";

        public string Text { get; set; }
    }
}
