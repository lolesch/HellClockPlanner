using Code.Data;
using UnityEngine;

namespace Code.Runtime.UI
{
    public class Test : MonoBehaviour
    {
        [ContextMenu("PrintPersistentPath")]
        private void PrinPersistentPath() => Debug.Log( Application.persistentDataPath );
        
        [ContextMenu("PrintSaveDirectory")]
        private void PrintSaveDirectory() => Debug.Log( Constants.GetSaveDirectory() );
        
    }
}