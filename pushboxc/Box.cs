using Godot;
using System;
using System.Collections.Generic;

public partial class Box : BaseObject
{
    public bool _isReached = false;
    private AnimationPlayer _animationPlayer;

    public override void _Ready()
    {
        _map = GetParent<TileMapLayer>();
        _cellPosition = _map.LocalToMap(Position);
        _animationPlayer = GetNode<AnimationPlayer>("AnimationPlayer");
    }

    public void Set(bool value)
    {
        if (_isReached == value)
            return;

        _isReached = value;
        _animationPlayer.Play(_isReached ? "reached" : "default");
        if (_isReached)
        {
            Game.CompleteAction?.Invoke();
        }
    }

    public new void MoveTo(Vector2I cell)
    {
        base.MoveTo(cell);
        Set(IsDest(cell)); 
    }
}
