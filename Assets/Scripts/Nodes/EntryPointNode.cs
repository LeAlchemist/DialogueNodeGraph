using System;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;

public class EntryPointNode : CustomNode
{
    public EntryPointNode GenerateNode()
    {
        var node = new EntryPointNode
        {
            title = "Start",
            GUID = Guid.NewGuid().ToString(),
            NodeType = "Entry",
            entryPoint = true
        };

        node.styleSheets.Add(Resources.Load<StyleSheet>("Node"));

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
