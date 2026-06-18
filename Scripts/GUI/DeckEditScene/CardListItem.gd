extends Button
class_name CardListItem

signal card_selected(card_data)
signal card_double_clicked(card_data)

signal hover_long(card_image)
signal hover_ended

var card_data
var last_click_time := 0

#var COLOR_GUARDIAN = Color(0.0/255, 77.0/255, 122.0/255, 255.0/255) # "#004D7A" #0 77 122
#var COLOR_SEEKER = Color(209, 136, 50, 0) # "#D18832" #209 136 50
#var COLOR_REBEL = Color(13, 93, 57, 0) # "#0D5D39" #13 93 57
#var COLOR_MYSTIC = Color(76, 80, 157, 0) # "#4C509D" #76 80 157
#var COLOR_SURVIVOR = Color(183, 40, 50, 0) # "#B72832" #183 40 50
#var COLOR_MULTICLASS = Color(165, 147, 87, 0) # "#A59357" #165 147 87
#var COLOR_NEUTRAL = Color(121, 118, 110, 0) # "#79766E" #121 118 110

var COLOR_GUARDIAN = "#004D7A"
var COLOR_SEEKER = "#D18832"
var COLOR_REBEL = "#0D5D39"
var COLOR_MYSTIC = "#4C509D"
var COLOR_SURVIVOR = "#B72832"
var COLOR_MULTICLASS = "#A59357"
var COLOR_NEUTRAL = "#79766E"

@onready var name_label = $HBoxContainer/Name
@onready var type_label = $HBoxContainer/Type
@onready var cost_label = $HBoxContainer/Cost
@onready var skills_container = $HBoxContainer/Skills
@onready var level_label = $HBoxContainer/Level
@onready var hover_timer = Timer.new()

# Called when the node enters the scene tree for the first time.
func _ready():
	mouse_entered.connect(_on_hover)
	mouse_exited.connect(_on_hover_exit)
	
	focus_entered.connect(_on_focus)
	focus_exited.connect(_on_focus_exit)
	
	add_child(hover_timer)
	hover_timer.one_shot = true
	hover_timer.wait_time = 1
	hover_timer.timeout.connect(_on_hover_timeout)

func setup(data):
	await ready
	card_data = data
	
	_set_card_data()
	_set_color()

func _set_card_data():
	set_card_name()
	set_card_class()
	set_card_level()
	set_card_type()
	set_card_cost()
	set_card_skills()
	set_card_image()

func _on_hover():
	hover_timer.start()

func _on_hover_exit():
	if hover_timer.is_stopped():
		hover_ended.emit()
	else:
		hover_timer.stop()
	
func _on_focus():
	pass

func _on_focus_exit():
	pass

func _gui_input(event):
	if event is InputEventMouseButton and event.pressed:
		var now = Time.get_ticks_msec()
		
		if now - last_click_time < 300:
			card_double_clicked.emit(card_data)
		
		last_click_time = now
	
func get_class_color(card_class):
	if len(card_class) > 1:
		return COLOR_MULTICLASS
	if 'M' in card_class:
		return COLOR_MYSTIC
	if 'G' in card_class:
		return COLOR_GUARDIAN
	if 'K' in card_class:
		return COLOR_SEEKER
	if 'R' in card_class:
		return COLOR_REBEL
	if 'S' in card_class:
		return COLOR_SURVIVOR
	
	return COLOR_NEUTRAL

func set_card_name():
	name_label.text = card_data.name

func set_card_class():
	pass

func set_card_level():
		#LEVEL
	if card_data.level == null:
		cost_label.text = "-"
	else:
		cost_label.text = str(card_data.level)

func set_card_type():
	type_label.text = card_data.type

func set_card_cost():
	if card_data.cost == null:
		cost_label.text = "-"
	else:
		cost_label.text = str(card_data.cost)

func set_card_skills():
	#clean old ones
	for child in skills_container.get_children():
		child.queue_free()
	
	_add_skill("W", card_data.skills.willpower)
	_add_skill("I", card_data.skills.intellect)
	_add_skill("C", card_data.skills.combat)
	_add_skill("A", card_data.skills.agility)
	_add_skill("?", card_data.skills.wild)

func set_card_image():
	pass

func _add_skill(letter: String, amount: int):
	for i in amount:
		var label = Label.new()
		label.text = letter
		skills_container.add_child(label)

func _set_color():
	var color = get_class_color(card_data.card_class)

	name_label.add_theme_color_override("font_color", Color(color))

func _on_hover_timeout():
	hover_long.emit(card_data)
