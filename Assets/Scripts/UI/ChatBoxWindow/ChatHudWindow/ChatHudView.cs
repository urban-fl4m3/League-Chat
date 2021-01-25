using System;
using UnityEngine;
using UnityEngine.UI;

namespace UI.ChatBoxWindow.ChatHudWindow
{
    public class ChatHudView : GenericView<ChatHudModel>
    {
        [SerializeField] private Button _deleteButton;
        [SerializeField] private Button _sendButton;
        [SerializeField] private InputField _messageField;
        
        protected override void OnInitialize(ChatHudModel model)
        {
            _sendButton.onClick.AddListener(SendButtonPress);
            _deleteButton.onClick.AddListener(DeleteButtonPress);
        }

        private void SendButtonPress()
        {
            Model.SendMessagePressed?.Invoke(this, _messageField.text);
            _messageField.text = "";
        }

        private void DeleteButtonPress()
        {
            Model.DeleteButtonPressed?.Invoke(this, EventArgs.Empty);
        }

        public override void Clear()
        {
            base.Clear();
            _sendButton.onClick.RemoveListener(SendButtonPress);
            _deleteButton.onClick.RemoveListener(DeleteButtonPress);
        }
    }
}