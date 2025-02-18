using UnityEngine;

namespace UI.Views
{
    public class ViewBase : MonoBehaviour
    {
        [SerializeField] protected GameObject firstSelected;
        
        public GameObject FirstSelected => firstSelected;
    }
}