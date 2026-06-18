extends GutTest

var DeckEdit = preload("res://Scripts/Scenes/DeckEdit.gd")
var deck = DeckEdit.new()

func test_top_half_preview_fits():
	var mouse = Vector2(100,100)
	var preview = Vector2(300,300)
	var screen = Vector2(500,500)

	var result = deck.calculate_position_preview(mouse, preview, screen)
	assert_eq(result, Vector2(100, 100))

func test_bottom_half_preview_fits():
	var mouse = Vector2(100,400)
	var preview = Vector2(300,300)
	var screen = Vector2(500,500)

	var result = deck.calculate_position_preview(mouse, preview, screen)
	assert_eq(result, Vector2(100, 100))

func test_top_half_preview_runs_off():
	var mouse = Vector2(50,50)
	var preview = Vector2(180,180)
	var screen = Vector2(300,200)

	var result = deck.calculate_position_preview(mouse, preview, screen)
	assert_eq(result, Vector2(50, 20))

func test_bottom_half_preview_runs_off():
	var mouse = Vector2(40,101)
	var preview = Vector2(150,150)
	var screen = Vector2(300,200)

	var result = deck.calculate_position_preview(mouse, preview, screen)
	assert_eq(result, Vector2(40, 0))
