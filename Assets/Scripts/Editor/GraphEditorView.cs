using UnityEngine;
using UnityEngine.UIElements;
using UnityEditor.Experimental.GraphView;
using UnityEditor;
using System;
using System.Linq;
using System.Collections.Generic;

public partial class GraphEditorView : GraphView
{
    public Blackboard blackboard;
    public Action<NodeView> OnNodeSelected;
    [System.Obsolete]
    public new class UxmlFactory : UxmlFactory<GraphEditorView, GraphView.UxmlTraits> { }
    DialogueGraph _graph;

    public GraphEditorView()
    {
        Insert(0, new GridBackground());

        this.AddManipulator(new ContentZoomer());
        this.AddManipulator(new ContentDragger());
        this.AddManipulator(new SelectionDragger());
        this.AddManipulator(new RectangleSelector());

        //Import USS
        var styleSheet = AssetDatabase.LoadAssetAtPath<StyleSheet>("Assets/Scripts/Editor/GraphEditor.uss");
        styleSheets.Add(styleSheet);

        Undo.undoRedoPerformed += OnUndoRedo;

        AddSearchWindow();
    }

    private void OnUndoRedo()
    {
        Populateview(_graph);
        AssetDatabase.SaveAssets();
    }

    NodeView FindNodeView(Node node)
    {
        return GetNodeByGuid(node.guid) as NodeView;
    }

    internal void Populateview(DialogueGraph graph)
    {
        this._graph = graph;

        graphViewChanged -= OnGraphViewChanged;
        DeleteElements(graphElements);
        graphViewChanged += OnGraphViewChanged;

        if (graph.rootNode == null)
        {
            graph.rootNode = graph.CreateNode(typeof(RootNode), new Vector2(0, 0)) as RootNode;
            EditorUtility.SetDirty(graph);
            AssetDatabase.SaveAssets();
        }

        //creates node views
        graph.nodes.ForEach(n => CreateNodeView(n));

        //creates edges
        graph.nodes.ForEach(n =>
        {
            var children = graph.GetChildren(n);
            children.ForEach(c =>
            {
                NodeView parentView = FindNodeView(n);
                NodeView childView = FindNodeView(c);

                Edge edge = parentView.output.ConnectTo(childView.input);
                AddElement(edge);
            });
        });
    }

    public override List<Port> GetCompatiblePorts(Port startPort, NodeAdapter nodeAdapter)
    {
        return ports.ToList().Where(endPort =>
        endPort.direction != startPort.direction &&
        endPort.node != startPort.node).ToList();
    }

    private GraphViewChange OnGraphViewChanged(GraphViewChange graphViewChange)
    {
        if (graphViewChange.elementsToRemove != null)
        {
            graphViewChange.elementsToRemove.ForEach(elem =>
            {
                NodeView nodeView = elem as NodeView;
                if (nodeView != null)
                {
                    _graph.DeleteNode(nodeView.node);
                }

                Edge edge = elem as Edge;
                if (edge != null)
                {
                    NodeView parentView = edge.output.node as NodeView;
                    NodeView childView = edge.input.node as NodeView;
                    _graph.RemoveChild(parentView.node, childView.node);
                }
            });
        }

        if (graphViewChange.edgesToCreate != null)
        {
            graphViewChange.edgesToCreate.ForEach(edge =>
            {
                NodeView parentView = edge.output.node as NodeView;
                NodeView childView = edge.input.node as NodeView;
                _graph.AddChild(parentView.node, childView.node);
            });

            nodes.ForEach((n) =>
            {
                NodeView view = n as NodeView;
                view.SortChildren();
            });
        }

        if (graphViewChange.movedElements != null)
        {
            nodes.ForEach((n) =>
            {
                NodeView view = n as NodeView;
                view.SortChildren();
            });
        }

        return graphViewChange;
    }

    public void CreateNode(System.Type type, Vector2 position)
    {

        Node node = _graph.CreateNode(type, position);

        CreateNodeView(node);
    }

    void CreateNodeView(Node node)
    {
        NodeView nodeView = new NodeView(node);
        nodeView.OnNodeSelected = OnNodeSelected;
        AddElement(nodeView);
    }

    public void UpdateNodeState()
    {
        nodes.ForEach(n =>
        {
            NodeView view = n as NodeView;
            view.UpdateStates();
        });
    }
}
