using UnityEngine;
using UnityEngine.UIElements;
using UnityEditor.Experimental.GraphView;
using UnityEditor;
using System;
using System.Collections.Generic;

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
                userData = ScriptableObject.CreateInstance<DialogueNode>(), level = 2
            },
            new(new GUIContent(text:"Choice"))
            {
                userData = ScriptableObject.CreateInstance<DialogueNode>(), level = 2
            },
            new SearchTreeGroupEntry(new GUIContent(text:"Actor"), level: 1),
            new SearchTreeGroupEntry(new GUIContent(text:"Scene"), level: 1),
            new SearchTreeGroupEntry(new GUIContent(text:"Action"), level: 1),
            new SearchTreeGroupEntry(new GUIContent(text:"Composite"), level: 1),
            new SearchTreeGroupEntry(new GUIContent(text:"Decorator"), level: 1),
            new SearchTreeGroupEntry(new GUIContent(text:"All"), level: 1),
            new(new GUIContent(text:"Dialogue"))
            {
                userData = ScriptableObject.CreateInstance<DialogueNode>(), level = 2
            },
            new(new GUIContent(text:"Choice"))
            {
                userData = ScriptableObject.CreateInstance<DialogueNode>(), level = 2
            },
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
            case DialogueNode dialogueNode:
                _graphEditorView.CreateNode(typeof(DialogueNode), _graphEditorView.position);
                return true;
            case ChoiceNode choiceNode:
                _graphEditorView.CreateNode(typeof(ChoiceNode), _graphEditorView.position);
                return true;
            default:
                return false;
        }
    }
}
