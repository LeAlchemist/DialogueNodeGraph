using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class BlackboardContainer : ScriptableObject
{
    public List<ExposedProperty> exposedProperties = new();
}
