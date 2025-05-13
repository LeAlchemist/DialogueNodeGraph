using UnityEngine;
using UnityEngine.UIElements;
using UnityEditor.Experimental.GraphView;
using UnityEditor;
using System;
using System.Collections.Generic;
using Codice.Utils;
using UnityEngine.UI;

public class NodeSeachWindow : ScriptableObject, ISearchWindowProvider
{
    private GraphEditorView _graphEditorView;

    public void Init(GraphEditorView graphEditorView)
    {
        _graphEditorView = graphEditorView;
        //_editorWindow = editorWindow;
    }

    public List<SearchTreeEntry> CreateSearchTree(SearchWindowContext context)
    {
        var tree = new List<SearchTreeEntry>
        {
            new SearchTreeGroupEntry(new GUIContent(text: "Create Node"), level: 0),
            new SearchTreeGroupEntry(new GUIContent(text:"Dialogue"), level: 1),
            new(new GUIContent(text:"Dialogue"))
            {
                userData = ScriptableObject.CreateInstance<DialogueNode>(),
                level = 2
            },
            new(new GUIContent(text:"Choice"))
            {
                userData = ScriptableObject.CreateInstance<DialogueNode>(),
                level = 2
            },
            new SearchTreeGroupEntry(new GUIContent(text:"Actor"), level: 1),
            new(new GUIContent(text:"This is a Placeholder"))
            {
                userData = "", //ScriptableObject.CreateInstance<DialogueNode>(), 
                level = 2
            },
            new SearchTreeGroupEntry(new GUIContent(text:"Scene"), level: 1),
            new(new GUIContent(text:"This is a Placeholder"))
            {
                userData = "", //ScriptableObject.CreateInstance<DialogueNode>(), 
                level = 2
            },
            new SearchTreeGroupEntry(new GUIContent(text:"Base"), level: 1),
            new(new GUIContent(text:"Sequence"))
            {
                userData = ScriptableObject.CreateInstance<SequencerNode>(),
                level = 2
            },
            new(new GUIContent(text:"Repeat"))
            {
                userData = ScriptableObject.CreateInstance<RepeatNode>(),
                level = 2
            },
            new(new GUIContent(text:"Wait"))
            {
                userData = ScriptableObject.CreateInstance<WaitNode>(),
                level = 2
            },
            new(new GUIContent(text:"DebugLog"))
            {
                userData = ScriptableObject.CreateInstance<DebugLogNode>(),
                level = 2
            },
            new SearchTreeGroupEntry(new GUIContent(text:"Template"), level: 1)
    };
        return tree;
    }

    public bool OnSelectEntry(SearchTreeEntry SearchTreeEntry, SearchWindowContext context)
    {
        //var worldMousePosition = _editorWindow.rootVisualElement.ChangeCoordinatesTo(_editorWindow.rootVisualElement.parent,
        //context.screenMousePosition - _editorWindow.position.position);
        //var localMousePosition = _graphEditorView.contentContainer.WorldToLocal(worldMousePosition);

        switch (SearchTreeEntry.userData)
        {
            case DialogueNode:
                _graphEditorView.CreateNode(typeof(DialogueNode), _graphEditorView.position);
                return true;
            case ChoiceNode:
                _graphEditorView.CreateNode(typeof(ChoiceNode), _graphEditorView.position);
                return true;
            case SequencerNode:
                _graphEditorView.CreateNode(typeof(SequencerNode), _graphEditorView.position);
                return true;
            case DebugLogNode:
                _graphEditorView.CreateNode(typeof(DebugLogNode), _graphEditorView.position);
                return true;
            case RepeatNode:
                _graphEditorView.CreateNode(typeof(RepeatNode), _graphEditorView.position);
                return true;
            case WaitNode:
                _graphEditorView.CreateNode(typeof(WaitNode), _graphEditorView.position);
                return true;
            default:
                Debug.Log($"{SearchTreeEntry.userData}");
                return false;
        }
    }
}
