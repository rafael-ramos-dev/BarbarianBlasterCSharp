using Godot;
using System;

public partial class Projectile : Area3D
{
    [Export] public Timer ProjectileLifeTimer;
	[Export] public float Speed { get; private set; } = 30.0f;
    [Export] public int Damage = 25;

	public Vector3 projectileDirection = Vector3.Forward;


    public override void _Ready()
    {
        ProjectileLifeTimer.Timeout += OnProjectileLifeTimerTimeOut;
        this.AreaEntered += OnProjectileAreaEntered;
    }

    
    public override void _PhysicsProcess(double delta)
    {
        this.Position += projectileDirection * Speed * (float)delta;
    }


    private void OnProjectileAreaEntered(Area3D area)
    {
        if (area.IsInGroup("EnemyArea"))
        {
            area.GetParent<Enemy>().CurrentHealth -= Damage;
            QueueFree();
        }
    }

   
    private void OnProjectileLifeTimerTimeOut()
    {
        QueueFree();
    }
}
