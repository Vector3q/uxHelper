using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

namespace GPTIntergration
{
    [Serializable]
    public struct ChatGPTRes
    {
        public List<ChatGPTChoices> choices;
    }
}