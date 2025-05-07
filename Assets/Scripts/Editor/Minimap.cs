using UnityEditor;
using UnityEngine;
using UnityEditor.Experimental.GraphView;

public partial class GraphEditor : EditorWindow
{
    private void GenerateMiniMap()
    {
        var miniMap = new MiniMap { anchored = true };
        miniMap.SetPosition(new Rect(10, 30, 200, 140));
        _view.Add(miniMap);
    }
}
