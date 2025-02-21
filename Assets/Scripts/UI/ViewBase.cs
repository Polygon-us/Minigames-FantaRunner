using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

namespace UI.Views
{
    public abstract class ViewBase : MonoBehaviour
    {
        [SerializeField] protected GameObject firstSelected;
        
        protected readonly List<Button> Buttons = new();
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
            Buttons.ForEach(button => button.interactable = active);   
        }
    }
}