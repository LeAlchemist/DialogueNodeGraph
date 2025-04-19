using UnityEngine;

public class RootNode : Node
{
    [HideInInspector]
    public Node child;

    public RootNode()
    {
        description = "This is where it all starts";
    }
    protected override void OnStart()
    {

    }

    protected override void OnStop()
    {

    }

    protected override State OnUpdate()
    {
        return child.Update();
    }

    public override Node Clone()
    {
        RootNode node = Instantiate(this);
        node.child = child.Clone();
        return node;
    }
}
