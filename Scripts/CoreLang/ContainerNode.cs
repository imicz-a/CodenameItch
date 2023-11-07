using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContainerNode : InstructionNode
{
    public EndNode end;
    public override void CrossCompile()
    {
        CrossCompiler.AddTab();
        if (nextNode == end)
        {
            CrossCompiler.AddLine("pass");
        }
        CrossCompiler.CompileNext(nextNode);
    }
    public virtual void InsertAtEnd(InstructionNode node, InstructionNode end)
    {
        node.previousNode = end.previousNode;
        node.previousNode.nextNode = node;
        NodeDragManager.instance.SnapNodes(this, node);
    }
}
