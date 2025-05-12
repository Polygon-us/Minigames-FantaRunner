using UnityEngine.UI;
using UnityEngine;
using TMPro;

namespace UI
{
    public class CodeView : MonoBehaviour
    {
        [SerializeField] private string code;
        [SerializeField] private TMP_Text codeTxt;
        [SerializeField] private Button copyBtn;

        private void Awake()
        {
            codeTxt.text = code;
            copyBtn.onClick.AddListener(CopyCode);
        }

        public void CopyCode()
        {
            WebGLCopyAndPaste.WebGLCopyAndPasteAPI.CopyToClipboard(code);
        }
    }
}