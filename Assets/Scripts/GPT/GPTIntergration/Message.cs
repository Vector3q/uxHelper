using System.Collections;
using System;

namespace GPTIntergration
{
    [Serializable]
    public class Message
    {
        public string role;
        public string content;

        public Message(string r, string c)
        {
            role = r;
            content = c;
        }

    }
}