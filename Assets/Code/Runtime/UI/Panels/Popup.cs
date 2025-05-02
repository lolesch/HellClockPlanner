using TMPro;

namespace Code.Runtime.UI.Panels
{
    public sealed class Popup : AbstractPanel
    {
        private TextMeshProUGUI popupText;

        protected override void OnDisable()
        {
            base.OnDisable();

           //OpenPopupCommand.OnShowPopup -= OpenPopup;
           //ClosePopupCommand.OnHidePopup -= ClosePopup;
        }
        private void OnEnable()
        {
            //OpenPopupCommand.OnShowPopup -= OpenPopup;
            //OpenPopupCommand.OnShowPopup += OpenPopup;

            //ClosePopupCommand.OnHidePopup -= ClosePopup;
            //ClosePopupCommand.OnHidePopup += ClosePopup;
        }

        private void Start() => popupText = GetComponentInChildren<TextMeshProUGUI>();

        private void OpenPopup(string text)
        {
            popupText.text = text;

            FadeIn();
        }

        private void ClosePopup() => FadeOut();
    }
}
