using Godot;
using System;

public partial class Enemy : PathFollow3D
{
	[Export] public float Speed { get; private set;} = 2.5f;

	private Base _playerBase;


    public override void _Ready()
    {
		CallDeferred(nameof(Initialize));
    }


    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(double delta)
	{
		this.Progress += Speed * (float)delta;

		if (this.ProgressRatio == 1.0f)
		{
			_playerBase.TakeDamage();
			SetProcess(false);
			// QueueFree();
		}
	}


	private void Initialize()
	{
		_playerBase = (Base)GetTree().GetFirstNodeInGroup(GameConstants.BASE);
	}
}
