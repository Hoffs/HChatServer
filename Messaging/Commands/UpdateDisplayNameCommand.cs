﻿using System.Threading.Tasks;
using HServer.ChatProtos.Networking;

namespace ChatServer.Messaging.Commands
{
    public class UpdateDisplayNameServerCommand : IChatServerCommand
    {
        public async Task ExecuteTask(HChatClient client, RequestMessage message)
        {
            throw new System.NotImplementedException();
        }
    }
}