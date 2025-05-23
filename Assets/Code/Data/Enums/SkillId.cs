using System.ComponentModel;

namespace Code.Data.Enums
{
    public enum SkillId
    {
        None = 0,
        Attack,
        SplitShot,
        ShadowDash,
        Repeater,
        Reflexes,
        OldBell,
        Judgement,
        DoubleKnives,
        Lasso,
        SlowTime,
        ClosedBody,
        Bombardment,
        [Description("Summon the Guard")]
        SummonTheGuard,
        Matadeira,
        HolyRosary,
        [Description("Veil of Quills")]
        VeilOfQuills,
    }
}