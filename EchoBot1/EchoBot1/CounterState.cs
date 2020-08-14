using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EchoBot1
{
    public class CounterState
    {
        public int TurnCount { get; set; } = 0;

        public string CurrentConversationFlow { get; set; } = "askName";
    }
}
