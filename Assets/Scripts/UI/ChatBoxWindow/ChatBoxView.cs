using System;
using System.Collections.Generic;
using System.Linq;
using Models;
using UI.ChatBoxWindow.ChatHudWindow;
using UnityEngine;
using UnityEngine.UI;

namespace UI.ChatBoxWindow
{
    //Normally you wouldn't place any buisness logic here but i wont code big MVC or MVVM system for this
    //so i will break up some SOLID rules in all views, and that's why i'm leaving this note here
    public class ChatBoxView : GenericView<ChatBoxModel>
    {
        [SerializeField] private ScrollRect _scrollView;
        [SerializeField] private ChatHudView _chatHudView;
        [SerializeField] private ChatInputComponent _chatInput;

        private float _boxHeights;
        private bool _deleteButtonPressed;
        private readonly List<ChatInputComponent> _chatInputComponents = new List<ChatInputComponent>();
        
        protected override void OnInitialize(ChatBoxModel model)
        {
            var chatHudModel = new ChatHudModel(HandleSendMessageButtonPressed, HandleDeleteButtonPressed);
            _chatHudView.Initialize(chatHudModel);
            
            model.Chat.MessageSend += HandleNewChatMessage;
        }

        private void HandleSendMessageButtonPressed(object sender, string args)
        {
            CreateChatBoxAndWriteMessage(Model.ChatUsersProvider.GetRandomUser(), args);
        }

        private void HandleNewChatMessage(object sender, IChatInput input)
        {
            CreateChatBoxAndWriteMessage(input.User, input.Message);
        }

        private void HandleDeleteButtonPressed(object sender, EventArgs args)
        {
            _deleteButtonPressed = !_deleteButtonPressed;
            
            var currentAuthor = Model.ChatUsersProvider.Author;

            foreach (var inputComponent in _chatInputComponents
                .Where(inputComponent => inputComponent.User == currentAuthor))
            {
                if (_deleteButtonPressed)
                {
                    inputComponent.ActivateDeleteButton(() =>
                    {
                        _chatInputComponents.Remove(inputComponent);
                        _boxHeights -= inputComponent.RectTransform.rect.height;
                    }, 
                        () =>
                    {
                        var count = _chatInputComponents.Count;
                        foreach (var chatInputComponent in _chatInputComponents)
                        {
                            var height = -_boxHeights / _chatInputComponents.Count; 
                            chatInputComponent.RectTransform.anchoredPosition = new Vector2(
                                chatInputComponent.RectTransform.anchoredPosition.x,
                                 height + height * (_chatInputComponents.Count - count)
                                + chatInputComponent.RectTransform.rect.height / 2);
                            count--;
                        }
                    });
                        
                }
                else
                {
                    inputComponent.DeactivateDeleteButton();   
                }
            }
        }

        private void CreateChatBoxAndWriteMessage(ChatUser user, string message)
        {
                var inputComponent = Instantiate(_chatInput, _scrollView.content);
                inputComponent.SetContext(user, message, user == Model.ChatUsersProvider.Author);

                var rect = inputComponent.RectTransform.rect;
                _boxHeights += rect.height;
                
                inputComponent.Pop(_boxHeights * -1 + rect.height / 2);

                _scrollView.content.sizeDelta
                    = new Vector2(_scrollView.content.sizeDelta.x, _boxHeights);

                _scrollView.verticalScrollbar.value = 0;

                _chatInputComponents.Add(inputComponent);
                Model.Chat.ClientSend(inputComponent);
        }

        public override void Clear()
        {
            base.Clear();
            _chatHudView.Clear();
            Model.Chat.MessageSend -= HandleNewChatMessage;
        }
    }
}