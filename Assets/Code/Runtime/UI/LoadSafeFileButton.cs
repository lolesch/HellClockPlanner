using Code.Data;
using Code.Runtime.Serialisation;
using UnityEngine;

namespace Code.Runtime.UI
{
    public sealed class LoadSafeFileButton : AbstractButton
    {
        [SerializeField] private PlayerSave playerSave;
        [SerializeField] private Const.PlayerSaveId playerSaveId;
        protected override void OnLeftClick() => playerSave.LoadJson( playerSaveId );
        protected override void OnRightClick() { }
    }
}