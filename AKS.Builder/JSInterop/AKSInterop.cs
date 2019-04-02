using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AKS.Builder.JSInterop
{
    public class AKSInterop
    {
        public static Action<string> SelectTopicAction { get; set; }
        [JSInvokable]
        public static void SelectTopic(string callBackCommandName)
        {
            SelectTopicAction(callBackCommandName);
        }
    }
}
