using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AKS.App.Core.JSInterop
{
    public static class AKSInterop
    {
        public static Action<string>? SelectTopicAction { get; set; }
        [JSInvokable]
        public static void SelectTopic(string callBackCommandName)
        {
            SelectTopicAction?.Invoke(callBackCommandName);
        }
    }
}
