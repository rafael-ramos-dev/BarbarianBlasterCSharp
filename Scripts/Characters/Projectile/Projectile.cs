using Godot;
using System;

public partial class Projectile : Area3D
{
	[Export] public float Speed { get; private set; } = 30.0f;

	public Vector3 projectileDirection = Vector3.Forward;


    public override void _PhysicsProcess(double delta)
    {
        this.Position += projectileDirection * Speed * (float)delta;
    }
}
