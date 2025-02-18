using UnityEngine;
using System;
using TMPro;

namespace Source.Popups
{
    public class ConfirmationPopUp : MonoBehaviour
    {
        #region Singleton

        static ConfirmationPopUp instance;

        public static ConfirmationPopUp Instance
        {
            get
            {
                if (instance)
                    return instance;

                instance = Instantiate(Resources.Load<GameObject>("Prefabs/Popups/ConfirmationPopup"))
                    .GetComponent<ConfirmationPopUp>();

                instance._panel = instance.gameObject.transform.GetChild(1).GetComponent<RectTransform>();
                instance.blocker = instance.gameObject.transform.GetChild(0).GetComponent<RectTransform>();

                return instance;
            }
        }

        #endregion

        #region Information

        [SerializeField] private Canvas canvas;
        [SerializeField] private GameObject msgText;
        [SerializeField] private GameObject confirmBtn;
        [SerializeField] private AnimationCurve openCurve;
        [SerializeField] private AnimationCurve closeCurve;

        public static bool IsOpen { get; private set; }

        #endregion

        #region Components

        [HideInInspector] public RectTransform blocker;
        [HideInInspector] public RectTransform _panel;

        #endregion

        #region Events

        Action OnConfirm;

        #endregion


        public void Open(string msgText, Action onConfirm = null)
        {
            this.msgText.GetComponent<TMP_Text>().text = msgText;

            OnConfirm = onConfirm;

            OpenTween();
        }

        private void OpenTween()
        {
            IsOpen = true;

            msgText.SetActive(false);

            confirmBtn.SetActive(false);

            _panel.localScale = Vector3.zero;

            blocker.gameObject.SetActive(true);

            LeanTween.alpha(blocker, 0f, 0f).setIgnoreTimeScale(true);

            LeanTween.alpha(blocker, 0.6f, 0.2f).setEase(openCurve).setIgnoreTimeScale(true);

            LeanTween.scale(_panel, Vector3.one, 0.2f).setEase(openCurve).setIgnoreTimeScale(true).setOnComplete(() =>
            {
                msgText.SetActive(true);

                confirmBtn.SetActive(true);
            });
        }

        public void Confirm()
        {
            Close(0.2f);

            OnConfirm?.Invoke();
        }

        private void Close(float timeToClose)
        {
            CloseTween(timeToClose);
        }

        private void CloseTween(float timeToClose)
        {
            IsOpen = false;

            msgText.SetActive(false);

            confirmBtn.SetActive(false);

            LeanTween.scale(_panel, Vector3.zero, timeToClose).setEase(closeCurve).setIgnoreTimeScale(true);
            LeanTween.alpha(blocker, 0f, timeToClose).setEase(closeCurve).setIgnoreTimeScale(true).setOnComplete(() =>
            {
                blocker.gameObject.SetActive(false);
            });
        }
    }
}