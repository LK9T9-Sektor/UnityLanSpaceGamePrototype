//using Assets.Scripts;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Chat
{
    public class ChatSystem : MonoBehaviour
    {
        [SerializeField] private GameObject _chatScrollView;
        [SerializeField] private GameObject _chatText;
        [SerializeField] private GameObject _chatInput;

        private ScrollRect _scrollRect;
        private Text _text;
        private InputField _textInput;

        private readonly StringBuilder _stringBuilder = new StringBuilder();

        void Awake()
        {
            if (_chatScrollView != null)
            {
                _scrollRect = _chatScrollView.GetComponent<ScrollRect>();
            }
            if (_chatText != null)
            {
                _text = _chatText.GetComponent<Text>();
            }
            if (_chatInput != null)
            {
                _textInput = _chatInput.GetComponent<InputField>();
                _textInput.onEndEdit.AddListener(AddMessageFromInput);
            }
        }

        public void AddMessage(string message)
        {
            if (_chatText != null && _text != null && !string.IsNullOrEmpty(message))
            {
                _stringBuilder.Append(message);
                _stringBuilder.Append("\r\n");
                _text.text = _stringBuilder.ToString();
                //_scrollRect.ScrollToBottom();
            }
        }

        private void AddMessageFromInput(string message)
        {
            if (_chatInput != null && _textInput != null && !string.IsNullOrEmpty(message))
            {
                _stringBuilder.Append(message);
                _stringBuilder.Append("\r\n");
                _text.text = _stringBuilder.ToString();
                _textInput.text = string.Empty;
            }
        }

        public void SetChatVisibility(bool hide)
        {

        }

    }
}