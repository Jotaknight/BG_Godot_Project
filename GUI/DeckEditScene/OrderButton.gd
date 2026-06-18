extends Button
class_name OrderButton

@onready var label = $HBoxContainer/Label
@onready var texture_rect = $HBoxContainer/TextureRect

func setup(button_name):
	label.text = button_name
