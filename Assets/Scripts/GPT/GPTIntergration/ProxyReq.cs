using System.Collections;
using System.Collections.Generic;

namespace GPTIntergration
{
    public struct ProxyReq
    {
        public string model;
        public List<Message> messages;
        public int max_tokens;
        public float temperature;
    }
}
