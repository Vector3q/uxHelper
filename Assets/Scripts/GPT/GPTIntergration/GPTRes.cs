using System;
using System.Collections.Generic;

namespace GPTIntergration
{
    [Serializable]
    public struct GPTRes
    {
        public string id;
        public List<GPTChoices> choices;

    }
}