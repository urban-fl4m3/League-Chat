using System.Collections.Generic;
using Configs;
using Models;
using UnityEngine;

namespace Providers
{
    public class ChatUsersProvider
    {
        private readonly List<ChatUser> _chatUsers;

        public ChatUsersProvider(ChatUsersConfig chatUsersConfig)
        {
            _chatUsers = chatUsersConfig.Users;
            Author = _chatUsers[0];
        }

        public void ChangeAuthor(ChatUser user)
        {
            Author = user;
        }
        
        public ChatUser Author { get; private set; }
        public IEnumerable<ChatUser> ChatUsers => _chatUsers;

        public ChatUser GetRandomUser()
        {
            var userIndex = Random.Range(0, _chatUsers.Count);
            return _chatUsers[userIndex];
        }
    }
}