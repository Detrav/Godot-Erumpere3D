using Erumpere3D.Scripts;
using Godot;
using System;

public partial class Ball : CharacterBody3D
{


    private Vector3 direction;
    private double timer;
    private bool snap;
    private Platform platform;
    private AudioStreamPlayer hitSound;
    static Random r = new Random();


    // Get the gravity from the project settings to be synced with RigidBody nodes.

    public override void _Ready()
    {
        hitSound = GetNode<AudioStreamPlayer>("AudioStreamPlayer");
        snap = true;
        platform = GetTree().Root.FindNode<Platform>();
        Position = platform.Position + new Vector3(0, 0.3f, 0);
    }



    public void StartMove()
    {
        if (snap)
        {
            var m = GetTree().Root.FindNode<ControlMobile>();
            m.HideStartButton();

            snap = false;
            direction = new Vector3(
                0,
                (float)(1 + r.NextDouble()),
                (float)(1 + r.NextDouble())).Normalized();
        }
    }

    public override void _PhysicsProcess(double delta)
    {
        if (snap)
        {
            Position = platform.Position + new Vector3(0, 0.3f, 0);
            return;

        }
        Vector3 velocity;

        velocity = direction * DetravSingleton.Instance.BallSpeed * (float)delta;

        var hit = MoveAndCollide(velocity);

        if (hit != null)
        {
            hitSound.Play();
            direction = direction.Bounce(hit.GetNormal());

            if (direction.X != 0)
            {
                direction.X = 0;
                direction = direction.Normalized();
            }

            if (Math.Abs(direction.Y) < 0.01)
            {
                direction.Y = 1;
                direction = direction.Normalized();
            }
            else if (Math.Abs(direction.Y) < 0.3)
            {
                direction.Y = Mathf.Sign(direction.Y);
                direction = direction.Normalized();
            }

            if (hit.GetCollider() is Brick brick)
            {
                DetravSingleton.Instance.Score += 10;
                brick.Hit();
            }
            else if (hit.GetCollider() is Platform platform)
            {
                DetravSingleton.Instance.Score += 1;
                if (direction.Y < 0)
                {
                    direction.Y = -direction.Y;
                }
                CollisionMask = CollisionMask & ~2u;
                timer = 0.2;
            }
            else if (hit.GetCollider() is StaticBody3D borderPlaneMeshStatic && borderPlaneMeshStatic.GetParent() is MeshInstance3D borderPlaneMesh && borderPlaneMesh.GetParent() is BorderPlane borderPlane)
            {
                borderPlane.Hit(borderPlaneMesh);
            }
            else if (hit.GetCollider() is FallBorder border)
            {
                QueueFree();
            }
            else
            {
                DetravSingleton.Instance.Score += 1;
            }
        }

        timer -= delta;

        if (timer < 0)
        {
            CollisionMask = CollisionMask | 2u;
        }

    }

    public void SpawnNear(Ball ball)
    {
        GetParent().AddChild(ball);
        ball.Position = Position;
        ball.snap = snap;
        ball.direction = new Vector3(
                0,
                (float)(1 + r.NextDouble()),
                (float)(1 + r.NextDouble())).Normalized(); ;
    }
}
