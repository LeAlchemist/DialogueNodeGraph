using System;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class EntryPointNode : CustomNode
{
    public CustomNode GenerateNode()
    {
        var node = new CustomNode
        {
            title = "Start",
            GUID = Guid.NewGuid().ToString(),
            NodeType = "Entry",
            entryPoint = true
        };


        var generatedPort = GeneratePort(node, Direction.Output);
        generatedPort.portName = "Next";
        node.outputContainer.Add(generatedPort);

        node.capabilities &= ~Capabilities.Movable;
        node.capabilities &= ~Capabilities.Deletable;

        node.RefreshExpandedState();
        node.RefreshPorts();

        node.SetPosition(new Rect(x: 100, y: 200, width: 100, height: 150));

        return node;
    }
}
