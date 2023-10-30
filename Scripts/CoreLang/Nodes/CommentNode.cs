using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommentNode : InstructionNode
{
    public override void CrossCompile()
    {
        nextNode.CrossCompile();
    }
}
