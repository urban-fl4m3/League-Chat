using System.Collections;
using Models;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace UI.ChatBoxWindow
{
    public class ChatInputComponent : MonoBehaviour, IChatInput
    {
        public RectTransform RectTransform => _rectTransform;
        private RectTransform _rectTransform;
        
        [SerializeField] private Image _avatar;
        [SerializeField] private Image _background;
        [SerializeField] private Text _userName;
        [SerializeField] private Text _userText;
        [SerializeField] private Transform _leftAvatarAlign;
        [SerializeField] private Transform _rightAvatarAlign;
        [SerializeField] private Button _deleteButton;
        [SerializeField] private float _popAnimationTime;
        [SerializeField] private float _destroyAnimationTime;
        
        private string _ownerName;
        private float _minHeight;
        private bool _isAuthor;
        private Coroutine _animationCoroutine;
        private UnityAction _onAnimationEnd;
        
        private void Awake()
        {
            _rectTransform = GetComponent<RectTransform>();
            _minHeight = _rectTransform.sizeDelta.y;
        }

        public void SetContext(ChatUser chatUser, string message, bool isAuthor)
        {
            User = chatUser;
            Message = message;
            
            _avatar.sprite = chatUser.UserAvatar;
            _background.color = chatUser.UserBackgroundColor;
            _userName.text = chatUser.UserName;
            _userText.text = message;
            _isAuthor = isAuthor;

            _avatar.transform.position = isAuthor ? _rightAvatarAlign.position : _leftAvatarAlign.position;

            var textHeight = _userName.preferredHeight + _userText.preferredHeight;
            var yDelta = textHeight > _minHeight ? textHeight : _minHeight;
            _rectTransform.sizeDelta = new Vector2(_rectTransform.rect.width, yDelta);
        }

        public void ActivateDeleteButton(UnityAction onDeleteStart, UnityAction onDeleteEnd)
        {
            if (_animationCoroutine != null)
            {
                return;
            }
         
            _onAnimationEnd = onDeleteEnd;   
            _deleteButton.gameObject.SetActive(true);
            _deleteButton.onClick.AddListener(onDeleteStart);
            _deleteButton.onClick.AddListener(AnimateAndDestroy);

            var position = _isAuthor ? _leftAvatarAlign.position : _rightAvatarAlign.position;
            _deleteButton.transform.position = position;
        }

        public void DeactivateDeleteButton()
        {
            if (_animationCoroutine != null)
            {
                return;
            }
            
            _deleteButton.gameObject.SetActive(false);
            _deleteButton.onClick.RemoveAllListeners();
        }

        public void Pop(float height)
        {
            _rectTransform.anchorMin = new Vector2(0, 1);
            _rectTransform.anchorMax = new Vector2(0, 1);
            
            var sign = _isAuthor ? -1 : 1;
            var startPositionX = _isAuthor
                ? _rectTransform.anchoredPosition.x + _rectTransform.sizeDelta.x * 2
                : _rectTransform.anchoredPosition.x;
            var startAnimatePosition = new Vector2(startPositionX, height);
            
            _animationCoroutine = StartCoroutine(Animate(_popAnimationTime, startAnimatePosition,
                sign * _rectTransform.sizeDelta.x));
        }

        private void OnDestroy()
        {
            _deleteButton.onClick.RemoveAllListeners();
        }


        private void AnimateAndDestroy()
        {
            _animationCoroutine = StartCoroutine(Animate(_destroyAnimationTime, _rectTransform.anchoredPosition,
                _rectTransform.rect.width));
            Destroy(gameObject, _destroyAnimationTime + 0.1f);
        }

        private IEnumerator Animate(float animationTime, Vector2 startPosition, float signedWidth)
        {
            var time = 0.0f;
            
            while (time < animationTime)
            {
                time += Time.deltaTime;
                
                _rectTransform.anchoredPosition = new Vector2(
                    startPosition.x + signedWidth * (time / animationTime),
                    startPosition.y);
                
                yield return null;
            }

            _onAnimationEnd?.Invoke();
            _onAnimationEnd = null;
            _animationCoroutine = null;
        }
        
        public ChatUser User { get; private set; }
        public string Message { get; private set; }
    }
}