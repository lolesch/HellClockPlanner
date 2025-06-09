using System.ComponentModel;
using UnityEngine.Serialization;

namespace Code.Data.Enums
{
    public enum LocaleId : byte
    {
        [Description(Const.LocaleIdEn)]
        En,
        [Description(Const.LocaleIdPtBr)]
        PtBr,
        [Description(Const.LocaleIdZhCn)]
        ZhCn,
    }
}