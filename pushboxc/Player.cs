using Godot;
using System;

public partial class Player : BaseObject
{
    private AnimationPlayer animationPlayer;

    public override void _Ready()
    {
        _map = GetParent<TileMapLayer>();
        _cellPosition = _map.LocalToMap(Position);
        animationPlayer = GetNode<AnimationPlayer>("AnimationPlayer");
    }
    public override void _Process(double delta)
    {
        if (_tween != null && _tween.IsRunning())
            return;
        var temp= Input.GetVector("left", "right", "up", "down").Normalized();
        Vector2I dir = new Vector2I((int)temp.X, (int)temp.Y);

        if (dir == Vector2.Zero)
            return;

        if (dir.X != 0)
            dir.Y = 0;

        Vector2I dest = _cellPosition + dir;
        if (Input.IsActionPressed("left"))
            animationPlayer.Play("left");
        else if (Input.IsActionPressed("right"))
            animationPlayer.Play("right");
        else if (Input.IsActionPressed("up"))
            animationPlayer.Play("up");
        else if (Input.IsActionPressed("down"))
            animationPlayer.Play("down");

        if (IsWall(dest))
        {
            Bump(dest);
            return;
        }

        var crate = GetCreate(dest);
        if (crate != null)
        {
            Vector2I crateDest = dest + dir;

            if (IsWall(crateDest) || GetCreate(crateDest) != null)
            {
                Bump(dest);
                return;
            }
            crate.MoveTo(crateDest);
        }

        MoveTo(dest);
    }
}
