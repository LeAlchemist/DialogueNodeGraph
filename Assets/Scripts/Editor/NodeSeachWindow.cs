using UnityEngine;
using UnityEditor.Experimental.GraphView;
using System.Collections.Generic;
using UnityEditor;

public class NodeSeachWindow : ScriptableObject, ISearchWindowProvider
{
    private GraphEditorView _graphEditorView;
    private Texture2D _indentationIcon;
    public List<SearchTreeEntry> tree;
    public BlackboardContainer blackboardContainer;

    public void Init(GraphEditorView graphEditorView)
    {
        _graphEditorView = graphEditorView;
        //_editorWindow = editorWindow;

        if (AssetDatabase.IsValidFolder("assets/resources") == true)
        {
            if (AssetDatabase.AssetPathExists("assets/resources/BlackboardContainer.asset") == false)
            {
                BlackboardContainer _container = ScriptableObject.CreateInstance<BlackboardContainer>();
                AssetDatabase.CreateAsset(_container, "assets/resources/BlackboardContainer.asset");
                blackboardContainer = AssetDatabase.LoadAssetAtPath<BlackboardContainer>("assets/resources/BlackboardContainer.asset");
            }
            else
            {
                blackboardContainer = AssetDatabase.LoadAssetAtPath<BlackboardContainer>("assets/resources/BlackboardContainer.asset");
            }
        }
        else
        {
            AssetDatabase.CreateFolder("assets", "resources");
            BlackboardContainer _container = ScriptableObject.CreateInstance<BlackboardContainer>();
            AssetDatabase.CreateAsset(_container, "assets/resources/BlackboardContainer.asset");
            blackboardContainer = AssetDatabase.LoadAssetAtPath<BlackboardContainer>("assets/resources/BlackboardContainer.asset");
        }

        _indentationIcon = new Texture2D(width: 1, height: 1);
        _indentationIcon.SetPixel(x: 0, y: 0, new Color(0, 0, 0, 0));
        _indentationIcon.Apply();
    }

    public List<SearchTreeEntry> CreateSearchTree(SearchWindowContext context)
    {
        tree = new List<SearchTreeEntry>
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
        };
        {
            tree.Add(new SearchTreeGroupEntry(new GUIContent(text: "Blackboard"), level: 1));
            tree.Add(new(new GUIContent(text: "Create New Node", _indentationIcon))
            {
                userData = "Create Blackboard Node",
                level = 2
            });
            for (int i = 0; i < blackboardContainer.exposedProperties.Count; i++)
            {
                tree.Add(new(new GUIContent(text: $"{blackboardContainer.exposedProperties[i].propertyName}", _indentationIcon))
                {
                    userData = "", //ScriptableObject.CreateInstance<DialogueNode>(), 
                    level = 2
                });
            }
        }
        {
            tree.Add(new SearchTreeGroupEntry(new GUIContent(text: "Template"), level: 1));
            tree.Add(new(new GUIContent(text: "This is a Placeholder", _indentationIcon))
            {
                userData = "", //ScriptableObject.CreateInstance<DialogueNode>(), 
                level = 2
            });
        }
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
            case "Create Blackboard Node":
                Debug.Log($"Before count: {blackboardContainer.exposedProperties.Count}");
                var property = new ExposedProperty();
                //Property Name
                property.propertyName = "New Name";
                //Property Type
                property.propertyType = ExposedProperty.PropertyType.String;
                //The default value
                property.propertyValueString = "New String";
                property.propertyValueInt = 0;
                property.propertyValueFloat = 0;
                property.propertyValueBool = false;

                blackboardContainer.exposedProperties.Add(property);
                Debug.Log($"Current level: {tree.Count}");
                Debug.Log($"After Count: {blackboardContainer.exposedProperties.Count}");
                return true;
            default:
                Debug.Log($"{SearchTreeEntry.userData}");
                return false;
        }
    }
}
