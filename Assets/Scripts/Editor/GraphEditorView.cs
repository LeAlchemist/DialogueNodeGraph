using UnityEngine;
using UnityEngine.UIElements;
using UnityEditor.Experimental.GraphView;
using UnityEditor;

public class GraphEditorView : GraphView
{
    public new class UxmlFactory : UxmlFactory<GraphEditorView, GraphView.UxmlTraits> { }
    public GraphEditorView()
    {
        Insert(0, new GridBackground());
        //Import USS
        var styleSheet = AssetDatabase.LoadAssetAtPath<StyleSheet>("Assets/Scripts/Editor/GraphEditor.uss");
        styleSheets.Add(styleSheet);
    }
}
