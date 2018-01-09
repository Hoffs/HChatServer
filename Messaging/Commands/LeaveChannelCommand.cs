﻿using System;
using System.Threading.Tasks;
using ChatProtos.Networking;
using ChatProtos.Networking.Messages;
using CoreServer;
using CoreServer.HMessaging;

namespace ChatServer.Messaging.Commands
{
    public class LeaveChannelServerCommand : IChatServerCommand
    {
        private readonly HChannelManager _channelManager;

        public LeaveChannelServerCommand(HChannelManager channelManager)
        {
            _channelManager = channelManager;
        }

        public async Task ExecuteTask(HChatClient client, RequestMessage message)
        {
            var joinRequest = LeaveChannelMessageRequest.Parser.ParseFrom(message.Message);
            if (!client.Authenticated)
            {
                Console.WriteLine("[SERVER] User {0} not authenticated to perform this action.",
                    client.Id);
            }
            else
            {
                HChannel channel = null;
                if (joinRequest.ChannelId != null)
                {
                    channel = _channelManager.FindChannelById(joinRequest.ChannelId);
                } else if (joinRequest.ChannelName != null && channel == null)
                {
                    channel = _channelManager.FindChannelByName(joinRequest.ChannelName);
                }

                if (channel?.RemoveClient(client) == true)
                {
                    // client.RemoveChannel(channel); TODO: Fix this after moving to Interfaced logic
                }

                Console.WriteLine("[SERVER] User {0} tried leaving channel {1}.",
                    client.Id, joinRequest.ChannelName);
            }
        }
    }
}