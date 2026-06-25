using Godot;
using Godot.Collections;
using System.Data.SQLite;

public partial class DeckEdit : Control
{
	private VBoxContainer _cardList;

	private PackedScene _cardListItemScene = GD.Load<PackedScene>("res://GUI/DeckEditScene/CardListItem.tscn");
	private PackedScene _cardPreviewScene = GD.Load<PackedScene>("res://GUI/DeckEditScene/CardPreview.tscn");

	private SQLiteConnection _db;
	private const string DataPath = "res://Resources/ahlcgdb";

	private CardPreview _currentPreview;
	private Vector2 _mousePositionOffset = new Vector2(20, 10);


	public override void _Ready()
	{
		base._Ready();

		_cardList = GetNode<VBoxContainer>("MarginContainer/PanelContainer/VBoxContainer/ScrollContainer/CardList");
		
		LoadData();

		_currentPreview = _cardPreviewScene.Instantiate<CardPreview>();
		AddChild(_currentPreview);
		_currentPreview.Visible = false;

		var _cardListItemHeader = GetNode<CardListItemHeader>("MarginContainer/PanelContainer/VBoxContainer/CardListHeader");

		_cardListItemHeader.OrderChangeOnName += OnOrderChangeOnName;
		_cardListItemHeader.OrderChangeOnType += OnOrderChangeOnType;
		_cardListItemHeader.OrderChangeOnCost += OnOrderChangeOnCost;
		_cardListItemHeader.OrderChangeOnSkills += OnOrderChangeOnSkills;
		_cardListItemHeader.OrderChangeOnLevel += OnOrderChangeOnLevel;
	}

	private void LoadData()
	{
		string absolutePath = ProjectSettings.GlobalizePath(DataPath);
		string connectionString = $"Data Source={absolutePath}.db;Version=3;";
		_db = new System.Data.SQLite.SQLiteConnection(connectionString);
		_db.Open();

		string query = "SELECT * FROM 'Investigator Cards'";

		using var command = new SQLiteCommand(query, _db);
		using var reader = command.ExecuteReader();

		while (reader.Read())
		{
			var card = ReaderToDict(reader);
			LoadCard(card);
		}
	}

	private Godot.Collections.Dictionary<string, Variant> ReaderToDict(SQLiteDataReader reader)
	{
		var dict = new Godot.Collections.Dictionary<string, Variant>();
		for (int i = 0; i < reader.FieldCount; i++)
		{
			if (reader.IsDBNull(i))
			{
				dict[reader.GetName(i)] = new Variant();
				continue;
			}

			Variant value = reader.GetFieldType(i).Name switch
			{
				"String"  => Variant.From(reader.GetString(i)),
				"Int64"   => Variant.From(reader.GetInt64(i)),
				"Int32"   => Variant.From(reader.GetInt32(i)),
				"Double"  => Variant.From(reader.GetDouble(i)),
				"Boolean" => Variant.From(reader.GetBoolean(i)),
				_         => Variant.From(reader.GetString(i)) // fallback a string
			};

			dict[reader.GetName(i)] = value;
		}
		return dict;
	}

	public override void _ExitTree()
	{
		if (_db != null && _db.State == System.Data.ConnectionState.Open)
		{
			_db.Close();
			_db.Dispose();
		}
	}

	private void LoadCard(Godot.Collections.Dictionary<string, Variant> card)
	{
		var newRow = _cardListItemScene.Instantiate<CardListItem>();

		var cardData = new CardData();
		cardData.SetupFromDB(card);

		newRow.Setup(cardData);

		newRow.CardSelected += OnCardSelected;
		newRow.CardDoubleClicked += OnCardDoubleClicked;
		newRow.HoverLong += OnCardHoverLong;
		newRow.HoverEnded += OnCardHoverEnded;

		_cardList.AddChild(newRow);
	}

	private void OnCardSelected(CardData cardData)
	{
		GD.Print($"Seleccioanda: {cardData.Name}");
	}

	private void OnCardDoubleClicked(CardData cardData)
	{
		GD.Print($"Doble click: {cardData.Name}");
		GD.Print($"mousey {GetViewport().GetMousePosition().Y}");
	}

	private void OnCardHoverLong(CardData cardData)
	{
		_currentPreview.Setup(cardData);
		RefreshPositionPreview();
		_currentPreview.Visible = true;
	}

	private void OnCardHoverEnded()
	{
		_currentPreview.Visible = false;
	}

	private void OnExitButtonPressed()
	{
		Exit();
	}

	public override void _Input(InputEvent @event)
	{
		if (Input.IsActionJustPressed("Escape"))
		{
			Exit();
		}
	}

	private void Exit()
	{
		GetTree().ChangeSceneToFile("res://Scenes/MainMenu.tscn");
	}

	public override void _Process(double delta)
	{
		if (_currentPreview.Visible)
		{
			RefreshPositionPreview();
		}
	}

	private void RefreshPositionPreview()
	{
		var mousePosition = GetViewport().GetMousePosition() + _mousePositionOffset;
		var previewSize = _currentPreview.GetTextureSize();
		var screenSize = GetViewportRect().Size;
		var newPosition = PreviewPositionCalculator.Calculate(mousePosition, previewSize, screenSize);
		_currentPreview.GlobalPosition = newPosition;
	}

	private void OnOrderChangeOnName(bool ascending)
	{
		GD.Print($"Order change on Name: {(ascending ? "Ascending" : "Descending")}");
	}

	private void OnOrderChangeOnType(bool ascending)
	{
		GD.Print($"Order change on Type: {(ascending ? "Ascending" : "Descending")}");
	}

	private void OnOrderChangeOnCost(bool ascending)
	{
		GD.Print($"Order change on Cost: {(ascending ? "Ascending" : "Descending")}");
	}

	private void OnOrderChangeOnSkills(bool ascending)
	{
		GD.Print($"Order change on Skills: {(ascending ? "Ascending" : "Descending")}");
	}

	private void OnOrderChangeOnLevel(bool ascending)
	{
		GD.Print($"Order change on Level: {(ascending ? "Ascending" : "Descending")}");
	}
}
