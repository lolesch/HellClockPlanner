using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using ZLinq;

namespace Code.Utility.Extensions
{
    public static class EnumerableExtensions
    {
        public static IEnumerable<T> Randomize<T>( this IEnumerable<T> source )
        {
            return source?.AsValueEnumerable().OrderBy( x => Guid.NewGuid() ).AsEnumerable();
        }

        public static IEnumerable<T> GetScriptableObjectsOfType<T>() where T : ScriptableObject
        {
#if UNITY_EDITOR
            var guids = AssetDatabase.FindAssets( $"t:{typeof(T).Name}" );
            var paths = guids.AsValueEnumerable().Select( AssetDatabase.GUIDToAssetPath );

            return paths.Select( AssetDatabase.LoadAssetAtPath<T> ).AsEnumerable();
#else
            return null;
#endif
        }

        /// <summary>
        ///     Wraps this object instance into an IEnumerable&lt;T&gt;
        ///     consisting of a single item.
        /// </summary>
        /// <typeparam name="T"> Type of the object. </typeparam>
        /// <param name="item"> The instance that will be wrapped. </param>
        /// <returns> An IEnumerable&lt;T&gt; consisting of a single item. </returns>
        public static IEnumerable<T> Yield<T>( this T item )
        {
            yield return item;
        }

        public static IEnumerable<T> AsEnumerable<TEnumerator, T>( this ValueEnumerable<TEnumerator, T> valueEnumerable ) where TEnumerator : struct, IValueEnumerator<T>
        {
            using var e = valueEnumerable.Enumerator;
            while ( e.TryGetNext( out var current) )
                yield return current;
        }
    }
}