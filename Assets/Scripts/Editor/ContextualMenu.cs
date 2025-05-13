using UnityEngine;
using UnityEditor.Experimental.GraphView;

public partial class GraphEditorView : GraphView
{
    private NodeSeachWindow _seachWindow;
    public Vector2 position = new(0, 0);

    public void AddSearchWindow()
    {
        _seachWindow = ScriptableObject.CreateInstance<NodeSeachWindow>();
        _seachWindow.Init(this);
        nodeCreationRequest = context => SearchWindow.Open(new SearchWindowContext(context.screenMousePosition), _seachWindow);
    }
}
