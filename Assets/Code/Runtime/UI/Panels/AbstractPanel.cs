using Code.Runtime.UI.Displays;
using LitMotion;
using UnityEngine;

namespace Code.Runtime.UI.Panels
{
    /// <summary>
    /// Panels provide appearance options such as fading in and out, scaling and movement.
    /// A panel should always stay enabled, only its canvasGroup alpha is set to 0.
    /// Therefore, use <see cref="BeforeAppear"/> instead of <see cref="OnEnable()"/> to refresh panel data.
    /// </summary>
    [RequireComponent(typeof(CanvasGroup))]
    public abstract class AbstractPanel : AbstractDisplay
    {
        private CanvasGroup _canvasGroup = null;
        public CanvasGroup canvasGroup => _canvasGroup ??= GetComponent<CanvasGroup>();

        private RectTransform _rectTransform;
        public RectTransform rectTransform => transform as RectTransform;

        public bool isActive => Mathf.Approximately( canvasGroup.alpha, 1 );
        [field: SerializeField, Range(0, 1)] public float fadeDuration { get; private set; } = .2f;

        private MotionHandle _handle;

        protected virtual void Awake()
        {
            if (isActiveAndEnabled)
                FadeOut(0);
        }

        protected virtual void OnDestroy() => KillTweens();

        protected virtual void OnDisable() => KillTweens();

        [ContextMenu("FadeIn")]
        public MotionHandle FadeIn() => FadeIn(fadeDuration);
        public MotionHandle FadeIn(float fadeInDuration)
        {
            KillTweens();

            BeforeAppear();

            if (fadeInDuration <= 0)
            {
                canvasGroup.alpha = 1;

                OnAppear();

                _handle = new MotionHandle();
            }
            else if ( !isActive )
                _handle = LMotion.Create( 0, 1, fadeInDuration ).WithEase( Ease.InQuad ).WithOnComplete( OnAppear )
                    .Bind( canvasGroup, ( x, target ) => target.alpha = x );
            
            return _handle;
        }

        //public void FadeInAfterDelay(float fadeInDelay = 0)
        //{
        //    if (0 >= fadeInDelay)
        //        FadeIn();
        //    else
        //        LSequence.Create().Insert( fadeInDelay, FadeIn() );
        //}

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

            if( fadeOutDuration <= 0 )
            {
                canvasGroup.alpha = 0;

                // OnDisappear();

                return;
            }
            else if( isActive )
                _handle = LMotion.Create( 1, 0, fadeOutDuration ).WithEase( Ease.InQuad )
                    .Bind( canvasGroup, ( x, target ) => target.alpha = x ); //.OnComplete(() => OnDisappear());
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
            if ( _handle.IsActive() ) 
                _handle.Cancel();
        }
    }
}
