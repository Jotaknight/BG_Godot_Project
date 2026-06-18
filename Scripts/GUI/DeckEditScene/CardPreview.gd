extends Control

@onready var texture_rect = $TextureRect

func setup(card):
	texture_rect.texture = CardImageResolver.get_texture(card)

func get_texture_size():
	return texture_rect.size
