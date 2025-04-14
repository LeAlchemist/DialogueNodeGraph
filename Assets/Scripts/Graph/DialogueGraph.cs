using UnityEngine;
using UnityEngine.UIElements;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEditor.UIElements;

public class DialogueGraph : EditorWindow
{
    private DialogueGraphView _graphView;
    private DialogueNode _dialogueNode;
    public string _fileName = "New Narrative";

    [MenuItem("Graph/New Dialogue Graph")]
    public static void OpenDialogueGraphWindow()
    {
        var window = GetWindow<DialogueGraph>();
        window.titleContent = new GUIContent(text: "Dialogue Graph");
    }

    public void GenerateMiniMap()
    {
        var minimap = new MiniMap { anchored = true };
        minimap.SetPosition(new Rect(10, 30, 200, 140));
        _graphView.Add(minimap);
    }

    public void GenerateToolbar()
    {
        var toolbar = new Toolbar();

        //Graph name
        var fileNameTextField = new TextField("File Name");
        fileNameTextField.SetValueWithoutNotify(_fileName);
        fileNameTextField.MarkDirtyRepaint();
        fileNameTextField.RegisterValueChangedCallback(evt => _fileName = evt.newValue);
        toolbar.Add(fileNameTextField);

        //save button
        toolbar.Add(new Button(() => RequestDataOperation(true)) { text = "Save Data" });

        //load button
        toolbar.Add(new Button(() => RequestDataOperation(false)) { text = "Load Data" });

        //create node button 
        //todo: change this to a dropdown later for other node types
        _dialogueNode = new DialogueNode { };
        var nodeCreateButton = new Button(clickEvent: () => { _graphView.AddElement(_dialogueNode.GenerateNode()); });
        nodeCreateButton.text = "Dialogue Node";
        toolbar.Add(nodeCreateButton);

        rootVisualElement.Add(toolbar);
    }

    public void RequestDataOperation(bool save)
    {
        if (string.IsNullOrEmpty(_fileName))
        {
            EditorUtility.DisplayDialog("Invald file name!", "Please enter a valid file name!", "OK");
            return;
        }

        //var saveUtility = GraphSaveUtility.GetInstance(_graphView);

        //if (save)
        //{
        //    saveUtility.SaveGraph(_fileName);
        //}
        //else
        //{
        //    saveUtility.LoadGraph(_fileName);
        //}
    }

    private void ContructGraphView()
    {
        _graphView = new DialogueGraphView
        {
            name = "Dialogue Graph"
        };

        _graphView.StretchToParentSize();
        rootVisualElement.Add(_graphView);
    }

    private void OnEnable()
    {
        ContructGraphView();
        GenerateToolbar();
        GenerateMiniMap();
    }

    private void OnDisable()
    {
        rootVisualElement.Remove(_graphView);
    }
}
