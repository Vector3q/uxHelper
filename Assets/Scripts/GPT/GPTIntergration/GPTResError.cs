using System.Collections;
using System;

namespace GPTIntergration
{
    [Serializable]
    public struct ChatGPTResError
    {
        public Error error;

    }

    [Serializable]
    public struct Error
    {
        public string message;
    }
}