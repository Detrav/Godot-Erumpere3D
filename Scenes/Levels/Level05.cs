using Erumpere3D.Scripts;
using Godot;
using System;

public partial class Level05 : DebugLevel
{



    public override void _Ready()
    {
        base._Ready();
        //SpawnBall();
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(double delta)
    {
        base._Process(delta);
        if (bricks != null)
        {
            if (bricks.GetChildCount() <= 5)
            {
                foreach (var ball in this.FindAllNode<Ball>())
                {
                    GetTree().Root.FindNode<MenuPause>().ShowComplete(NextLevel);
                    bricks = null;
                }
            }
        }
    }

    protected override void FillLevel()
    {

        //var brickScene = ResourceLoader.Load<PackedScene>("res://Actors/Brick/Brick.tscn");

        //for (int i = 0; i < 9; i++)
        //{
        //    for (int j = 0; j < 6; j++)
        //    {
        //        var brick = brickScene.Instantiate<Brick>();
        //        brick.Position = new Vector3(-0.25f, j / 2f + 1, i - 4f);
        //        bricks.AddChild(brick);
        //    }
        //}
    }
}
