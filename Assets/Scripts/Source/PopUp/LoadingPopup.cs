using UnityEngine;

namespace Source.Popups
{
    public class LoadingPopUp : MonoBehaviour
    {
        #region Singleton

        static LoadingPopUp instance;

        public static LoadingPopUp Instance
        {
            get
            {
                if (instance)
                    return instance;

                instance = Instantiate(Resources.Load<GameObject>("Prefabs/Popups/Loading Popup"))
                    .GetComponent<LoadingPopUp>();

                instance.panel = instance.gameObject.transform.GetChild(1).GetComponent<RectTransform>();
                instance.blocker = instance.gameObject.transform.GetChild(0).GetComponent<RectTransform>();

                return instance;
            }
        }

        #endregion

        #region Information

        [SerializeField] private AnimationCurve openCurve;
        [SerializeField] private AnimationCurve closeCurve;

        public static bool IsOpen { get; private set; }

        #endregion

        #region Components

        [HideInInspector] public RectTransform blocker;
        [HideInInspector] public RectTransform panel;

        #endregion

        public void Open()
        {
            IsOpen = true;

            blocker.gameObject.SetActive(true);
            panel.gameObject.SetActive(true);

            LeanTween.alpha(blocker, 0f, 0f).setIgnoreTimeScale(true);
            LeanTween.alpha(blocker, 0.6f, 0.2f).setEase(openCurve).setIgnoreTimeScale(true);
            LeanTween.scale(panel, Vector3.one, 0.2f).setEase(openCurve).setIgnoreTimeScale(true);
        }

        public void Close()
        {
            IsOpen = false;

            LeanTween.scale(panel, Vector3.zero, 0.2f).setEase(closeCurve).setIgnoreTimeScale(true);
            LeanTween.alpha(blocker, 0f, 0.2f).setEase(closeCurve).setIgnoreTimeScale(true).setOnComplete(() =>
            {
                blocker.gameObject.SetActive(false);
                panel.gameObject.SetActive(false);
            });
        }
    }
}