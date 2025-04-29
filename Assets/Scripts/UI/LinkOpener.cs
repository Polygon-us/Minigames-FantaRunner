using UnityEngine;

public class LinkOpener : MonoBehaviour
{
    [SerializeField, TextArea] private string link;

    public void OpenLink()
    {
        Application.OpenURL(link);
    }
}
