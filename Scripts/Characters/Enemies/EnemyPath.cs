using Godot;
using System;
using System.Linq;

public partial class EnemyPath : Path3D
{
    [Export] public PackedScene EnemyScene;
    [Export] public Timer EnemySpawnerTimer;
    [Export] public DifficultManager DifficultManager;
    [Export] public VictoryLayer VictoryLayer;


    public override void _Ready()
    {
        EnemySpawnerTimer.Timeout += OnEnemySpawnerTimerTimeout;

        DifficultManager.StopSpawningEnemies += OnDifficultManagerStopSpawningEnemies;
    }

    
    private void SpawnEnemy()
    {
        Enemy newEnemy = (Enemy)EnemyScene.Instantiate();
        newEnemy.MaxHealth = (int)DifficultManager.SetEnemyNewHealth();
        AddChild(newEnemy);
        
        EnemySpawnerTimer.WaitTime = DifficultManager.GetSpawnTime();
        newEnemy.TreeExited += OnNewEnemyTreeExited;        
    }


    private void EnemyDefeated()
    {
        if (EnemySpawnerTimer.IsStopped())
        {
            foreach (PathFollow3D child in GetChildren().OfType<PathFollow3D>())
                { return; }
                
            VictoryLayer.Victory();
        }
    }


    private void OnEnemySpawnerTimerTimeout()
    {
        SpawnEnemy();
    }


    private void OnDifficultManagerStopSpawningEnemies()
    {
        EnemySpawnerTimer.Stop();
    }


    private void OnNewEnemyTreeExited()
    {
        EnemyDefeated();
    }
}
