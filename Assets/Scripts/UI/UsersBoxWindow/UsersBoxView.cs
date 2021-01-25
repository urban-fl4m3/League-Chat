using System.Collections.Generic;
using UI.UsersBoxWindow.Components;
using UnityEngine;
using UnityEngine.UI;

namespace UI.UsersBoxWindow
{
    public class UsersBoxView : GenericView<UsersBoxModel>
    {
        [SerializeField] private ScrollRect _usersScroll;
        [SerializeField] private ChangeAuthorButtonComponent _changeAuthorButton;

        private readonly List<ChangeAuthorButtonComponent> _changeAuthorButtons = new List<ChangeAuthorButtonComponent>();
        
        protected override void OnInitialize(UsersBoxModel model)
        {
            foreach (var user in model.ChatUsersProvider.ChatUsers)
            {
                var button = Instantiate(_changeAuthorButton, _usersScroll.content);
                button.SetContext(user);
                button.Button.onClick.AddListener(() =>
                {
                    Model.ChatUsersProvider.ChangeAuthor(user);
                });
                
                _changeAuthorButtons.Add(button);
            }
        }

        public override void Clear()
        {
            base.Clear();

            foreach (var button in _changeAuthorButtons)
            {
                button.Button.onClick.RemoveAllListeners();
            }
        }
    }
}