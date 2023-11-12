using Godot;
using Godot.Collections;
using System;

public partial class BorderPlane : Node3D
{

    Dictionary<string, int> hits = new Dictionary<string, int>();
    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(double delta)
    {
    }

    public void Hit(MeshInstance3D node)
    {
        if (hits.TryGetValue(node.Name, out var value))
        {
            value--;
            hits[node.Name] = value;

            if (value <= 0)
            {
                node.QueueFree();
            }
        }
        else
        {
            hits[node.Name] = 5;

        }

    }
}
