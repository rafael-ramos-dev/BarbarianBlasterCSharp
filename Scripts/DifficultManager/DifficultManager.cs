using Godot;
using System;

public partial class DifficultManager : Node
{
	[Signal] public delegate void StopSpawningEnemiesEventHandler();

	[Export] public Timer DifficultTimer;
	[Export] public Curve SpawnTimeCurve;
	[Export] public Curve EnemyHealthCurve;
	[Export] public float GameLength { get; private set; } = 30.0f;


	public override void _Ready()
	{
		DifficultTimer.Start(GameLength);

		DifficultTimer.Timeout += OnDifficultTimerTimeout;
	}


	public float GetSpawnTime()
	{
		return SpawnTimeCurve.Sample(GameProgressRatio());
	}


	public float SetEnemyNewHealth()
	{
		return EnemyHealthCurve.Sample(GameProgressRatio());
	}


	private float GameProgressRatio()
	{
		return 1.0f - (float)(DifficultTimer.TimeLeft / GameLength);
	}


	private void OnDifficultTimerTimeout()
    {
        EmitSignal(nameof(StopSpawningEnemies));
    }
}
