using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEditor.Search;
using UnityEngine;
using UnityEngine.UIElements;

public class PortraitNode : CustomNode
{
    public object portrait;
    public PortraitNode GenerateNode()
    {
        var node = new PortraitNode
        {
            title = "Portrait Node",
            GUID = Guid.NewGuid().ToString(),
            NodeType = "Character",
            portrait = new object()
        };

        node.styleSheets.Add(Resources.Load<StyleSheet>("Node"));

        var label = new Label();
        label.text = "Image:";
        node.inputContainer.Add(label);

        var imageField = new ObjectField();
        portrait = imageField;
        node.inputContainer.Add(imageField);

        var _image = new Image();
        _image.image = Resources.Load<Texture2D>("Assets/TextMesh Pro/Sprites/EmojiOne.png");
        node.inputContainer.Add(_image);

        var portraitPort = GeneratePort(node, Direction.Output);
        portraitPort.portName = "Portrait";
        node.outputContainer.Add(portraitPort);

        return node;
    }
}
