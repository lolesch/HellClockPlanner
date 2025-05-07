using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace Code.Runtime.UI.Panels
{
    /// <summary>
    /// Panels provide appearance options such as fading in and out, scaling and movement.
    /// A panel should always stay enabled, only its canvasGroup alpha is set to 0.
    /// Therefore, use <see cref="BeforeAppear"/> instead of <see cref="OnEnable()"/> to refresh panel data.
    /// </summary>
    [RequireComponent(typeof(CanvasGroup), typeof(GraphicRaycaster), typeof(RectTransform))]
    public abstract class AbstractPanel : MonoBehaviour
    {
        public CanvasGroup canvasGroup = null;

        private RectTransform _rectTransform;
        public RectTransform rectTransform => transform as RectTransform;

        public bool isActive => Mathf.Approximately( canvasGroup.alpha, 1 );
        [field: SerializeField, Range(0, 1)] public float fadeDuration { get; private set; } = .2f;

        protected virtual void Awake()
        {
            if (isActiveAndEnabled)
                FadeOut(0);
        }

        protected virtual void OnDestroy() => KillTweens();

        protected virtual void OnDisable() => KillTweens();

        [ContextMenu("FadeIn")]
        public void FadeIn() => FadeIn(fadeDuration);
        public void FadeIn(float fadeInDuration)
        {
            KillTweens();

            BeforeAppear();

            if (fadeInDuration <= 0)
            {
                canvasGroup.alpha = 1;

                OnAppear();

                return;
            }

            _ = canvasGroup.DOFade(1, fadeInDuration).SetEase(Ease.InOutQuad).OnComplete(() => OnAppear());
        }

        public Sequence FadeInAfterDelay(float fadeInDelay = 0)
        {
            if (0 >= fadeInDelay)
            {
                FadeIn();
                return null;
            }
            else
            {
                var sequence = DOTween.Sequence();
                return sequence.InsertCallback(fadeInDelay, FadeIn);
            }
        }

        /// <summary>
        /// Called right before the CanvasGroup fades in.
        /// </summary>
        protected virtual void BeforeAppear() { }

        /// <summary>
        /// Called after the CanvasGroup completed fading in.
        /// </summary>
        protected virtual void OnAppear() => canvasGroup.blocksRaycasts = true;

        [ContextMenu("FadeOut")]
        public void FadeOut() => FadeOut(fadeDuration);
        public void FadeOut(float fadeOutDuration)
        {
            KillTweens();

            // BeforeDisappear()

            canvasGroup.blocksRaycasts = false;

            if (fadeOutDuration <= 0)
            {
                canvasGroup.alpha = 0;

                // OnDisappear();

                return;
            }

            _ = canvasGroup.DOFade(0, fadeOutDuration).SetEase(Ease.InQuad); //.OnComplete(() => OnDisappear());
        }

        [ContextMenu("Toggle Visibility")]
        public void Toggle()
        {
            if (canvasGroup.alpha < 1)
                FadeIn();
            else
                FadeOut();
        }

        private void KillTweens()
        {
            if (canvasGroup == null)
                return;

            if (DOTween.IsTweening(canvasGroup))
                _ = DOTween.Kill(canvasGroup);
        }
    }
}
