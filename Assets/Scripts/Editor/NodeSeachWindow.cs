using UnityEngine;
using UnityEditor.Experimental.GraphView;
using System.Collections.Generic;
using UnityEditor;
using System.Reflection;
using Unity.VisualScripting;
using System.Linq;

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

        #region BlackboardContainer Check
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
        #endregion

        _indentationIcon = new Texture2D(width: 1, height: 1);
        _indentationIcon.SetPixel(x: 0, y: 0, new Color(0, 0, 0, 0));
        _indentationIcon.Apply();
    }

    public List<SearchTreeEntry> CreateSearchTree(SearchWindowContext context)
    {
        #region Node based selections
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
        #endregion
        #region Blackboard Elements
        {
            tree.Add(new SearchTreeGroupEntry(new GUIContent(text: "Blackboard"), level: 1));
            #region Custom Elements
            tree.Add(new SearchTreeGroupEntry(new GUIContent(text: "Custom"), level: 2));
            tree.Add(new(new GUIContent(text: "Create New Node", _indentationIcon))
            {
                userData = "Create Blackboard Node",
                level = 3
            });
            for (int i = 0; i < blackboardContainer.exposedProperties.Count; i++)
            {
                tree.Add(new(new GUIContent(text: $"[{blackboardContainer.exposedProperties[i].propertyType}] {blackboardContainer.exposedProperties[i].propertyName}", _indentationIcon))
                {
                    userData = $"{blackboardContainer.exposedProperties[i].propertyName}",
                    level = 3
                });
            }
            tree.Add(new(new GUIContent(text: "Clear Blackboard", _indentationIcon))
            {
                userData = "Clear Blackboard",
                level = 3
            });
            #endregion
            #region Scene Elements
            GameObject[] scene = GameObject.FindObjectsByType<GameObject>(FindObjectsSortMode.None);
            foreach (GameObject sceneObj in scene)
            {
                var sceneObjName = sceneObj.name;
                var sceneObjID = sceneObj.GetInstanceID();
                var sceneIDtoObj = Resources.InstanceIDToObject(sceneObjID);
                bool hasFields = false;

                tree.Add(new SearchTreeGroupEntry(new GUIContent(text: $"[Object] {sceneObjName}"), level: 2));

                Component[] components = sceneObj.GetComponents(typeof(Component));
                foreach (Component component in components)
                {
                    var componentType = component.GetType();
                    var componentName = componentType.Name;
                    var getComponent = sceneIDtoObj.GetComponent(componentType);
                    var getComponentType = getComponent.GetType();

                    BindingFlags allInstance = BindingFlags.DeclaredOnly |
                        BindingFlags.Public |
                        BindingFlags.Instance;

                    if (componentType.GetFields(allInstance).Length != 0)
                    {
                        tree.Add(new SearchTreeGroupEntry(new GUIContent(text: $"[Component] {componentName}"), level: 3));
                        if (hasFields == false)
                        {
                            hasFields = true;
                        }

                        foreach (FieldInfo fieldInfo in componentType.GetFields(allInstance))
                        {
                            var fieldInfoName = fieldInfo.Name;
                            var field = getComponentType.GetField($"{fieldInfoName}");
                            var fieldType = field.FieldType;
                            var fieldTypeName = fieldType.Name;

                            if (fieldType.IsArray == false)
                            {
                                tree.Add(new(new GUIContent(text: $"[{fieldTypeName}] {fieldInfoName}", _indentationIcon))
                                {
                                    userData = field.GetValue(getComponent),
                                    level = 4
                                });
                            }
                        }

                        //foreach (PropertyInfo propertyInfo in component.GetType().GetProperties(allInstance))
                        //{
                        //    tree.Add(new(new GUIContent(text: $"[{component.GetType().Name}] {propertyInfo.Name}", _indentationIcon))
                        //    {
                        //        userData = $"{sceneObj.GetInstanceID()} {component.GetType().Name} {propertyInfo.Name}",
                        //        level = 4
                        //    });
                        //}
                    }
                }

                //removes objects that have no accessable fields
                if (hasFields == false)
                {
                    //Debug.Log($"{tree[tree.Count - 1].name} has no fields");
                    tree.RemoveAt(tree.Count - 1);
                }
            }
            #endregion
        }
        #endregion
        #region Template Elements
        {
            tree.Add(new SearchTreeGroupEntry(new GUIContent(text: "Template"), level: 1));
            tree.Add(new(new GUIContent(text: "This is a Placeholder", _indentationIcon))
            {
                userData = "", //ScriptableObject.CreateInstance<DialogueNode>(), 
                level = 2
            });
        }
        #endregion

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
                Debug.Log(_graphEditorView._graph.nodes[0].GetType().Name);
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
            #region Create Blackboard Nodes
            case "Create Blackboard Node":
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
                return true;
            #endregion
            #region Clear Blackboard Elements
            case "Clear Blackboard":
                //needs a dialog prompt to verify
                //maybe set it up to remove a specific node
                if (EditorUtility.DisplayDialog(title: "Clear Blackboard Elements",
                message: "Are you sure you want to clear all Blackboard Elements",
                ok: "Confirm",
                cancel: "Cancel"))
                {
                    blackboardContainer.exposedProperties.Clear();
                }

                return true;
            #endregion
            default:
                for (int i = 0; i < blackboardContainer.exposedProperties.Count; i++)
                {
                    #region Custom Blackboard Node creation
                    if (SearchTreeEntry.userData.ToString() == blackboardContainer.exposedProperties[i].propertyName)
                    {
                        _graphEditorView.CreateNode(typeof(BlackboardNode), _graphEditorView.position);
                        var blackboardNode = _graphEditorView._graph.nodes[_graphEditorView._graph.nodes.Count - 1] as BlackboardNode;

                        blackboardNode._name = blackboardContainer.exposedProperties[i].propertyName;
                        blackboardNode.propertyType = blackboardContainer.exposedProperties[i].propertyType;
                        blackboardNode.stringValue = blackboardContainer.exposedProperties[i].propertyValueString;
                        blackboardNode.intValue = blackboardContainer.exposedProperties[i].propertyValueInt;
                        blackboardNode.floatValue = blackboardContainer.exposedProperties[i].propertyValueFloat;
                        blackboardNode.boolValue = blackboardContainer.exposedProperties[i].propertyValueBool;

                        Debug.Log($"{SearchTreeEntry.userData} is an exposed property of type {blackboardContainer.exposedProperties[i].propertyType} \n {blackboardContainer.exposedProperties[i].propertyName}");
                        return true;
                    }
                    #endregion
                }
                Debug.Log($"{SearchTreeEntry.userData}");
                return false;
        }
    }
}
