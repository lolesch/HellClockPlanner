using System;
using System.Text.RegularExpressions;
using Code.Utility.Extensions;
using UnityEngine;

namespace Code.Runtime.Provider
{
    public abstract class AbstractProvider<T> : MonoBehaviour where T : MonoBehaviour
    {
        // Lazy<T> is thread-safe for initialization by default.
        private static readonly Lazy<T> _instance = new( CreateNewInstance, true );

        public static T Instance => _instance.Value;

        [ContextMenu( "Reset" )]
        protected virtual void Reset() => name = GetProviderName();

        private static bool InstanceExists( out T instance )
        {
            var candidates = FindObjectsByType<T>( FindObjectsInactive.Include, FindObjectsSortMode.InstanceID );

            if( 0 < candidates.Length )
            {
                instance = candidates[0];
                instance.enabled = true;

                instance.name = GetProviderName();

                Debug.Log( $"Found {candidates.Length} existing {instance.name.ColoredComponent()}",
                    candidates[0].gameObject );

                /// instance as component of "non-root" gameObjects
                if( instance.transform.parent != null )
                    Debug.LogWarning(
                        $"{instance.name.Colored( Color.yellow )} is no root object and can't be moved by DonstDestroyOnLoad",
                        _instance.Value.gameObject );
                // concider force reparent the GameObject as root
                for( var i = candidates.Length; i-- > 1; ) DisableInstance( candidates[i] );

                return true;
            }

            instance = null;

            return false;
        }

        private static T CreateNewInstance()
        {
            if( !InstanceExists( out var instance ) )
            {
                GameObject gameObject = new( GetProviderName() );
                instance = gameObject.AddComponent<T>(); // this calls Awake on the new GameObject

                Debug.LogWarning( $"Created new {instance.name.ColoredComponent()}", gameObject );
            }

            if( Application.isPlaying )
                DontDestroyOnLoad( instance.gameObject );

            return instance;
        }

        private static void DisableInstance( T candidate )
        {
            if( candidate == _instance.Value )
            {
                Debug.LogWarning( $"You are trying to disable your instance of {typeof( T ).Name}!", candidate );
                return;
            }

            if( candidate != null )
            {
                Debug.LogWarning(
                    $"Disabled {_instance.Value.name.Colored( Color.red )} because there is already an Instance!",
                    candidate );

                candidate.enabled = false;
            }
        }

        private static string GetProviderName() =>
            Regex.Replace( typeof( T ).Name, "(?<=[a-z])([A-Z])", "_$1", RegexOptions.Compiled ).ToUpper();
    }
}