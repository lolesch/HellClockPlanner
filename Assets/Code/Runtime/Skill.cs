using System.Linq;
using Code.Data;
using Code.Data.Enums;

namespace Code.Runtime
{
    public sealed class Skill
    {
        public SkillImportData config { get; private set; }
        private SkillProficiency[] _proficiencies = new SkillProficiency[10];
        // SkillStats
        // global buff
        public int rank => _proficiencies.Count( x => x.proficiency != ProficiencyId.None );// (+1?)
        public bool isAssigned => GameState.Player.SkillSlots.Select( x => x._skillHashId ).Contains( config.Id );

        public void AddProficiency( SkillProficiency proficiency )
        {
            
        }
    }
}