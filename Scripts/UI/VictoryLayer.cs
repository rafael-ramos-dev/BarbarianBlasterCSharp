using Godot;
using System;

public partial class VictoryLayer : CanvasLayer
{
	[Export] public Button RetryButton;
	[Export] public Button QuitButton;

	private Base _base;
	private Bank _bank;
	private TextureRect _starOne;
	private TextureRect _starTwo;
	private TextureRect _starThree;
	private Label _moneyLabel;
	private Label _healthLabel;


	public override void _Ready()
	{
		this.Visible = false;

		_base = (Base)GetTree().GetFirstNodeInGroup(GameConstants.BASE);
		_bank = (Bank)GetTree().GetFirstNodeInGroup(GameConstants.BANK);

		_starOne = GetNode<TextureRect>("%Star1");
		_starTwo = GetNode<TextureRect>("%Star2");
		_starThree = GetNode<TextureRect>("%Star3");
		_moneyLabel = GetNode<Label>("%MoneyLabel");
		_healthLabel = GetNode<Label>("%HealthLabel");

		_moneyLabel.Visible = false;
		_healthLabel.Visible = false;

		RetryButton.Pressed += OnRetryButtonPressed;
		QuitButton.Pressed += OnQuitButtonPressed;
		// Engine.TimeScale = 5;
	}


	public void Victory()
	{
		this.Visible = true;

		if (_base.MaxHealth == _base.CurrentHealth)
		{ 
			_starTwo.Modulate = Colors.White;
			_healthLabel.Visible = true;
		}

		if (_bank.CurrentGold >= 500)
		{
			if (_starTwo.Modulate == Colors.White)
				{ _starThree.Modulate = Colors.White; }
			else
				{ _starTwo.Modulate = Colors.White; }
			
			_moneyLabel.Visible = true;
		}
	}

    
    private void OnRetryButtonPressed()
    {
		GetTree().ReloadCurrentScene();
    }


	private void OnQuitButtonPressed()
    {
        GetTree().Quit();
    }
}
