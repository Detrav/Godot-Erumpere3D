using Erumpere3D.Scripts;
using Godot;
using System;

public partial class MenuPause : Control, IPauseProcessor
{
    private Control ctrlPause;
    private Control ctrlComplete;
    private string nextLevel;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        ctrlPause = GetNode<Control>("ColorRect");
        ctrlComplete = GetNode<Control>("LevelCompleteRect");

    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(double delta)
    {
        var lbl = GetNode<Label>("LabelScore");
        lbl.Text = "Score: " + DetravSingleton.Instance.Score.ToString() + "\n" +
            "Balls: " + DetravSingleton.Instance.Balls;

        if (Input.IsActionJustReleased("move_pause"))
        {
            if (GetTree().Paused)
            {
                HidePause();
            }
            else
            {
                ShowPause();
            }
        }
    }

    public void HidePause()
    {
        if (!ctrlComplete.Visible)
        {
            GetTree().Paused = false;
            ctrlPause.Visible = false;
        }
    }

    public void _on_touch_screen_button_button_released()
    {
        ShowPause();
    }

    public void _on_btn_continue_button_released()
    {
        HidePause();
    }

    public void ShowComplete(string nextLevel)
    {
        if (string.IsNullOrEmpty(nextLevel))
        {
            GetNode<Label>("LevelCompleteRect/CenterContainer/VBoxContainer/Label").Text = "GAME OVER";
            GetNode<Control>("LevelCompleteRect/CenterContainer/VBoxContainer/BtnContinue").Visible = false;
        }
        else
        {
            GetNode<Label>("LevelCompleteRect/CenterContainer/VBoxContainer/Label").Text = "Level complete";
            GetNode<Control>("LevelCompleteRect/CenterContainer/VBoxContainer/BtnContinue").Visible = true;
        }
        GetNode<Label>("LevelCompleteRect/CenterContainer/VBoxContainer/LabelScore").Text = "Total score: " + DetravSingleton.Instance.Score;
        if (DetravSingleton.Instance.BestScore < DetravSingleton.Instance.Score)
        {
            DetravSingleton.Instance.BestScore = DetravSingleton.Instance.Score;
            DetravSingleton.Instance.Save();
        }
        this.nextLevel = nextLevel;
        GetTree().Paused = true;
        ctrlPause.Visible = false;
        ctrlComplete.Visible = true;
    }



    public void _on_btn_exit_button_released()
    {
        GetTree().Quit();
    }

    public void _on_btn_to_main_menu_button_released()
    {
        GetTree().Paused = false;
        GetTree().ChangeSceneToFile("res://Scenes/MainMenu.tscn");
    }

    public void ShowPause()
    {
        if (!ctrlComplete.Visible)
        {
            GetTree().Paused = true;
            ctrlPause.Visible = true;
        }
    }

    public void QuitRequest()
    {
        if (GetTree().Paused || ctrlComplete.Visible)
        {
            GetTree().Quit();
        }
        else
        {
            ShowPause();
        }
    }

    public void _on_btn_continue_next_button_released()
    {
        if (!String.IsNullOrEmpty(nextLevel))
        {
            GetTree().Paused = false;
            GetTree().ChangeSceneToFile(nextLevel);
        }
        else
        {
            GetTree().Paused = false;
            GetTree().ChangeSceneToFile("res://Scenes/MainMenu.tscn");
        }
    }
}
