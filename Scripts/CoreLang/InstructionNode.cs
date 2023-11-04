using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class InstructionNode : LangNode
{
    public InstructionNode nextNode;
    public InstructionNode previousNode;
    public void ReSnap()
    {
        if(previousNode != null)
            NodeDragManager.instance.SnapNodes(previousNode, this);
        if (nextNode != null)
            nextNode.ReSnap();
    }
    public override void Grab()
    {
        base.Grab();
        if (previousNode != null)
        {
            Orphan();
        }
    }
    public void Orphan()
    {
        previousNode.nextNode = null;
        transform.SetParent(NodeDragManager.nodeParent);
        previousNode = null;
    }
}