using Erumpere3D.Scripts;
using Godot;
using System;

public partial class Platform : CharacterBody3D
{
    public const float Speed = 8.0f;

    public override void _PhysicsProcess(double delta)
    {
        Vector3 velocity = new Vector3();

        Scale = Vector3.One * DetravSingleton.Instance.PlatformSizes[DetravSingleton.Instance.PlatformSize];

        float inputDir = Input.GetAxis("move_left", "move_right");
        velocity.Z = -(float)(inputDir * Speed * delta);


        //GD.Print(velocity);
        MoveAndCollide(velocity);
    }
}
