using System;
using UnityEngine;

namespace Models
{
    [Serializable]
    public class ChatUser
    {
        [SerializeField] private string _userName;
        [SerializeField] private Sprite _userAvatar;
        [SerializeField] private Color _userBackgroundColor = Color.white;

        public string UserName => _userName;
        public Sprite UserAvatar => _userAvatar;
        public Color UserBackgroundColor => _userBackgroundColor;
    }
}