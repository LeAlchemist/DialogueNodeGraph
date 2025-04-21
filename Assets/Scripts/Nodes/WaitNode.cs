using UnityEngine;

public class WaitNode : ActionNode
{
    public float duration = 1;
    float startTime;

    public WaitNode()
    {
        description = "Will wait x amount of time between playing nodes in a sequence";
    }

    protected override void OnStart()
    {
        startTime = Time.time;
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
        if (Time.time - startTime > duration)
        {
            return State.Success;
        }

        return State.Running;
    }
}