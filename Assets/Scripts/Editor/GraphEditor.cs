using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEditor.Callbacks;

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
    }

    private void OnSelectionChange()
    {
        DialogueGraph _graph = Selection.activeObject as DialogueGraph;
        {
            if (_graph && AssetDatabase.CanOpenAssetInEditor(_graph.GetInstanceID()))
            {
                _view.Populateview(_graph);
            }
        }
    }

    void OnNodeSelectionChanged(NodeView nodeView)
    {
        _inspector.UpdateSelection(nodeView);
    }
}
