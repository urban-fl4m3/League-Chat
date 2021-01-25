using System.Collections.Generic;
using Models;
using UnityEngine;

namespace Configs
{
    [CreateAssetMenu(fileName = "Chat Users Config", menuName = "Chat/Users")]
    public class ChatUsersConfig : ScriptableObject
    {
        [SerializeField] private List<ChatUser> _users;

        public List<ChatUser> Users => _users;
    }
}