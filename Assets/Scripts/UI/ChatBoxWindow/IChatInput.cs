using Models;

namespace UI.ChatBoxWindow
{
    public interface IChatInput
    {
        ChatUser User { get; }
        string Message { get; }
    }
}