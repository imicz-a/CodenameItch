using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class InstructionNode : LangNode
{
    public InstructionNode nextNode;
    public InstructionNode previousNode;
    public ContainerNode currentContainer = null;
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
        if (currentContainer != null)
        {
            Debug.Log("container not null");
            if (!(currentContainer is IfNode))
            {
                currentContainer.end.Orphan();
                currentContainer.InsertAtEnd(currentContainer.end, this);
                transform.SetParent(NodeDragManager.nodeParent);
                previousNode = null;
                currentContainer = null;
                return;
            }
            else
            {
                (currentContainer as IfNode).enode.Orphan();
                currentContainer.InsertAtEnd((currentContainer as IfNode).enode, this);
                transform.SetParent(NodeDragManager.nodeParent);
                previousNode = null;
                currentContainer = null;
                return;

            }
        }
        previousNode.nextNode = null;
        transform.SetParent(NodeDragManager.nodeParent);
        previousNode = null;
        currentContainer = null;
    }
}