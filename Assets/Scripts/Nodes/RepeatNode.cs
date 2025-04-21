using UnityEngine;

public class RepeatNode : DecoratorNode
{
    [Tooltip("If set to 0 will repeat non stop")]
    public int repeatCount;
    int timesRepeated;
    public RepeatNode()
    {
        description = "Plays the sequence of connected nodes over and over";
    }
    protected override void OnStart()
    {

    }

    protected override void OnStop()
    {

    }

    protected override State OnUpdate()
    {
        if (repeatCount != 0)
        {
            timesRepeated++;
            child.Update();
            if (timesRepeated == repeatCount)
            {
                return State.Success;
            }
        }
        else
        {
            child.Update();
        }
        return State.Running;
    }
}