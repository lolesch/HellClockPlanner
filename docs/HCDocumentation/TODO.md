
### FIX ME
- [ ] global buff mod should be rarity dependent like the rank
- [ ]  PROFICIENCY DROPDOWN options are broken visually
- [ ] proficiency dropdown top-most option has no effect
- [ ] skill stats display no longer updates

CONTINUE HERE:
- [x] skill details display
	- [ ] proficiencies apply skill modifier
	- [ ] show proficiencies in the "affected by" area???
- [x] Rank is rarity dependent
- [ ] Gear / Trinket dropdowns
	- [ ] random trinket generation
	- [ ] rarity distribution
- [ ] foundations
- [ ] shrines
- [ ] create a hashNumber that restores the current build
	- [ ] update hash while building
	- [ ] copy/paste into input field
	- [ ] restore the pasted hash

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
- [x] listen for save file load and display the loaded skills
- [ ] ~~onHover -> show skill details popup
- [x] onClick -> show skill dropdown to select from
	- [x] onSelect -> update saveFile with new skill
- [ ] ~~dragAndDrop -> swap skills

### Proficiencies
- [x] show available slots
- [x] onHover -> show proficiency details popup
- [x] onClick -> show proficiencies dropdown
	- [x] make it skill-dependent
	- [x] apply global buff
	- [ ] apply skill modifier
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
	- [ ] stat modifier
<!-- - gold -->