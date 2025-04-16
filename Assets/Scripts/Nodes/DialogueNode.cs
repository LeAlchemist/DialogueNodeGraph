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

public class DialogueNode : CustomNode
{
    public string dialogueText;
    public DialogueNode GenerateNode()
    {
        var node = new DialogueNode
        {
            title = "Dialogue Node",
            GUID = Guid.NewGuid().ToString(),
            NodeType = "Dialogue",
            dialogueText = ""
        };

        node.styleSheets.Add(Resources.Load<StyleSheet>("Node"));

        //input port
        var inputPort = GeneratePort(node, Direction.Input, Port.Capacity.Multi);
        inputPort.portName = "Input";
        node.inputContainer.Add(inputPort);

        var characterPort = GeneratePort(node, Direction.Input);
        characterPort.portName = "Character";
        node.inputContainer.Add(characterPort);

        //Add choices
        var button = new Button(() => { AddChoicePort(node); });
        button.text = "New Choice";
        node.titleContainer.Add(button);

        var label = new Label();
        label.text = "Dialogue:";
        node.mainContainer.Add(label);

        var textField = new TextField(string.Empty);
        textField.multiline = true;
        textField.maxLength = 150;
        textField.RegisterValueChangedCallback(evt =>
        {
            node.dialogueText = evt.newValue;
        });
        textField.SetValueWithoutNotify(node.dialogueText);
        node.mainContainer.Add(textField);

        node.RefreshExpandedState();
        node.RefreshPorts();

        node.SetPosition(new Rect(x: 100, y: 200, width: 100, height: 150));

        return node;
    }
}
