using System;
public abstract class State
{
    public virtual void OnEnter() { }
    public virtual void OnExit() { }
    public abstract State OnUpdate(PlayerInputHandler playerInputHandler);
}
