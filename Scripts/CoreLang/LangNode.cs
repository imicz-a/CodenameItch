using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
[RequireComponent(typeof(LayoutElement))]
[RequireComponent(typeof(RectTransform))]
public class LangNode : DraggableObject
{
    public HorizontalLayoutGroup hlaygroup;
    public List<InstructionArgument> arguments;
    public bool hasVariable;
    public virtual void insertVariable(VariableNode node, Vector2 localPointer)
    {
        if (!hasVariable)
            throw new System.Exception("Even though the node does not support it, you want to insert a variable");
        foreach(var arg in arguments)
        {
            if (NodeDragManager.instance.pointerOverlaps(arg.transform as RectTransform, arg))
            {
                arg.recieveArgument(node);
                Debug.Log("found an arg!");
                break;
            }
            Debug.Log("Still looking for overlapping arg");
        }
        RecalculateSize();
    }
    void RecalculateSize()
    {
        if (hlaygroup == null)
            return;
        float sum = 0;
        foreach (RectTransform c in transform)
        {
            sum += c.rect.width;
            sum += hlaygroup.spacing;
        }
        sum += hlaygroup.padding.right + hlaygroup.padding.left;
        (transform as RectTransform).SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, sum);
    }
    public virtual void Init() { }
    public virtual void CrossCompile()
        { throw new System.NotImplementedException("this should be implemented!"); }
    
}
