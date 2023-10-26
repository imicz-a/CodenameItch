using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
[RequireComponent(typeof(RectTransform))]
public class LangNode : DraggableObject
{
    public bool hasVariable;
    public LangNode nextNode;
    public virtual void Init() { }
    public virtual void CrossCompile()
        { throw new System.NotImplementedException("this should be implemented!"); }
    public virtual void insertVariable()
    {
        if (!hasVariable)
            throw new System.Exception("Even though the node does not support, you want to insert a variable");
    }
}
