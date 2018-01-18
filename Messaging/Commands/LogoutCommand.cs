﻿using System;
using System.Threading.Tasks;
using ChatProtos.Networking;
using ChatProtos.Networking.Messages;
using HServer.Networking;

namespace ChatServer.Messaging.Commands
{
    public class LogoutServerCommand : IChatServerCommand
    {
        private readonly HClientManager _clientManager;

        public LogoutServerCommand(HClientManager clientManager)
        {
            _clientManager = clientManager;
        }

        public async Task ExecuteTask(HChatClient client, RequestMessage message)
        {
            var logoutRequest = LogoutRequest.Parser.ParseFrom(message.Message);
            await client.TryDeauthenticatingTask();
            _clientManager.RemoveClient(client.Connection);
            Console.WriteLine("[SERVER] After logout for client {0}", client.Id);
        }
    }
}