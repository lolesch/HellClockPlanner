#if UNITY_EDITOR
using System;
using Code.Utility.Extensions;
using UnityEditor;
using UnityEditor.Build;
using UnityEditor.Build.Reporting;
using UnityEngine;
using System.ComponentModel;

namespace Code.Utility
{
    public sealed class BundleVersionSetter : IPreprocessBuildWithReport
    {
        private enum ReleaseType
        {
            None = 0,
            
            [Description("pa")]
            PreAlpha = 1,
            [Description("a")]
            Alpha = 2,
            [Description("b")]
            Beta = 3,
            [Description("rc")]
            ReleaseCandidate = 4,
            [Description("r")]
            Release = 5, // Gold
        }

        private enum IncrementType
        {
            TimeStamp,
            Patch,
            Minor,
            Major,
            ReleaseType,
        }

        public int callbackOrder => 0;

        public void OnPreprocessBuild(BuildReport report) => IncreasePatchNumber();

        private static void SplitBundleVersion(out int major, out int minor, out int patch, out ReleaseType release, out string timeStamp)
        {
            var bundleVersion = PlayerSettings.bundleVersion;

            bundleVersion = bundleVersion.Trim(); //clean up whitespace if necessary
            var parts = bundleVersion.Split('.', '-', '_');

            major = 0;
            minor = 0;
            patch = 0;
            release = ReleaseType.None;
            timeStamp = string.Empty;

            if (parts.Length > 0)
                int.TryParse(parts[0], out major);
            if (parts.Length > 1)
                int.TryParse(parts[1], out minor);
            if (parts.Length > 2)
                int.TryParse(parts[2], out patch);
            if (parts.Length > 3)
                timeStamp = parts[3];
            if (parts.Length > 4)
                Enum.TryParse(parts[4], out release);
        }

        public static string GetDisplayString()
        {
            SplitBundleVersion(out var major, out var minor, out var patch, out var releaseType, out var timeStamp);
            
            var versionNumber = $"v{major:0}.{minor:0}.{patch:0}";

            if( releaseType is not ReleaseType.None and < ReleaseType.Release )
                versionNumber = $"{versionNumber}_{releaseType.ToDescription()}";
            
            var commitHash = string.Empty;
            var gitHash = Resources.Load<TextAsset>("GitHash"); 
            if (gitHash != null) 
                commitHash = gitHash.text;
            
            return $"{versionNumber}_{commitHash}";
        }

        private static string IncrementBundleVersion( IncrementType increment )
        {
            SplitBundleVersion(out var major, out var minor, out var patch, out var releaseType, out var timeStamp);

            switch ( increment )
            {
                case IncrementType.TimeStamp:
                    break;
                case IncrementType.Patch:
                    patch++;
                    break;
                case IncrementType.Minor:
                    minor++;
                    patch = 0;
                    break;
                case IncrementType.Major:
                    major++;
                    minor = 0;
                    patch = 0;
                    break;
                case IncrementType.ReleaseType:
                    releaseType++;
                    patch = 0;
                    break;
                default:
                    throw new ArgumentOutOfRangeException( nameof(increment), increment, null );
            }
            
            timeStamp = DateTime.UtcNow.ToString("yyMMddHHmm"); 
            //uint.TryParse( timeStamp, out var number );
            //var hexString = number.ToString( "x" );
            
            var versionNumber = $"{major:0}.{minor:0}.{patch:0}_{timeStamp}";//_{hexString}";

            if (releaseType is not ReleaseType.None and < ReleaseType.Release)
                versionNumber = $"{versionNumber}_{releaseType}";
            
            if (PlayerSettings.bundleVersion != $"{versionNumber}")
            {
                PlayerSettings.bundleVersion = $"{versionNumber}";
                Debug.LogWarning($"bundleVersion: {PlayerSettings.bundleVersion.Colored(ColorExtensions.Orange)}");
            }
            return versionNumber;
        }

        [MenuItem("Tools/VersionNumber/Increase Patch Number", false, 800)]
        private static string IncreasePatchNumber() => IncrementBundleVersion( IncrementType.Patch);

        [MenuItem("Tools/VersionNumber/Increase Minor Number", false, 801)]
        private static string IncreaseMinorNumber() => IncrementBundleVersion( IncrementType.Minor);

        [MenuItem("Tools/VersionNumber/Increase Major Number", false, 802)]
        private static string IncreaseMajorNumber() => IncrementBundleVersion( IncrementType.Major);

        [MenuItem("Tools/VersionNumber/Increase ReleaseType", false, 803)]
        private static string IncreaseReleaseType() => IncrementBundleVersion( IncrementType.ReleaseType);

        [MenuItem("Tools/VersionNumber/Update TimeStamp", false, 804)]
        private static string UpdateTimeStamp() => IncrementBundleVersion( IncrementType.TimeStamp);
    }
}
#endif
