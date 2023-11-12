using Erumpere3D.Scripts;
using Godot;
using System;

public partial class Camera3dKeepAspect : Camera3D
{
    private Vector2 projectResolution;
	private Ball ball;
	[Export]
	public NodePath Ball { get; set; }
	private Vector3 lastTarget = new Vector3();

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
	{
		projectResolution = new Vector2((float)ProjectSettings.GetSetting("display/window/size/viewport_width"), (float)ProjectSettings.GetSetting("display/window/size/viewport_height"));
		
		//ball = GetNode<Ball>(Ball);
    }

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		var size = GetViewport().GetVisibleRect().Size;
		if (size.X > projectResolution.X)
			KeepAspect = KeepAspectEnum.Height;
        else if (size.Y > projectResolution.Y)
            KeepAspect = KeepAspectEnum.Width;

		Vector3 target = new Vector3();

		if (IsInstanceValid(ball))
		{
			target = ball.Transform.Origin / 10;
		}
		else
		{
            ball = GetTree().Root.FindNode<Ball>();
        }

		target = lastTarget.Lerp(target, (float)delta * 5);
        LookAt(target, Vector3.Up);

        lastTarget = target;
    }
}
