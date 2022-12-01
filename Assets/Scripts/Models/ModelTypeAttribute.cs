using System;

public class ModelTypeAttribute:Attribute
    {
    public string PropName { get; set; }
    public Type TargetModelType { get; set; }  
    public ModelTypeAttribute(Type t,string propName=null)
    {
        PropName = propName;
        TargetModelType = t;
    }
}

