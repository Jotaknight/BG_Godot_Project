using Godot;

public partial class CardListItemHeader : HBoxContainer
{
	private OrderButton _nameButton;
	private OrderButton _typeButton;
	private OrderButton _costButton;
	private OrderButton _skillsButton;
	private OrderButton _levelButton;

	public override void _Ready()
	{
		base._Ready();

		_nameButton = GetNode<OrderButton>("Name");
		_typeButton = GetNode<OrderButton>("Type");
		_costButton = GetNode<OrderButton>("Cost");
		_skillsButton = GetNode<OrderButton>("Skills");
		_levelButton = GetNode<OrderButton>("Level");

		_nameButton.Setup("Name");
		_typeButton.Setup("Type");
		_costButton.Setup("Cost");
		_skillsButton.Setup("Skills");
		_levelButton.Setup("Level");
	}
}
