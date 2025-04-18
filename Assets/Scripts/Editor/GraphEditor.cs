using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

public class GraphEditor : EditorWindow
{
    [MenuItem("GraphEditor/Editor")]
    public static void OpenWindow()
    {
        GraphEditor wnd = GetWindow<GraphEditor>();
        wnd.titleContent = new GUIContent("GraphEditor");
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
    }
}
