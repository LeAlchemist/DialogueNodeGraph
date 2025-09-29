using UnityEngine;
using System.Collections.Generic;

public class BlackboardNode : CompositeNode
{
    public string resource;
    public List<Node> blackboardNodes = new();
    public ExposedProperty.PropertyType propertyType = ExposedProperty.PropertyType.String;
    public string stringValue;
    public int intValue;
    public float floatValue;
    public bool boolValue;

    protected override void OnStart()
    {
        throw new System.NotImplementedException();
    }

    protected override void OnStop()
    {
        throw new System.NotImplementedException();
    }

    protected override State OnUpdate()
    {
        throw new System.NotImplementedException();
    }
}
