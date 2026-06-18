extends Control

#var Row = preload("res://GUI/DeckEditScene/CardInfoRow.tscn")

var db : SQLite
var data_path = "res://Resources//ahlcgdb"

func _input(_event):
	if Input.is_action_just_pressed("Escape"):
		exit()

func _ready():
	db = SQLite.new()
	db.path = data_path
	db.open_db()
	load_data()
	
func load_data():
	db.select_rows("'Investigator Cards'", "", ["*"])
	perform_investigator_filter()
	
	for result in db.query_result:
		var new_row = Row.instantiate()
		new_row.set_data(result)
		$MarginContainer/ScrollContainer/DBCards.add_child(new_row)
		new_row.connect("row_selected", Callable(self, "_on_row_selected"))  # Conectar señal
		print(result)

# Función que maneja la selección de filas
func _on_row_selected(selected_card_id):
	for row in $MarginContainer/ScrollContainer/DBCards.get_children():
		# Resetear modulación para deseleccionar
		row.modulate = Color(1, 1, 1)
	# Resaltar solo la fila seleccionada
	var selected_row = $MarginContainer/ScrollContainer/DBCards.find_node(selected_card_id, true, false)
	if selected_row:
		selected_row.modulate = Color(0.8, 0.8, 1)  # Resaltar color seleccionado

func perform_investigator_filter():
	pass


func exit():
	get_tree().change_scene_to_file("res://Scenes//MainMenu.tscn")

func _on_db_cards_ready():
	pass

func _on_exit_button_pressed():
	exit()
