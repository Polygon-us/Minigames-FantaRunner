using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Views
{
    public abstract class ViewBase : MonoBehaviour
    {
        [SerializeField] protected GameObject firstSelected;
     
        protected const string UserInfoKey = "UserInfo";
     
        protected List<Button> ToggableButtons = new List<Button>();
        public GameObject FirstSelected => firstSelected;

        private void Start()
        {
            OnCreation();
        }

        protected abstract void OnCreation();
        
        public abstract void OnShow();
        
        public abstract void OnHide();

        protected void ToggleButtons(bool active)
        {
            ToggableButtons.ForEach(button => button.interactable = active);   
        }
    }
}