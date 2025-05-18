using Code.Data;
using UnityEngine;

namespace Code.Runtime.UI.Buttons
{
    public sealed class LoadSafeFileButton : AbstractButton
    {
        [SerializeField] private Const.PlayerSaveId playerSaveId;
        protected override void OnLeftClick() => GameState.LoadJson( playerSaveId );

        protected override void OnRightClick()
        {
        }
    }
}

namespace Code.Runtime.UI.Toggles
{
}