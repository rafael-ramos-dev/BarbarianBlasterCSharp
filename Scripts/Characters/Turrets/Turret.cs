using Godot;
using System;

public partial class Turret : Node3D
{
	[Export] public PackedScene Projectile;


    public override void _Ready()
    {
        Node3D bullet = (Node3D)Projectile.Instantiate();
		AddChild(bullet);
    }
}
