using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEditor.UIElements;

public class DialogueGraph : EditorWindow
{
    private DialogueGraphView _graphview;
    private string _fileName = "New Narrative";

    [MenuItem("Graph/Dialogue Graph")]
    public static void OpenDialogueGraphWindow()
    {
        var window = GetWindow<DialogueGraph>();
        window.titleContent = new GUIContent (text: "Dialogue Graph");
    }

    
    private void ContructGraphView()
    {
        _graphview = new DialogueGraphView
        {
            name = "Dialogue Graph"
        };

        _graphview.StretchToParentSize();
        rootVisualElement.Add(_graphview);
    }

    private void GenerateToolbar()
    {
        var toolbar = new Toolbar();

        //file name set
        var fileNameTextField = new TextField("File Name");
        fileNameTextField.SetValueWithoutNotify(_fileName);
        fileNameTextField.MarkDirtyRepaint();
        fileNameTextField.RegisterValueChangedCallback(evt => _fileName = evt.newValue);
        toolbar.Add(fileNameTextField);

        //save button
        toolbar.Add(new Button(() => RequestDataOperation(true)){text = "Save Data"});

        //load button
        toolbar.Add(new Button(() => RequestDataOperation(false)){text = "Load Data"});

        //create node button
        var nodeCreateButton = new Button(clickEvent: () => {_graphview.CreateNode("Dialogue Node");});
        nodeCreateButton.text = "Create Node";
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

        var saveUtility = GraphSaveUtility.GetInstance(_graphview);

        if (save)
        {
            saveUtility.SaveGraph(_fileName);
        }
        else
        {
            saveUtility.LoadGraph(_fileName);
        }
    }

    private void GenerateMiniMap()
    {
        var minimap = new MiniMap{anchored = true};
        minimap.SetPosition(new Rect(10, 30, 200, 140));
        _graphview.Add(minimap);

    }

    private void OnEnable()
    {
        ContructGraphView();
        GenerateToolbar();
        GenerateMiniMap();
    }

    private void OnDisable()
    {
        rootVisualElement.Remove(_graphview);
    }
}
