class_name CardImageResolver

static func get_texture(card: CardData) -> Texture2D:
	var card_filename = "%s%d%s"%[card.name, card.level, card.card_class]
	var path = "res://Resources//Images//Cards//Player//%s.jpg"%card_filename
	
	var texture = load(path)
	
	return texture
