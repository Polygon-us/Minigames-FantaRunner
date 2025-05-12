using UnityEngine;

namespace UI
{
    public class LinkOpener : MonoBehaviour
    {
        [SerializeField, TextArea] private string link;
        [SerializeField] private CodeView codeView;
        
        public void OpenLink()
        {
            codeView.CopyCode();
            Application.OpenURL(link);
        }
    }
}