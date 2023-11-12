using Godot;
using System;


public partial class TouchScreenButton : Control
{
    private NinePatchRect patch;

    [Export]
    public Texture2D Normal { get; set; }
    [Export]
    public Texture2D Pressed { get; set; }


    [Signal]
    public delegate void ButtonPressedEventHandler();
    
    [Signal]
    public delegate void ButtonReleasedEventHandler();

    private int index = -1;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
        
		patch = GetNode<NinePatchRect>("NinePatchRect");
        patch.Texture = Normal;
    }

    public override void _GuiInput(InputEvent @event)
    {
        if(@event is InputEventScreenTouch eventScreenTouch)
        {
            if(index == -1)
            {
                if (eventScreenTouch.Pressed)
                {
                    patch.Texture = Pressed;
                    index = eventScreenTouch.Index;
                    EmitSignal(SignalName.ButtonPressed);
                }
            }
            
        }
    }

    public override void _Input(InputEvent @event)
    {
        if (@event is InputEventScreenTouch eventScreenTouch)
        {
            if (index == eventScreenTouch.Index)
            {
                if (!eventScreenTouch.Pressed)
                {
                    
                    index = -1;
                    patch.Texture = Normal;
                    EmitSignal(SignalName.ButtonReleased);
                }
            }
        }
    }
}
