using UnityEngine;

namespace Code.Runtime.UI.Displays
{
    public sealed class CharacterStatsPanel : MonoBehaviour
    {
        [SerializeField] private CharacterStatDisplay statDisplayPrefab;
        [SerializeField] private RectTransform parent;
        
        private void Start() => CreateStatDisplays();

        void CreateStatDisplays()
        {
            foreach( var stat in GameState.Player.Stats )
            {
                var statDisplay = Instantiate( statDisplayPrefab, parent );
                statDisplay.RefreshDisplay( stat );
            }
            
        }
    }
}