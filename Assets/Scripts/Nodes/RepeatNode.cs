using UnityEngine;

public class RepeatNode : DecoratorNode
{
    [Tooltip("If set to 0 will repeat non stop")]
    public int repeatCount;
    public RepeatNode()
    {
        description = "Plays the sequence of connected nodes over and over";
    }
    protected override void OnStart()
    {
        if (repeatCount != 0)
        {
            child.loop = true;
        }
    }

    protected override void OnStop()
    {
        if (loop == true)
        {
            loopCount++;
        }
    }

    protected override State OnUpdate()
    {

        if (child.loop == true)
        {
            if (child.loopCount != repeatCount)
            {
                child.Update();
            }
            else
            {
                child.loop = false;
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