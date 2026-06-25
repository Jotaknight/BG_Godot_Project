using Godot;
using System;

public partial class OrderButton : Button
{

	public event Action<OrderButton> ButtonClicked;

	private Texture2D _ascendingIcon;
	private Texture2D _descendingIcon;
	private Label _label;
	private TextureRect _icon;

	public override void _Ready()
	{
		base._Ready();

		_ascendingIcon = GD.Load<Texture2D>("res://Resources/Images/GUI/arrowButtonUp.png");
		_descendingIcon = GD.Load<Texture2D>("res://Resources/Images/GUI/arrowButtonDown.png");

		_label = GetNode<Label>("HBoxContainer/Label");
		_icon = GetNode<TextureRect>("HBoxContainer/TextureRect");

		Pressed += OnButtonPressed;
	}

	public void Setup(string buttonName)
	{
		_label.Text = buttonName;
	}

	public void OnButtonPressed()
	{
		ButtonClicked?.Invoke(this);
	}

	public void SetAscending(bool ascending)
	{
		_icon.Texture = ascending ? _ascendingIcon : _descendingIcon;
	}

	public void ClearIcon()
	{
		_icon.Texture = null;
	}
}
