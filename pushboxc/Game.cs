using Godot;
using System;

public partial class Game : Node2D
{
    public static Action CompleteAction;

    Game()
    {
        CompleteAction += () =>
        {
            foreach (var node1 in GetTree().GetNodesInGroup("Boxs"))
            {
                var node = (Box)node1;
                if (!node._isReached)
                {
                    return;
                }
            }

            GD.Print("Complete");
        };
    }
    
}
