using System;

namespace EasyNetQMessages
{
    public class TextMessage
    {
        public static string ConnectionString = "host=10.3.2.188;virtualHost=/;username=admin;password=admin";

        public string Text { get; set; }
    }
}
