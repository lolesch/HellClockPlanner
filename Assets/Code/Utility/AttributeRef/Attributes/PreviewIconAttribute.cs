namespace Code.Utility.AttributeRef.Attributes
{
    public sealed class PreviewIconAttribute : UnityEngine.PropertyAttribute
    {
        public readonly float TextureSize;

        public PreviewIconAttribute( int textureSize = 64 )
        {
            TextureSize = textureSize;
        }
    }
}
