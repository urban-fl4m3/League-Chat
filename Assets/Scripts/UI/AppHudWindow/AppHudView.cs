using UnityEngine;
using UnityEngine.UI;

namespace UI.AppHudWindow
{
    public class AppHudView : GenericView<AppHudModel>
    {
        [SerializeField] private Button _chatButton;
        [SerializeField] private Button _usersButton;
        
        protected override void OnInitialize(AppHudModel model)
        {
            _chatButton.onClick.AddListener(ShowChat);
            _usersButton.onClick.AddListener(ShowUsers);
        }

        private void ShowChat()
        {
            ChangeWindowStates(true, false);
        }

        private void ShowUsers()
        {
            ChangeWindowStates(true, true);
        }

        private void ChangeWindowStates(bool chatState, bool usersState)
        {
            Model.ChatBox.gameObject.SetActive(chatState);
            Model.UserBox.gameObject.SetActive(usersState);
        }

        public override void Clear()
        {
            base.Clear();
            _chatButton.onClick.RemoveListener(ShowChat);
            _usersButton.onClick.RemoveListener(ShowUsers);
        }
    }
}