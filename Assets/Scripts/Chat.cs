using System;
using System.Collections.Generic;
using Models;
using UI.ChatBoxWindow;

//You can add interfaces for multipurpose such as online chatting etc
public class Chat : IClientChat
{
    public event EventHandler<IChatInput> MessageSend;

    private readonly List<IChatInput> _chatInputs;
    
    public Chat()
    {
        _chatInputs = new List<IChatInput>();
    }

    public void SendMessage(ChatUser user, string message)
    {
        var input = new ChatInputModel(user, message);
        _chatInputs.Add(input);
        MessageSend?.Invoke(this, input);
    }
    
    public void ClientSend(IChatInput chatInput)
    {
        _chatInputs.Add(chatInput);
    }
}