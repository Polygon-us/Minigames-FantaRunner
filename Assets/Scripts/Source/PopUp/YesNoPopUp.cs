using UnityEngine;
using System;
using TMPro;

namespace Source.Popups
{
    public class YesNoPopUp : MonoBehaviour
    {
        #region Singleton

        static YesNoPopUp instance;

        public static YesNoPopUp Instance
        {
            get
            {
                if (instance)
                    return instance;
                
                instance = Instantiate(Resources.Load<GameObject>("Prefabs/Popups/YesNoPopup"))
                    .GetComponent<YesNoPopUp>();

                instance._panel = instance.gameObject.transform.GetChild(1).GetComponent<RectTransform>();
                instance.blocker = instance.gameObject.transform.GetChild(0).GetComponent<RectTransform>();

                return instance;
            }
        }

        #endregion

        #region Information

        [SerializeField] private Canvas canvas;
        [SerializeField] private GameObject tittleText, msgText;
        [SerializeField] private GameObject yes, no;
        [SerializeField] private AnimationCurve openCurve;
        [SerializeField] private AnimationCurve closeCurve;

        public static bool IsOpen { get; private set; }

        #endregion

        #region Components

        [HideInInspector] public RectTransform blocker;
        [HideInInspector] public RectTransform _panel;

        #endregion

        #region Events

        Action onYes;
        Action onNo;

        #endregion

        private void Open(Action onYes = null, Action onNo = null, string yesText = "Yes", string noText = "No")
        {
            yes.GetComponentInChildren<TMP_Text>().text = yesText;
            no.GetComponentInChildren<TMP_Text>().text = noText;

            this.onYes = onYes;
            this.onNo = onNo;

            OpenTween();
        }

        public void Open(string tittleText = "", string msgText = "", string continueText = "", string cancelText = "",
            Action onYes = null, Action onNo = null)
        {
            this.tittleText.GetComponent<TMP_Text>().text = tittleText;
            this.msgText.GetComponent<TMP_Text>().text = msgText;

            Open(onYes, onNo, continueText, cancelText);
        }

        private void OpenTween()
        {
            IsOpen = true;

            tittleText.SetActive(false);
            msgText.SetActive(false);

            yes.SetActive(false);

            no.SetActive(false);

            _panel.localScale = Vector3.zero;

            blocker.gameObject.SetActive(true);

            LeanTween.alpha(blocker, 0f, 0f).setIgnoreTimeScale(true);

            LeanTween.alpha(blocker, 0.6f, 0.2f).setEase(openCurve).setIgnoreTimeScale(true);

            LeanTween.scale(_panel, Vector3.one, 0.2f).setEase(openCurve).setIgnoreTimeScale(true).setOnComplete(() =>
            {
                tittleText.SetActive(true);
                msgText.SetActive(true);

                yes.SetActive(true);

                no.SetActive(true);
            });
        }

        public void Yes()
        {
            Close(0.1f);

            onYes?.Invoke();
        }

        public void No()
        {
            Close(0.2f);

            onNo?.Invoke();
        }

        private void Close(float timeToClose)
        {
            CloseTween(timeToClose);
        }

        private void CloseTween(float timeToClose)
        {
            IsOpen = false;

            tittleText.SetActive(false);
            msgText.SetActive(false);

            yes.SetActive(false);

            no.SetActive(false);

            LeanTween.scale(_panel, Vector3.zero, timeToClose).setEase(closeCurve).setIgnoreTimeScale(true);
            LeanTween.alpha(blocker, 0f, timeToClose).setEase(closeCurve).setIgnoreTimeScale(true).setOnComplete(() =>
            {
                blocker.gameObject.SetActive(false);
            });
        }
    }
}