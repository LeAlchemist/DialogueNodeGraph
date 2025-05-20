using System.Collections.Generic;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;
using System.Linq;

public partial class GraphEditor : EditorWindow
{
    public BlackboardContainer blackboardContainer;
    public List<ExposedProperty> exposedProperties = new();

    private void GenerateBlackboard()
    {
        var blackBoard = new Blackboard(_view);
        blackBoard.Add(new BlackboardSection
        {
            title = "Exposed Properties"
        });
        blackBoard.scrollable = true;
        blackBoard.addItemRequested = _blackboard => { AddPropertyToBlackboard(new ExposedProperty()); };
        blackBoard.editTextRequested = (_blackBoard, element, newValue) =>
        {
            var oldPropertyName = ((BlackboardField)element).text;
            if (exposedProperties.Any(x => x.PropertyName == newValue))
            {
                EditorUtility.DisplayDialog(title: "Error", message: "This property name already exists", ok: "OK");
            }

            var propertyNameIndex = exposedProperties.FindIndex(x => x.PropertyName == oldPropertyName);
            exposedProperties[propertyNameIndex].PropertyName = newValue;
            ((BlackboardField)element).text = newValue;
        };
        blackBoard.editTextRequested = (_blackBoard, element, newValue) =>
        {
            var oldPropertyValue = ((TextField)element).text;
            var propertyValueIndex = exposedProperties.FindIndex(x => x.PropertyValue == oldPropertyValue);
            exposedProperties[propertyValueIndex].PropertyValue = newValue;
            ((BlackboardField)element).text = newValue;
        };

        blackBoard.SetPosition(new Rect(x: 10, y: 180, width: 200, height: 300));
        _view.Add(blackBoard);
        _view.blackboard = blackBoard;
    }

    public void AddPropertyToBlackboard(ExposedProperty exposedProperty)
    {
        var property = new ExposedProperty();
        property.PropertyName = exposedProperty.PropertyName;
        property.PropertyValue = exposedProperty.PropertyValue;

        //exposedProperties.Add(property);
        //Debug.Log($"exposed properties count {exposedProperties.Count} blackboard count {_view.blackboard.childCount}");

        var container = new VisualElement();

        var blackBoardNameField = new BlackboardField()
        {
            text = property.PropertyName,
            typeText = "String"
        };

        if (blackBoardNameField.canGrabFocus == true)
        {
            Debug.Log("selected");
        }


        container.Add(blackBoardNameField);
        //var blackboardValueField = new TextField()
        //{
        //    value = property.PropertyValue,
        //};
        //
        //var blackboardValueRow = new BlackboardRow(blackBoardNameField, blackboardValueField);
        //
        //container.Add(blackboardValueRow);

        _view.blackboard.Add(container);


        //var localPropertyName = exposedProperty.PropertyName;
        //var localPropertyValue = exposedProperty.PropertyValue;
        //while (exposedProperties.Any(x => x.PropertyName == localPropertyName))
        //    localPropertyName = $"{localPropertyName}(1)";

        //PropertyValueTextField.RegisterValueChangedCallback(evt =>
        //{
        //    var changingPropertyIndex = exposedProperties.FindIndex(match: x => x.PropertyName == property.PropertyName);
        //    exposedProperties[changingPropertyIndex].PropertyValue = property.PropertyValue;
        //});
        //var blackBoardValueRow = new BlackboardRow(blackBoardField, PropertyValueTextField);
        //container.Add(blackBoardValueRow);

        //if (_view.blackboard != null)
        //{
        //    _view.blackboard.Add(container);
        //}
        //else
        //{
        //    AssignBlackboard();
        //    LoadBlackboard();
        //}
        //blackboardContainer.exposedProperties.Add(property);

        if (_view.blackboard.childCount - 1 > blackboardContainer.exposedProperties.Count)
        {
            var _count = _view.blackboard.childCount;
            for (int i = 0; i < _count; i++)
            {
                _view.blackboard.RemoveAt(1);
            }
        }
    }

    public void LoadBlackboard()
    {
        if (_view.blackboard != null)
        {
            AssignBlackboardContainer();

            exposedProperties = new List<ExposedProperty>(blackboardContainer.exposedProperties);
            for (int i = 0; i < blackboardContainer.exposedProperties.Count; i++)
            {
                var property = exposedProperties[i];

                AddPropertyToBlackboard(property);
            }

            Debug.Log("blackboard loaded");
        }
    }

    public void AssignBlackboardContainer()
    {
        if (blackboardContainer == null)
        {
            if (AssetDatabase.IsValidFolder("assets/resources") == true)
            {
                if (AssetDatabase.AssetPathExists("assets/resources/BlackboardContainer.asset") == false)
                {
                    BlackboardContainer _container = ScriptableObject.CreateInstance<BlackboardContainer>();
                    AssetDatabase.CreateAsset(_container, "assets/resources/BlackboardContainer.asset");
                }
                else
                {
                    blackboardContainer = AssetDatabase.LoadAssetAtPath<BlackboardContainer>("assets/resources/BlackboardContainer.asset");
                }
            }
            else
            {
                AssetDatabase.CreateFolder("assets", "resources");
                BlackboardContainer _container = ScriptableObject.CreateInstance<BlackboardContainer>();
                AssetDatabase.CreateAsset(_container, "assets/resources/BlackboardContainer.asset");
                blackboardContainer = AssetDatabase.LoadAssetAtPath<BlackboardContainer>("assets/resources/BlackboardContainer.asset");
            }
        }
    }
}