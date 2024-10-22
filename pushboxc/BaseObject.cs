using Godot;
using System;

public partial class BaseObject : Node2D
{
    public Tween _tween; // Declare a variable for Tween
    public TileMapLayer _map; // Declare a variable for TileMapLayer
    public Vector2I _cellPosition; // Declare a variable for Vector2i

    public void MoveTo(Vector2I cell)
    {
        _cellPosition = cell;
        _tween?.Kill();
        _tween = CreateTween();
        _tween.SetEase(Tween.EaseType.InOut).SetTrans(Tween.TransitionType.Sine);
        _tween.TweenProperty(this, "position", _map.MapToLocal(cell), 0.2f);
    }
    public void Bump(Vector2I cell)
    {
        if (_tween != null)
        {
            _tween.Kill();
        }
        _tween = CreateTween();
        _tween.SetEase(Tween.EaseType.InOut).SetTrans(Tween.TransitionType.Sine);
        _tween.TweenProperty(this, "position", (_map.MapToLocal(cell) + Position) / 2, 0.2f);
        _tween.TweenProperty(this, "position", Position, 0.2f);
    }
    public bool IsWall(Vector2I cell)
    {
        var data = _map.GetCellTileData(cell);
        
        if (data == null)
        {
            return false;
        }
        return (bool)data.GetCustomData("isWall");
    }
    public Box GetCreate(Vector2I cell)
    {
        foreach (Box box in GetTree().GetNodesInGroup("Boxs"))
        {
            if (box._cellPosition == cell)
            {
                return box;
            }
        }
        return null;
    }

    public bool IsDest(Vector2I cell)
    {
        var data = _map.GetCellTileData(cell);
        if (data == null)
        {
            return false;
        }
        return (bool)data.GetCustomData("isDest");
    }



}
