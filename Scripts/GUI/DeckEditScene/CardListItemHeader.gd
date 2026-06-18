extends HBoxContainer
class_name CardListItemHeader

@onready var name_button = $Name
@onready var type_button = $Type
@onready var cost_button = $Cost
@onready var skills_button = $Skills
@onready var level_button = $Level

# Called when the node enters the scene tree for the first time.
func _ready():
	print(get_tree_string())
	name_button.setup("Name")
	$Type.setup("Type")
	$Cost.setup("Cost")
	$Skills.setup("Skills")
	$Level.setup("Level")
