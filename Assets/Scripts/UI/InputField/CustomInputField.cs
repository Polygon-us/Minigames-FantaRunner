using UnityEngine.UI;
using UnityEngine;
using TMPro;

namespace UI.InputField
{
    public class CustomInputField : MonoBehaviour
    {
        [SerializeField] private string labelText;
        [SerializeField] private string placeHolderText;
        [SerializeField] private int textsSize;
        [SerializeField] private bool hasIcon;
        [ShowIf(nameof(hasIcon), true, true)]
        [SerializeField] private Sprite iconSprite;
        [SerializeField] private TMP_InputField.ContentType contentType;
        [SerializeField] private TouchScreenKeyboardType keyboardType;
        
        private TMP_Text label;
        private TMP_InputField inputField;
        private TMP_Text[] texts;
        private Image iconImage;

        public string Text => inputField.text;

        [ContextMenu(nameof(ResetReferences))]
        private void ResetReferences()
        {
            label = null;
            inputField = null;
            texts = null;
            iconImage = null;
        }

        private void EnsureInitialized()
        {
            label ??= GetComponentInChildren<TMP_Text>();
            inputField ??= GetComponentInChildren<TMP_InputField>();
            texts ??= GetComponentsInChildren<TMP_Text>();
            iconImage ??= transform.GetChild(1).GetChild(2).GetComponent<Image>();
        }

        private void OnValidate()
        {
            EnsureInitialized();

            label.text = labelText;
            ((TMP_Text)inputField.placeholder).text = placeHolderText;
            inputField.contentType = contentType;
            
            iconImage.enabled = hasIcon;
            if (hasIcon)
                iconImage.sprite = iconSprite;

            foreach (TMP_Text text in texts)
            {
                text.fontSize = textsSize;
            }
        }

        private void Awake()
        {
            EnsureInitialized();

            inputField.onSelect.AddListener(ShowKeyboard);
        }

        private void ShowKeyboard(string text)
        {
            TouchScreenKeyboard.Open(text, keyboardType, false, false, false, false, text);
        }

        private void OnDestroy()
        {
            inputField.onSelect.RemoveAllListeners();
        }
    }
}