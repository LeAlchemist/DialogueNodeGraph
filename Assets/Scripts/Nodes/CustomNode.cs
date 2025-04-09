using UnityEditor.Experimental.GraphView;

public class CustomNode : Node
{
    public string GUID;
    public string NodeType;
    public string dialogueText;
    public bool entryPoint = false;

    public Port GeneratePort(CustomNode node, Direction portDirection, Port.Capacity capacity = Port.Capacity.Single)
    {
        return node.InstantiatePort(Orientation.Horizontal, portDirection, capacity, typeof(float));
    }
}
