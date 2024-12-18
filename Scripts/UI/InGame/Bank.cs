using Godot;
using System;

public partial class Bank : MarginContainer
{
    [Export] public Label goldLabel;
    [Export] public int StartingGold = 150;

    private int _currentGold;
    public int CurrentGold
    {
        get => _currentGold;
        set
        {
            _currentGold = (int)Mathf.Clamp(value, 0, Mathf.Inf);

            if (goldLabel.Text != null) 
                { goldLabel.Text = "Gold: " + _currentGold.ToString(); }
        }
    }


    public override void _EnterTree()
    {
        foreach(Node node in GetTree().GetNodesInGroup(GameConstants.BANK))
            { node.RemoveFromGroup(GameConstants.BANK); }

        this.AddToGroup(GameConstants.BANK);
    }


    public override void _Ready()
    {
        CurrentGold = StartingGold;
    }
}
