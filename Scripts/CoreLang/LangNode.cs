using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
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
    public void doArgsOverlap(Vector2 localPointer)
    {
        foreach (var arg in arguments)
        {
            if (NodeDragManager.instance.pointerOverlaps(arg.transform as RectTransform, arg))
            {
                
                Debug.Log("found an arg!");
                break;
            }
            Debug.Log("Still looking for overlapping arg");
        }
    }
    public void RecalculateSize()
    {
        if (hlaygroup == null)
            return;
        float sum = 0;
        float maxHeight = 0;
        foreach (RectTransform c in transform)
        {
            sum += c.rect.width;
            sum += hlaygroup.spacing;
            if(c.rect.height > maxHeight)
            {
                maxHeight = c.rect.height;
            }
        }
        sum += hlaygroup.padding.right + hlaygroup.padding.left;
        (transform as RectTransform).SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, sum);
        (transform as RectTransform).SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, maxHeight+20);
        if (this is InstructionNode)
        {
            (this as InstructionNode).ReSnap();
        }
    }
    public virtual void Init() { }
    public virtual void CrossCompile()
        { throw new System.NotImplementedException("this should be implemented!"); }
    
}
