using Godot;
using System;

public partial class MainMenu : Control
{

	public override void _Ready()
	{
		var firstFocusButton = GetNode<Button>("ButtonList/NewCampaignButton");
		firstFocusButton.GrabFocus();

		GetNode<Button>("ButtonList/DeckBuildingButton").Pressed += OnDeckBuildingButtonPressed;
		GetNode<Button>("ButtonList/ExitButton").Pressed += Exit;
		
		OnButtonListReady();
	}

	public override void _Input(InputEvent @event)
	{
		if (Input.IsActionJustPressed("Escape"))
		{
			Exit();
		}
	}

	public void Exit()
	{
		GetTree().Quit();
	}

	private void OnButtonMouseEntered(Button button)
	{
		ReleaseButtons();
		button.GrabFocus();
	}

	private void ReleaseButtons()
	{
		var buttonList = GetNode("ButtonList");
		foreach (var child in buttonList.GetChildren())
		{
			if (child is Button button)
			{
				button.ReleaseFocus();
			}
		}
	}

	public void OnButtonListReady()
	{
		var buttonList = GetNode("ButtonList");
		foreach (var child in buttonList.GetChildren())
		{
			if (child is Button button)
			{
				GD.Print("ADDD"+button.Name);
				button.MouseEntered += () => OnButtonMouseEntered(button);
			}
		}
	}

	public void OnDeckBuildingButtonPressed()
	{
		GetTree().ChangeSceneToFile("res://Scenes/DeckEdit.tscn");
	}
}
