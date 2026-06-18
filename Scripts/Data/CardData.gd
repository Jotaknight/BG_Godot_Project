class_name CardData

var id
var name
var card_class
var level
var type
var cost
var skills

func setup_from_db(item):
	id = item["CARD_ID"]
	name = item["CARD_NAME"]
	card_class = item["CARD_CLASS"]
	level = item["CARD_LEVEL"]
	type = item["CARD_TYPE"]
	cost = item["CARD_COST"]
	
	skills = Skills.new()
	skills.willpower = item["SKILL_WILLPOWER"]
	skills.intellect = item["SKILL_INTELLECT"]
	skills.combat = item["SKILL_COMBAT"]
	skills.agility = item["SKILL_AGILITY"]
	skills.wild = item["SKILL_WILD"]
