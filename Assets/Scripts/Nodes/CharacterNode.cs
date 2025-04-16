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

public class CharacterNode : CustomNode
{
    public string characterName;
    public CharacterNode GenerateNode()
    {
        var node = new CharacterNode
        {
            title = "Character Node",
            GUID = Guid.NewGuid().ToString(),
            NodeType = "Character",
            characterName = ""
        };

        node.styleSheets.Add(Resources.Load<StyleSheet>("Node"));

        var nameTextField = new TextField(string.Empty);
        nameTextField.multiline = true;
        nameTextField.maxLength = 150;
        nameTextField.RegisterValueChangedCallback(evt =>
        {
            node.characterName = evt.newValue;
            node.title = node.characterName;
        });
        nameTextField.SetValueWithoutNotify(node.characterName);
        node.inputContainer.Add(nameTextField);

        var portraitPort = GeneratePort(node, Direction.Input, Port.Capacity.Multi);
        portraitPort.portName = "Portrait";
        node.inputContainer.Add(portraitPort);

        var posePort = GeneratePort(node, Direction.Input, Port.Capacity.Multi);
        posePort.portName = "Pose";
        node.inputContainer.Add(posePort);

        var characterPort = GeneratePort(node, Direction.Output);
        characterPort.portName = "Character";
        node.outputContainer.Add(characterPort);

        return node;
    }
}
