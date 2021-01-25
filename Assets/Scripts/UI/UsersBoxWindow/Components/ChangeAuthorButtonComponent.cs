using Models;
using UnityEngine;
using UnityEngine.UI;

namespace UI.UsersBoxWindow.Components
{
    public class ChangeAuthorButtonComponent : MonoBehaviour
    {
        [SerializeField] private Image _avatarImage;
        [SerializeField] private Text _nameText;
        [SerializeField] private Button _button;

        public Button Button => _button;

        public void SetContext(ChatUser user)
        {
            _avatarImage.sprite = user.UserAvatar;
            _nameText.text = user.UserName;
        }
    }
}