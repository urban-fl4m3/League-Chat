using Providers;

namespace UI.UsersBoxWindow
{
    public class UsersBoxModel
    {
        public UsersBoxModel(ChatUsersProvider chatUsersProvider)
        {
            ChatUsersProvider = chatUsersProvider;
        }
        
        public ChatUsersProvider ChatUsersProvider { get; }
    }
}