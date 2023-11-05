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
    public bool doArgsOverlap(out InstructionArgument argument)
    {
        foreach (var arg in arguments)
        {
            if (NodeDragManager.instance.pointerOverlaps(arg.transform as RectTransform, arg))
            {
                argument = arg;
                return true;
            }
        }
        argument = null;
        return false;
    }
    public void RecalculateSize()
    {
        if (hlaygroup == null)
            return;
        RecalculateSizeOnly();
        if (this is VariableNode)
        {
            var varnode = this as VariableNode;
            if(varnode.assignedArgument != null)
            {
                varnode.assignedArgument.RecalculateSize();
                varnode.assignedArgument.parentNode.RecalculateSize();
            }
        }
        if (this is InstructionNode)
        {
            (this as InstructionNode).ReSnap();
        }
    }
    public void RecalculateSizeOnly()
    {
        
        float sum = 0;
        float maxHeight = 0;
        InstructionNode ins = null;
        if (this is InstructionNode)
        {
            ins = this as InstructionNode;
        }
        foreach (RectTransform c in transform)
        {
            if(ins != null)
            {
                if (ins.nextNode != null)
                {
                    if (c == ins.nextNode.transform)
                    {
                        continue;
                    }
                }
            }
            sum += c.rect.width;
            sum += hlaygroup.spacing;
            if (c.rect.height > maxHeight)
            {
                maxHeight = c.rect.height;
            }
        }
        sum += hlaygroup.padding.right + hlaygroup.padding.left;
        (transform as RectTransform).SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, sum);
        (transform as RectTransform).SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, maxHeight + hlaygroup.padding.top + hlaygroup.padding.bottom);
    }
    [ContextMenu("calculate size")]
    public void RecalculateSizeEditor()
    {
        if (hlaygroup == null)
            return;
        RecalculateSizeOnly();
    }
    public virtual void Init() { }
    public virtual void CrossCompile()
        { throw new System.NotImplementedException("this should be implemented!"); }
}
