using Godot;
using System;

public partial class LevelButton : TouchScreenButton
{

    public int Level { get; set; }
    public string LevelPath { get; set; }
    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        base._Ready();
        var lbl = GetNode<Label>("Label");
        lbl.Text = Level.ToString();
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(double delta)
    {
    }

    public void _on_button_released()
    {
        GetTree().ChangeSceneToFile(LevelPath);
    }
}
