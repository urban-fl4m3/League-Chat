using System;
using System.Collections;
using Configs;
using Providers;
using UI.AppHudWindow;
using UI.ChatBoxWindow;
using UI.UsersBoxWindow;
using UnityEngine;

public class EntryPoint : MonoBehaviour
{
    [SerializeField] private ChatBoxView _chatBoxView;
    [SerializeField] private UsersBoxView _usersBoxView;
    [SerializeField] private AppHudView _appHudView;
    [SerializeField] private ChatUsersConfig _chatUsersConfig;

    private Chat _chat;
    private ChatUsersProvider _chatUsersProvider;
    
    //Too lazy for custom DI container, i wanna use zenject.....
    private void Start()
    {
        _chatUsersProvider = new ChatUsersProvider(_chatUsersConfig);
        _chat = new Chat();
        
        var chatBoxModel = new ChatBoxModel(_chat, _chatUsersProvider);
        var usersBoxModel = new UsersBoxModel(_chatUsersProvider);
        
        _chatBoxView.Initialize(chatBoxModel);
        _usersBoxView.Initialize(usersBoxModel);

        var appHudModel = new AppHudModel(_chatBoxView, _usersBoxView);
        _appHudView.Initialize(appHudModel);
        
        //StartCoroutine(SendMessageFromAllAuthors());
    }

    // private IEnumerator SendMessageFromAllAuthors()
    // {
    //     foreach (var user in _chatUsersProvider.ChatUsers)
    //     {
    //         _chat.SendMessage(user, $"Hello, it's me, {user.UserName}!");
    //         yield return new WaitForSeconds(0.5f);
    //     }
    // }

    private void OnDestroy()
    {
        _appHudView.Clear();
        _chatBoxView.Clear();
        _usersBoxView.Clear();
    }
}
