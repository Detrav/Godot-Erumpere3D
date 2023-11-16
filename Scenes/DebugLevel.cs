using Erumpere3D.Scripts;
using Godot;
using System;

public partial class DebugLevel : Node3D
{
    [Export(PropertyHint.File, "*.tscn")]
    public string NextLevel { get; set; }

    private Ball lastBall;
    protected Node bricks;
    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        bricks = GetNode<Node>("Bricks");
        FillLevel();

        DetravSingleton.Instance.RestartLevel();
        SpawnBall();
    }

    protected virtual void FillLevel()
    {
        var brickScene = ResourceLoader.Load<PackedScene>("res://Actors/Brick/Brick.tscn");

        for (int i = 0; i < 9; i++)
        {
            for (int j = 0; j < 6; j++)
            {
                var brick = brickScene.Instantiate<Brick>();
                brick.Position = new Vector3(-0.25f, j / 2f + 1, i - 4f);
                bricks.AddChild(brick);
            }
        }
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(double delta)
    {

        if (!IsInstanceValid(lastBall))
        {
            lastBall = this.FindNode<Ball>();
            if (!IsInstanceValid(lastBall))
            {
                DetravSingleton.Instance.Balls--;

                if (DetravSingleton.Instance.Balls <= 0)
                {
                    GetTree().Root.FindNode<MenuPause>().ShowComplete(null);
                }
                else
                {

                    SpawnBall();
                    var m = GetTree().Root.FindNode<ControlMobile>();
                    m.ShowStartButton();
                }
            }
        }

        if (Input.IsActionPressed("move_fire"))
        {
            foreach (var ball in this.FindAllNode<Ball>())
            {
                ball.StartMove();
            }
        }
    }

    protected virtual void CompleteLevel()
    {

    }

    public void SpawnBall()
    {
        var ballScene = ResourceLoader.Load<PackedScene>("res://Actors/Ball/Ball.tscn");
        var ball = ballScene.Instantiate<Ball>();
        this.AddChild(ball);
    }
}
