using UnityEngine;

namespace UI
{
    public class LinkOpener : MonoBehaviour
    {
        [SerializeField, TextArea] protected string link;
        
        public virtual void OpenLink()
        {
            Application.OpenURL(link);
        }
    }
}