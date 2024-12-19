using Godot;
using System;
using System.Collections.Generic;
using System.Linq;

public partial class Turret : Node3D
{
	[Export] public PackedScene Projectile;
    [Export] public Timer ProjectileTimer;
    [Export] public Node3D ProjectileSpawner;
    [Export] public AnimationPlayer AnimPlayer;
    [Export] public Node3D Cannon;
    [Export] public Node3D TurretBase;
    [Export] public float TurretRange = 10.0f;

    public Path3D enemyPath;
    public IEnumerable<PathFollow3D> enemies;
    public PathFollow3D closestEnemyToBase;

    private float _enemyTargetDistance;


    public override void _Ready()
    {
        ProjectileTimer.Timeout += OnProjectileTimerTimeout;
    }


    public override void _PhysicsProcess(double delta)
    {
        enemies = enemyPath.GetChildren().OfType<PathFollow3D>();
        closestEnemyToBase = FindBestTarget(enemies);

        if (closestEnemyToBase != null)
            { TurretBase.LookAt(closestEnemyToBase.GlobalPosition,Vector3.Up, true); }
    }


    public PathFollow3D FindBestTarget(IEnumerable<PathFollow3D> target)
    {
        PathFollow3D bestTarget = null;
        float bestProgress = 0f;

        foreach (PathFollow3D enemyTarget in target)
        {
            if (enemyTarget == null || !enemyTarget.IsInsideTree()) { continue; }

            _enemyTargetDistance = GlobalPosition.DistanceTo(enemyTarget.GlobalPosition);

            if (enemyTarget.Progress > bestProgress && _enemyTargetDistance <= TurretRange)
            {
                bestTarget = enemyTarget;
                bestProgress = enemyTarget.Progress;
            }
        }

        return bestTarget;
    }


    private void OnProjectileTimerTimeout()
    {
        if (closestEnemyToBase != null)
        {
            AnimPlayer.Play(GameConstants.FIRE);

            Projectile bullet = (Projectile)Projectile.Instantiate();
            AddChild(bullet);
            
            bullet.GlobalPosition = Cannon.GlobalPosition;
            bullet.projectileDirection = TurretBase.GlobalTransform.Basis.Z;
        }
    }
}
