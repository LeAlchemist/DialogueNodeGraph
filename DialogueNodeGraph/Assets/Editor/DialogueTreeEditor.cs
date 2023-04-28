using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEditor.UIElements;


public class DialogueTreeEditor : EditorWindow
{
    [MenuItem("/DialogueTreeEditor/Editor ...")]
    public static void OpenWindow()
    {
        DialogueTreeEditor wnd = GetWindow<DialogueTreeEditor>();
        wnd.titleContent = new GUIContent("DialogueTreeEditor");
    }

    public void CreateGUI()
    {
        // Each editor window contains a root VisualElement object
        VisualElement root = rootVisualElement;

        // Import UXML
        var visualTree = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>("Assets/Editor/DialogueTreeEditor.uxml");
        visualTree.CloneTree(root);

        // A stylesheet can be added to a VisualElement.
        // The style will be applied to the VisualElement and all of its children.
        var styleSheet = AssetDatabase.LoadAssetAtPath<StyleSheet>("Assets/Editor/DialogueTreeEditor.uss");
        root.styleSheets.Add(styleSheet);
    }

    private void OnSelectionChange()
    {
        DialogueGraph graph = Selection.activeObject as DialogueGraph;
    }
}