using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditor.Experimental.GraphView;

[CreateAssetMenu(menuName = "Dialogue Graph")]
public class DialogueGraph : ScriptableObject
{
    public Node rootNode;
    public Node.State graphState = Node.State.Running;
    public List<Node> nodes = new List<Node>();
    public Blackboard blackboard = new Blackboard();

    public Node.State Update()
    {
        if (rootNode.state == Node.State.Running)
        {
            return rootNode.Update();
        }

        return graphState;
    }

#if UNITY_EDITOR
    public Node CreateNode(System.Type type)
    {
        Node node = ScriptableObject.CreateInstance(type) as Node;
        node.name = type.Name;
        node.guid = GUID.Generate().ToString();
        Undo.RecordObject(node, "Dialogue Graph (Create Node)");
        nodes.Add(node);

        if (!Application.isPlaying)
        {
            AssetDatabase.AddObjectToAsset(node, this);
        }
        Undo.RegisterCreatedObjectUndo(node, "Dialogue Graph (Create Node)");
        AssetDatabase.SaveAssets();
        return node;
    }

    public void DeleteNode(Node node)
    {
        Undo.RecordObject(node, "Dialogue Graph (Delete Node)");
        nodes.Remove(node);
        AssetDatabase.RemoveObjectFromAsset(node);
        AssetDatabase.SaveAssets();
    }

    public void AddChild(Node parent, Node Child)
    {
        DecoratorNode decorator = parent as DecoratorNode;
        if (decorator)
        {
            Undo.RecordObject(decorator, "Dialogue Graph (Add Child)");
            decorator.child = Child;
            EditorUtility.SetDirty(decorator);
        }

        CompositeNode composite = parent as CompositeNode;
        if (composite)
        {
            Undo.RecordObject(composite, "Dialogue Graph (Add Child)");
            composite.children.Add(Child);
            EditorUtility.SetDirty(composite);
        }

        RootNode root = parent as RootNode;
        if (root)
        {
            Undo.RecordObject(root, "Dialogue Graph (Add Child)");
            root.child = Child;
            EditorUtility.SetDirty(root);
        }

    }

    public void RemoveChild(Node parent, Node Child)
    {
        DecoratorNode decorator = parent as DecoratorNode;
        if (decorator)
        {
            Undo.RecordObject(decorator, "Dialogue Graph (Remove Child)");
            decorator.child = null;
            EditorUtility.SetDirty(decorator);
        }

        CompositeNode composite = parent as CompositeNode;
        if (composite)
        {
            Undo.RecordObject(composite, "Dialogue Graph (Remove Child)");
            composite.children.Remove(Child);
            EditorUtility.SetDirty(composite);
        }

        RootNode root = parent as RootNode;
        if (root)
        {
            Undo.RecordObject(root, "Dialogue Graph (Remove Child)");
            root.child = null;
            EditorUtility.SetDirty(root);
        }
    }

    public List<Node> GetChildren(Node parent)
    {
        List<Node> children = new List<Node>();

        DecoratorNode decorator = parent as DecoratorNode;
        if (decorator && decorator.child != null)
        {
            children.Add(decorator.child);
            EditorUtility.SetDirty(decorator);
        }

        CompositeNode composite = parent as CompositeNode;
        if (composite)
        {
            return composite.children;
        }

        RootNode root = parent as RootNode;
        if (root && root.child != null)
        {
            children.Add(root.child);
            EditorUtility.SetDirty(root);
        }

        return children;
    }
#endif

    public void Traverse(Node node, System.Action<Node> visitor)
    {
        if (node)
        {
            visitor.Invoke(node);
            var children = GetChildren(node);
            children.ForEach((n) => Traverse(n, visitor));
        }
    }

    public DialogueGraph Clone()
    {
        DialogueGraph graph = Instantiate(this);
        graph.rootNode = graph.rootNode.Clone();
        graph.nodes = new List<Node>();
        Traverse(graph.rootNode, (n) =>
        {
            graph.nodes.Add(n);
        });
        return graph;
    }
}
