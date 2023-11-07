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
    protected virtual Vector2 defaultSize { get; set; } = new Vector2(150, 30);
    public virtual void recieveArgument(VariableNode node)
    {
        (transform as RectTransform).SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, (node.transform as RectTransform).rect.width);
        (transform as RectTransform).SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, (node.transform as RectTransform).rect.height);
        node.transform.position = transform.position;
        node.transform.SetParent(transform);
        assignedVar = node;
        node.assignedArgument = this;
        isAssigned = true;
        parentNode.RecalculateSize();
    }
    public virtual void derecieveArgument() {
        isAssigned = false;
        assignedVar.assignedArgument = null;
        assignedVar.transform.SetParent(NodeDragManager.nodeParent);
        (transform as RectTransform).SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, defaultSize.x);
        (transform as RectTransform).SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, defaultSize.y);
        parentNode.RecalculateSize();
        assignedVar = null;
    }
    public virtual void CrossCompile()
    {

    }
    public void RecalculateSize()
    {
        if (isAssigned)
        {
            (transform as RectTransform).SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, (assignedVar.transform as RectTransform).rect.width);
            (transform as RectTransform).SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, (assignedVar.transform as RectTransform).rect.height);
            assignedVar.transform.position = transform.position;
        }
        else
        {
            (transform as RectTransform).SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, defaultSize.x);
            (transform as RectTransform).SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, defaultSize.y);
        }
    }
}