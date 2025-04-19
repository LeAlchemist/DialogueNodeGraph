public class RepeatNode : DecoratorNode
{
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
        child.Update();
        return State.Running;
    }
}