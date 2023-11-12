using Godot;
using System;

public partial class EndlessLevel : DebugLevel
{
    Brick[,] bricksList = new Brick[9, 6];
    private double timer = 1;
    private PackedScene brickScene;
    private Random r;

    public override void _Ready()
    {
        
        brickScene = ResourceLoader.Load<PackedScene>("res://Actors/Brick/Brick.tscn");
        r = new Random();
        base._Ready();
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(double delta)
    {
        base._Process(delta);
        timer -= delta;
        if (timer < 0)
        {
            int count = 5;

            for (int j = 0; j < 6 && count > 0; j++)
            {
                for (int i = 0; i < 9 && count > 0; i++)
                {
                    if (bricksList[i, j].IsFree && r.NextDouble() < 0.1)
                    {
                        var brick = brickScene.Instantiate<Brick>();
                        bricksList[i, j] = brick;
                        brick.Position = new Vector3(-0.25f, j / 2f + 1, i - 4f);
                        bricks.AddChild(brick);
                        count--;
                    }
                }
            }
            timer = 5;
        }
    }

    protected override void FillLevel()
    {


        for (int i = 0; i < 9; i++)
        {
            for (int j = 0; j < 6; j++)
            {
                var brick = brickScene.Instantiate<Brick>();
                bricksList[i, j] = brick;
                brick.Position = new Vector3(-0.25f, j / 2f + 1, i - 4f);
                bricks.AddChild(brick);
            }
        }
    }
}
