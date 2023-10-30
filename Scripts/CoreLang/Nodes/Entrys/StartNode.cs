using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartNode : InstructionNode
{
    public override void CrossCompile()
    {
        throw new System.NotImplementedException("An start node is not cross compiled!");
    }
}
