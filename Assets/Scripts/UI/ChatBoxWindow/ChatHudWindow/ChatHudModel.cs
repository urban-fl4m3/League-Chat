using System;

namespace UI.ChatBoxWindow.ChatHudWindow
{
    public class ChatHudModel
    {
        public ChatHudModel(EventHandler<string> sendMessagePressed, EventHandler deleteButtonPressed)
        {
            SendMessagePressed = sendMessagePressed;
            DeleteButtonPressed = deleteButtonPressed;
        }
        
        public EventHandler<string> SendMessagePressed { get; }
        public EventHandler DeleteButtonPressed { get; }
    }
}