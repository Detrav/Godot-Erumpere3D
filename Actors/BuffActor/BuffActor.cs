using Erumpere3D.Scripts;
using Godot;
using System;
using System.Linq;

public partial class BuffActor : CharacterBody3D
{

    // Get the gravity from the project settings to be synced with RigidBody nodes.
    public float gravity = ProjectSettings.GetSetting("physics/3d/default_gravity").AsSingle();
    private Vector3 velocity = Vector3.Down * 3;
    private static Random random = new Random();
    public BuffType BuffType { get; set; }

    public override void _Ready()
    {
        base._Ready();


        var vals = Enum.GetValues<BuffType>();

        if (BuffType == BuffType.None)
        {
            BuffType = vals[random.Next(vals.Length - 1) + 1];
        }
        switch (BuffType)
        {
            case BuffType.MinusBall:
                GetNode<Node3D>("MinusBall").Visible = true;
                break;
            case BuffType.PlusBull:
                GetNode<Node3D>("PlusBall").Visible = true;
                break;
            case BuffType.PlusPlatform:
                GetNode<Node3D>("PlusPlatform").Visible = true;
                break;
            case BuffType.MinusPlatform:
                GetNode<Node3D>("MinusPlatform").Visible = true;
                break;
            case BuffType.SplitBall:
                GetNode<Node3D>("SplitBall").Visible = true;
                break;

            case BuffType.AddLife:
                GetNode<Node3D>("AddLife").Visible = true;
                break;
        }

    }

    public override void _PhysicsProcess(double delta)
    {

        //velocity.Y -= gravity * (float)delta;
        var hit = MoveAndCollide(velocity * (float)delta);

        if (hit != null)
        {
            if (hit.GetCollider() is Platform)
            {
                switch (BuffType)
                {
                    case BuffType.MinusBall:
                        DetravSingleton.Instance.BallSpeed--;
                        break;
                    case BuffType.PlusBull:
                        DetravSingleton.Instance.BallSpeed++;
                        break;
                    case BuffType.PlusPlatform:
                        DetravSingleton.Instance.PlatformSize++;
                        break;
                    case BuffType.MinusPlatform:
                        DetravSingleton.Instance.PlatformSize--;
                        break;
                    case BuffType.SplitBall:

                        var ballScene = ResourceLoader.Load<PackedScene>("res://Actors/Ball/Ball.tscn");

                        GD.Print("t: " + GetTree().Root.FindAllNode<Ball>().ToArray().Count());

                        foreach (var ball in GetTree().Root.FindAllNode<Ball>().ToArray())
                        {
                            var newBall = ballScene.Instantiate<Ball>();
                            ball.SpawnNear(newBall);
                        }

                        break;
                    case BuffType.AddLife:
                        DetravSingleton.Instance.Balls++;
                        break;
                }
            }
            QueueFree();
        }

        if (Position.Y < -100)
        {
            QueueFree();
        }
    }

    public static BuffType GenerateBuffType()
    {
        double weight = BuffTypes.Sum(m => m.weight);
        double cache = random.NextDouble() * weight;
        double currentChance = 0;

        foreach (var kv in BuffTypes)
        {
            currentChance += kv.weight;
            if (cache < currentChance)
            {
                return kv.type;
            }
        }

        return BuffType.None;
    }

    private static (BuffType type, double weight)[] BuffTypes = new (BuffType, double)[]
    {
        (BuffType.None, 100),
        (BuffType.PlusBull, 20),
        (BuffType.MinusBall, 20),
        (BuffType.PlusPlatform, 20),
        (BuffType.MinusPlatform, 20),
        (BuffType.SplitBall, 20),
        (BuffType.AddLife, 5),
    };
}

public enum BuffType
{
    None,
    PlusBull,
    MinusBall,
    PlusPlatform,
    MinusPlatform,
    SplitBall,
    AddLife,
}
