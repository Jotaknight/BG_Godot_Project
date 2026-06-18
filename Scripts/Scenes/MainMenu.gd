extends Control

func _input(_event):
	if Input.is_action_just_pressed("Escape"):
		exit()

func _on_exit_button_pressed():
	exit()
	
func exit():
	get_tree().quit()

func _on_ready():
	var first_focus_button = $ButtonList/NewCampaignButton
	
	# Enfoca el primer botón
	first_focus_button.grab_focus()


func _on_button_mouse_entered(button):
	release_buttons()
	button.grab_focus()


func release_buttons():
	for button in $ButtonList.get_children():
		button.release_focus()


func _on_button_list_ready():
	for button in $ButtonList.get_children():
		button.mouse_entered.connect(_on_button_mouse_entered.bind(button))

func _on_deck_building_button_pressed():
	get_tree().change_scene_to_file("res://Scenes//DeckEdit.tscn")
