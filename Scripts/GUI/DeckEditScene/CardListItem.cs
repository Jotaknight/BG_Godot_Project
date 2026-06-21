using Godot;
using System;

public partial class CardListItem : Button
{
	public event Action<CardData> CardSelected;
	public event Action<CardData> CardDoubleClicked;

	public event Action<CardData> HoverLong;
	public event Action HoverEnded;

	private CardData _cardData;
	private ulong _lastClickTime = 0;

	private const string COLOR_GUARDIAN = "#004D7A";
	private const string COLOR_SEEKER = "#D18832";
	private const string COLOR_REBEL = "#0D5D39";
	private const string COLOR_MYSTIC = "#4C509D";
	private const string COLOR_SURVIVOR = "#B72832";
	private const string COLOR_MULTICLASS = "#A59357";
	private const string COLOR_NEUTRAL = "#79766E";

	private Label _nameLabel;
	private Label _typeLabel;
	private Label _costLabel;
	private HBoxContainer _skillsContainer;
	private Label _levelLabel;

	private Timer _hoverTimer;

	public override void _Ready()
	{
		_nameLabel = GetNode<Label>("HBoxContainer/Name");
		_typeLabel = GetNode<Label>("HBoxContainer/Type");
		_costLabel = GetNode<Label>("HBoxContainer/Cost");
		_skillsContainer = GetNode<HBoxContainer>("HBoxContainer/Skills");
		_levelLabel = GetNode<Label>("HBoxContainer/Level");

		MouseEntered += OnHover;
		MouseExited += OnHoverExit;

		FocusEntered += OnFocus;
		FocusExited += OnFocusExit;

		_hoverTimer = new Timer();
		AddChild(_hoverTimer);
		_hoverTimer.OneShot = true;
		_hoverTimer.WaitTime = 1.0;
		_hoverTimer.Timeout += OnHoverTimeout;
	}

	public async void Setup(CardData data)
	{
		await ToSignal(this, Node.SignalName.Ready);

		_cardData = data;

		SetCardData();
		SetColor();
	}

	private void SetCardData()
	{
		SetCardName();
		SetCardClass();
		SetCardLevel();
		SetCardType();
		SetCardCost();
		SetCardSkills();
		SetCardImage();
	}

	private void OnHover()
	{
		_hoverTimer.Start();
	}

	private void OnHoverExit()
	{
		if (_hoverTimer.IsStopped())
			HoverEnded?.Invoke();
		else
			_hoverTimer.Stop();
	}

	private void OnFocus()
	{
	}

	private void OnFocusExit()
	{
	}

	public override void _GuiInput(InputEvent @event)
	{
		if (@event is InputEventMouseButton mouseEvent &&
			mouseEvent.Pressed)
		{
			ulong now = Time.GetTicksMsec();

			if (now - _lastClickTime < 300)
			{
				CardDoubleClicked?.Invoke(_cardData);
			}

			_lastClickTime = now;
		}
	}

	private string GetClassColor(string cardClass)
	{
		if (cardClass.Length > 1)
			return COLOR_MULTICLASS;

		if (cardClass.Contains('M'))
			return COLOR_MYSTIC;

		if (cardClass.Contains('G'))
			return COLOR_GUARDIAN;

		if (cardClass.Contains('K'))
			return COLOR_SEEKER;

		if (cardClass.Contains('R'))
			return COLOR_REBEL;

		if (cardClass.Contains('S'))
			return COLOR_SURVIVOR;

		return COLOR_NEUTRAL;
	}

	private void SetCardName()
	{
		_nameLabel.Text = _cardData.Name;
	}

	private void SetCardClass()
	{
	}

	private void SetCardLevel()
	{
		_levelLabel.Text =
			_cardData.Level == null
				? "-"
				: _cardData.Level.ToString();
	}

	private void SetCardType()
	{
		_typeLabel.Text = _cardData.Type;
	}

	private void SetCardCost()
	{
		_costLabel.Text =
			_cardData.Cost == null
				? "-"
				: _cardData.Cost.ToString();
	}

	private void SetCardSkills()
	{
		foreach (Node child in _skillsContainer.GetChildren())
		{
			child.QueueFree();
		}

		AddSkill("W", _cardData.Skills.Willpower);
		AddSkill("I", _cardData.Skills.Intellect);
		AddSkill("C", _cardData.Skills.Combat);
		AddSkill("A", _cardData.Skills.Agility);
		AddSkill("?", _cardData.Skills.Wild);
	}

	private void SetCardImage()
	{
	}

	private void AddSkill(string letter, int amount)
	{
		for (int i = 0; i < amount; i++)
		{
			var label = new Label();
			label.Text = letter;

			_skillsContainer.AddChild(label);
		}
	}

	private void SetColor()
	{
		string color = GetClassColor(_cardData.Class);

		_nameLabel.AddThemeColorOverride(
			"font_color",
			Color.FromString(color, Colors.White)
		);
	}

	private void OnHoverTimeout()
	{
		HoverLong?.Invoke(_cardData);
	}
}
