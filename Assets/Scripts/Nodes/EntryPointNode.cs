using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;

public class EntryPointNode
{
    public CustomNode _node;
    public CustomNode GenerateEntryPointNode()
    {
        var node = new CustomNode
        {
            title = "Start",
            GUID = Guid.NewGuid().ToString(),
            NodeType = "Entry",
            entryPoint = true
        };

        _node = new CustomNode { };
        var generatedPort = _node.GeneratePort(node, Direction.Output);
        generatedPort.portName = "Next";
        node.outputContainer.Add(generatedPort);

        node.capabilities &= ~Capabilities.Movable;

        node.RefreshExpandedState();
        node.RefreshPorts();

        node.SetPosition(new Rect(x: 100, y: 200, width: 100, height: 150));

        return node;
    }
}
