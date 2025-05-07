using UnityEngine;
using UnityEngine.UIElements;
using UnityEditor.Experimental.GraphView;
using UnityEditor;

public partial class GraphEditorView : GraphView
{
    public Vector2 position = new(0, 0);
    public override void BuildContextualMenu(ContextualMenuPopulateEvent evt)
    {
        base.BuildContextualMenu(evt);
        {
            //dialogue nodes
            evt.menu.AppendAction($"Dialogue/Dialogue", (a) => CreateNode(typeof(DialogueNode), position));
            evt.menu.AppendAction($"Dialogue/Choice", (a) => CreateNode(typeof(ChoiceNode), position));
        }

        {
            //actor nodes
            evt.menu.AppendAction($"Actor/Placeholder/test", (a) => UnityEngine.Debug.Log("This is a placeholder for actor nodes"));
        }

        {
            //scene nodes
            evt.menu.AppendAction($"Scene/Placeholder", (a) => UnityEngine.Debug.Log("This is a placeholder for scene nodes"));
        }

        {
            //action nodes
            var types = TypeCache.GetTypesDerivedFrom<ActionNode>();
            foreach (var type in types)
            {
                evt.menu.AppendAction($"{type.BaseType.Name.Replace("Node", "")}/{type.Name.Replace("Node", "")}", (a) => CreateNode(type, position));
            }
        }

        {
            //composite nodes
            var types = TypeCache.GetTypesDerivedFrom<CompositeNode>();
            foreach (var type in types)
            {
                evt.menu.AppendAction($"{type.BaseType.Name.Replace("Node", "")}/{type.Name.Replace("Node", "")}", (a) => CreateNode(type, position));
            }
        }

        {
            //decorator nodes
            var types = TypeCache.GetTypesDerivedFrom<DecoratorNode>();
            foreach (var type in types)
            {

                evt.menu.AppendAction($"{type.BaseType.Name.Replace("Node", "")}/{type.Name.Replace("Node", "")}", (a) => CreateNode(type, position));
            }
        }

        {
            //all nodes
            var types = TypeCache.GetTypesDerivedFrom<Node>();
            foreach (var type in types)
            {

                evt.menu.AppendAction($"All/{type.Name.Replace("Node", "")}", (a) => CreateNode(type, position));
            }
        }

        {
            //node templates
            evt.menu.AppendAction($"Templates/Placeholder", (a) => UnityEngine.Debug.Log("This is a placeholder for template collections of nodes"));
        }
    }
}
