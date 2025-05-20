using UnityEngine;

[System.Serializable]
public class ExposedProperty
{
    public string propertyName = "New String";
    public enum PropertyType
    {
        String,
        Int,
        Float,
        Bool,
    };
    public PropertyType propertyType = PropertyType.String;
    public string propertyValueString = "New Value";
    public int propertyValueInt = 0;
    public float propertyValueFloat = 0.0f;
    public bool propertyValueBool = false;
}
