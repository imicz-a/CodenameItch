using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndNode : InstructionNode
{
    public override void Grab() { }
    public override void CrossCompile()
    {
        CrossCompiler.RemoveTab();
        CrossCompiler.CompileNext(nextNode);
    }
}
