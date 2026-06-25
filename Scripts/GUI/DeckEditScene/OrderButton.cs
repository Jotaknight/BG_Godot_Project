using Godot;
using System;

public partial class OrderButton : Button
{

	public event Action<OrderButton> ButtonClicked;

	private Texture2D _ascendingIcon;
	private Texture2D _descendingIcon;

	public override void _Ready()
	{
		base._Ready();

		_ascendingIcon = GD.Load<Texture2D>("res://Resources/Images/GUI/arrowButtonUp.png");
		_descendingIcon = GD.Load<Texture2D>("res://Resources/Images/GUI/arrowButtonDown.png");

		Pressed += OnButtonPressed;
	}

	public void Setup(string buttonName)
	{
		Text = buttonName;
	}

	public void OnButtonPressed()
	{
		ButtonClicked?.Invoke(this);
	}

	public void SetAscending(bool ascending)
	{
		Icon = ascending ? _ascendingIcon : _descendingIcon;
	}

	public void ClearIcon()
	{
		Icon = null;
	}
}
