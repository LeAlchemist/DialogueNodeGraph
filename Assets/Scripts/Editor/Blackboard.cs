using System.Collections.Generic;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public partial class GraphEditor : EditorWindow
{
    public List<ExposedProperty> exposedProperties = new();
    private void GenerateBlackboard()
    {
        var blackBoard = new Blackboard(_view);
        blackBoard.Add(new BlackboardSection
        {
            title = "Exposed Properties"
        });
        blackBoard.addItemRequested = _blackboard => { AddPropertyToBlackboard(new ExposedProperty()); };
        blackBoard.SetPosition(new Rect(x: 10, y: 180, width: 200, height: 300));
        _view.Add(blackBoard);
        _view.blackboard = blackBoard;
    }

    public void AddPropertyToBlackboard(ExposedProperty exposedProperty)
    {
        var property = new ExposedProperty();
        property.PropertyName = exposedProperty.PropertyName;
        property.PropertyValue = exposedProperty.PropertyValue;
        exposedProperties.Add(property);

        var container = new VisualElement();
        var blackBoardField = new BlackboardField { text = property.PropertyName, typeText = "string property" };
        container.Add(blackBoardField);

        var PropertyValueTextField = new TextField(label: "Value:")
        {
            value = property.PropertyValue
        };
        PropertyValueTextField.RegisterValueChangedCallback(evt =>
        {
            var changingPropertyIndex = exposedProperties.FindIndex(match: x => x.PropertyName == property.PropertyName);
            exposedProperties[changingPropertyIndex].PropertyValue = property.PropertyValue;
        });
        var blackBoardValueRow = new BlackboardRow(blackBoardField, PropertyValueTextField);
        container.Add(blackBoardValueRow);

        _view.blackboard.Add(container);
    }
}
