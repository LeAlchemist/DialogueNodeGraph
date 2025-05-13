using UnityEngine;
using UnityEditor.Experimental.GraphView;
using System.Collections.Generic;

public class NodeSeachWindow : ScriptableObject, ISearchWindowProvider
{
    private GraphEditorView _graphEditorView;
    private Texture2D _indentationIcon;

    public void Init(GraphEditorView graphEditorView)
    {
        _graphEditorView = graphEditorView;
        //_editorWindow = editorWindow;

        _indentationIcon = new Texture2D(width: 1, height: 1);
        _indentationIcon.SetPixel(x: 0, y: 0, new Color(0, 0, 0, 0));
        _indentationIcon.Apply();
    }

    public List<SearchTreeEntry> CreateSearchTree(SearchWindowContext context)
    {
        var tree = new List<SearchTreeEntry>
        {
            new SearchTreeGroupEntry(new GUIContent(text: "Create Node"), level: 0),
            new SearchTreeGroupEntry(new GUIContent(text:"Dialogue"), level: 1),
            new(new GUIContent(text:"Dialogue", _indentationIcon))
            {
                userData = ScriptableObject.CreateInstance<DialogueNode>(),
                level = 2
            },
            new(new GUIContent(text:"Choice", _indentationIcon))
            {
                userData = ScriptableObject.CreateInstance<DialogueNode>(),
                level = 2
            },
            new SearchTreeGroupEntry(new GUIContent(text:"Actor"), level: 1),
            new(new GUIContent(text:"This is a Placeholder", _indentationIcon))
            {
                userData = "", //ScriptableObject.CreateInstance<DialogueNode>(), 
                level = 2
            },
            new SearchTreeGroupEntry(new GUIContent(text:"Scene"), level: 1),
            new(new GUIContent(text:"This is a Placeholder", _indentationIcon))
            {
                userData = "", //ScriptableObject.CreateInstance<DialogueNode>(), 
                level = 2
            },
            new SearchTreeGroupEntry(new GUIContent(text:"Base"), level: 1),
            new(new GUIContent(text:"Sequence", _indentationIcon))
            {
                userData = ScriptableObject.CreateInstance<SequencerNode>(),
                level = 2
            },
            new(new GUIContent(text:"Repeat", _indentationIcon))
            {
                userData = ScriptableObject.CreateInstance<RepeatNode>(),
                level = 2
            },
            new(new GUIContent(text:"Wait", _indentationIcon))
            {
                userData = ScriptableObject.CreateInstance<WaitNode>(),
                level = 2
            },
            new(new GUIContent(text:"DebugLog", _indentationIcon))
            {
                userData = ScriptableObject.CreateInstance<DebugLogNode>(),
                level = 2
            },
            new SearchTreeGroupEntry(new GUIContent(text:"Template"), level: 1),
            new(new GUIContent(text:"This is a Placeholder", _indentationIcon))
            {
                userData = "", //ScriptableObject.CreateInstance<DialogueNode>(), 
                level = 2
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
