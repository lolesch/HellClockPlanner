
### FIX ME
- [ ] proficiencies are broken, no proficiency to select from
- [ ] skill level button throws error
- [ ] add skillStats for 
	- [ ] conviction gain
	- [ ] conviction decay resistance
- [ ] rank 0 is yellow
- [ ] default physical damage tag from dataImport

CONTINUE HERE:
- [ ] exchange global buffs to new import and rethink who is the source
- [ ] review statModifiersPerLevel and rethink who is the source
- [ ] implement language selection and notify displays
- [ ] relics
- [ ] add a toggle to show CharacterSheet / AllStats / ModifiedOnly ...
- [ ] improve stat displays 
	- [ ] details -> base + modifiers -> clamping range
	- [ ] show/hide details onHover -> stay visible onClick
- [ ] add "Attack" default skill details
- [ ] load icons once and store them 
- [ ] act unlock lookup for skills, relics and gear tiers
	- [x] Const.cs -> what act is unlocked in the builder version
- [x] welcome popup
- [x] changelog
	- [ ] automate this?
- [x] import data from submodule
- [ ] Check For Trinket Upgrade ( see InventoryTetris currency for reference )
- [x] skill details display
	- [ ] show all skill stats
	- [ ] calculate dps -> hit damage x attackSpeed x skillSpeed?
		- [ ] concider mana spent
	- [x] proficiencies apply skill modifier
	- [ ] show proficiencies in the "affected by" area???
- [x] Rank is rarity dependent
- [ ] Gear / Trinket dropdowns
	- [ ] random trinket generation
	- [ ] rarity distribution
- [ ] foundations
- [x] shrines
- [ ] create a hashNumber that restores the current build
	- [ ] update hash while building
	- [ ] copy/paste into input field
	- [ ] restore the pasted hash
- [ ] link known issues to the discord channel

### Trinkets
- [x] panel to show trinkets
	- [ ] dropdown to select rarity and tier
	- [ ] slider to select minMaxRoll

### Equipment
- [x] panel to show equipment
	- [ ] dropdown to select tier
	- [ ] slider to select minMaxRoll

### CharacterData
- [x] show character stats
	- [ ] stat cap ( max res = 75% )
### Skill Slots
- [ ] ~~dragAndDrop -> swap skills

### Proficiencies
- [x] show available slots
- [x] onHover -> show proficiency details popup
- [x] onClick -> show proficiencies dropdown
	- [x] make it skill-dependent
	- [x] apply global buff
	- [x] apply skill modifier
	- [ ] concat relic dependent proficiencies
		- implement relics
		- split proficiencyImportData so that the default dropdown list excludes relic proficiencies.
		- add relic proficiencies if the relic is present/equipped
			- [ ] change proficiency icon to show the relic

##### Meta Progression
-  gear
	- [ ] stat modifier
- great bell
	- [ ] stat modifier
	- [ ] passive skills
	- [ ] etc.
- relics
	- [ ] stat modifier
	- [ ] skill modifier / proc
<!--
- skill unlocking
- soul stones 
-->
##### Run Progression
- trinkets
	- [ ] stat modifier
- skills
	-  global buff
		- [x] stat modifier
	- proficiencies
		- [x] skill modifier
- foundations
	- [ ] stat modifier
	- [ ] proc
- shrines
	- [x] stat modifier
<!-- - gold -->