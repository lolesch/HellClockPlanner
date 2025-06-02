using Code.Data;
using UnityEngine;

namespace Code.Runtime.UI.Buttons
{
    public sealed class LoadSafeFileButton : AbstractButton
    {
        [SerializeField] private Const.PlayerSaveId playerSaveId;
        protected override void OnLeftClick() => GameState.LoadSaveFile( playerSaveId );

        protected override void OnRightClick()
        {
        }
    }
}