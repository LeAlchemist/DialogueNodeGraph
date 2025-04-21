public class SequencerNode : CompositeNode
{
    int current;
    public SequencerNode()
    {
        description = "Plays connected nodes in the order they are connected";
    }

    protected override void OnStart()
    {
        current = 0;

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
        var child = children[current];
        switch (child.Update())
        {
            case State.Running:
                return State.Running;
            case State.Failure:
                return State.Failure;
            case State.Success:
                current++;
                break;
        }

        return current == children.Count ? State.Success : State.Running;
    }
}