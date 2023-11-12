using Godot;
using System;
using System.Threading.Tasks;

public partial class ControlMobile : Control
{

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
	{
		//Visible = DisplayServer.Singleton.IsTouchscreenAvailable();

    }

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{

		
	}

	public void ShowStartButton()
	{
		var btn = GetNode<Control>("ButtonStart");
		btn.Visible = true;
	}

	public void _on_button_left_button_down()
	{
        Input.ActionPress("move_left");
    }

	public void _on_button_left_button_up()
	{
		Input.ActionRelease("move_left");
	}
	
	public void _on_button_right_button_down()
	{
        Input.ActionPress("move_right");
    }

	public void _on_button_right_button_up()
	{
		Input.ActionRelease("move_right");
	}

    public void _on_button_start_button_down()
	{
        Input.ActionPress("move_fire");
    }   
	
	public void _on_button_start_button_up()
	{
        Input.ActionRelease("move_fire");
    }

    public void _on_button_start_pressed()
	{
        //Input.ActionPress("move_fire");
        //Input.ActionRelease("move_fire");

    }

    internal void HideStartButton()
    {
        var btn = GetNode<Control>("ButtonStart");
        btn.Visible = false;
    }
}
