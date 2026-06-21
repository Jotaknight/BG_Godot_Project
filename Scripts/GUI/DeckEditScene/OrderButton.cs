using Godot;

public partial class OrderButton : Button
{
	private Label _label;
	private TextureRect _textureRect;

	public override void _Ready()
	{
		base._Ready();

		_label = GetNode<Label>("HBoxContainer/Label");
		_textureRect = GetNode<TextureRect>("HBoxContainer/TextureRect");
	}

	public void Setup(string buttonName)
	{
		_label.Text = buttonName;
	}
}
