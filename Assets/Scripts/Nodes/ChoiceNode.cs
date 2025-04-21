public class ChoiceNode : CompositeNode
{
    public string[] choices;
    ChoiceNode()
    {
        description = "This or that";
    }
    protected override void OnStart()
    {

    }

    protected override void OnStop()
    {

    }

    protected override State OnUpdate()
    {
        return State.Success;
    }
}