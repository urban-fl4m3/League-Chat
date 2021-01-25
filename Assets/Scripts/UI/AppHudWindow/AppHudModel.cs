using UI.ChatBoxWindow;
using UI.UsersBoxWindow;

namespace UI.AppHudWindow
{
    public class AppHudModel
    {
        public AppHudModel(ChatBoxView chatBoxView, UsersBoxView usersBoxView)
        {
            ChatBox = chatBoxView;
            UserBox = usersBoxView;
        }
        
        public ChatBoxView ChatBox { get; }
        public UsersBoxView UserBox { get; }
    }
}