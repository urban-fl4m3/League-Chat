using System;
using UI.ChatBoxWindow;

public interface IClientChat
{
    event EventHandler<IChatInput> MessageSend;
    void ClientSend(IChatInput chatInput);
}