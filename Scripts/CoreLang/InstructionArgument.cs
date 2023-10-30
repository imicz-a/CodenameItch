using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(LayoutElement))]
public class InstructionArgument : MonoBehaviour
{
    public VariableNode assignedVar;
    public bool isAssigned;
    public virtual void recieveArgument(VariableNode node)
    {
        node.transform.position = transform.position;
        node.transform.SetParent(transform);
        (transform as RectTransform).SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, (node.transform as RectTransform).rect.width);
        assignedVar = node;
        isAssigned = true;
    }
    public virtual void derecieveArgument() {
        isAssigned = false;
    }
    public virtual void CrossCompile()
    {

    }
}