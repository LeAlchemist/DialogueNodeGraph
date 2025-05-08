using System;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;

public class NodeView : UnityEditor.Experimental.GraphView.Node
{
    public Action<NodeView> OnNodeSelected;
    public Node node;
    public Port input;
    public Port output;

    public NodeView(Node node)
    {
        this.node = node;

        if (node._name == "")
        {
            this.title = node.name.Replace("Node", "");
        }
        else
        {
            this.title = node._name;
        }

        this.viewDataKey = node.guid;

        style.left = node.position.x;
        style.top = node.position.y;
        style.minWidth = node.scale.x;
        style.maxWidth = node.scale.x;
        style.maxHeight = node.scale.y;

        //Import USS
        var styleSheet = AssetDatabase.LoadAssetAtPath<StyleSheet>("Assets/Scripts/Editor/Node.uss");
        styleSheets.Add(styleSheet);


        switch (node)
        {
            case RootNode:
                capabilities -= Capabilities.Selectable;
                capabilities -= Capabilities.Deletable;
                break;
        }

        SetDescription();
        CreateInputPorts();
        CreateOutputPorts();
        SetupClasses();
    }

    private void SetupClasses()
    {
        switch (node)
        {
            case ActionNode:
                AddToClassList("action");
                AddToClassList(this.title);
                break;
            case CompositeNode:
                AddToClassList("composite");
                AddToClassList(this.title);
                break;
            case DecoratorNode:
                AddToClassList("decorator");
                AddToClassList(this.title);
                break;
            case RootNode:
                AddToClassList("root");
                AddToClassList(this.title);
                break;
        }
    }

    private void SetDescription()
    {
        var description = new Label(node.description);
        mainContainer.Insert(1, description);
    }

    private void CreateInputPorts()
    {
        switch (node)
        {
            case DialogueNode:
                input = InstantiatePort(Orientation.Horizontal, Direction.Input, Port.Capacity.Multi, typeof(bool));
                break;
            case ActionNode:
                input = InstantiatePort(Orientation.Horizontal, Direction.Input, Port.Capacity.Single, typeof(bool));
                break;
            case SequencerNode:
                input = InstantiatePort(Orientation.Horizontal, Direction.Input, Port.Capacity.Multi, typeof(bool));
                break;
            case ChoiceNode:
                input = InstantiatePort(Orientation.Horizontal, Direction.Input, Port.Capacity.Multi, typeof(bool));
                break;
            case CompositeNode:
                input = InstantiatePort(Orientation.Horizontal, Direction.Input, Port.Capacity.Single, typeof(bool));
                break;
            case DecoratorNode:
                input = InstantiatePort(Orientation.Horizontal, Direction.Input, Port.Capacity.Single, typeof(bool));
                break;
            case RootNode:
                break;
        }

        if (input != null)
        {
            input.portName = "Input";
            inputContainer.Add(input);
        }
    }

    private void CreateOutputPorts()
    {
        switch (node)
        {
            case ActionNode:
                break;
            case CompositeNode:
                output = InstantiatePort(Orientation.Horizontal, Direction.Output, Port.Capacity.Multi, typeof(bool));
                break;
            case DecoratorNode:
                output = InstantiatePort(Orientation.Horizontal, Direction.Output, Port.Capacity.Single, typeof(bool));
                break;
            case RootNode:
                output = InstantiatePort(Orientation.Horizontal, Direction.Output, Port.Capacity.Single, typeof(bool));
                break;
        }

        if (output != null)
        {
            output.portName = "Output";
            outputContainer.Add(output);
        }
    }

    public override void SetPosition(Rect newPos)
    {
        base.SetPosition(newPos);
        Undo.RecordObject(node, "Dialogue Graph (Set Position)");
        node.position.x = newPos.xMin;
        node.position.y = newPos.yMin;
        EditorUtility.SetDirty(node);
    }

    public override void OnSelected()
    {
        base.OnSelected();
        OnNodeSelected?.Invoke(this);
    }

    public void SortChildren()
    {
        CompositeNode composite = node as CompositeNode;
        if (composite)
        {
            composite.children.Sort(SortByPosition);
        }
    }

    private int SortByPosition(Node x, Node y)
    {
        return x.position.y < y.position.y ? -1 : 1;
    }

    public void UpdateStates()
    {
        RemoveFromClassList("running");
        RemoveFromClassList("failure");
        RemoveFromClassList("success");

        if (Application.isPlaying)
        {
            switch (node.state)
            {
                case Node.State.Running:
                    if (node.started)
                    {
                        AddToClassList("running");
                        RemoveFromClassList("failure");
                        RemoveFromClassList("success");
                    }
                    break;
                case Node.State.Failure:
                    AddToClassList("failure");
                    RemoveFromClassList("running");
                    RemoveFromClassList("success");
                    break;
                case Node.State.Success:
                    AddToClassList("success");
                    RemoveFromClassList("running");
                    RemoveFromClassList("failure");
                    break;
            }
        }
    }
}
