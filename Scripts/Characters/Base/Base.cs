using Godot;
using System;
using System.Linq;

public partial class Base : Node3D
{
  [Export] int MaxHealth = 100;

  private Label3D healthLabel;

  // Ensure it runs before the _Ready method
  public override void _EnterTree()
  {
    foreach(Node node in GetTree().GetNodesInGroup(GameConstants.BASE))
    {
      node.RemoveFromGroup(GameConstants.BASE);
    }

	  this.AddToGroup(GameConstants.BASE);

    healthLabel = GetChildren().OfType<Label3D>().FirstOrDefault(x => x.Name == "HealthLabel");        
  }


  public override void _Ready()
  {
    healthLabel.Text = MaxHealth.ToString();
  }


  public void TakeDamage()
  {
    MaxHealth -= 1;
    healthLabel.Text = MaxHealth.ToString();

    GD.Print("Damage Dealt To Base!");
  }
}
