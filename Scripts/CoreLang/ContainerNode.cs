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
}
