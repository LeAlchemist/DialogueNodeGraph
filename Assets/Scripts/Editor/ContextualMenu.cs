using UnityEngine;
using UnityEngine.UIElements;
using UnityEditor.Experimental.GraphView;
using UnityEditor;
using System.Runtime.Remoting.Contexts;

public partial class GraphEditorView : GraphView
{
    private NodeSeachWindow _seachWindow;
    public Vector2 position = new(0, 0);
    public override void BuildContextualMenu(ContextualMenuPopulateEvent evt)
    {
        string _add = "Add";

        base.BuildContextualMenu(evt);
        {
            string _category = "Dialogue";

            //dialogue nodes
            evt.menu.AppendAction($"{_add}/{_category}/Dialogue", (a) => CreateNode(typeof(DialogueNode), position));
            evt.menu.AppendAction($"{_add}/{_category}/Choice", (a) => CreateNode(typeof(ChoiceNode), position));
        }

        {
            string _category = "Actor";

            //actor nodes
            evt.menu.AppendAction($"{_add}/{_category}/Placeholder", (a) => UnityEngine.Debug.Log("This is a placeholder for actor nodes"));
        }

        {
            string _category = "Scene";

            //scene nodes
            evt.menu.AppendAction($"{_add}/{_category}/Placeholder", (a) => UnityEngine.Debug.Log("This is a placeholder for scene nodes"));
        }

        {
            //action nodes
            var types = TypeCache.GetTypesDerivedFrom<ActionNode>();
            foreach (var type in types)
            {
                string _category = type.BaseType.Name.Replace("Node", "");

                evt.menu.AppendAction($"{_add}/{_category}/{type.Name.Replace("Node", "")}", (a) => CreateNode(type, position));
            }
        }

        {
            //composite nodes
            var types = TypeCache.GetTypesDerivedFrom<CompositeNode>();
            foreach (var type in types)
            {
                string _category = type.BaseType.Name.Replace("Node", "");

                evt.menu.AppendAction($"{_add}/{_category}/{type.Name.Replace("Node", "")}", (a) => CreateNode(type, position));
            }
        }

        {
            //decorator nodes
            var types = TypeCache.GetTypesDerivedFrom<DecoratorNode>();
            foreach (var type in types)
            {
                string _category = type.BaseType.Name.Replace("Node", "");

                evt.menu.AppendAction($"{_add}/{_category}/{type.Name.Replace("Node", "")}", (a) => CreateNode(type, position));
            }
        }

        {
            //all nodes
            var types = TypeCache.GetTypesDerivedFrom<Node>();
            foreach (var type in types)
            {
                string _category = "All";

                evt.menu.AppendAction($"{_add}/{_category}/{type.Name.Replace("Node", "")}", (a) => CreateNode(type, position));
            }
        }

        {
            string _category = "Template";

            //node templates
            evt.menu.AppendAction($"{_add}/{_category}/Placeholder", (a) => UnityEngine.Debug.Log("This is a placeholder for template collections of nodes"));
        }
    }

    public void AddSearchWindow()
    {
        _seachWindow = ScriptableObject.CreateInstance<NodeSeachWindow>();
        _seachWindow.Init(this);
        nodeCreationRequest = context => SearchWindow.Open(new SearchWindowContext(context.screenMousePosition), _seachWindow);
    }
}
