using System.Collections.Generic;

public class ChoiceNode : CompositeNode
{
    public List<string> choices = new();
    public int selection;
    ChoiceNode()
    {
        description = "This or that";
    }
    protected override void OnStart()
    {

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
        var child = children[selection];
        switch (child.Update())
        {
            case State.Running:
                return State.Running;
            case State.Failure:
                return State.Failure;
            case State.Success:
                break;
        }
        return State.Success;
    }
}