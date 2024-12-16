using Godot;
using System;

public partial class TurretManager : Node3D
{
	[Export] public PackedScene turret;

		
	public void BuildTurret(Vector3 turretPosition)
	{
		if (turret != null)
		{
			Node3D newTurret = (Node3D)turret.Instantiate();
			AddChild(newTurret);
			newTurret.GlobalPosition = turretPosition;
		}
		else
		{
			GD.Print("Turret scene not found in the TurretManager. You need to set one scene");	
		}
	}
}
