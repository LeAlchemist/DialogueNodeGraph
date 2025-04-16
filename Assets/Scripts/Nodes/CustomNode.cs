using System.Linq;
using UnityEditor.Experimental.GraphView;
using UnityEngine.UIElements;

public class CustomNode : Node
{
    public string GUID;
    public string NodeType;
    //Notates if the node is an entry point
    public bool entryPoint = false;
    //public string dialogueText;
    public DialogueGraphView _graphView;

    public Port GeneratePort(CustomNode node, Direction portDirection, Port.Capacity capacity = Port.Capacity.Single)
    {
        return node.InstantiatePort(Orientation.Horizontal, portDirection, capacity, typeof(float));
    }

    public void AddChoicePort(CustomNode node, string overridenPortName = "")
    {
        var generatedPort = GeneratePort(node, Direction.Output, Port.Capacity.Single);

        var oldLabel = generatedPort.contentContainer.Q<Label>("type");
        generatedPort.contentContainer.Remove(oldLabel);

        var outputPortCount = node.outputContainer.Query("connector").ToList().Count;

        var choicePortName = string.IsNullOrEmpty(overridenPortName)
            ? $"Choice {outputPortCount}"
            : overridenPortName;

        var textField = new TextField
        {
            name = string.Empty,
            value = choicePortName
        };
        textField.RegisterValueChangedCallback(evt => generatedPort.portName = evt.newValue);
        generatedPort.contentContainer.Add(new Label("  "));
        generatedPort.contentContainer.Add(textField);

        var deleteButton = new Button(() => RemovePort(node, generatedPort))
        {
            text = "X"
        };
        generatedPort.contentContainer.Add(deleteButton);

        generatedPort.portName = choicePortName;
        node.outputContainer.Add(generatedPort);
        node.RefreshPorts();
        node.RefreshExpandedState();
    }

    private void RemovePort(CustomNode node, Port generatedPort)
    {
        var targetEdge = _graphView.edges.ToList().Where(x =>
           x.output.portName == generatedPort.portName && x.output.node == generatedPort.node);

        if (targetEdge.Any())
        {
            var edge = targetEdge.First();
            edge.input.Disconnect(edge);
            //RemoveElement(targetEdge.First());
        }

        node.outputContainer.Remove(generatedPort);
        node.RefreshPorts();
        node.RefreshExpandedState();
    }
}
