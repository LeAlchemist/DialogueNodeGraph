using UnityEngine;

public abstract class Node : ScriptableObject
{
    public enum State
    {
        Running,
        Failure,
        Success
    }

    [HideInInspector]
    public State state = State.Running;
    [HideInInspector]
    public bool started = false;
    [HideInInspector]
    public string guid;
    [HideInInspector]
    public bool loop;
    [HideInInspector]
    public int loopCount;
    [HideInInspector]
    public Vector2 position;
    [HideInInspector]
    public string description;

    public State Update()
    {
        if (!started)
        {
            OnStart();
            started = true;
        }

        state = OnUpdate();

        if (state == State.Failure || state == State.Success)
        {
            OnStop();
            started = false;
        }

        return state;
    }

    protected abstract void OnStart();
    protected abstract void OnStop();
    protected abstract State OnUpdate();

    public virtual Node Clone()
    {
        return Instantiate(this);
    }
}
