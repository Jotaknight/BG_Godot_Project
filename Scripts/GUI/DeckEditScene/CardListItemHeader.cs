using Godot;
using System;

public partial class CardListItemHeader : HBoxContainer
{
	private OrderButton _nameButton;
	private OrderButton _typeButton;
	private OrderButton _costButton;
	private OrderButton _skillsButton;
	private OrderButton _levelButton;

	private (OrderButton button, bool ascending)? _currentOrder = null;

	public event Action<bool> OrderChangeOnName;
	public event Action<bool> OrderChangeOnType;
	public event Action<bool> OrderChangeOnCost;
	public event Action<bool> OrderChangeOnSkills;
	public event Action<bool> OrderChangeOnLevel;

	public override void _Ready()
	{
		base._Ready();

		CustomMinimumSize = new Vector2(0, CardListLayout.HeaderHeight);
	
		_nameButton = GetNode<OrderButton>("Name");
		_typeButton = GetNode<OrderButton>("Type");
		_costButton = GetNode<OrderButton>("Cost");
		_skillsButton = GetNode<OrderButton>("Skills");
		_levelButton = GetNode<OrderButton>("Level");

		_nameButton.CustomMinimumSize = new Vector2(CardListLayout.SpacerWidth + CardListLayout.NameWidth, 0);
		_typeButton.CustomMinimumSize = new Vector2(CardListLayout.TypeWidth, 0);
		_costButton.CustomMinimumSize = new Vector2(CardListLayout.CostWidth, 0);
		_skillsButton.CustomMinimumSize = new Vector2(CardListLayout.SkillsWidth, 0);
		_levelButton.CustomMinimumSize = new Vector2(CardListLayout.LevelWidth, 0);

		_nameButton.Setup("Name");
		_typeButton.Setup("Type");
		_costButton.Setup("Cost");
		_skillsButton.Setup("Skills");
		_levelButton.Setup("Level");

		_nameButton.ButtonClicked += OnOrderButtonClicked;
		_typeButton.ButtonClicked += OnOrderButtonClicked;
		_costButton.ButtonClicked += OnOrderButtonClicked;
		_skillsButton.ButtonClicked += OnOrderButtonClicked;
		_levelButton.ButtonClicked += OnOrderButtonClicked;
	}

	public void OnOrderButtonClicked(OrderButton button)
	{
		if (_currentOrder.HasValue && _currentOrder.Value.button == button)
		{
			_currentOrder = (_currentOrder.Value.button, !_currentOrder.Value.ascending);
			_currentOrder.Value.button.SetAscending(_currentOrder.Value.ascending);
		}
		else
		{
			if (_currentOrder.HasValue)
			{
				_currentOrder.Value.button.ClearIcon();
			}

			_currentOrder = (button, true);
			_currentOrder.Value.button.SetAscending(true);
		}

		SendOrderChangeEvent();
	}

	private void SendOrderChangeEvent()
	{
		if (_currentOrder.Value.button == _nameButton)
		{
			OrderChangeOnName?.Invoke(_currentOrder.Value.ascending);
		}
		else if (_currentOrder.Value.button == _typeButton)
		{
			OrderChangeOnType?.Invoke(_currentOrder.Value.ascending);
		}
		else if (_currentOrder.Value.button == _costButton)
		{
			OrderChangeOnCost?.Invoke(_currentOrder.Value.ascending);
		}
		else if (_currentOrder.Value.button == _skillsButton)
		{	
			OrderChangeOnSkills?.Invoke(_currentOrder.Value.ascending);
		}
		else if (_currentOrder.Value.button == _levelButton)
		{
			OrderChangeOnLevel?.Invoke(_currentOrder.Value.ascending);
		}
	}
}
