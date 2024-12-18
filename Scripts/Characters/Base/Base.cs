using Godot;
using System;
using System.Linq;

public partial class Base : Node3D
{
  [Export] public int MaxHealth { get; private set; } = 100;

  private Label3D _healthLabel;
  private int _damage = 1;

  private int _currentHealth;
  private int CurrentHealth
  {
    get => _currentHealth;
    set
    {
      // To Update the internal value
      _currentHealth = value;

      if (_healthLabel.Text != null)
      {
        _healthLabel.Text = _currentHealth.ToString() + "/" + MaxHealth.ToString();

        Color labelRed = Colors.Red;
        Color labelWhite = Colors.White;
        
        _healthLabel.Modulate = labelRed.Lerp(labelWhite, (float)_currentHealth / (float)MaxHealth);
      }

      if (_currentHealth < 1) { GetTree().ReloadCurrentScene(); }
    }
  }

  // Ensure it runs before the _Ready method
  public override void _EnterTree()
  {
    foreach(Node node in GetTree().GetNodesInGroup(GameConstants.BASE))
      { node.RemoveFromGroup(GameConstants.BASE); }

	  this.AddToGroup(GameConstants.BASE);

    _healthLabel = GetChildren().OfType<Label3D>().FirstOrDefault(x => x.Name == "HealthLabel");        
  }


  public override void _Ready()
  {
    CurrentHealth = MaxHealth;
  }


  public void TakeDamage()
  {
    // Reduce health using the setter
    CurrentHealth -= _damage;
  }
}
