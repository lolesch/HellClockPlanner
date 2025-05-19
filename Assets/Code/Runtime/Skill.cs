using System.Collections.Generic;
using System.Linq;
using Code.Data;
using Code.Data.Enums;
using Code.Runtime.Statistics;

namespace Code.Runtime
{
    public sealed class Skill : IModifierSource
    {
        private readonly SkillImportData _config;
        private readonly List<SkillProficiency> _proficiencies;
        private readonly List<GlobalBuffImportData> _globalBuffs;
        public string description => _config.description;
        public int manaCost => _config.manaCost;
        public float cooldown  => _config.cooldown;
        public int baseDamage => _config.baseDamage;
        
        public int rank => _proficiencies.Count( x => x.proficiencyId != ProficiencyId.None );// (+1?)
        public bool isAssigned => GameState.Player.SkillSlots.Select( x => x._skillHashId ).Contains( _config.id );

        public void AddProficiency( SkillProficiency proficiency )
        {
            // TODO: add slot lookup -> replace proficiencies in the corresponding dropdown slot
            // else add to list
            _proficiencies.Add( proficiency );
            
            foreach( var buff in _globalBuffs )
                GameState.Player.GetStat( buff.globalBuff ).AddModifier( new Modifier( buff.amountPerRank, this ) );
        }
        
        public void RemoveProficiency( SkillProficiency proficiency )
        {
            _proficiencies.Remove( proficiency );
            
            _globalBuffs.ForEach( x => GameState.Player.GetStat( x.globalBuff )
                .TryRemoveAllModifiersBySource(  this ) );
        }
        
        public void RevertGlobalBuffs() => _globalBuffs.ForEach( x => GameState.Player.GetStat( x.globalBuff ).TryRemoveAllModifiersBySource( this ) );
        
        public Skill( SkillImportData config, List<GlobalBuffImportData> globalBuffs )
        {
            _config = config;
            _globalBuffs = globalBuffs;
            _proficiencies = new List<SkillProficiency>();
        }
    }
}