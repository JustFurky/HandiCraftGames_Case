using UnityEngine;

public interface IMovementAttach
{
    public void Attach(Vector2 TargetPosition);
}
public interface IStackable
{
    public void Stack(bool isPlayer);
    public void Initialize(bool isRed, Vector3 position);
}
public interface IGate
{
    public void ScoreCheck();
}
public interface IReplaceable
{
    public void ReplaceCheck();
    public void Replace();
}
