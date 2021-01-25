using Providers;

namespace UI.ChatBoxWindow
{
    public class ChatBoxModel
    {

        public ChatBoxModel(IClientChat clientChat, ChatUsersProvider chatUsersProvider)
        {
            Chat = clientChat;
            ChatUsersProvider = chatUsersProvider;
        }
        
        public ChatUsersProvider ChatUsersProvider { get; }
        public IClientChat Chat { get; }
    }
}