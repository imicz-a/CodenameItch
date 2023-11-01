using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(LayoutElement))]
public class InstructionArgument : MonoBehaviour
{
    public VariableNode assignedVar;
    public LangNode parentNode;
    public bool isAssigned;
    public virtual void recieveArgument(VariableNode node)
    {
        (transform as RectTransform).SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, (node.transform as RectTransform).rect.width);
        node.transform.position = transform.position;
        node.transform.SetParent(transform);
        assignedVar = node;
        node.assignedArgument = this;
        isAssigned = true;
    }
    public virtual void derecieveArgument() {
        isAssigned = false;
        assignedVar.assignedArgument = null;
        assignedVar.transform.SetParent(NodeDragManager.nodeParent);
        parentNode.RecalculateSize();
        assignedVar = null;
    }
    public virtual void CrossCompile()
    {

    }
}