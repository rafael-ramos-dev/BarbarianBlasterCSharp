using Godot;
using System;

public partial class Enemy : PathFollow3D
{
	[Export] public AnimationPlayer AnimPlayer;
	[Export] public float Speed { get; private set;} = 2.5f;
	[Export] public int GoldReward { get; private set; } = 15;
	[Export] public int MaxHealth = 50;

	private int _currentHealth;
	public int CurrentHealth
	{
		get => _currentHealth;
		set
		{
			if (value < _currentHealth) { AnimPlayer.Play(GameConstants.TAKEDAMAGE); }

			_currentHealth = value;

			if (_currentHealth < 1) 
			{
				_bank.CurrentGold += GoldReward;
				QueueFree();
			}
		}
	}

	private Base _playerBase;
	private Bank _bank;
	private float _endLine = 1.0f;


    public override void _Ready()
    {
		CallDeferred(nameof(Initialize));

		_bank = (Bank)GetTree().GetFirstNodeInGroup(GameConstants.BANK);

		CurrentHealth = MaxHealth;
    }


    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(double delta)
	{
		this.Progress += Speed * (float)delta;

		if (this.ProgressRatio == _endLine)
		{
			_playerBase.TakeDamage();
			SetProcess(false);
			QueueFree();
		}
	}


	private void Initialize()
	{
		_playerBase = (Base)GetTree().GetFirstNodeInGroup(GameConstants.BASE);
	}
}
