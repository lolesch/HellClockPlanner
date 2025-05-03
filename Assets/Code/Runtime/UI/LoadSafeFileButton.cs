using Code.Data;
using Code.Runtime.Provider;
using Code.Runtime.UI.Buttons;
using UnityEngine;

namespace Code.Runtime.UI
{
    public sealed class LoadSafeFileButton : AbstractButton
    {
        [SerializeField] private Const.PlayerSaveId playerSaveId;
        protected override void OnLeftClick() => DataProvider.Instance.LoadJson( playerSaveId );
        protected override void OnRightClick() { }
    }
}