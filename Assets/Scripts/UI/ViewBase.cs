using UnityEngine;

namespace UI.Views
{
    public abstract class ViewBase : MonoBehaviour
    {
        [SerializeField] protected GameObject firstSelected;
     
        protected const string UserInfoKey = "UserInfo";
        
        public GameObject FirstSelected => firstSelected;

        private void Start()
        {
            OnCreation();
        }

        protected abstract void OnCreation();
        
        public abstract void OnShow();
        
        public abstract void OnHide();
    }
}