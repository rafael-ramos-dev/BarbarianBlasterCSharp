using Godot;
using System;
using System.Linq;

public partial class RayPickerCamera : Camera3D
{
	[Export] public float clickableRangeFromCamera { get; private set; } = 100.0f;

    [ExportGroup("Set In Level Scene")]
	[Export] public GridMap gridMap;
	[Export] public TurretManager turretManager;

	public RayCast3D rayCast3DFromCamera;


    public override void _EnterTree()
    {
        rayCast3DFromCamera = GetChildren().OfType<RayCast3D>().FirstOrDefault(x => x.Name == "MouseRayCast3D");
    }

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		Vector2 mousePosition = GetViewport().GetMousePosition();

		// The multiplication is to get from the camera all the way to the ground.
		// This value changes according to camera distance from the ground in the game
		rayCast3DFromCamera.TargetPosition = ProjectLocalRayNormal(mousePosition) * clickableRangeFromCamera;
		
		// force the raycast to update before the next frame so we can have the information now
		// and not only on the next frame
		rayCast3DFromCamera.ForceRaycastUpdate();

		bool rayIsColliding = rayCast3DFromCamera.IsColliding();
		GodotObject rayGetCollider = rayCast3DFromCamera.GetCollider();

		if (rayIsColliding && rayGetCollider is GridMap)
		{	
			Input.SetDefaultCursorShape(Input.CursorShape.PointingHand);
			if (Input.IsActionJustPressed(GameConstants.LEFTMOUSECLICK))
			{
				Vector3 objectCollisionPoint = rayCast3DFromCamera.GetCollisionPoint();
				Vector3I gridCell = gridMap.LocalToMap(objectCollisionPoint);
				int gridCellIndex = gridMap.GetCellItem(gridCell);
				
				if (gridCellIndex == 0) 
				{
					gridMap.SetCellItem(gridCell, 1);
					Vector3 tilePosition = gridMap.MapToLocal(gridCell);
					turretManager.BuildTurret(tilePosition);
				}
			}
		}
		else Input.SetDefaultCursorShape(Input.CursorShape.Arrow);

	}
}
