using System.ComponentModel;

namespace Code.Data.Enums
{
    public enum ModType : byte
    {
        Flat,
        Percent,
        //Mult = ModifierType.MultiplicativeMultiplicative,
    }
    
    public enum StatValueType : byte
    {
        [Description("DEFAULT")]
        Number = 0,
        [Description("PERCENTAGE")]
        Percent = 1,
    }
}