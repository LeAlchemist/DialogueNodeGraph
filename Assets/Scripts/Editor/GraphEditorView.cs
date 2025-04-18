using UnityEngine;
using UnityEngine.UIElements;
using UnityEditor.Experimental.GraphView;
using UnityEditor;

public class GraphEditorView : GraphView
{
    [System.Obsolete]
    public new class UxmlFactory : UxmlFactory<GraphEditorView, GraphView.UxmlTraits> { }
    public GraphEditorView()
    {
        Insert(0, new GridBackground());

        this.AddManipulator(new ContentZoomer());
        this.AddManipulator(new ContentDragger());
        this.AddManipulator(new SelectionDragger());
        this.AddManipulator(new RectangleSelector());

        //Import USS
        var styleSheet = AssetDatabase.LoadAssetAtPath<StyleSheet>("Assets/Scripts/Editor/GraphEditor.uss");
        styleSheets.Add(styleSheet);
    }
}
