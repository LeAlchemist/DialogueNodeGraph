using System.Collections.Generic;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;
using System.Linq;

public partial class GraphEditor : EditorWindow
{
    public List<ExposedProperty> exposedProperties = new();
    private void GenerateBlackboard()
    {
        if (AssetDatabase.IsValidFolder("assets/resources") == true)
        {
            if (AssetDatabase.AssetPathExists("assets/resources/BlackboardContainer.asset") == false)
            {
                BlackboardContainer _container = ScriptableObject.CreateInstance<BlackboardContainer>();
                AssetDatabase.CreateAsset(_container, "assets/resources/BlackboardContainer.asset");
            }
        }
        else
        {
            AssetDatabase.CreateFolder("assets", "resources");
            BlackboardContainer _container = ScriptableObject.CreateInstance<BlackboardContainer>();
            AssetDatabase.CreateAsset(_container, "assets/resources/BlackboardContainer.asset");
        }


        var blackBoard = new Blackboard(_view);
        blackBoard.Add(new BlackboardSection
        {
            title = "Exposed Properties"
        });
        blackBoard.addItemRequested = _blackboard => { AddPropertyToBlackboard(new ExposedProperty()); };
        blackBoard.editTextRequested = (_blackBoard, element, newValue) =>
        {
            var oldPropertyName = ((BlackboardField)element).text;
            if (exposedProperties.Any(x => x.PropertyName == newValue))
            {
                EditorUtility.DisplayDialog(title: "Error", message: "This property name already exists", ok: "OK");
            }

            var propertyIndex = exposedProperties.FindIndex(x => x.PropertyName == oldPropertyName);
            exposedProperties[propertyIndex].PropertyName = newValue;
            ((BlackboardField)element).text = newValue;
        };

        blackBoard.SetPosition(new Rect(x: 10, y: 180, width: 200, height: 300));
        _view.Add(blackBoard);
        _view.blackboard = blackBoard;
    }

    public void AddPropertyToBlackboard(ExposedProperty exposedProperty)
    {
        var localPropertyName = exposedProperty.PropertyName;
        var localPropertyValue = exposedProperty.PropertyValue;
        while (exposedProperties.Any(x => x.PropertyName == localPropertyName))
            localPropertyName = $"{localPropertyName}(1)";

        var property = new ExposedProperty();
        property.PropertyName = localPropertyName;
        property.PropertyValue = localPropertyValue;
        exposedProperties.Add(property);

        var container = new VisualElement();
        var blackBoardField = new BlackboardField { text = property.PropertyName, typeText = "string property" };
        container.Add(blackBoardField);

        var PropertyValueTextField = new TextField(label: "Value:")
        {
            value = localPropertyValue
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
