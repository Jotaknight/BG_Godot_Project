extends Control

@onready var card_list = $MarginContainer/PanelContainer/VBoxContainer/ScrollContainer/CardList

var card_item_scene = preload("res://GUI/DeckEditScene/CardListItem.tscn")
var preview_scene = preload("res://GUI/DeckEditScene/CardPreview.tscn")
#var card_item_header = preload("res://GUI/DeckEditScene/CardListItemHeader.tscn")

var db : SQLite
var data_path = "res://Resources//ahlcgdb"

var current_preview = null
var mouse_position_offset := Vector2(20, 10)

# Called when the node enters the scene tree for the first time.
func _ready():
	load_data()
	
	current_preview = preview_scene.instantiate()
	add_child(current_preview)
	current_preview.visible = false
	
	var card_item_header = preload("res://GUI/DeckEditScene/CardListItemHeader.tscn")
	card_item_header.setup()
	

func load_data():
	db = SQLite.new()
	db.path = data_path
	db.open_db()
	
	db.select_rows("'Investigator Cards'", "", ["*"])
	#perform_investigator_filter()
	
	for item in db.query_result:
		load_card(item)

func load_card(card):
	var new_row = card_item_scene.instantiate()
	
	var card_data = CardData.new()
	card_data.setup_from_db(card)
	
	new_row.setup(card_data)
	
	new_row.card_selected.connect(_on_card_selected)
	new_row.card_double_clicked.connect(_on_card_double_clicked)

	new_row.hover_long.connect(_on_hover_long)
	new_row.hover_ended.connect(_on_hover_ended)

	card_list.add_child(new_row)

func _on_card_selected(card):
	print("Seleccionada:", card.name)

func _on_card_double_clicked(card):
	print("DOBLE CLICK:", card.name)
	print("mousey ", get_viewport().get_mouse_position().y)

func _on_hover_long(card):
	current_preview.setup(card)
	refresh_position_preview()
	current_preview.visible = true
	
func _on_hover_ended():
	current_preview.visible = false
	
func _on_exit_button_pressed():
	exit()

func _input(_event):
	if Input.is_action_just_pressed("Escape"):
		exit()

func exit():
	get_tree().change_scene_to_file("res://Scenes//MainMenu.tscn")
	
func _process(_delta):
	if current_preview.visible:
		refresh_position_preview()

func refresh_position_preview():
	var mouse_position = get_viewport().get_mouse_position() + mouse_position_offset
	var preview_size = current_preview.get_texture_size()
	var screen_size = get_viewport_rect().size
	var new_position = calculate_position_preview(mouse_position, preview_size, screen_size)
	current_preview.global_position = new_position
	
func calculate_position_preview(mouse_position, preview_size, screen_size):
	var new_position = mouse_position
	
	if new_position.y < screen_size.y/2:
		var sobrepaso = max(0, new_position.y + preview_size.y - screen_size.y)
		new_position.y = new_position.y - sobrepaso
	else:
		new_position.y = max(0, new_position.y - preview_size.y)
		
	return new_position
