using Models;
using UI.ChatBoxWindow;

public class ChatInputModel : IChatInput
{
    public ChatInputModel(ChatUser user, string message)
    {
        User = user;
        Message = message;
    }

    public ChatUser User { get; }
    public string Message { get; }
}