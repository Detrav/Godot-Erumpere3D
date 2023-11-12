using Godot;
using System;

public partial class Brick : StaticBody3D
{

    [Export]
    public int Hits { get; set; } = 1;

    [Export]
    public BuffType BuffType { get; set; }

    private PackedScene buffScene = ResourceLoader.Load<PackedScene>("res://Actors/BuffActor/BuffActor.tscn");
    private static Random r = new Random();
    private MeshInstance3D mesh;

    [Export]
    public PackedScene Particles { get; set; }
    public bool IsFree { get; set; }
    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        mesh = GetNode<MeshInstance3D>("Brick/Brick_001");
        UpdateColor();
        PlaySpawn();
    }


    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(double delta)
    {

    }

    public void PlaySpawn()
    {
        var ap = GetNode<AnimationPlayer>("AnimationPlayer");
        ap.Play("SpawnAnimation");
    }

    public void UpdateColor()
    {
        var mat = mesh.GetSurfaceOverrideMaterial(0) as StandardMaterial3D;
        switch (Hits)
        {
            case 1:
                mat.AlbedoColor = new Color(0.65f, 0.65f, 0.65f);
                break;

            case 2:
                mat.AlbedoColor = Colors.DarkRed;
                break;

            case 3:
                mat.AlbedoColor = Colors.DarkGreen;
                break;
        }
    }


    public void Hit()
    {
        Hits--;
        UpdateColor();
        if (Hits <= 0)
        {

            if (Particles != null)
            {
                var p = Particles.Instantiate<BrickBreakParticles>();
                GetParent().AddChild(p);
                p.Position = Position;
            }
            var buffType = BuffType;
            if (buffType ==  BuffType.None)
            {
                buffType = BuffActor.GenerateBuffType();
            }
            if (buffType != BuffType.None)
            {

                var buff = buffScene.Instantiate<BuffActor>();
                buff.BuffType = BuffType;
                buff.Position = Position;
                GetParent().AddChild(buff);
            }

            IsFree = true;
            QueueFree();
        }
    }
}
