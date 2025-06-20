﻿using UnityEditor;
using UnityEngine;

namespace Code.Utility.Extensions
{
    public static class LogExtensions
    {
        public enum LogType
        {
            Log = 1,
            Warning = 2,

            Error = 3
            //Assert = 4,
        }

        private static bool doLog = true;

#if UNITY_EDITOR
        [MenuItem( "Tools/ToolSmiths/ToggleLogExtensions", false, 100 )]
        public static void ToggleLogExtensions()
        {
            doLog = !doLog;

            Debug.LogWarning(
                $"LogExtensions are now turned {( doLog ? "ON".Colored( Color.green ) : "OFF".Colored( Color.red ) )}" );
        }
#endif

        public static void MissingComponent( string type, GameObject gameObject, LogType logType = LogType.Error )
        {
            Log(
                $"{"MISSING:".Colored( Color.red )}\t{type.Colored( Color.yellow )} on {gameObject.ColoredComponent()} under {gameObject.transform.parent.name.ColoredComponent()}.",
                gameObject, logType );
        }

        public static void LogBroadcast( string type, GameObject gameObject, Color? color = null )
        {
            Log( type.ToUpper().Colored( color != null ? (Color) color : ColorExtensions.Orange ), gameObject,
                LogType.Warning );
        }

        public static void Select( GameObject gameObject, LogType logType = LogType.Log )
        {
            Log( $"{"SELECTION:".Colored( ColorExtensions.Orange )}\t{gameObject.ColoredComponent()}.", gameObject,
                logType );
        }

        public static void Position( Vector3 position, string description = "", LogType logType = LogType.Log )
        {
            Log( $"{"POSITION:".Colored( ColorExtensions.Purple )}\t{position}\t{description}", null, logType );
        }

        public static void Position( Vector2 position, string description = "", LogType logType = LogType.Log )
        {
            Log( $"{"POSITION:".Colored( ColorExtensions.Purple )}\t{position}\t\t{description}", null, logType );
        }
        //public static void Position(Coordinate position, string description = "", LogType logType = LogType.Log) => Log($"{"COORDINATE:".Colored(ColorExtensions.Purple)}\t{position}\t\t{description}", null, logType);

        private static void Log( string message, Object context, LogType type )
        {
            if ( !doLog )
                return;

            switch ( type )
            {
                case LogType.Log:
                    Debug.Log( message, context );
                    break;
                case LogType.Warning:
                    Debug.LogWarning( message, context );
                    break;
                case LogType.Error:
                    Debug.LogError( message, context );
                    break;
                //case LogType.Assert:
                //    Debug.LogAssertion(message, context);
                //    break;
                default:
                    Debug.LogError( "Something went wrong!\n" + message, context );
                    break;
            }
        }
    }
}