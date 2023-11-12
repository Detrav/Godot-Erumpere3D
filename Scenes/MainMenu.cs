using Erumpere3D.Scripts;
using Godot;
using System;
using static System.Formats.Asn1.AsnWriter;

public partial class MainMenu : Control
{
    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        GetTree().Paused = false;

        DetravSingleton.Instance.Score = 0;
        DetravSingleton.Instance.Balls = 3;
        DetravSingleton.Instance.RestartLevel();

        foreach (var lvl in DetravSingleton.Instance.Levels)
        {
            AddLevel(lvl.Item1, lvl.Item2);
        }

        GetNode<Label>("ColorRect/CenterContainer/VBoxContainer/Label3").Text = "Best score: " + DetravSingleton.Instance.BestScore;
    }

    private void AddLevel(int i, string path)
    {
        var grid = GetNode<Control>("ColorRect/CenterContainerLevels/VBoxContainer/GridContainer");

        var btnScene = ResourceLoader.Load<PackedScene>("res://Actors/TouchScreenButton/LevelButton.tscn");
        var btn = btnScene.Instantiate<LevelButton>();
        btn.Level = i;
        btn.LevelPath = path;

        grid.AddChild(btn);
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(double delta)
    {
    }

    public void _on_btn_endless_mode_button_released()
    {
        GetTree().ChangeSceneToFile("res://Scenes/Levels/EndlessLevel.tscn");
    }

    public void _on_btn_exit_button_released()
    {
        GetTree().Quit();
    }

    public void _on_btn_back_button_released()
    {
        var c1 = GetNode<Control>("ColorRect/CenterContainerLevels");
        c1.Visible = false;
        var c0 = GetNode<Control>("ColorRect/CenterContainer");
        c0.Visible = true;
    }

    public void _on_btn_start_button_released()
    {
        var c1 = GetNode<Control>("ColorRect/CenterContainerLevels");
        c1.Visible = true;
        var c0 = GetNode<Control>("ColorRect/CenterContainer");
        c0.Visible = false;
    }
}