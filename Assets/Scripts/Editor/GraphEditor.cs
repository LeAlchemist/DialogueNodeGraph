using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEditor.Callbacks;
using System;
using UnityEditor.Experimental.GraphView;

public class GraphEditor : EditorWindow
{

    GraphEditorView _view;
    InspectorView _inspector;

    [MenuItem("GraphEditor/Editor")]
    public static void OpenWindow()
    {
        GraphEditor wnd = GetWindow<GraphEditor>();
        wnd.titleContent = new GUIContent("GraphEditor");
    }

    [OnOpenAsset]
    public static bool OnOpenAsset(int instanceId, int line)
    {
        if (Selection.activeObject is DialogueGraph)
        {
            OpenWindow();
            return true;
        }
        return false;
    }

    public void CreateGUI()
    {
        // Each editor window contains a root VisualElement object
        VisualElement root = rootVisualElement;

        //Import UXML
        var visualTree = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>("Assets/Scripts/Editor/GraphEditor.uxml");
        visualTree.CloneTree(root);

        //Import USS
        var styleSheet = AssetDatabase.LoadAssetAtPath<StyleSheet>("Assets/Scripts/Editor/GraphEditor.uss");
        root.styleSheets.Add(styleSheet);

        _view = root.Q<GraphEditorView>();
        _inspector = root.Q<InspectorView>();
        _view.OnNodeSelected = OnNodeSelectionChanged;

        OnSelectionChange();
        GenerateMiniMap();
    }

    private void GenerateMiniMap()
    {
        var miniMap = new MiniMap { anchored = true };
        miniMap.SetPosition(new Rect(10, 30, 200, 140));
        _view.Add(miniMap);
    }

    private void OnEnable()
    {
        EditorApplication.playModeStateChanged -= OnPlayModeStateChanged;
        EditorApplication.playModeStateChanged += OnPlayModeStateChanged;
    }

    private void OnDisable()
    {
        EditorApplication.playModeStateChanged -= OnPlayModeStateChanged;
    }

    private void OnPlayModeStateChanged(PlayModeStateChange obj)
    {
        switch (obj)
        {
            case PlayModeStateChange.EnteredEditMode:
                OnSelectionChange();
                break;
            case PlayModeStateChange.ExitingEditMode:
                break;
            case PlayModeStateChange.EnteredPlayMode:
                OnSelectionChange();
                break;
            case PlayModeStateChange.ExitingPlayMode:
                break;
        }
    }

    private void OnSelectionChange()
    {
        DialogueGraph _graph = Selection.activeObject as DialogueGraph;
        if (!_graph)
        {
            if (Selection.activeGameObject)
            {
                DialogueGraphRunner runner = Selection.activeGameObject.GetComponent<DialogueGraphRunner>();
                if (runner)
                {
                    _graph = runner._graph;
                }
            }
        }

#if UNITY_EDITOR
        if (Application.isPlaying)
        {
            if (_graph != null)
            {
                _view.Populateview(_graph);
            }
        }
        else
        {
            if (_graph && AssetDatabase.CanOpenAssetInEditor(_graph.GetInstanceID()))
            {
                _view.Populateview(_graph);
            }
        }
    }
#endif

    void OnNodeSelectionChanged(NodeView nodeView)
    {
        _inspector.UpdateSelection(nodeView);
    }

    private void OnInspectorUpdate()
    {
        _view?.UpdateNodeState();
    }
}
