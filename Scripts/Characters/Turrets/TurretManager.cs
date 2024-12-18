using Godot;
using System;

public partial class TurretManager : Node3D
{
	[Export] public PackedScene TurretScene;
	[Export] public Path3D EnemyPath;

		
	public void BuildTurret(Vector3 turretPosition)
	{
		if (TurretScene != null)
		{
			Turret newTurret = (Turret)TurretScene.Instantiate();
			AddChild(newTurret);
			newTurret.GlobalPosition = turretPosition;
			newTurret.enemyPath = EnemyPath;
		}
		else { GD.Print("Turret scene not found in the TurretManager. You need to set one scene"); }
	}
}
